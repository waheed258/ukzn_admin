using BusinessManager;
using DataManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_SendEmailUsers : System.Web.UI.Page
{
    BOUtiltiy _BOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            BOUKZN _objBOUKZN = new BOUKZN();
            DataSet ds = _objBOUKZN.GetDataSendmail();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string username = dr["UserName"].ToString();

                    string SmtpHost = "smtp.gmail.com";
                    int SmtpPort = 587;
                    string MailFrom = "testingformail12@gmail.com";
                    string FromPassword = "testing123";
                    string MailTo = ds.Tables[0].Rows[0]["UserEmail"].ToString();
                    //  string MailTo = "budagavichandra@gmail.com";
                    //ds.Tables[0].Rows[0]["Airline_Name"].ToString();

                    string DisplayNameTo = "Ticket Admin";
                    string MailCc = "";//"serendipityflightbookings@gmail.com" + "," + TravellerEmail;
                    string DisplayNameCc = "";
                    string MailBcc = "";


                    string Subject = "test Subject ";

                    //Set Body On Table User Id and Password 
                    string MailText = "Hello" + " " + username + "<Br>" + "User Name" + " : " + dr["LoginId"].ToString() + "<Br>" + "Password" + ":" + dr["Password"].ToString();
                    string filename = "";
                    string Attachment = "";


                    bool Sendmail = Utilitys.Email.SendEmail(SmtpHost, SmtpPort, MailFrom, "TMS", FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
                    DisplayNameCc, MailBcc, Subject, MailText, "");



                }
            }
            else
            {
                lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", "no data");
            }

        }
        catch (Exception ex)
        {

            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);
        }
    }
}