using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using iTextSharp;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Net;
using BusinessManager;
using System.Globalization;


public partial class SalesAdmin_PrintFile : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BACarResult objCarResult = new BACarResult();
    public string CurrencyCode = string.Empty;

    private BAHotelSearch _objBAHotelSearch = new BAHotelSearch();

    private uAPIClassLib.HotelSearchUtility _objHotelSearchUtility = new uAPIClassLib.HotelSearchUtility();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
        if (Request.QueryString["FileNo"] != null)
        {
            string FileNo = Request.QueryString["FileNo"];
            GenerateAllPdf(FileNo);
        }
    }

    #region FlightPrint
    private uAPIClassLib.UniversalReference.AirCreateReservationRsp ReadXmlFile(string xmlString)
    {

        try
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "AirCreateReservationRsp";
            XmlSerializer deserializer = new XmlSerializer(typeof(uAPIClassLib.UniversalReference.AirCreateReservationRsp), xRoot);
            TextReader textReader = new StreamReader(xmlString);
            uAPIClassLib.UniversalReference.AirCreateReservationRsp Responce;
            Responce = (uAPIClassLib.UniversalReference.AirCreateReservationRsp)deserializer.Deserialize(textReader);
            return Responce;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {

        }
    }
    private void PrintFlightTicket(int FlightRequestId, string FileNo)
    {
        try
        {
            DataSet objDs = objBAFlightSearch.GetFlightBookingResponce(FlightRequestId);
            DataSet objDsTicket = objBAFlightSearch.GetAirticketDetailsByFlightRequestId(FlightRequestId);
            StringBuilder str = new StringBuilder();
            if (objDs.Tables[1].Rows.Count > 0)
            {

                XmlSerializer deserializer = new XmlSerializer(typeof(uAPIClassLib.UniversalReference.AirCreateReservationRsp));
                TextReader reader = new StringReader(objDs.Tables[1].Rows[0]["BookingResponce"].ToString());
                object obj = deserializer.Deserialize(reader);
                uAPIClassLib.UniversalReference.AirCreateReservationRsp objAirCreateReservationRsp = (uAPIClassLib.UniversalReference.AirCreateReservationRsp)obj;
                reader.Close();
                string strimgLogo = string.Empty;

                string Creationdate = System.DateTime.Now.ToString("dddd") + " " + System.DateTime.Now.ToString("dd MMMM, yyyy");
                string AgencyNumber = string.Empty;
                string TravlerName = string.Empty;
                string FrequentFlyerNumber = string.Empty;
                string BookingRefNo = string.Empty;
                string FlightNumber = string.Empty;
                string Confirmed = string.Empty;
                string Origin = string.Empty;
                string Time = string.Empty;
                string FlightCarrier = string.Empty;
                string TravelDate = string.Empty;
                string Destination = string.Empty;
                string Baggage = string.Empty;
                string Agencycode = string.Empty;

                string strPnr = string.Empty;
                string strAirLineReferenceNo = string.Empty;
                if (objDsTicket.Tables[1].Rows.Count > 0)
                {
                    strPnr = objDsTicket.Tables[1].Rows[0]["ProviderResevrvationInfo"].ToString();
                    strAirLineReferenceNo = objDsTicket.Tables[1].Rows[0]["SupplierlocatorCode"].ToString();
                }

                foreach (var AgencyInfo in objAirCreateReservationRsp.UniversalRecord.AgencyInfo)
                {
                    Agencycode = AgencyInfo.AgencyCode;
                }
                BookingRefNo = objAirCreateReservationRsp.UniversalRecord.LocatorCode;
                str.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title> </title></head>");
                str.Append("<body>");
                str.Append("<div style='max-width: 100%;margin: auto;padding: 50px;border: 1px solid #eee;font-size: 14px;line-height: 24px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #555;'>");
                if (objDs.Tables[0].Rows[0]["communicationlogo"].ToString() != "")
                {
                    strimgLogo = _objBOUtiltiy.LogoUrl(objDs.Tables[0].Rows[0]["communicationlogo"].ToString());//Server.MapPath("../pdfimages/" + objDs.Tables[0].Rows[0]["communicationlogo"].ToString()); 

                    str.Append("<table><tr><td><table><tr><td valign='top' style='width: 690px;'><img style='width:200px;height:50px;' src='" + strimgLogo + "'/></td>");
                }
                else
                {
                    str.Append("<table><tr><td><table><tr><td style='width: 690px;'></td>");
                }
                string Address = string.Empty;

                string strWeb = string.Empty;
                string NoteEmail = string.Empty;
                string NoteFaxNo = string.Empty;

                string strMobiel = objDs.Tables[0].Rows[0]["BranchPhone"].ToString();
                string NoteWeb = objDs.Tables[0].Rows[0]["Website"].ToString();
                string AgentOrCntName = objDs.Tables[0].Rows[0]["FirstName"].ToString() + " " + objDs.Tables[0].Rows[0]["LastName"].ToString();

                if (objDs.Tables[0].Rows[0]["UserRole"].ToString() == "3")
                {
                    Address = objDs.Tables[0].Rows[0]["Address"].ToString();
                    strMobiel = objDs.Tables[0].Rows[0]["UserPhone"].ToString();
                    strWeb = objDs.Tables[0].Rows[0]["Website"].ToString();
                    NoteEmail = objDs.Tables[0].Rows[0]["UserEmail"].ToString();
                }
                else
                {
                    Address = objDs.Tables[0].Rows[0]["BranchAddress"].ToString();
                    strMobiel = objDs.Tables[0].Rows[0]["BranchPhone"].ToString();
                    strWeb = objDs.Tables[0].Rows[0]["Website"].ToString();
                    NoteFaxNo = objDs.Tables[0].Rows[0]["BranchFax"].ToString();
                    NoteEmail = objDs.Tables[0].Rows[0]["BranchEmail"].ToString();
                }
                Address = Address.Replace("/n", "<br />");
                // str.Append("<td style='font-size:13px;'><div>Silver oaks Building<br />2nd Floor Suite 3<br />36 Silverton Road Musgrave<br />Durban,South Africa <br />Tel: +27 (031) 201 0630<br />");
                str.Append("<td  style='font-size:13px;width: 260px;'><div>" + Address);
                str.Append("<br />Tel : " + strMobiel + " <br />Fax: " + NoteFaxNo + "<br />Web : " + strWeb + "</div></td></tr></table></td></tr></table>");
                str.Append("<br />");
                str.Append("<table border='1' style='border-collapse: collapse;'>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>File No :</td>");
                str.Append("<td align='right' style='width: 404px;height:9px;font-size:13px;'>" + FileNo + "</td></tr>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>Date :</td>");
                str.Append("<td align='right' style='width: 404px;height:9px;font-size:13px;'>" + Creationdate + "</td></tr>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>Booking reference number : </td>");
                str.Append("<td align='right' style='width: 404px;height:9px;font-size:13px;'>" + BookingRefNo + "</td></tr>");

                str.Append("<tr><td style='font-size:13px;width:500px;'>PNR : </td>");

                str.Append("<td align='right' style='width: 404px;height:9px;font-size:13px;'>" + strPnr + "</td></tr>");

                str.Append("<tr><td style='font-size:13px;width:500px;'>Airline reference number : </td>");

                str.Append("<td align='right' style='width: 404px;height:9px;font-size:13px;'>" + strAirLineReferenceNo + "</td></tr>");

                str.Append("</table>");
                str.Append("<br />");

                str.Append("<table border='1' style='border-collapse: collapse;'><tr><td align='center' style='width: 910px; background-color: gray; font-weight: bold;'>Your Travel Itinerary</td></tr></table>");
                str.Append("<br />");
                str.Append("<table border='1' style='border-collapse: collapse;'>");
                //str.Append("<table border='1'><tr><td align='left' style='width: 500px;font-size:10px;'>Passenger(s)</td>");
                //str.Append("<br />");
                //str.Append("<td align='left' style='width: 408px;font-size:10px;'>Frequent Flyer Numbers</td></tr>");
                str.Append("<tr><td align='left' style='font-size:13px;width:500px;'>Passenger(s)</td>");

                str.Append("<td align='left' style='font-size:13px; width:406px;'>Frequent Flyer Numbers</td></tr>");
                foreach (var Passenger in objAirCreateReservationRsp.UniversalRecord.BookingTraveler)
                {
                    str.Append("<tr><td align='left' style='font-size:13px;width:500px;'>" + StartWithCapitalLetter(Passenger.BookingTravelerName.First + " " + Passenger.BookingTravelerName.Last) + " (" + _objBOUtiltiy.PassengerTypeReturn(Passenger.TravelerType) + ")</td>");

                    if (Passenger.LoyaltyCard != null)
                        str.Append("<td align='left'>" + Passenger.LoyaltyCard[0].CardNumber + "</td></tr>");
                    else
                        str.Append("<td align='left'>NA</td></tr>");
                }
                str.Append("</table>");
                str.Append("<br />");

                foreach (uAPIClassLib.UniversalReference.AirReservation objAirReservation in objAirCreateReservationRsp.UniversalRecord.Items)
                {

                    int i = 1;
                    int k = objAirReservation.AirSegment.Length;
                    foreach (var segment in objAirReservation.AirSegment)
                    {
                        string OriginTerminal = string.Empty;
                        string DestinationTerminal = string.Empty;
                        foreach (var FlightDetailsin in segment.FlightDetails)
                        {
                            if (FlightDetailsin.OriginTerminal != null)
                                OriginTerminal = "Terminal  " + FlightDetailsin.OriginTerminal;
                            if (FlightDetailsin.DestinationTerminal != null)
                                DestinationTerminal = "Terminal  " + FlightDetailsin.DestinationTerminal;
                        }
                        //str.Append("<table border='1'><tr><td align='center' style='width: 911px; background-color: gray; font-weight: bold;'>AirSegment (" + i + ")</td></tr></table>");
                        str.Append("<br />");
                        str.Append("<table border='1' style='border-collapse: collapse;'>");
                        str.Append("<tr><td align='center' style='width: 125px;font-size:13px;'>Flight</td>");
                        string strAirLineName = string.Empty;
                        string OriginDesc = string.Empty;
                        string DestinationDesc = string.Empty;
                        DataRow[] drAirLineList = objDsTicket.Tables[2].Select("airline_code='" + segment.Carrier + "'");
                        if (drAirLineList.Length > 0)
                        {
                            strAirLineName = drAirLineList[0]["airline_name"].ToString();
                        }
                        else
                        {
                            strAirLineName = segment.Carrier;
                        }

                        DataRow[] drOrgGdsCodes;
                        drOrgGdsCodes = objDsTicket.Tables[3].Select("GdsCode='" + segment.Origin + "'");
                        if (drOrgGdsCodes.Length > 0)
                        {
                            OriginDesc = drOrgGdsCodes[0]["GdsCodeDescription"].ToString();
                        }
                        else
                        {
                            OriginDesc = segment.Origin;
                        }
                        DataRow[] drDestGdsCodes;
                        drDestGdsCodes = objDsTicket.Tables[3].Select("GdsCode='" + segment.Destination + "'");
                        if (drDestGdsCodes.Length > 0)
                        {
                            DestinationDesc = drDestGdsCodes[0]["GdsCodeDescription"].ToString();
                        }
                        else
                        {
                            DestinationDesc = segment.Origin;
                        }

                        str.Append("<td align='center' style='width: 400px;font-size:13px;'>" + strAirLineName + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-weight: bold;font-size: 13px;background-color: gray;'>" + ConvertDate(segment.DepartureTime) + "</td></tr>");

                        str.Append("<tr><td align='center' style='width: 125px;font-size: 13px'>Airline Number</td>");
                        str.Append("<td align='center' style='width: 400px;font-size: 13px'>" + segment.Carrier + segment.FlightNumber + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-size: 13px'>" + segment.Status + " - Confirmed</td></tr>");

                        str.Append("<tr><td align='center' style='width: 125px;font-size: 13px'>Class</td>");
                        str.Append("<td align='center' style='width: 400px;font-size: 13px'>" + segment.CabinClass + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-size: 13px'>" + segment.ClassOfService + "</td></tr>");

                        str.Append("<tr><td align='center' style='width: 125px;font-size: 13px'>Departure</td>");
                        str.Append("<td align='center' style='width: 400px;font-size: 13px'>" + OriginDesc + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-size: 13px'>" + ConvertDateString(segment.DepartureTime) + "   " + OriginTerminal + "</td></tr>");


                        str.Append("<tr><td align='center' style='width: 125px;font-size: 13px'>Arrives</td>");
                        str.Append("<td align='center' style='width: 400px;font-size: 13px'>" + DestinationDesc + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-size: 13px'>" + ConvertDateString(segment.ArrivalTime) + "   " + DestinationTerminal + "</td></tr>");

                        DataRow[] drTicketDetails;
                        drTicketDetails = objDsTicket.Tables[1].Select("Origin='" + segment.Origin + "' and Destination='" + segment.Destination + "'");
                        string PreviousPassType = string.Empty;
                        Baggage = string.Empty;
                        foreach (DataRow drin in drTicketDetails)
                        {
                            if (drin["TravelerType"].ToString() != PreviousPassType)
                            {
                                PreviousPassType = drin["TravelerType"].ToString();
                                Baggage = Baggage + "," + _objBOUtiltiy.PassengerTypeReturn(drin["TravelerType"].ToString()) + " " + drin["BaggageAllowance"];
                            }
                        }
                        str.Append("<tr><td align='center' style='width: 125px;font-size: 13px'>Comments</td>");
                        str.Append("<td align='center' style='width: 400px;font-size: 13px'>*Baggage Allowance :" + Baggage.TrimStart(',') + "</td>");
                        str.Append("<td align='center' style='width: 379px;font-size: 13px'>*Contact airline to confirm baggage allowance.</td></tr>");
                        str.Append("</table>");
                        str.Append("<br />");


                        str.Append("<table border='1' style='border-collapse: collapse;'>");
                        str.Append("<tr><td align='center' style='width: 270px;font-size: 13px'>Passenger(s)</td>");
                        str.Append("<td align='center' style='width: 250px;font-size: 13px'>Ticket Number</td>");
                        str.Append("<td align='center' style='width: 129px;font-size: 13px'>Seat</td>");
                        str.Append("<td align='center' style='width: 250px;font-size: 13px'>Special Meals</td></tr>");

                        //foreach (var Passenger in objAirCreateReservationRsp.UniversalRecord.BookingTraveler)
                        //{

                        foreach (DataRow drin in drTicketDetails)
                        {
                            str.Append("<tr><td align='center' style='width: 270px;font-size: 13px'>" + StartWithCapitalLetter(drin["FirstName"] + " " + drin["LastName"]) + " (" + _objBOUtiltiy.PassengerTypeReturn(drin["TravelerType"].ToString()) + ")</td>");
                            str.Append("<td align='center' style='width: 250px;font-size: 13px'>" + drin["TicketNumber"] + " (Electronic)</td>");
                            str.Append("<td align='center' style='width: 129px;font-size: 13px'>N/A</td>");
                            str.Append("<td align='center' style='width: 250px;font-size: 13px'>N/A</td></tr>");
                        }
                        // }
                        BookingRefNo = objAirCreateReservationRsp.UniversalRecord.LocatorCode;
                        str.Append("</table>");

                        str.Append("<br />");
                        i++;
                    }
                }

                #region FlightConditions
                str.Append("<table border='1' style='border-collapse: collapse;'>");
                str.Append("<tr><td align='center'  style='width: 910px; background-color: gray; font-weight: bold;'>IMPORTANT BOOKING INFORMATION</td></tr>");
                str.Append("<tr><td style='width: 904px;  font-weight: bold;'>  Booking Conditions</td></tr>");
                str.Append("<tr>");
                str.Append("<td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append("All rates provided are subject to change without prior notice due to currency adjustments and any unforeseen operator increases, until such time as full payment has been received and final documentation has been issued. Cancellation charges will be levied on all ticketed hotel and airline reservations.");
                str.Append("</p>");
                str.Append("</td>");
                str.Append(" </tr>");
                str.Append("<tr>");
                str.Append(" <td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append(" No Refund for No-shows and for cancellation after departure.");
                str.Append("</p>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("<tr>");

                str.Append("<td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append("Professional fees apply to all transactions and are not refundable in the case of cancellation.");
                str.Append("</p>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("<tr>");

                str.Append("<td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append("Until such time as full payment has been received and final documentation has been issued, rates/fares provided are subject to change, without prior notice, due to currency fluctuation and/or any unforeseen operator expenses. Confirmation and possible deposits are necessary, within 24 hours as the operator(s) reserve the right to cancel all/any provisional reservations.");
                str.Append("</p>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("<tr><td style='width: 904px;  font-weight: bold;'>Terms And Conditions</td></tr>");
                str.Append("<tr>");

                str.Append("<td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append("All reservations are subject to our Standard Terms and Conditions, copies available on request. Serendipity Tours and Travel acts as an agent between the passenger and the airline, car rental companies and tour operators. As such, we cannot be held liable for loss, damage, accident, delay or inconvenience caused by the principals concerned. Any cancellation fees incurred, or specified by the principal, will be for your account.");
                str.Append("</p>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("<tr><td style='width: 904px;  font-weight: bold;'>Cancellation Fees</td></tr>");
                str.Append("<tr>");

                str.Append("<td style='width: 904px;'>");
                str.Append("<p style='margin:1px;'>");
                str.Append("All bookings are subject to cancellation fees as stipulated by the relevant airline and/or operator");
                str.Append("</p>");
                str.Append("</td>");
                str.Append("</tr>");
                str.Append("</table>");
                #endregion FlightConditions

                str.Append("<br />");

                str.Append("<table><tr><td><div style='font-weight: bold;font-size:13px;'> Kind regards,</div><div style='font-weight: bold;font-size: 10px'></div>");
                str.Append("<div>" + AgentOrCntName + "</div>");
                str.Append(" <div>Tel: " + strMobiel + " | Fax: " + NoteFaxNo + " | Email:" + NoteEmail + "</div>");
                str.Append("<div>All transactions processed are subject to our Standard Terms and Conditions.</div></td></tr> </table>");
                str.Append("</div></body></html>");
            }
            string PDFFlightPath = Server.MapPath("~") + "DinoSales/TicketsPrint/Flight/" + "Ticket_" + FlightRequestId + ".pdf";
            //  Text2PDF(str.ToString(), PDFFlightPath);
            GenerateHTML_TO_PDF(str.ToString(), true, PDFFlightPath, true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    public string ConvertDateString(string sDateString)
    {
        DateTime dtDateTime = Convert.ToDateTime(sDateString);
        string sTime = dtDateTime.ToString("HH:mm");
        //sTime = sTime.Replace(":", "h");
        //sTime = sTime + "m";
        return sTime;
    }
    public string ConvertDate(string sDateString)
    {
        DateTime dtDateTime = Convert.ToDateTime(sDateString);
        //return dtDateTime.ToString("MMMM dd, yyyy");
        // return String.Format("{0:ddd, MMM d, yyyy}", dtDateTime);

        return dtDateTime.ToString("dddd") + " " + dtDateTime.ToString("dd MMMM, yyyy");
    }
    public string StartWithCapitalLetter(string strInput)
    {
        //return new CultureInfo("en-US").TextInfo.ToTitleCase(strInput);
        return strInput.ToUpper();
    }
    protected void Text2PDF(string PDFText, string FileName)
    {

        //HttpContext context = HttpContext.Current;
        StringReader reader = new StringReader(PDFText);

        //Create PDF document 
        Document document = new Document(PageSize.A4);

        HTMLWorker parser = new HTMLWorker(document);
        PdfWriter.GetInstance(document, new FileStream(FileName, FileMode.Create));
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
        }
        finally
        {
            document.Close();

        }
    }
    private void DownLoadPdf(string PDF_FileName)
    {
        WebClient client = new WebClient();
        Byte[] buffer = client.DownloadData(PDF_FileName);
        Response.ContentType = "application/pdf";
        Response.AddHeader("content-length", buffer.Length.ToString());
        Response.BinaryWrite(buffer);
    }
    #endregion FlightPrint

    #region HotelPrint
    private void PrintHotelTicket(int RequestId, string FileNo)
    {
        try
        {
            DataSet objds = _objBAHotelSearch.GetHotelSearchRequestForGenerateInvoice(RequestId);
            StringBuilder str = new StringBuilder();
            string BookingRefNo = string.Empty;
            if (objds.Tables[0].Rows.Count > 0)
            {
                string strimgLogo = _objBOUtiltiy.LogoUrl(objds.Tables[0].Rows[0]["communicationlogo"].ToString()); //Server.MapPath("../pdfimages/" + objds.Tables[0].Rows[0]["communicationlogo"].ToString());


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
                str.Append("<table><tr><td><table><tr><td style='width: 800px;'><img style='width:200px; height:200px;' src='" + strimgLogo + "'/></td>");
                str.Append("<td align='right' valign='top' style='font-size:13px;'><div>" + Address + "<br />Tel : " + strMobiel + "<br />");
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
                str.Append("<tr><td align='center' style='width: 300px;font-size:10px;'>Room</td><td align='center' style='width: 40px;font-size:10px;'>Adult(s)</td><td align='center' style='width: 40px;font-size:10px;'>Child(s)</td>");
                str.Append("<td align='center' style='width: 190px;font-size:10px;'>Infant(s)</td> <td align='center' style='width: 147px;font-size:10px;'>STATUS</td> </tr>");
                if (objds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < objds.Tables[0].Rows.Count; i++)
                    {
                        str.Append("<tr><td align='center' style='width: 300px;font-size:10px;'>Room " + (i + 1) + "</td><td align='center' style='width: 80px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Adults"].ToString() + "</td><td align='center' style='width: 80px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Children"].ToString() + "</td>");
                        str.Append("<td align='center' style='width: 190px;font-size:10px;'>" + objds.Tables[0].Rows[0]["Infants"].ToString() + "</td><td align='center' style='width: 147px;font-size:10px;'>Confirmed</td> </tr>");
                    }
                }
                str.Append("</table>");

                //str.Append("<br />");
                //str.Append("<table>");
                ////str.Append("<tr><td></td>  <td></td> <td align='left' style='width: 482px;font-weight: bold;font-size:10px;'> </td><td align='left' style='font-size:10px;>"  + objds.Tables[0].Rows[0]["Amount"].ToString() + "</td></tr>");
                //str.Append("<tr><td></td>  <td></td> <td align='left' style='width: 482px;font-weight: bold;font-size:10px;'>Amount Paid :</td><td align='left' style='font-size:10px;'>R " + objds.Tables[0].Rows[0]["Amount"].ToString() + "</td></tr>");
                //str.Append("<tr><td></td>  <td></td> <td align='left' style='width: 482px;font-weight: bold;font-size:10px;'>Mode of Payment :</td><td align='left' style='font-size:10px;'>" + objds.Tables[0].Rows[0]["PaymentType"].ToString() + "</td></tr></table>");
                //str.Append("</br>");
                str.Append("<div style='font-size: 10px; color: red; font-weight: bold;'>Note : </div>");
                str.Append("<br />");
                str.Append(strErrotMsg);

                str.Append("<br />");

                str.Append("<table>");
                str.Append("<tr><td><div style='font-weight: bold;font-size:13px;'> Kind regards,</div><div style='font-weight: bold;font-size:10px;'>" + AgentOrCntName + " | Travel Consultant </div>");
                str.Append(" <div>Tel: " + strMobiel + " | Fax: " + NoteFaxNo + " |Email:" + NoteEmail + "</div>");
                str.Append("<div>All transactions processed are subject to our Standard Terms and Conditions.</div></td></tr> </table>");
                str.Append("</body></html>");
            }
            string PDFHotelPath = Server.MapPath("~") + "/DinoSales/TicketsPrint/" + "Hotel" + RequestId + ".pdf";
            Text2PDF(str.ToString(), PDFHotelPath);
            // GenerateHTML_TO_PDF(str.ToString(), true, PDFHotelPath, true);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }


    #endregion HotelPrint


    #region PrivateMethods
    private void GenerateAllPdf(string FileNo)
    {
        try
        {
            string RequestId = "0";
            List<string> Sourcefiles = new List<string>();
            string PDFFileNoPath = Server.MapPath("~") + "Admin/FilePdf/" + FileNo + ".pdf";
            BALFileManager objBALFileManager = new BALFileManager();
            DataSet objDs = objBALFileManager.GetAllBookingsByFileNo(FileNo);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drflight in objDs.Tables[0].Rows)
                {
                    RequestId = drflight["FlightRequestId"].ToString();
                    PrintFlightTicket(Convert.ToInt32(drflight["FlightRequestId"]), FileNo);
                    string PDFFlightPath = Server.MapPath("~") + "DinoSales/TicketsPrint/Flight/" + "Ticket_" + drflight["FlightRequestId"] + ".pdf";
                    if (File.Exists(PDFFlightPath))
                    {
                        Sourcefiles.Add(PDFFlightPath);

                    }
                }
            }
            if (objDs.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow drhotel in objDs.Tables[1].Rows)
                {
                    if (RequestId == "0")
                        RequestId = drhotel["HotelRequestId"].ToString();
                    PrintHotelTicket(Convert.ToInt32(drhotel["HotelRequestId"]), FileNo);
                    string PDFHotelPath = Server.MapPath("~") + "/DinoSales/TicketsPrint/" + "Hotel" + drhotel["HotelRequestId"] + ".pdf";
                    if (File.Exists(PDFHotelPath))
                    {
                        Sourcefiles.Add(PDFHotelPath);
                    }
                }
            }
            DataSet objCarDs = objCarResult.GetCarResult("", "", "", "", 0, FileNo, "");
            if (objCarDs.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow drhotel in objCarDs.Tables[2].Rows)
                {

                    PrintCarTicekt(FileNo);
                    string PDFHotelPath = Server.MapPath("~") + "/DinoSales/TicketsPrint/" + "Car" + drhotel["CarDescId"] + ".pdf";
                    if (File.Exists(PDFHotelPath))
                    {
                        Sourcefiles.Add(PDFHotelPath);
                    }
                }
            }


            if (Sourcefiles.Count > 0)
            {
                MergeFiles(PDFFileNoPath, Sourcefiles.ToArray(), RequestId, FileNo);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    #endregion PrivateMethods


    #region MergerPdfs
    public void MergeFiles(string destinationFile, string[] sourceFiles, string FileRequestId, string FileNo)
    {
        if (System.IO.File.Exists(destinationFile))
            System.IO.File.Delete(destinationFile);

        string[] sSrcFile;
        sSrcFile = new string[sourceFiles.Length];

        string[] arr = new string[sourceFiles.Length];
        for (int i = 0; i < sourceFiles.Length; i++)
        {
            if (sourceFiles[i] != null)
            {
                if (sourceFiles[i].Trim() != "")
                    arr[i] = sourceFiles[i].ToString();
            }
        }

        if (arr != null)
        {

            for (int ic = 0; ic < arr.Length; ic++)
            {
                if (arr[ic] != null)
                {
                    sSrcFile[ic] = arr[ic].ToString();
                }
            }
        }
        try
        {
            int f = 0;

            PdfReader reader = new PdfReader(sSrcFile[f]);
            int n = reader.NumberOfPages;

            Document document = new Document(PageSize.A4);

            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(destinationFile, FileMode.Create));

            document.Open();
            PdfContentByte cb = writer.DirectContent;
            PdfImportedPage page;

            int rotation;
            while (f < sSrcFile.Length)
            {
                int i = 0;
                while (i < n)
                {
                    i++;

                    document.SetPageSize(PageSize.A4);
                    document.NewPage();
                    page = writer.GetImportedPage(reader, i);

                    rotation = reader.GetPageRotation(i);
                    if (rotation == 90 || rotation == 270)
                    {
                        cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                    }
                    else
                    {
                        cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }

                }

                f++;
                if (f < sSrcFile.Length)
                {
                    if (sSrcFile[f] != null)
                    {
                        reader = new PdfReader(sSrcFile[f]);
                        n = reader.NumberOfPages;
                    }

                }
            }

            document.Close();
        }
        catch (Exception e)
        {
            Response.Write(e.Message);
        }
        finally
        {
            Email(FileRequestId, FileNo, destinationFile);
            DownLoadPdf(destinationFile);

        }


    }
    #endregion MergerPdf

    private void Email(string FlightRequestId, string FileNo, string Attachment)
    {
        #region SendingMail
        string Subject = string.Empty;
        string TravellerEmail = string.Empty;
        string DisplayNameFrom = string.Empty;
        string strName = string.Empty;
        DataSet objDs = objBAFlightSearch.GetFlightBookingResponce(Convert.ToInt32(FlightRequestId));
        if (objDs.Tables[0].Rows.Count <= 0)
        {
            objDs = _objBAHotelSearch.GetHotelSearchRequestForGenerateInvoice(Convert.ToInt32(FlightRequestId));
        }

        if (objDs.Tables[1].Rows[0]["RequestFrom"].ToString() != "3")
        {
            if (objDs.Tables[2].Rows.Count > 0)
            {
                TravellerEmail = objDs.Tables[2].Rows[0]["PaxEmail"].ToString();
                strName = objDs.Tables[2].Rows[0]["PaxFirstName"].ToString() + "  " + objDs.Tables[2].Rows[0]["PaxLastName"].ToString();


            }
        }
        else
        {
            TravellerEmail = objDs.Tables[0].Rows[0]["UserEmail"].ToString();

            strName = objDs.Tables[0].Rows[0]["UserName"].ToString();
        }

        int MailPersonId = Convert.ToInt32(Session["loginId"]);
        int RoleId = Convert.ToInt32(Session["role_id"]);
        int CompanyId = Convert.ToInt32(Session["CompanyId"]);
        int MailCategoryId = 1;

        DataSet objDsEmailConfigue = _objBOUtiltiy.GetEmailCofigue(MailPersonId, RoleId, CompanyId, MailCategoryId);
        if (objDsEmailConfigue.Tables[0].Rows.Count > 0)
        {

            DisplayNameFrom = objDsEmailConfigue.Tables[0].Rows[0]["DisplayNameFrom"].ToString();
            Subject = objDs.Tables[0].Rows[0]["CompanyName"].ToString() + " Ticket's Confirmation with file no : " + FileNo;

            string SmtpHost = objDsEmailConfigue.Tables[0].Rows[0]["SmtpHost"].ToString();
            int SmtpPort = Convert.ToInt32(objDsEmailConfigue.Tables[0].Rows[0]["SmtpPort"].ToString());// 25;
            string MailFrom = objDsEmailConfigue.Tables[0].Rows[0]["MailFrom"].ToString();

            string FromPassword = objDsEmailConfigue.Tables[0].Rows[0]["EmailPassword"].ToString();
            string MailTo = TravellerEmail;
            string DisplayNameTo = strName;

            string MailCc = objDsEmailConfigue.Tables[0].Rows[0]["Mailcc"].ToString();
            string DisplayNameCc = "";
            string MailBcc = "";

            string MailText = "Dear " + StartWithCapitalLetter(strName) + "<br/> Thank you for using " + Session["agentname"].ToString() + " ." + "<br/> Please  find the attachment for tickets";
            Utilitys.Email.SendEmail(SmtpHost, SmtpPort, MailFrom, DisplayNameFrom, FromPassword, MailTo.TrimEnd(','), DisplayNameTo, MailCc,
                 DisplayNameCc, MailBcc, Subject, MailText, Attachment);
        }
        else
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info", "Email details not found");
        }
        #endregion SendingMail
    }

    #region SelectPdfLogic
    private void GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string FileName, bool SaveFileDir)
    {
        string pdf_page_size = "A4";
        SelectPdf.PdfPageSize pageSize = (SelectPdf.PdfPageSize)Enum.Parse(typeof(SelectPdf.PdfPageSize),
            pdf_page_size, true);

        string pdf_orientation = "Portrait";
        SelectPdf.PdfPageOrientation pdfOrientation =
            (SelectPdf.PdfPageOrientation)Enum.Parse(typeof(SelectPdf.PdfPageOrientation),
            pdf_orientation, true);


        int webPageWidth = 1024;


        int webPageHeight = 0;




        // instantiate a html to pdf converter object
        SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

        // set converter options
        converter.Options.PdfPageSize = pageSize;
        converter.Options.PdfPageOrientation = pdfOrientation;
        converter.Options.WebPageWidth = webPageWidth;
        converter.Options.WebPageHeight = webPageHeight;

        // create a new pdf document converting an url
        SelectPdf.PdfDocument doc = converter.ConvertHtmlString(HtmlString, "");

        // save pdf document
        if (!SaveFileDir)
            doc.Save(Response, ResponseShow, FileName);
        else
            doc.Save(FileName);

        // close pdf document
        doc.Close();
    }
    #endregion SelectPdfLogic


    #region CarPrint
    private void PrintCarTicekt(string strFileNo)
    {
        try
        {

            DataSet objds = objCarResult.GetCarResult("", "", "", "", 0, strFileNo, "");

            StringBuilder str = new StringBuilder();
            string BookingRefNo = string.Empty;
            string FileNo = string.Empty;
            string QuotationMasterID = "0";
            if (objds.Tables[0].Rows.Count > 0)
            {


                QuotationMasterID = objds.Tables[0].Rows[0]["QuotationMasterId"].ToString();
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
                str.Append("<td align='left' style='font-weight: bold;'>File No</td><td align='left' style='font-size:10px;'>" + BookingRefNo + "</td></tr>");

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
            string PDFHotelPath = Server.MapPath("~") + "/DinoSales/TicketsPrint/" + "Car" + QuotationMasterID + ".pdf";
            Text2PDF(str.ToString(), PDFHotelPath);
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    #endregion CarPrint
}