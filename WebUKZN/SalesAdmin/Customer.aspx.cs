using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using EntityManager;
using BusinessManager;

public partial class SalesAdmin_Customer : System.Web.UI.Page
{
    BOUtiltiy objBOUtility = new BOUtiltiy();
    BALUser objBALUser = new BALUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"]!= null)
        {
            Response.Redirect("../Login.aspx");

        }
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx", true);
        }
        if (!IsPostBack)
        {
            GetRoleDetails();
            GetStatusDetails();
            if (!string.IsNullOrEmpty(Request.QueryString["user_master_id"]))
            {
                int user_master_id = Convert.ToInt32(Request.QueryString["user_master_id"]);

                GetUserDetails(user_master_id);

            }

        }
    }


    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        InsertUpdateCustomer();
    }


    #region PrivateMethods
    private void InsertUpdateCustomer()
    {
        try
        {
            BALUser objBALUser = new BALUser();
            UserEntity objUserEntity = new UserEntity();
            objUserEntity.user_master_id = Convert.ToInt32(hfUserId.Value);
            objUserEntity.RoleId = Convert.ToInt32(dropUserType.SelectedValue);
            objUserEntity.LocationId = Convert.ToInt32(dropLocation.SelectedValue);
            objUserEntity.user_master_status = Convert.ToInt32(dropStatus.SelectedValue);
            //objUserEntity.IsLogin = bool.Parse(chkIsLogin.Text);
            objUserEntity.FirstName = txtFirstName.Text;
            objUserEntity.LastName = txtLastName.Text;
            objUserEntity.user_master_mobile = txtMobileNumber.Text;
            objUserEntity.user_master_land = txtLandLineNumber.Text;
            objUserEntity.user_master_mail = txtEmailId.Text;

            objUserEntity.user_master_name = "";
            objUserEntity.user_master_desc = "";
            objUserEntity.user_master_address = "";
            objUserEntity.user_master_contact_person = "";
            objUserEntity.user_master_link = "";
            if (fuUserImage.HasFile)
            {
                string str = fuUserImage.FileName;
                fuUserImage.SaveAs(Server.MapPath("/Admin/CustomerImages/" + fuUserImage.FileName));

            }
            else
            {
                labelError.Text = "Please Select Image";
                return;
            }
            objUserEntity.user_master_logo = fuUserImage.FileName;
            objUserEntity.password = txtPassword.Text;
            objUserEntity.user_name = txtEmailId.Text;
            objUserEntity.password = txtConfirmPawd.Text;

            objUserEntity.user_master_created_by = Session["loginId"].ToString();


            int nResult = objBALUser.InsertUpdateUser(objUserEntity);

            if (nResult == -1)
            {
                labelError.Text = "User already Exist";
            }

            if (nResult > 0)
            {
                Response.Redirect("CustomerList.aspx", true);
            }
        }
        catch (Exception ex)
        {

        }

    }



    private void GetUserDetails(int user_master_id)
    {
        try
        {
            DataSet objds = objBALUser.GetUserDetails(user_master_id);
            if (objds.Tables[0].Rows.Count > 0)
            {
                hfUserId.Value = objds.Tables[0].Rows[0]["user_master_id"].ToString();
                dropUserType.SelectedIndex = dropUserType.Items.IndexOf(dropUserType.Items.FindByValue(objds.Tables[0].Rows[0]["role_id"].ToString()));
                dropLocation.SelectedIndex = dropLocation.Items.IndexOf(dropLocation.Items.FindByValue(objds.Tables[0].Rows[0]["LocationId"].ToString()));
                dropStatus.SelectedIndex = dropStatus.Items.IndexOf(dropStatus.Items.FindByValue(objds.Tables[0].Rows[0]["user_master_status"].ToString()));
                txtFirstName.Text = objds.Tables[0].Rows[0]["FirstName"].ToString();
                txtLastName.Text = objds.Tables[0].Rows[0]["LastName"].ToString();
                txtMobileNumber.Text = objds.Tables[0].Rows[0]["user_master_mobile"].ToString();
                txtLandLineNumber.Text = objds.Tables[0].Rows[0]["user_master_land"].ToString();
                txtEmailId.Text = objds.Tables[0].Rows[0]["user_master_mail"].ToString();
                txtEmailId.Text = objds.Tables[0].Rows[0]["user_name"].ToString();
                Image1.ImageUrl = "CustomerImages/" + objds.Tables[0].Rows[0]["user_master_logo"].ToString();
                //txtPassword.Text = objds.Tables[0].Rows[0]["password"].ToString();
                PanelPassword.Visible = false;
                //txtConfirmPawd.Text = objds.Tables[0].Rows[0]["password"].ToString();

            }

        }

        catch (Exception ex)
        {

        }
    }

    #endregion PrivateMethods


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Customer.aspx", false);
    }


    void GetRoleDetails()
    {

        DataSet ds = new DataSet();

        try
        {
            ds = objBALUser.GetRoleMaster(0);

            if (ds.Tables.Count > 0)
            {
                dropUserType.DataSource = ds.Tables[0];
                dropUserType.DataTextField = "role_desc";
                dropUserType.DataValueField = "role_id";
                dropUserType.DataBind();
            }
            dropUserType.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch (Exception ex)
        {

        }

    }

    void GetStatusDetails()
    {

        DataSet ds = new DataSet();

        try
        {
            ds = objBALUser.GetStatusMaster(0);

            if (ds.Tables.Count > 0)
            {
                dropStatus.DataSource = ds.Tables[0];
                dropStatus.DataTextField = "status_desc";
                dropStatus.DataValueField = "status_id";
                dropStatus.DataBind();
            }
            dropStatus.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch (Exception ex)
        {

        }

    }
}