using BusinessManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_SalesAdmin : System.Web.UI.MasterPage
{
    BALUserUKZN objBalUser = new BALUserUKZN();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        int LogoutResult = objBalUser.UserLoginHistoryInsertUpdate(Convert.ToInt32(Session["UserMasterId"].ToString()), "", "Update");
        if (LogoutResult >= 1)
        {
            Response.Redirect("Login.aspx");
        }
    }
}
