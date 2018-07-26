using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.UI.HtmlControls;
using uAPIClassLib;
using System.Data;
using System.IO;
using System.Xml;
using BusinessManager;
using EntityManager;
using iTextSharp;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net;
using System.Globalization;

public partial class SalesAdmin_PrintCarTicket : System.Web.UI.Page
{
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    public string CurrencyCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        try
        {
            if (Request.QueryString["reqstId"] != null)
            {
                int RequestId = Convert.ToInt32(Request.QueryString["reqstId"]);
                PrintHotelTicket(RequestId);
            }
        }
        catch (Exception ex)
        {
            _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void PrintHotelTicket(int RequestId)
    {
        try
        {
            BACarResult objCarResult = new BACarResult();
            DataSet objds = objCarResult.GetCarResult("", "", "", "", RequestId, "", "");

            StringBuilder str = new StringBuilder();
            string BookingRefNo = string.Empty;
            string FileNo = string.Empty;
            if (objds.Tables[0].Rows.Count > 0)
            {



                string IssueDate = _objBOUtiltiy.ReverseConvertDateFormat(System.DateTime.Now.ToShortDateString(), "yyyy-MM-dd");
                string CheckinDate = objds.Tables[0].Rows[0]["pickdateandtime"].ToString();
                string Pickuploaction = objds.Tables[0].Rows[0]["pickuplocation"].ToString();
                string dropLocation = objds.Tables[0].Rows[0]["droplocation"].ToString();
                string CheckOutDate = objds.Tables[0].Rows[0]["droptimeanddate"].ToString();
                string CarModel = objds.Tables[0].Rows[0]["CarModel"].ToString();

                BookingRefNo = objds.Tables[0].Rows[0]["FileNo"].ToString();

                FileNo = objds.Tables[0].Rows[0]["OrderNo"].ToString();

                str.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title> </title></head>");
                str.Append("<body>");


                str.Append("<table>");
                str.Append("<tr>");
                str.Append("<td style='width: 500px;'>");
                str.Append("<img src='http://swglive.dinoosystech.com/pdfimages/logo_ukzn.png' />");
                str.Append("</td>");
                str.Append("<td align='right' valign='top'>");
                str.Append("UKZN <br/> Durban");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("<tr>");
                str.Append("<td colspan='2' align='center' style='padding: 14px;  font-size: 26px;  color: #121213;  font-weight: bold;'>");
                str.Append("Car Rental Confirmation");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");

                str.Append("<br />");

                str.Append("<table>");

                str.Append("<tr><td align='left' style='font-weight: bold;'>Order No : </td><td style='width: 482px;font-size:10px;'>" + FileNo + "</td>");
                str.Append("<td align='left' style='font-weight: bold;'></td>File No<td align='left' style='font-size:10px;'>" + BookingRefNo + "</td></tr>");

                str.Append("<tr><td align='left' style='font-weight: bold;'>Booking Ref no : </td><td style='width: 482px;font-size:10px;'>REF00125</td>");
                str.Append("<td align='left' style='font-weight: bold;'>Car Model : </td><td align='left' style='font-size:10px;'>" + CarModel + " </td></tr>");



                str.Append("<tr>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Date of Issue : </td><td align='left' style='font-size:10px;'>" + IssueDate + "</td></tr>");

                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Pickup Date & Time : </td><td align='left' style='width: 482px;font-size:10px;'>" + CheckinDate + "</td>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Pickup location:</td><td align='left' style='font-size:10px;'>" + Pickuploaction + "</td></tr>");

                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Drop Date & Timet :</td><td align='left' style='width: 482px;' style='font-size:10px;'>" + CheckOutDate + "</td>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Drop Location:	</td><td align='left' style='font-size:10px;'>" + dropLocation + "</td></tr>");

                str.Append("<td></td><td></td></tr>");
                str.Append("</table>");

                str.Append("<br />");





                str.Append("</body></html>");
            }
            #region SendingMail
            int MailPersonId = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            int MailCategoryId = 2;

            DataSet objDsEmailConfigue = _objBOUtiltiy.GetEmailCofigue(MailPersonId, RoleId, CompanyId, MailCategoryId);
            if (objDsEmailConfigue.Tables[0].Rows.Count > 0)
            {
                string Subject = string.Empty;
                string TravellerEmail = string.Empty;
                string DisplayNameFrom = string.Empty;
                string strName = string.Empty;
                string RefNo = string.Empty;

                TravellerEmail = objds.Tables[0].Rows[0]["GuestEmail"].ToString();

                strName = objds.Tables[0].Rows[0]["GuestFirstName"].ToString() + " " + objds.Tables[0].Rows[0]["GuestLastName"].ToString();
                RefNo = objds.Tables[0].Rows[0]["HotelRequestId"].ToString();
                DisplayNameFrom = objDsEmailConfigue.Tables[0].Rows[0]["DisplayNameFrom"].ToString();
                Subject = objds.Tables[0].Rows[0]["CompanyName"].ToString() + " Hotel Booking Confirmation With File No : " + FileNo;
                string SmtpHost = objDsEmailConfigue.Tables[0].Rows[0]["SmtpHost"].ToString();
                int SmtpPort = Convert.ToInt32(objDsEmailConfigue.Tables[0].Rows[0]["SmtpPort"].ToString());// 25;
                string MailFrom = objDsEmailConfigue.Tables[0].Rows[0]["MailFrom"].ToString();

                string FromPassword = objDsEmailConfigue.Tables[0].Rows[0]["EmailPassword"].ToString();
                string MailTo = TravellerEmail;
                string DisplayNameTo = strName;
                string MailCc = objDsEmailConfigue.Tables[0].Rows[0]["Mailcc"].ToString();
                string DisplayNameCc = "";
                string MailBcc = "";

                string MailText = "Dear " + strName + "<br/>" + str.ToString();
                Utilitys.Email.SendEmail(SmtpHost, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
                     DisplayNameCc, MailBcc, Subject, MailText, "");
            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info", "Email details not found");
            }
            #endregion SendingMail
            Text2PDFnew(str.ToString(), "Car" + RequestId);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    protected void Text2PDFnew(string PDFText, string FileName)
    {
        //HttpContext context = HttpContext.Current;
        StringReader reader = new StringReader(PDFText);

        //Create PDF document 
        Document document = new Document(PageSize.A4);
        HTMLWorker parser = new HTMLWorker(document);

        string PDF_FileName = Server.MapPath("~") + "/DinoSales/TicketsPrint/" + FileName + ".pdf";
        PdfWriter.GetInstance(document, new FileStream(PDF_FileName, FileMode.Create));
        document.Open();
        try
        {
            parser.Parse(reader);
        }
        catch (Exception ex)
        {
            //Display parser errors in PDF. 
            Paragraph paragraph = new Paragraph("Error!" + ex.Message);
            Chunk text = paragraph.Chunks[0] as Chunk;
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
        finally
        {
            document.Close();
            DownLoadPdf(PDF_FileName);
        }
    }
    private void DownLoadPdf(string PDF_FileName)
    {
        try
        {
            WebClient client = new WebClient();
            Byte[] buffer = client.DownloadData(PDF_FileName);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-length", buffer.Length.ToString());
            Response.BinaryWrite(buffer);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }

    }
    public string StartWithCapitalLetter(string strInput)
    {
        //return new CultureInfo("en-US").TextInfo.ToTitleCase(strInput);
        return strInput.ToUpper();
    }
}