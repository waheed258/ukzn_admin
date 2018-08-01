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
using UKZNInterface.WebReference;



public partial class Login : System.Web.UI.Page
{
    private BALUser _objBALUser = new BALUser();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();

    #region UKZNWEBSERVICE
    TravelManagementUKZN objUKZN = new TravelManagementUKZN();

    G0securitytokentypeUser objSec = new G0securitytokentypeUser();

    #endregion UKZNWEBSERVICE

    //private BALUserUKZN _objBALogin = new BALUserUKZN();
    //private BOUtiltiyUKZN _objBOUtiltiy = new BOUtiltiyUKZN();
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
            DataSet objDs = _objBALUser.GetUserMasterForLogin(txtLoginId.Text.Trim(), txtPassword.Text.Trim());
            if (objDs.Tables[0].Rows.Count > 0)
            {

                Session["loginuser"] = objDs.Tables[0].Rows[0]["loginusername"].ToString();
                Session["loginId"] = objDs.Tables[0].Rows[0]["UserLoginId"].ToString();
                Session["ukzn_staff"] = objDs.Tables[0].Rows[0]["staffno"].ToString();
                Session["role_id"] = objDs.Tables[0].Rows[0]["UserRole"].ToString();
                Session["logo"] = objDs.Tables[0].Rows[0]["CompanyLoogo"].ToString();
                Session["commlogo"] = objDs.Tables[0].Rows[0]["communicationlogo"].ToString();
                Session["CompanyId"] = objDs.Tables[0].Rows[0]["UserCompany"].ToString();
                Session["BranchId"] = objDs.Tables[0].Rows[0]["BranchId"].ToString();
                Session["CompanyAddress"] = objDs.Tables[0].Rows[0]["CompanyAddress"].ToString();
                Session["agentname"] = objDs.Tables[0].Rows[0]["agentname"].ToString().ToUpper();
                Session["TicketConsId"] = objDs.Tables[0].Rows[0]["TicketConsId"].ToString().ToUpper();
                //Session["BudgetOfTravellers"] = objDs.Tables[0].Rows[0]["BudgetOfTravellers"].ToString().ToUpper();
                Session["EmpCategory"] = objDs.Tables[0].Rows[0]["empcategory"].ToString().ToUpper();
                Session["UserEmail"] = objDs.Tables[0].Rows[0]["UserEmail"].ToString().ToUpper();
                Session["UserPhone"] = objDs.Tables[0].Rows[0]["UserPhone"].ToString().ToUpper();

                Response.Redirect("SalesAdmin/Index.aspx");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info ", "Invalid username/password.");
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