using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AgentStaffCreation : System.Web.UI.Page
{
    #region Declarations
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BALUser _objBalUser = new BALUser();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    #endregion Declarations
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["stfid"]))
            {
                hfStaffDetailsId.Value = Request.QueryString["stfid"];
                GetStaffDetails(Convert.ToInt32(hfStaffDetailsId.Value));
            }
        }
    }

    #region PrivateMethods


    private void InsertUpdateStaff()
    {
        User_Login objUser_Login = new User_Login();
        try
        {
            objUser_Login.UserLoginId = Convert.ToInt32(hfStaffLoginId.Value);
            objUser_Login.UserCompany = Convert.ToInt32(Session["CompanyId"]);
            objUser_Login.UserRole = 6;
            objUser_Login.LoginId = txtStaffLoginId.Text;
            objUser_Login.Password = txtPassWord.Text;
            objUser_Login.can_change_password = chkChangePassword.Checked;
            objUser_Login.active_code = 0;
            objUser_Login.UserEmail = txtEmailId.Text;
            objUser_Login.UserPhone = txtMobileNumber.Text;
            objUser_Login.UserType = 3;
            objUser_Login.UserName = txtFirstName.Text + " " + txtLastName.Text;

            objUser_Login.BranchId = "0"; //ddlBranch.SelectedValue;
            objUser_Login.ProvinceId = "0";//ddlProvince.SelectedValue;
            objUser_Login.CityId = "0";// ddlCities.SelectedValue;

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

            objUserDetailsMaster.CompanyLoogo = Session["logo"].ToString();
            objUserDetailsMaster.communicationlogo = Session["commlogo"].ToString();

            int nResult = _objBALUserManager.InsertUpdateStaff(objUserDetailsMaster);
            if (nResult > 0)
            {
                Response.Redirect("AgentStaffList.aspx");


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


                txtAddress.Text = objDs.Tables[0].Rows[0]["Address"].ToString();
                txtFirstName.Text = objDs.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = objDs.Tables[0].Rows[0]["LastName"].ToString();
                txtDesignation.Text = objDs.Tables[0].Rows[0]["Designation"].ToString();
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(objDs.Tables[0].Rows[0]["IsActive"].ToString()));

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

        txtStaffLoginId.Text = "";
        txtPassWord.Text = "";
        chkChangePassword.Checked = false;
        txtEmailId.Text = "";
        txtMobileNumber.Text = "";
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
        Response.Redirect("AgentStaffList.aspx");
    }
    #endregion Events

}