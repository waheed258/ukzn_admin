using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using System.Data;
using BusinessManager;
using System.IO;

public partial class Admin_CreateAgent : System.Web.UI.Page
{
    #region Declarations
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    private BALUser _objBalUser = new BALUser();
    #endregion Declarations
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");

        }
        if (!IsPostBack)
        {
            BindProvince();
            BindConsultant();
            if (!string.IsNullOrEmpty(Request.QueryString["agentId"]))
            {
                hfAgentDetailsId.Value = Request.QueryString["agentId"];
                GetAgentDetails(Convert.ToInt32(hfAgentDetailsId.Value));
            }
        }
    }
    #region PrivateMethods
    private void InsertUpdateAgent()
    {
        User_Login objUser_Login = new User_Login();
        try
        {
            if (!fuAgentLogo.HasFile)
            {



                if (hfcommunicationlogo.Value == "")
                {
                    lbllogoman.Text = "Please upload logo";
                    return;
                }



            }
            objUser_Login.UserLoginId = Convert.ToInt32(hfAgentLoginId.Value);
            objUser_Login.UserCompany = ddlAgentCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlAgentCompany.SelectedValue) : 0;
            objUser_Login.UserRole = Convert.ToInt32(ddlRole.SelectedValue);
            objUser_Login.LoginId = txtEmailId.Text;
            objUser_Login.Password = txtPassword.Text;
            objUser_Login.can_change_password = chkChangePassword.Checked;
            objUser_Login.active_code = 0;
            objUser_Login.UserEmail = txtEmailId.Text;
            objUser_Login.UserPhone = txtMobileNumber.Text;
            objUser_Login.UserType = 2;
            objUser_Login.UserName = txtCompanyName.Text;
            objUser_Login.BranchId = ddlBranch.SelectedValue;
            objUser_Login.ProvinceId = ddlProvince.SelectedValue;
            objUser_Login.CityId = ddlCities.SelectedValue;
            objUser_Login.ticket_fee_agent = txtTicketPrice.Text != "" ? Convert.ToDecimal(txtTicketPrice.Text) : 0;
            objUser_Login.TicketConsId = ddlMappingcons.SelectedIndex > 0 ? Convert.ToDecimal(ddlMappingcons.SelectedValue) : 0;

            int nResult = _objBALUserManager.InsertUpdateUserLogin(objUser_Login);
            if (nResult == -1)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", "This Login id already exist.");
            }
            else if (nResult == -2)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", "This Email  id already exist.");
            }
            else if (nResult == -3)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", "This Mobile no already exist.");
            }
            else
            {
                InsertUpdateAgentDetails(nResult);
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void InsertUpdateAgentDetails(int UserLoginId)
    {
        try
        {
            UserDetailsMaster objUserDetailsMaster = new UserDetailsMaster();
            objUserDetailsMaster.UserDetailsId = Convert.ToInt32(hfAgentDetailsId.Value);
            objUserDetailsMaster.CompanyName = txtCompanyName.Text;
            objUserDetailsMaster.Address = txtAddress.Text;
            objUserDetailsMaster.FAXNo = txtFaxNo.Text;
            objUserDetailsMaster.Website = txtWebSite.Text;
            objUserDetailsMaster.FirstName = txtFirstName.Text;
            objUserDetailsMaster.LastName = txtLastName.Text;
            objUserDetailsMaster.ContactPersonMobileNo = txtContMobileNo.Text;
            objUserDetailsMaster.ContactPersonEmailId = txtContEmailId.Text;
            objUserDetailsMaster.Designation = txtDesignation.Text;
            objUserDetailsMaster.IATANo = txtIataNo.Text;
            objUserDetailsMaster.BusinessLicense = txtBusinessLicense.Text;
            objUserDetailsMaster.PseudoCode = txtPseudoCode.Text;
            objUserDetailsMaster.IsApproved = chkIsApprove.Checked;
            objUserDetailsMaster.ApprovedBy = Convert.ToInt32(Session["loginId"].ToString());
            objUserDetailsMaster.OverdraftYN = chkIsOverDraft.Checked;
            if (chkIsOverDraft.Checked)
                objUserDetailsMaster.OverdraftLimitAmt = Convert.ToDecimal(txtOverDraftLimt.Text);
            objUserDetailsMaster.IsActive = Convert.ToInt32(ddlStatus.SelectedValue);
            objUserDetailsMaster.CreatedBy = Convert.ToInt32(Session["loginId"].ToString());

            objUserDetailsMaster.Dom_Comm_Per = txtdomcmper.Text != "" ? Convert.ToDecimal(txtdomcmper.Text) : 0;


            objUserDetailsMaster.Dom_Comm_Rate = txtdomcmAmount.Text != "" ? Convert.ToDecimal(txtdomcmAmount.Text) : 0;
            objUserDetailsMaster.Int_Comm_Per = txtintcmper.Text != "" ? Convert.ToDecimal(txtintcmper.Text) : 0;
            objUserDetailsMaster.Int_Comm_Rate = txtintcmamt.Text != "" ? Convert.ToDecimal(txtintcmamt.Text) : 0;
            objUserDetailsMaster.Comm_Active_type = Convert.ToInt32(ddlComActType.SelectedValue);


            objUserDetailsMaster.CompanyLoogo = "logo_ukzn.png";

            objUserDetailsMaster.UserLoginId = UserLoginId;

            string fileName = string.Empty;
            if (fuAgentLogo.HasFile)
            {
                string extension = Path.GetExtension(fuAgentLogo.PostedFile.FileName);
                fileName = UserLoginId + extension;
                fuAgentLogo.SaveAs(Server.MapPath("../Logos/") + fileName);
                fuAgentLogo.SaveAs(Server.MapPath("../pdfimages/") + fileName);

            }
            else
            {

                fileName = hfcommunicationlogo.Value;

            }
            objUserDetailsMaster.communicationlogo = fileName;
            int nResult = _objBALUserManager.InsertUpdateAgent(objUserDetailsMaster);
            if (nResult > 0)
            {
                Response.Redirect("AgentList.aspx");

                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Action Completed.");
            }
            else
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("Info", "Info", "Agent not created pleae try again.");
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void GetAgentDetails(int StaffId)
    {
        try
        {
            DataSet objDs = _objBALUserManager.GetStaffDetails(StaffId, 0);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                hfAgentLoginId.Value = objDs.Tables[0].Rows[0]["UserLoginId"].ToString();
                hfAgentDetailsId.Value = objDs.Tables[0].Rows[0]["UserDetailsId"].ToString();

                ddlProvince.SelectedIndex = ddlProvince.Items.IndexOf(ddlProvince.Items.FindByValue(objDs.Tables[0].Rows[0]["ProvincesId"].ToString()));

                Bindcities(Convert.ToInt32(ddlProvince.SelectedValue));

                ddlCities.SelectedIndex = ddlCities.Items.IndexOf(ddlCities.Items.FindByValue(objDs.Tables[0].Rows[0]["CityId"].ToString()));

                BindBranchs(Convert.ToInt32(ddlProvince.SelectedValue), Convert.ToInt32(ddlCities.SelectedValue));
                ddlBranch.SelectedIndex = ddlBranch.Items.IndexOf(ddlBranch.Items.FindByValue(objDs.Tables[0].Rows[0]["BranchId"].ToString()));


                txtAddress.Text = objDs.Tables[0].Rows[0]["Address"].ToString();
                txtFirstName.Text = objDs.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = objDs.Tables[0].Rows[0]["LastName"].ToString();
                txtDesignation.Text = objDs.Tables[0].Rows[0]["Designation"].ToString();
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(objDs.Tables[0].Rows[0]["IsActive"].ToString()));
                ddlAgentCompany.SelectedIndex = ddlAgentCompany.Items.IndexOf(ddlAgentCompany.Items.FindByValue(objDs.Tables[0].Rows[0]["UserCompany"].ToString()));
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(objDs.Tables[0].Rows[0]["UserRole"].ToString()));

                txtPassword.Text = objDs.Tables[0].Rows[0]["Password"].ToString();
                txtCnfPassword.Text = objDs.Tables[0].Rows[0]["Password"].ToString();
                chkChangePassword.Checked = Convert.ToBoolean(objDs.Tables[0].Rows[0]["can_change_password"].ToString());
                txtEmailId.Text = objDs.Tables[0].Rows[0]["UserEmail"].ToString();
                txtMobileNumber.Text = objDs.Tables[0].Rows[0]["UserPhone"].ToString();
                txtCompanyName.Text = objDs.Tables[0].Rows[0]["CompanyName"].ToString();
                txtAddress.Text = objDs.Tables[0].Rows[0]["Address"].ToString();
                txtFaxNo.Text = objDs.Tables[0].Rows[0]["FAXNo"].ToString();
                txtWebSite.Text = objDs.Tables[0].Rows[0]["Website"].ToString();

                txtContMobileNo.Text = objDs.Tables[0].Rows[0]["ContactPersonMobileNo"].ToString();
                txtContEmailId.Text = objDs.Tables[0].Rows[0]["ContactPersonEmailId"].ToString();

                txtDesignation.Text = objDs.Tables[0].Rows[0]["Designation"].ToString();


                txtIataNo.Text = objDs.Tables[0].Rows[0]["IATANo"].ToString();
                txtBusinessLicense.Text = objDs.Tables[0].Rows[0]["BusinessLicense"].ToString();
                txtPseudoCode.Text = objDs.Tables[0].Rows[0]["PseudoCode"].ToString();
                chkIsApprove.Checked = Convert.ToBoolean(objDs.Tables[0].Rows[0]["IsApproved"].ToString());

                chkIsOverDraft.Checked = Convert.ToBoolean(objDs.Tables[0].Rows[0]["OverdraftYN"].ToString());
                if (chkIsOverDraft.Checked)
                    txtOverDraftLimt.Text = objDs.Tables[0].Rows[0]["OverdraftLimitAmt"].ToString();
                else
                    txtOverDraftLimt.Text = "0";


                txtdomcmper.Text = objDs.Tables[0].Rows[0]["Dom_Comm_Per"].ToString();


                txtdomcmAmount.Text = objDs.Tables[0].Rows[0]["Dom_Comm_Rate"].ToString();
                txtintcmper.Text = objDs.Tables[0].Rows[0]["Int_Comm_Per"].ToString();
                txtintcmamt.Text = objDs.Tables[0].Rows[0]["Int_Comm_Rate"].ToString();
                ddlComActType.SelectedValue = objDs.Tables[0].Rows[0]["Comm_Active_type"].ToString();
                hfcommunicationlogo.Value = objDs.Tables[0].Rows[0]["communicationlogo"].ToString();
                txtTicketPrice.Text = objDs.Tables[0].Rows[0]["ticket_fee_agent"].ToString();
                ddlMappingcons.SelectedIndex = ddlMappingcons.Items.IndexOf(ddlMappingcons.Items.FindByValue(objDs.Tables[0].Rows[0]["TicketConsId"].ToString()));

                ControlEnableDdisable(false);
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void ControlEnableDdisable(bool status)
    {
        txtMobileNumber.Enabled = status;
        txtEmailId.Enabled = status;
    }
    private void BindProvince()
    {
        try
        {
            DataSet objDs = _objBalUser.GetProvince();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlProvince.DataSource = objDs;
                ddlProvince.DataValueField = "ProvinceId";
                ddlProvince.DataTextField = "ProvinceName";
                ddlProvince.DataBind();
                ddlProvince.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlProvince.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void Bindcities(int ProvinceId)
    {
        try
        {
            ddlBranch.Items.Clear();
            ddlCities.Items.Clear();
            if (ProvinceId == 0)
            {
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
                return;
            }

            DataSet objDs = _objBalUser.GetCities(ProvinceId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlCities.DataSource = objDs;
                ddlCities.DataValueField = "CityId";
                ddlCities.DataTextField = "CityName";
                ddlCities.DataBind();
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlCities.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void BindBranchs(int ProvinceId, int CitiesId)
    {
        try
        {
            ddlBranch.Items.Clear();
            if (ProvinceId == 0 || CitiesId == 0)
            {
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
                return;
            }

            DataSet objDs = _objBalUser.GetBranchs(ProvinceId, CitiesId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlBranch.DataSource = objDs;
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataBind();
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
            else
            {
                ddlBranch.Items.Insert(0, new ListItem("Select", "0"));
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void BindConsultant()
    {
        DataSet objDs = _objBALUserManager.GetStaffDetails(0, 1);
        if (objDs.Tables[0].Rows.Count > 0)
        {
            ddlMappingcons.DataSource = objDs;
            ddlMappingcons.DataValueField = "UserLoginId";
            ddlMappingcons.DataTextField = "ConsultantName";
            ddlMappingcons.DataBind();
            ddlMappingcons.Items.Insert(0, new ListItem("Select", "0"));
        }
        else
        {
            ddlMappingcons.Items.Insert(0, new ListItem("Select", "0"));
        }

    }
    #endregion PrivateMethods

    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateAgent();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentList.aspx");
    }
    protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {

        int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
        Bindcities(ProvinceId);

    }
    protected void ddlCities_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ProvinceId = Convert.ToInt32(ddlProvince.SelectedValue);
        int CotiesId = Convert.ToInt32(ddlCities.SelectedValue);
        BindBranchs(ProvinceId, CotiesId);
    }
}