using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;
using System.Text;
using Utilitys;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.IO;
using BusinessManager;
using EntityManager;
using uAPIClassLib;

public partial class SalesAdmin_ShowBookinginfo : System.Web.UI.Page
{
    private BAFlightSearch _objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    decimal decServiceFee = 0;
    decimal decVatFee = 0;
    string strCurrencyCode = "R";
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["frid"]))
            {
                int nFlightReqId = 0;
                nFlightReqId = Request.QueryString["frid"].Trim() != "" ? Convert.ToInt32(Request.QueryString["frid"].ToString().Trim()) : 0;

                GenerateBookingConfirmationHtml(nFlightReqId);

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #region PrivateMethods
    private void GenerateBookingConfirmationHtml(int FlightRequestId)
    {
        try
        {

            StringBuilder str = new StringBuilder();
            DataSet objDs = _objBAFlightSearch.GetFlightBookingResponce(FlightRequestId);
            DataSet objDsTicket = _objBAFlightSearch.GetAirticketDetailsByFlightRequestId(FlightRequestId);

            string strLatDateToTicket = System.DateTime.Now.ToShortDateString();
            if (objDs.Tables[0].Rows.Count > 0)
            {

                XmlSerializer deserializer = new XmlSerializer(typeof(uAPIClassLib.UniversalReference.AirCreateReservationRsp));
                TextReader reader = new StringReader(objDs.Tables[0].Rows[0]["BookingResponce"].ToString());
                if (objDs.Tables[0].Rows[0]["TrueLastDateToTicket"].ToString() != "")
                    strLatDateToTicket = objDs.Tables[0].Rows[0]["TrueLastDateToTicket"].ToString();
                object obj = deserializer.Deserialize(reader);
                uAPIClassLib.UniversalReference.AirCreateReservationRsp objAirCreateReservationRsp = (uAPIClassLib.UniversalReference.AirCreateReservationRsp)obj;
                reader.Close();
                string strimgLogo = string.Empty;

                strimgLogo = _objBOUtiltiy.LogoUrl(objDs.Tables[0].Rows[0]["communicationlogo"].ToString()); //Server.MapPath("../pdfimages/" + objDs.Tables[0].Rows[0]["communicationlogo"].ToString());

                string Creationdate = System.DateTime.Now.ToString("dddd") + " " + System.DateTime.Now.ToString("dd MMMM, yyyy");
                string AgencyNumber = string.Empty;
                string TravlerName = string.Empty;
                string FrequentFlyerNumber = string.Empty;

                string FlightNumber = string.Empty;
                string Confirmed = string.Empty;
                string Origin = string.Empty;
                string Time = string.Empty;
                string FlightCarrier = string.Empty;
                string TravelDate = string.Empty;
                string Destination = string.Empty;
                string Baggage = string.Empty;
                string BookingRefNo = string.Empty;
                string Agencycode = string.Empty;
                string PNR = string.Empty;
                string strSupplierLocatorCode = string.Empty;
                foreach (var AgencyInfo in objAirCreateReservationRsp.UniversalRecord.AgencyInfo)
                {
                    Agencycode = AgencyInfo.AgencyCode;
                }
                BookingRefNo = objAirCreateReservationRsp.UniversalRecord.LocatorCode;

                if (objAirCreateReservationRsp.UniversalRecord.ProviderReservationInfo != null)
                {
                    PNR = objAirCreateReservationRsp.UniversalRecord.ProviderReservationInfo[0].LocatorCode;
                }
                foreach (var AirReservationin in objAirCreateReservationRsp.UniversalRecord.Items)
                {
                    if (AirReservationin.SupplierLocator[0].SupplierLocatorCode != null)
                        strSupplierLocatorCode = AirReservationin.SupplierLocator[0].SupplierLocatorCode;
                    else
                        strSupplierLocatorCode = "";
                }

                str.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><title> </title></head>");
                str.Append("<body>");

                if (objDs.Tables[0].Rows[0]["communicationlogo"].ToString() != "")
                {
                    strimgLogo = _objBOUtiltiy.LogoUrl(objDs.Tables[0].Rows[0]["communicationlogo"].ToString());//Server.MapPath("../pdfimages/" + objDs.Tables[0].Rows[0]["communicationlogo"].ToString());
                    str.Append("<table><tr><td><table><tr><td valign='top' style='width: 1000px;'><img style='width:250px;height:150px;' src='" + strimgLogo + "'/></td>");
                }
                else
                {
                    str.Append("<table><tr><td><table><tr><td style='width: 1000px;'></td>");
                }
                string Address = string.Empty;

                string strWeb = string.Empty;
                string NoteEmail = string.Empty;
                string NoteFaxNo = string.Empty;

                string strMobiel = objDs.Tables[0].Rows[0]["BranchPhone"].ToString();
                string NoteWeb = objDs.Tables[0].Rows[0]["Website"].ToString();
                string AgentOrCntName = objDs.Tables[0].Rows[0]["FirstName"].ToString() + " " + objDs.Tables[0].Rows[0]["LastName"].ToString();
                string FileNo = objDs.Tables[0].Rows[0]["FileNo"].ToString();

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
                str.Append("<td style='font-size:13px;'><div>" + Address);
                str.Append("<br />Tel : " + strMobiel + " <br />Fax: " + NoteFaxNo + "<br /> Web : " + strWeb + "</div></td></tr></table></td></tr></table>");
                str.Append("<br />");
                str.Append("<table border='1'>");
                str.Append("<tr><td style='font-size:13px; width:500px;'>File No :</td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + FileNo + "</td></tr>");
                str.Append("<tr><td style='font-size:13px; width:500px;'>Date :</td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + Creationdate + "</td></tr>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>Booking reference number : </td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + BookingRefNo + "</td></tr>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>PNR : </td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + PNR + "</td></tr>");
                str.Append("<tr><td style='font-size:13px;width:500px;'>Airline reference number : </td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + strSupplierLocatorCode + "</td></tr>");

                str.Append("<tr><td style='font-size:13px;width:500px;'>Last Date To Ticket : </td>");
                str.Append("<td align='right' style='width: 728px;height:9px;font-size:13px;'>" + _objBOUtiltiy.ReverseConvertDateFormat(strLatDateToTicket, "yyyy-MM-dd") + "</td></tr>");

                str.Append("</table>");

                str.Append("<br />");

                str.Append("<table border='1'><tr><td align='center' style='width: 1128px; background-color: gray; font-weight: bold;'>Your Travel Itinerary</td></tr></table>");
                str.Append("<br />");
                str.Append("<table border='1'  style='width: 1128px;'>");
                //str.Append("<table border='1'><tr><td align='left' style='width: 500px;font-size:10px;'>Passenger(s)</td>");
                //str.Append("<br />");
                //str.Append("<td align='left' style='width: 408px;font-size:10px;'>Frequent Flyer Numbers</td></tr>");
                str.Append("<tr><td align='left' style='font-size:11px;'>Passenger(s)</td>");

                str.Append("<td align='left' style='font-size:11px;'>Frequent Flyer Numbers</td></tr>");
                foreach (var Passenger in objAirCreateReservationRsp.UniversalRecord.BookingTraveler)
                {
                    str.Append("<tr><td align='left'>" + StartWithCapitalLetter(Passenger.BookingTravelerName.First + " " + Passenger.BookingTravelerName.Last) + " (" + _objBOUtiltiy.PassengerTypeReturn(Passenger.TravelerType) + ")</td>");

                    if (Passenger.LoyaltyCard != null)
                        str.Append("<td align='left'>" + Passenger.LoyaltyCard[0].CardNumber + "</td></tr>");
                    else
                        str.Append("<td align='left'>NA</td></tr>");
                }
                str.Append("</table>");
                str.Append("<br />");

                foreach (uAPIClassLib.UniversalReference.AirReservation objAirReservation in objAirCreateReservationRsp.UniversalRecord.Items)
                {
                    BookingRefNo = objAirCreateReservationRsp.UniversalRecord.LocatorCode;
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
                        //str.Append("<table border='1'><tr><td align='center' style='width: 911px; background-color: gray; font-weight: bold;'>AirSegment(" + i + ")</td></tr></table>");
                        str.Append("<br />");
                        str.Append("<table border='1'>");
                        str.Append("<tr><td align='center' style='width: 150px;font-size:10px;'>Flight</td>");


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
                        str.Append("<td align='left' style='width: 500px;font-size:10px;'>" + segment.Carrier + "</td>");
                        str.Append("<td align='left' style='width: 478px;font-weight: bold;font-size: 10px;background-color: gray;'>" + ConvertDate(segment.DepartureTime) + "</td></tr>");

                        str.Append("<tr><td align='left' style='width: 125px;font-size: 10px'>Airline Number</td>");
                        str.Append("<td align='left' style='width: 400px;font-size: 10px'>" + segment.FlightNumber + "</td>");
                        str.Append("<td align='left' style='width: 379px;font-size: 10px'>" + segment.Status + " - Confirmed</td></tr>");

                        str.Append("<tr><td align='left' style='width: 125px;font-size: 10px'>Class</td>");
                        str.Append("<td align='left' style='width: 400px;font-size: 10px'>" + segment.CabinClass + "</td>");
                        str.Append("<td align='left' style='width: 379px;font-size: 10px'>" + segment.ClassOfService + "</td></tr>");

                        str.Append("<tr><td align='left' style='width: 125px;font-size: 10px'>Departure</td>");
                        str.Append("<td align='left' style='width: 400px;font-size: 10px'>" + OriginDesc + "</td>");
                        str.Append("<td align='left' style='width: 379px;font-size: 10px'>" + ConvertDateString(segment.DepartureTime) + "   " + OriginTerminal + "</td></tr>");


                        str.Append("<tr><td align='left' style='width: 125px;font-size: 10px'>Arrives</td>");
                        str.Append("<td align='left' style='width: 400px;font-size: 10px'>" + DestinationDesc + "</td>");
                        str.Append("<td align='left' style='width: 379px;font-size: 10px'>" + ConvertDateString(segment.ArrivalTime) + "   " + DestinationTerminal + "</td></tr>");


                        str.Append("<tr><td align='left' style='width: 125px;font-size: 10px'>Comments</td>");
                        str.Append("<td align='left' style='width: 400px;font-size: 10px'>*Baggage Allowance 15 kgs</td>");
                        str.Append("<td align='left' style='width: 379px;font-size: 10px'>*Contact airline to confirm baggage allowance.</td></tr>");
                        str.Append("</table>");
                        str.Append("<br />");


                        str.Append("<table border='1'>");
                        str.Append("<tr><td align='left' style='width: 300px;font-size: 10px'>Passenger(s)</td>");
                        str.Append("<td align='left' style='width: 350px;font-size: 10px'>Booking Reference Number</td>");
                        str.Append("<td align='left' style='width: 178px;font-size: 10px'>Seat</td>");
                        str.Append("<td align='left' style='width: 300px;font-size: 10px'>Special Meals</td></tr>");

                        foreach (var Passenger in objAirCreateReservationRsp.UniversalRecord.BookingTraveler)
                        {
                            str.Append("<tr><td align='left' style='width: 300px;font-size: 10px'>" + Passenger.BookingTravelerName.First + " " + Passenger.BookingTravelerName.Last + " (" + _objBOUtiltiy.PassengerTypeReturn(Passenger.TravelerType) + ")</td>");
                            str.Append("<td align='left' style='width: 350px;font-size: 10px'>" + objAirCreateReservationRsp.UniversalRecord.LocatorCode + "</td>");
                            str.Append("<td align='left' style='width: 178px;font-size: 10px'>N/A</td>");
                            str.Append("<td align='left' style='width: 300px;font-size: 10px'>N/A</td></tr>");
                        }

                        str.Append("</table>");

                        str.Append("<br />");
                        i++;

                    }
                }


                str.Append("<table><tr><td><div style='font-weight: bold;'>All transactions processed are ssubject to standard terms and conditions</div>");
                str.Append("<div> We endeavour to match or beat any written quotation. The above quote is subject to availabillity at the time of the booking and is subject to the terms and conditions of ");
                str.Append(objDs.Tables[0].Rows[0]["CompanyName"].ToString() + ". It is importand to note that all airline bookings must be ticketed immeediately to avoid fluctuations and are valid for 12 hours from the time of this");
                str.Append("quotation.Prices are correct at the time of quotation adn subject to change without prior notice.</div></td> </tr>");
                str.Append("<tr><td><div style='font-weight: bold;font-size: 13px'> Kind regards,</div><div style='font-weight: bold;font-size: 10px'></div>");
                str.Append("<div>" + AgentOrCntName + "</div>");
                str.Append(" <div>Tel: " + strMobiel + " | Fax: " + NoteFaxNo + " |Email:" + NoteEmail + "</div>");
                str.Append("<div>All transactions processed are subject to our Standard Terms and Conditions.</div></td></tr> </table>");
                str.Append("</body></html>");


                GenerateHTML_TO_PDF(str.ToString(), true, "Eticket", false);
            }

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
    public string ConvertDate(string sDateString)
    {
        DateTime dtDateTime = Convert.ToDateTime(sDateString);
        //return dtDateTime.ToString("MMMM dd, yyyy");
        // return String.Format("{0:ddd, MMM d, yyyy}", dtDateTime);

        return dtDateTime.ToString("dddd") + " " + dtDateTime.ToString("dd MMMM, yyyy");
    }
    public string ConvertDateString(string sDateString)
    {
        DateTime dtDateTime = Convert.ToDateTime(sDateString);
        string sTime = dtDateTime.ToString("HH:mm");
        //sTime = sTime.Replace(":", "h");
        //sTime = sTime + "m";
        return sTime;
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
    #endregion PrivateMethods
}