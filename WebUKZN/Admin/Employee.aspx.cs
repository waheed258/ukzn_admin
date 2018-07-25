using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Employee : System.Web.UI.Page
{
    EMUser ObjEmUser = new EMUser();
    BALUserUKZN objBalUser = new BALUserUKZN();
    BOUtiltiyUKZN _objBOUtiltiy = new BOUtiltiyUKZN();
    BALBranch objBalBranch = new BALBranch();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetRoles();
            BindBranch();
            if (!string.IsNullOrEmpty(Request.QueryString["UserId"]))
            {
                int UserMasterId = Convert.ToInt32(Request.QueryString["UserId"].ToString());
                GetUserDetails(UserMasterId);
                btnUseSave.Text = "Update";
            }
        }
    }

    protected void btnUseSave_Click(object sender, EventArgs e)
    {
        try
        {
            ObjEmUser.UserFirstName = string.IsNullOrEmpty(txtUserFirstName.Text) ? "" : txtUserFirstName.Text;
            ObjEmUser.UserLastName = string.IsNullOrEmpty(txtUserLastName.Text) ? "" : txtUserLastName.Text;
            ObjEmUser.UserEmail = string.IsNullOrEmpty(txtUserMail.Text) ? "" : txtUserMail.Text;
            ObjEmUser.UserPhone = string.IsNullOrEmpty(txtUserPhone.Text) ? "" : txtUserPhone.Text;
            ObjEmUser.UserRole = string.IsNullOrEmpty((ddlUserRole.SelectedItem.Value.ToString())) ? 0 : Convert.ToInt32(ddlUserRole.SelectedItem.Value);
            ObjEmUser.UserStatus = string.IsNullOrEmpty(ddlUserStatus.SelectedItem.Value.ToString()) ? 0 : Convert.ToInt32(ddlUserStatus.SelectedItem.Value);
             ObjEmUser.UserCompanyId = Convert.ToInt32(Session["UserCompanyId"].ToString());
            //ObjEmUser.UserCompanyId = 1;
            ObjEmUser.UserPassword = string.IsNullOrEmpty(txtUserPwd.Text) ? "" : txtUserPwd.Text;
             ObjEmUser.CreatedBy = Convert.ToInt32(Session["UserMasterId"].ToString());
           // ObjEmUser.CreatedBy = 1;
             ObjEmUser.BranchId = string.IsNullOrEmpty((ddlUserBranch.SelectedItem.Value.ToString())) ? 0 : Convert.ToInt32(ddlUserBranch.SelectedItem.Value);
            ObjEmUser.UserMasterId = Convert.ToInt32(hf_UserMasterId.Value);

            int userResult = objBalUser.InsertUpdateUser(ObjEmUser);
            if (userResult > 0)
            {
                Response.Redirect("EmployeeList.aspx");
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);

        }
    }
    protected void btnUserCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeList.aspx");
    }
    #region PrivateMethods
    private void GetUserDetails(int UserMasterId)
    {
        try
        {
            DataSet ds = objBalUser.GetUserList(UserMasterId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hf_UserMasterId.Value = ds.Tables[0].Rows[0]["UserMasterId"].ToString();
                txtUserFirstName.Text = ds.Tables[0].Rows[0]["UserFirstName"].ToString();
                txtUserLastName.Text = ds.Tables[0].Rows[0]["UserLastName"].ToString();
                txtUserMail.Text = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                txtUserMail.ReadOnly = true;
                txtUserPhone.Text = ds.Tables[0].Rows[0]["UserPhone"].ToString();
                ddlUserRole.SelectedIndex = ddlUserRole.Items.IndexOf(ddlUserRole.Items.FindByValue(ds.Tables[0].Rows[0]["UserRole"].ToString()));
                ddlUserStatus.SelectedIndex = ddlUserStatus.Items.IndexOf(ddlUserStatus.Items.FindByValue(ds.Tables[0].Rows[0]["UserStatus"].ToString()));
                ddlUserBranch.SelectedIndex = ddlUserBranch.Items.IndexOf(ddlUserBranch.Items.FindByValue(ds.Tables[0].Rows[0]["BranchId"].ToString()));
               //txtUserPwd.Text = ds.Tables[0].Rows[0]["UserPassword"].ToString();
                txtUserPwd.Attributes.Add("value", ds.Tables[0].Rows[0]["UserPassword"].ToString());
                txtUserPwd.Attributes["type"] = "Password";
                txtUserPwd.ReadOnly = true;

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = "Some Technical Error occurred,Please visit after some time";
        }
    }

    private void GetRoles()
    {
        try
        {
            DataSet RolesResult = objBalUser.GetRoles();
            if (RolesResult.Tables[0].Rows.Count > 0)
            {

                ddlUserRole.DataSource = RolesResult.Tables[0];
                ddlUserRole.DataTextField = "RoleDescription";
                ddlUserRole.DataValueField = "RoleMasterId";
                ddlUserRole.DataBind();
                ddlUserRole.Items.Insert(0, new ListItem("-- Please Select --", "-1"));

            }
            else
            {
                ddlUserRole.DataSource = "";
                ddlUserRole.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);

        }
    }
    private void BindBranch()
    {
        try
        {
            DataSet BranchResult = objBalBranch.GetBranchList(0);
            if (BranchResult.Tables[0].Rows.Count > 0)
            {

                ddlUserBranch.DataSource = BranchResult.Tables[0];
                ddlUserBranch.DataTextField = "BranchName";
                ddlUserBranch.DataValueField = "BranchId";
                ddlUserBranch.DataBind();
                ddlUserBranch.Items.Insert(0, new ListItem("-- Please Select --", "-1"));

            }
            else
            {
                ddlUserBranch.DataSource = "";
                ddlUserBranch.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);

        }
    }
    #endregion PrivateMethods
}