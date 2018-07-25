using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.IO;
using System.Data;

public partial class ForgotPassword : System.Web.UI.Page
{



    BOUtiltiyUKZN _BOUtility = new BOUtiltiyUKZN();
    string userMasterId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
             
        }
    }
   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
       
       
    
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        
        try
        {
            // DataSet objDs = objBALInvoice.GetPdfDetails(Convert.ToInt32(lblID.Text));

           // int comapnyId = Convert.ToInt32(Session["UserCompanyId"].ToString());

            // int mergeId = 0;
            //string TempuniqCode = "";

            //     mergeId = Convert.ToInt32(objBALInvoice.GetServiceFeeMergeValue(Convert.ToInt32(lblID.Text), TempuniqCode));





            string SmtpHost = "smtp.gmail.com";
            int SmtpPort = 587;
            string MailFrom = "anitha.kodati@gmail.com";

            string FromPassword = "ammadad143";
            string MailTo = txtEmailId.Text.ToString();
            string DisplayNameTo = "Ticket Admin";
            string MailCc = "";//"serendipityflightbookings@gmail.com" + "," + TravellerEmail;
            string DisplayNameCc = "";
            string MailBcc = "";

            string Subject = "Forgot Passowrd Link";

            BALUserUKZN _objbalUser = new BALUserUKZN();

           DataSet  ds = _objbalUser.GetUserListByEmail(txtEmailId.Text.ToString());
            //string MailText = "<!DOCTYPE html><html><body><h2>COMPUTER GENERATED TAX INVOICE</h2><table><tr><td>Document No</td><td>Hofinvoice143</td></tr><tr><td>Date</td><td>Wednesday,June 28,2017</td></tr><tr><td>Consultant</td><td>Bruce  Rodda</td></tr><tr><td>Client</td><td>Sales Travel & Tours</td></tr><tr><td>Currency</td><td>ZAR(South African Rand</td></tr></table><br /><table border='1'><tr><th>PRN</th><th>Ticket No</th><th>Passenger/Dep Date/Route/Class</th><th>Excl Amt</th><th>VAT</th><th>Incl Amt</th></tr><tr><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td><td>Peter</td><td>Griffin</td></tr></table></body></html>";

            if(ds.Tables[0].Rows.Count>0)
            {
                userMasterId = ds.Tables[0].Rows[0]["UserMasterId"].ToString();
            }

            string MailText = "http://localhost:36881/ChangePassword.aspx?Id=" + HttpUtility.UrlEncode(_BOUtility.Encrypt(userMasterId));

            //string path = Server.MapPath("~/PdfDocuments");
            FileStream fStream;
            // DirectoryInfo dir = new DirectoryInfo(path);
            string filename = "";
            string Attachment = "";


            // string MailText = "<!DOCTYPE html><html lang='en'><head><meta charset='utf-8'><title>Invoice</title><style>.clearfix:after {content: '';display: table;clear: both;}</style></head><body style=' position: relative;width: 100%;height: 20%;margin: 0 auto;color: #555555;background: #FFFFFF;font-family: Arial, sans-serif;font-size: 14px;font-family: SourceSansPro;'><header class='clearfix' style='padding: 10px 0;margin-bottom: 20px;border-bottom: 1px solid #AAAAAA;'><div id='' style='float:right;'><h1 class=''>Rapid Acconting Program For Travel <br>Industry</h1><address>Albania<br>Flat No:A1,konadpur<br>Hyderabad<br>Telangana,500084</address></div><div id='' style='width:15%;'><img src='Untitled.png'></div></header><main><div id='details' class='clearfix' style='margin-bottom: 50px;'><center><strong><h2>COMPUTER GENERATED TAX INVOICE<h2></strong></center><div id='' style='float: right;text-align: right;'><h3></h3><div><span style='margin-right:100px;font-weight:bold;'>Document No</span><span>Hofinvoice143</span></div><div><span style='margin-right:98px;font-weight:bold'>Date</span><span>Wednesday,June 28,2017 </span></div><div><span style='margin-right:128px;font-weight:bold'>Consultant</span><span>Bruce Rodda</span></div><div><span style='margin-right:110px;font-weight:bold'>Client</span><span>Sales Travel & Tours</span></div><div><span style='margin-right:60px;font-weight:bold'>Currency</span><span>ZAR(South African Rand)</span></div></div></div><table style='border: 1px ridge black; width: 100%;margin-bottom: 20px;border-collapse:collapse;'><thead style='border: 1px ridge black;'><tr><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight; border-bottom: 1px ridge black;border-radius:5px;'>Prn</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight; border-bottom: 1px ridge black;'>Ticket No</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Passenger/Dep Date/Route/Class</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Excl Amt</th><th style='font-weight:bold;border: 1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>VAT</th><th style='font-weight:bold;border:1px ridge black;padding: 5px;background: weight;border-bottom: 1px ridge black;'>Incl Amt</th></tr></thead><tbody><tr><td style='border: 1px ridge black; font-weight:bold;padding:3px;'>157</td><td style='border: 1px ridge black; font-weight:bold;padding:3px;'>9148253270</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>HANIF/MUHAMMAD 27May<br>2016-01 January 1900<br>LAHORE-DOHA DOHA-<br>JOHANNESBURG Cls 0 <br>Airport Taxes<br><br><span>Ticket Totals</span></td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>8140.00<br><br><br>0.00<br><br>8140.00</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>2423.00<br><br><br><br><br>2423.00</td><td style='border: 1px ridge black;font-weight:bold;padding:3px;'>10563.00<br><br><br>0.00<br><br>10563.00</td></tr></tbody></table><hr></main><footer><div style='width:33.3%;float:right;'><img src='4.jpg' style='margin-left:200px;'></div><div style='width:33.3%;float:right;'><img src='3.jpg' style='margin-left:100px;'></div><div style='width:33.3%;float:left;'><img src='2.jpg'></div></footer></body></html>";


            //string Sendmail = _BOUtility.SendEmail.SendEmail(SmtpHost, SmtpPort, MailFrom, FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
            //              DisplayNameCc, MailBcc,  MailText, "");

            bool Sendmail = _BOUtility.SendEmail(SmtpHost, SmtpPort, MailFrom, "", FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
                          DisplayNameCc, MailBcc, Subject, MailText, Attachment);


            lblMsg.Text = _BOUtility.ShowMessage("success", "Success", "Message Successfully Send..");

        }
        catch (Exception ex)
        {

            lblMsg.Text = _BOUtility.ShowMessage("danger", "Danger", ex.Message);

        }


    
    }
}
