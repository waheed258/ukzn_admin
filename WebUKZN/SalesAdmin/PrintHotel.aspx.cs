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
public partial class SalesAdmin_PrintHotel : System.Web.UI.Page
{
    private BAHotelSearch _objBAHotelSearch = new BAHotelSearch();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    private HotelSearchUtility _objHotelSearchUtility = new HotelSearchUtility();
    public string CurrencyCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
        CurrencyCode = _objBOUtiltiy.Currencycode();
        if (!IsPostBack)
        {
            if (Request.QueryString["reqstId"] != null)
            {
                int RequestId = Convert.ToInt32(_objBOUtiltiy.Decrypt(Request.QueryString["reqstId"], true));
                GetHotelRequestDetails(RequestId);
            }

        }
    }

    #region PrivateMethods
    private void GetHotelRequestDetails(int RequestId)
    {
        try
        {
            DataSet objds = _objBAHotelSearch.GetHotelSearchRequestForGenerateInvoice(RequestId);
            if (objds.Tables[0].Rows.Count > 0)
            {
                lblBookingRefNo.Text = objds.Tables[0].Rows[0]["BookingRefNo"].ToString();
                lblSupplierRefNo.Text = objds.Tables[0].Rows[0]["SupplierHotel"].ToString() + " & " + objds.Tables[0].Rows[0]["SupplierReference"].ToString();
                lblGuestName.Text = objds.Tables[0].Rows[0]["GuestTitel"].ToString() + " " + objds.Tables[0].Rows[0]["GuestFirstName"].ToString() + " " + objds.Tables[0].Rows[0]["GuestLastName"].ToString();
                lblGuestAddress.Text = objds.Tables[0].Rows[0]["GuestAddress"].ToString();
                lblCheckinDate.Text = _objBOUtiltiy.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["ArrivalDate"].ToString(), "MMMM dd, yyyy");
                lblCheckInDateformat.Text = _objBOUtiltiy.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["ArrivalDate"].ToString(), "yyyy-MM-dd");

                lblCheckoutDate.Text = _objBOUtiltiy.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["CheckOutDate"].ToString(), "MMMM dd, yyyy");
                lblDuration.Text = objds.Tables[0].Rows[0]["Duration"].ToString();
                lblPropertyName.Text = objds.Tables[0].Rows[0]["SupplierHotel"].ToString();
                lblPropertyAddress.Text = objds.Tables[0].Rows[0]["HotelAddress"].ToString();
                lblTotalToPay.Text = objds.Tables[0].Rows[0]["CustomerTotalPrice"].ToString();
                lblTotalRooms.Text = objds.Tables[0].Rows[0]["NoRooms"].ToString();
                lblRoomtype.Text = objds.Tables[0].Rows[0]["HotelRoomType"].ToString();
                lblErrotMsg.Text = objds.Tables[0].Rows[0]["errata_details"].ToString();
                if (objds.Tables[0].Rows.Count > 0)
                {

                    rptTotalGuest.DataSource = objds.Tables[0];
                    rptTotalGuest.DataBind();

                }
                else
                {
                    rptTotalGuest.DataSource = null;
                    rptTotalGuest.DataBind();
                }
            }
            PrintHotelTicket(RequestId);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "System Error", ex.Message);
        }
    }
    private void PrintHotelTicket(int RequestId)
    {
        try
        {
            DataSet objds = _objBAHotelSearch.GetHotelSearchRequestForGenerateInvoice(RequestId);
            StringBuilder str = new StringBuilder();
            string BookingRefNo = string.Empty;
            if (objds.Tables[0].Rows.Count > 0)
            {

                string strimgLogo = Server.MapPath("../pdfimages/" + Session["commlogo"].ToString());

                string CustomerName = objds.Tables[0].Rows[0]["GuestTitel"].ToString() + " " + StartWithCapitalLetter(objds.Tables[0].Rows[0]["GuestFirstName"].ToString() + " " + objds.Tables[0].Rows[0]["GuestLastName"].ToString());
                string IssueDate = _objBOUtiltiy.ReverseConvertDateFormat(System.DateTime.Now.ToShortDateString(), "yyyy-MM-dd");
                string CheckinDate = _objBOUtiltiy.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["ArrivalDate"].ToString(), "MMMM dd, yyyy");
                string NumberOfRooms = objds.Tables[0].Rows[0]["NoRooms"].ToString();
                string CheckOutDate = _objBOUtiltiy.ReverseConvertDateFormat(objds.Tables[0].Rows[0]["CheckOutDate"].ToString(), "MMMM dd, yyyy");
                string RoomType = objds.Tables[0].Rows[0]["HotelRoomType"].ToString();
                string NoRights = objds.Tables[0].Rows[0]["Duration"].ToString();
                BookingRefNo = objds.Tables[0].Rows[0]["BookingRefNo"].ToString();
                string hotelSupplier = objds.Tables[0].Rows[0]["SupplierHotel"].ToString();
                string HotelSupplierRef = objds.Tables[0].Rows[0]["SupplierReference"].ToString();
                string strHotelName = objds.Tables[0].Rows[0]["PropertyName"].ToString();
                string HotelLocation = objds.Tables[0].Rows[0]["HotelAddress"].ToString();
                string strErrotMsg = objds.Tables[0].Rows[0]["errata_details"].ToString();



                string Address = string.Empty;

                string strWeb = string.Empty;
                string NoteEmail = string.Empty;
                string NoteFaxNo = string.Empty;

                string strMobiel = objds.Tables[0].Rows[0]["BranchPhone"].ToString();
                string NoteWeb = objds.Tables[0].Rows[0]["Website"].ToString();
                string AgentOrCntName = objds.Tables[0].Rows[0]["FirstName"].ToString() + " " + objds.Tables[0].Rows[0]["LastName"].ToString();
                string FileNo = objds.Tables[0].Rows[0]["FileNo"].ToString();
                if (objds.Tables[0].Rows[0]["UserRole"].ToString() == "3")
                {
                    Address = objds.Tables[0].Rows[0]["Address"].ToString();
                    strMobiel = objds.Tables[0].Rows[0]["UserPhone"].ToString();
                    strWeb = objds.Tables[0].Rows[0]["Website"].ToString();
                    NoteEmail = objds.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                else
                {
                    Address = objds.Tables[0].Rows[0]["BranchAddress"].ToString();
                    strMobiel = objds.Tables[0].Rows[0]["BranchPhone"].ToString();
                    strWeb = objds.Tables[0].Rows[0]["Website"].ToString();
                    NoteFaxNo = objds.Tables[0].Rows[0]["BranchFax"].ToString();
                    NoteEmail = objds.Tables[0].Rows[0]["BranchEmail"].ToString();
                }

                Address = Address.Replace("/n", "<br />");

                str.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title> </title></head>");
                str.Append("<body>");
                str.Append("<table><tr><td><table><tr><td style='width: 630px;'><img src='" + strimgLogo + "'/></td>");
                str.Append("<td style='font-size:13px;'><div>" + Address + "<br />Tel : " + strMobiel + "<br />");
                str.Append("Fax: " + NoteFaxNo + "<br />Web: " + strWeb + "</div></td></tr></table></td></tr></table>");
                str.Append("<br />");

                str.Append("<table>");

                str.Append("<tr><td align='left' style='font-weight: bold;'>File No : </td><td style='width: 482px;font-size:10px;'>" + FileNo + "</td>");
                str.Append("<td align='left' style='font-weight: bold;'></td><td align='left' style='font-size:10px;'></td></tr>");

                str.Append("<tr><td align='left' style='font-weight: bold;'>Booking Ref no : </td><td style='width: 482px;font-size:10px;'>" + BookingRefNo + "</td>");
                str.Append("<td align='left' style='font-weight: bold;'>Supplier Name : </td><td align='left' style='font-size:10px;'>" + hotelSupplier + " </td></tr>");



                str.Append("<tr><td align='left' style='font-weight: bold;font-size:10px;'>Name of guest : </td><td style='width: 482px;font-size:10px;'>" + CustomerName + "</td>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Date of Issue : </td><td align='left' style='font-size:10px;'>" + IssueDate + "</td></tr>");

                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Check in : </td><td align='left' style='width: 482px;font-size:10px;'>" + CheckinDate + "</td>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>No of Rooms :</td><td align='left' style='font-size:10px;'>" + NumberOfRooms + "</td></tr>");

                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Check Out :</td><td align='left' style='width: 482px;' style='font-size:10px;'>" + CheckOutDate + "</td>");
                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Room Type :	</td><td align='left' style='font-size:10px;'>" + RoomType + "</td></tr>");

                str.Append("<td align='left' style='font-weight: bold;font-size:10px;'>Number Of Night(s) :</td><td  align='left' style='width: 482px;font-size:10px;'>" + NoRights + "</td>");
                str.Append("<td></td><td></td></tr>");
                str.Append("</table>");

                str.Append("<br />");

                str.Append("<table border='1' style='border-color: blue;'>");
                str.Append("<tr><td style='width: 300px;font-size:10px;'>Room</td><td style='width: 40px;font-size:10px;'>Adult(s)</td><td style='width: 40px;font-size:10px;'>Child(s)</td>");
                str.Append("<td style='width: 190px;font-size:10px;'>Infant(s)</td> <td style='width: 147px;font-size:10px;'>STATUS</td> </tr>");
                if (objds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < objds.Tables[0].Rows.Count; i++)
                    {
                        str.Append("<tr><td style='width: 300px;font-size:10px;'>Room " + (i + 1) + "</td><td style='width: 80px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Adults"].ToString() + "</td><td style='width: 80px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Children"].ToString() + "</td>");
                        str.Append("<td style='width: 190px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Infants"].ToString() + "</td><td style='width: 147px;font-size:10px;'>Confirmed</td> </tr>");
                    }
                }
                str.Append("</table>");

                //str.Append("<br />");
                //str.Append("<table>");
                //str.Append("<tr><td></td>  <td></td> <td align='left' style='width: 482px;font-weight: bold;font-size:10px;'>Amount Paid : </td><td align='left' style='font-size:10px;>" + CurrencyCode + " " + objds.Tables[0].Rows[0]["Amount"].ToString() + "</td></tr>");
                //str.Append("<tr><td></td>  <td></td> <td align='left' style='width: 482px;font-weight: bold;font-size:10px;'>Mode of Payment :</td><td align='left' style='font-size:10px;'>" + objds.Tables[0].Rows[0]["PaymentType"].ToString() + "</td></tr></table>");
                //str.Append("</br>");
                str.Append("  <div style='font-size: 10px; color: red; font-weight: bold;'>Note : </div>");
                str.Append("<br />");
                str.Append(strErrotMsg);

                str.Append("<br />");

                str.Append("<table>");
                str.Append("<tr><td><div style='font-weight: bold;font-size:13px;'> Kind regards,</div><div style='font-weight: bold;font-size:10px;'>" + AgentOrCntName + " | Travel Consultant </div>");
                str.Append(" <div>Tel: " + strMobiel + " | Fax: " + NoteFaxNo + " |Email:" + NoteEmail + "</div>");
                str.Append("<div>All transactions processed are subject to our Standard Terms and Conditions.</div></td></tr> </table>");
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
                TravellerEmail = objds.Tables[0].Rows[0]["UserEmail"].ToString();

                strName = objds.Tables[0].Rows[0]["UserName"].ToString();
                RefNo = objds.Tables[0].Rows[0]["HotelRequestId"].ToString();
                DisplayNameFrom = objDsEmailConfigue.Tables[0].Rows[0]["DisplayNameFrom"].ToString();
                Subject = objds.Tables[0].Rows[0]["CompanyName"].ToString() + " E-Ticket Confirmation with Ref No : " + BookingRefNo;
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
            Text2PDFnew(str.ToString(), "Hotel" + RequestId);
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
    #endregion PrivateMethods
}