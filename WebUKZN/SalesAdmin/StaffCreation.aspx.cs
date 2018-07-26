using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using EntityManager;

public partial class SalesAdmin_StaffCreation : System.Web.UI.Page
{
    #region Declarations
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BALUser _objBalUser = new BALUser();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    #endregion Declarations
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        if (!IsPostBack)
        {
           BindRolesDropDwon();
            BindProvince();
            if (!string.IsNullOrEmpty(Request.QueryString["stfid"]))
            {
                hfStaffDetailsId.Value = Request.QueryString["stfid"];
                GetStaffDetails(Convert.ToInt32(hfStaffDetailsId.Value));
            }
        }
    }

    #region PrivateMethods

    private void BindRolesDropDwon()
    {
        try
        {
            ddlRole.Items.Clear();
            DataSet objDs = _objBALUserManager.GetRoleMaster(0);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlRole.DataSource = objDs;
                ddlRole.DataValueField = "role_id";
                ddlRole.DataTextField = "role_desc";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select Role", "0"));
            }
            else
            {
                ddlRole.Items.Insert(0, new ListItem("Select Role", "0"));
            }
        }
        catch
        {
            throw;
        }
    }
    private void InsertUpdateStaff()
    {
        User_Login objUser_Login = new User_Login();
        try
        {
            objUser_Login.UserLoginId = Convert.ToInt32(hfStaffLoginId.Value);
            objUser_Login.UserCompany = ddlStaffCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlStaffCompany.SelectedValue) : 0;
            objUser_Login.UserRole = ddlRole.SelectedIndex > 0 ? Convert.ToInt32(ddlRole.SelectedValue) : 0;
            objUser_Login.LoginId = txtStaffLoginId.Text;
            objUser_Login.Password = txtPassWord.Text;
            objUser_Login.can_change_password = chkChangePassword.Checked;
            objUser_Login.active_code = 0;
            objUser_Login.UserEmail = txtEmailId.Text;
            objUser_Login.UserPhone = txtMobileNumber.Text;
            objUser_Login.UserType = 1;
            objUser_Login.UserName = txtFirstName.Text + " " + txtLastName.Text;

            objUser_Login.BranchId = ddlBranch.SelectedValue;
            objUser_Login.ProvinceId = ddlProvince.SelectedValue;
            objUser_Login.CityId = ddlCities.SelectedValue;

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
                InsertUpdateStaffPersonalDetails(nResult);
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void InsertUpdateStaffPersonalDetails(int LoginId)
    {
        UserDetailsMaster objUserDetailsMaster = new UserDetailsMaster();
        try
        {
            objUserDetailsMaster.UserDetailsId = Convert.ToInt32(hfStaffDetailsId.Value);
            objUserDetailsMaster.Address = txtAddress.Text;
            objUserDetailsMaster.FirstName = txtFirstName.Text;
            objUserDetailsMaster.LastName = txtLastName.Text;
            objUserDetailsMaster.Designation = txtDesignation.Text;
            objUserDetailsMaster.IsActive = Convert.ToInt32(ddlStatus.SelectedValue);
            objUserDetailsMaster.CreatedBy = Convert.ToInt32(Session["loginId"]);
            objUserDetailsMaster.UserLoginId = LoginId;
            if (ddlStaffCompany.SelectedValue == "1")
            {
                objUserDetailsMaster.CompanyLoogo = "Serendipitylogo.png";
                objUserDetailsMaster.communicationlogo = "Serendipitylogo.png";

            }
            else
            {
                objUserDetailsMaster.CompanyLoogo = "logo_ukzn.png";
                objUserDetailsMaster.communicationlogo = "logo_ukzn.png";
            }


            int nResult = _objBALUserManager.InsertUpdateStaff(objUserDetailsMaster);
            if (nResult > 0)
            {
                Response.Redirect("StaffList.aspx");


                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Action Completed.");
            }
            else
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("Info", "Info", "Staff not created pleae try again.");
            }
        }
        catch (Exception ex)
        {
            labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void GetStaffDetails(int StaffId)
    {
        try
        {

            DataSet objDs = _objBALUserManager.GetStaffDetails(StaffId, 0);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                hfStaffDetailsId.Value = objDs.Tables[0].Rows[0]["UserLoginId"].ToString();
                hfStaffLoginId.Value = objDs.Tables[0].Rows[0]["UserDetailsId"].ToString();
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
                ddlStaffCompany.SelectedIndex = ddlStaffCompany.Items.IndexOf(ddlStaffCompany.Items.FindByValue(objDs.Tables[0].Rows[0]["UserCompany"].ToString()));
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByValue(objDs.Tables[0].Rows[0]["UserRole"].ToString()));
                txtStaffLoginId.Text = objDs.Tables[0].Rows[0]["LoginId"].ToString();
                txtPassWord.Text = objDs.Tables[0].Rows[0]["Password"].ToString();
                txtConfirmPassword.Text = objDs.Tables[0].Rows[0]["Password"].ToString();
                chkChangePassword.Checked = Convert.ToBoolean(objDs.Tables[0].Rows[0]["can_change_password"].ToString());
                txtEmailId.Text = objDs.Tables[0].Rows[0]["UserEmail"].ToString();
                txtMobileNumber.Text = objDs.Tables[0].Rows[0]["UserPhone"].ToString();
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
        // chkChangePassword.Enabled = status;
        txtStaffLoginId.Enabled = status;
        //txtPassWord.Enabled = status;

    }
    private void ClearControls()
    {
        hfStaffDetailsId.Value = "0";
        hfStaffLoginId.Value = "0";
        txtAddress.Text = "";
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtDesignation.Text = "";
        ddlStatus.SelectedIndex = 0;
        ddlStaffCompany.SelectedIndex = 0;
        ddlRole.SelectedIndex = 0;
        txtStaffLoginId.Text = "";
        txtPassWord.Text = "";
        chkChangePassword.Checked = false;
        txtEmailId.Text = "";
        txtMobileNumber.Text = "";
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
    #endregion PrivateMethods

    #region Events
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateStaff();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //ClearControls();
        Response.Redirect("StaffList.aspx");
    }
    #endregion Events


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