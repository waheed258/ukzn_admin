using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
using System.Net;
using System.Net.Sockets;



public partial class Login : System.Web.UI.Page
{
    private BALUserUKZN _objBALogin = new BALUserUKZN();
    private BOUtiltiyUKZN _objBOUtiltiy = new BOUtiltiyUKZN();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Abandon();
        }

    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet objDs = _objBALogin.UserAuthentication(txtLoginId.Text.Trim(), txtPassword.Text.Trim());
            if (objDs.Tables[0].Rows.Count == 1)
            {

                Session["UserLoginId"] = objDs.Tables[0].Rows[0]["UserLoginId"].ToString();
                Session["UserMasterId"] = objDs.Tables[0].Rows[0]["UserMasterId"];
                Session["UserCompanyId"] = objDs.Tables[0].Rows[0]["UserCompanyId"].ToString();
                Session["UserRole"] = objDs.Tables[0].Rows[0]["UserRole"].ToString();
                Session["UserFullName"] = objDs.Tables[0].Rows[0]["UserFullName"].ToString();
               // Session["BranchId"] = objDs.Tables[0].Rows[0]["BranchId"].ToString();
                //Session["UserCode"] = objDs.Tables[0].Rows[0]["UserCode"].ToString();
                Session["CompanyLogo"] = objDs.Tables[0].Rows[0]["CompanyLogo"].ToString();

                string ipaddress = GetLocalIPAddress();
                


             int LoginHistory =   _objBALogin.UserLoginHistoryInsertUpdate( Convert.ToInt32(Session["UserMasterId"].ToString()), ipaddress,"Insert");
                if(LoginHistory >=1)

                Response.Redirect("Admin/Dashboard.aspx");
               

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", "Invalid username/password");
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
   

    protected void cmdPopup_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx");
    }
    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}
