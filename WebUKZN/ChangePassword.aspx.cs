using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;

public partial class ChangePassword : System.Web.UI.Page
{
    BOUtiltiyUKZN _objBoUtility = new BOUtiltiyUKZN();
    BALUserUKZN _baUser = new BALUserUKZN();
    string userId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            userId = Convert.ToString(Request.QueryString["Id"]);
            Session["userId"] = _objBoUtility.Decrypt(HttpUtility.UrlDecode(userId));
        }
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {

        try
        {
            int results = _baUser.ChangePassword(Convert.ToInt32(Session["userId"]), txtNewPassword.Text.ToString());
            if (results > 0)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
}