using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;


public partial class Admin_ChangePassword : System.Web.UI.Page
{
    #region Declarations
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    #endregion Declarations
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }
        if (!IsPostBack)
        {

            if (Session["loginId"] == null)
            {
                hfStaffDetailsId.Value = Session["loginId"].ToString();

                GetStaffDetails(Convert.ToInt32(hfStaffDetailsId.Value));

            }
        }
    }

    #region PrivateMethods


    private void InsertUpdateStaff()
    {

        try
        {
            int UserLoginId = Convert.ToInt32(hfStaffLoginId.Value);

            int nResult = _objBALUserManager.UpdatePassword(UserLoginId, txtStaffLoginId.Text, txtPassWord.Text, txtNewPassword.Text);
            if (nResult > 0)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("success", "Success", "Password Changed.");
                txtPassWord.Text = "";
                txtNewPassword.Text = "";
                txtConfirmPassword.Text = "";
                Response.Redirect("../passwordchange.aspx");
                Session.Abandon();
            }

            else if (nResult == -1)
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Info ", "invalid login details");
            }
            else
            {
                labelError.Text = _objBOUtiltiy.ShowMessage("danger", "Info ", "You can't change the password please contact you admin.");
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
                bool ChangePasswordStatus = Convert.ToBoolean(objDs.Tables[0].Rows[0]["can_change_password"].ToString());
                txtStaffLoginId.Text = objDs.Tables[0].Rows[0]["LoginId"].ToString();
                if (ChangePasswordStatus)
                {
                    cmdSubmit.Visible = true;

                }
                else
                {
                    cmdSubmit.Visible = false;
                    labelError.Text = _objBOUtiltiy.ShowMessage("info", "Info", "You can't change password pelase contact your admin");
                }

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
    #endregion Events
}