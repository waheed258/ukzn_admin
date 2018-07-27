using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UKZNInterface;

public partial class SalesAdmin_AdminInvoicePdf : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAinvoice _objBAinvoice = new BAinvoice();
    BOQutation _objBOQutation = new BOQutation();
    BALFileManager _objBALFileManager = new BALFileManager();
    UKZNTravelRequest _objUKZNTravelRequest = new UKZNTravelRequest();
    string strCurrencyCode = "R";
    string _strUKZNsupplierno = ConfigurationManager.AppSettings["UKZNsupplierno"].ToString();
    decimal _decVarPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["VatPercentage"]);
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            if (Session["loginId"] == null)
            {
                Response.Redirect("../SalesLogin.aspx");
                return;
            }
            strCurrencyCode = _objBOUtiltiy.Currencycode();
            if (!IsPostBack)
            {
                string strInvocieId = "0"; ;

                if (!string.IsNullOrEmpty(Request.QueryString["FileNo"]))
                {
                    strInvocieId = Request.QueryString["FileNo"];
                }

                string strMessage = UKZNCreateInvoice(strInvocieId);
                if (strMessage != "Success")
                {
                    lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Info ", strMessage);
                    return;
                }
                DataSet objDs = _objBAinvoice.GetInvoiceDetails(strInvocieId);
                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/InvoicePdf.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();

                StringBuilder sbMainrow = new StringBuilder();

                int Flight = 0;
                int Land = 0;
                int car = 0;


                if (objDs.Tables.Count > 0)
                {
                    #region Company Deatils
                    if (objDs.Tables[5].Rows.Count > 0)
                    {
                        string strimgLogo = _objBOUtiltiy.LogoUrl(objDs.Tables[5].Rows[0]["communicationlogo"].ToString());
                        readFile = readFile.Replace("{CompanyName}", objDs.Tables[5].Rows[0]["CompanyName"].ToString());
                        readFile = readFile.Replace("{address}", objDs.Tables[5].Rows[0]["CompanyAddress"].ToString());
                        readFile = readFile.Replace("{Image}", "<img   src='" + strimgLogo + "'></img>");
                        readFile = readFile.Replace("{Image3}", " ");

                        readFile = readFile.Replace("{Invoice_No}", objDs.Tables[5].Rows[0]["FileNo"].ToString());
                        readFile = readFile.Replace("{Date}", DateTime.Now.ToString("D"));
                        readFile = readFile.Replace("{Consultant}", objDs.Tables[5].Rows[0]["UserName"].ToString());
                        readFile = readFile.Replace("{Client1}", objDs.Tables[5].Rows[0]["TravellerFullName"].ToString());
                        readFile = readFile.Replace("{Client}", objDs.Tables[5].Rows[0]["TravellerFullName"].ToString());
                        readFile = readFile.Replace("{Currency}", strCurrencyCode);
                        readFile = readFile.Replace("{ClientAddress}", objDs.Tables[5].Rows[0]["TravellerAddress"].ToString() != "" ? objDs.Tables[5].Rows[0]["TravellerAddress"].ToString() : "Durban");

                    }
                    else
                    {
                        readFile = readFile.Replace("{CompanyName}", " ");
                        readFile = readFile.Replace("{address}", " ");
                        readFile = readFile.Replace("{Country}", " ");
                        readFile = readFile.Replace("{State}", " ");
                        readFile = readFile.Replace("{City}", " ");
                        readFile = readFile.Replace("{Image}", " ");
                        readFile = readFile.Replace("{Image3}", " ");
                        readFile = readFile.Replace("{ClientAddress}", "");
                    }

                    #endregion

                    decimal LandClientTotal = 0;
                    decimal CarLandClientTotal = 0;
                    decimal FlihgtClientTotal = 0;
                    decimal ServiceFeeClientTotal = 0;
                    decimal GeneralChargeClienttotal = 0;
                    decimal FlightServiceFeeTotal = 0;
                    decimal HotelServiceFeeTotal = 0;


                    decimal FlightExclAmt = 0;
                    decimal LandExclAmt = 0;
                    decimal CarLandExclAmt = 0;
                    decimal ServiceFeeExclAmt = 0;
                    decimal GeneralChargeExclAmt = 0;

                    decimal FlightVat = 0;
                    decimal LandVat = 0;
                    decimal CarLandVat = 0;
                    decimal ServiceFeeVat = 0;
                    decimal GeneralChargeVat = 0;
                    decimal AirportTaxes = 0;

                    #region AirTicket
                    if (objDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dtlRow in objDs.Tables[0].Rows)
                        {
                            decimal FlightServiceFee = 0;
                            if (Flight == 0)
                            {
                                if (objDs.Tables[1].Rows.Count > 0)
                                {
                                    FlightServiceFee = Convert.ToDecimal(objDs.Tables[1].Rows[0]["ChargeServiceFee"]);
                                    FlightServiceFeeTotal = FlightServiceFeeTotal + FlightServiceFee;
                                }

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Air Tickets</td>");
                                sbMainrow.Append("</tr>");

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Prn</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ticket No</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Passenger/Dep Date/Route/Class</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'> Airport Taxes</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            FlihgtClientTotal = FlihgtClientTotal + Convert.ToDecimal(dtlRow["TotalPrice"]);

                            string strFlightExclAmt = "0.00";
                            if (Convert.ToDecimal(dtlRow["EquivalentBasePrice"]) != 0)
                            {
                                strFlightExclAmt = (Convert.ToDecimal(dtlRow["EquivalentBasePrice"]) + FlightServiceFee).ToString();
                                FlightExclAmt = FlightExclAmt + Convert.ToDecimal(strFlightExclAmt);
                            }
                            else
                            {
                                FlightExclAmt = FlightExclAmt + Convert.ToDecimal(dtlRow["BasePrice"]) + FlightServiceFee;
                                strFlightExclAmt = (Convert.ToDecimal(dtlRow["BasePrice"]) + FlightServiceFee).ToString();
                            }
                            FlightVat = FlightVat + 0;// Vat
                            AirportTaxes = AirportTaxes + Convert.ToDecimal(dtlRow["Taxes"]);
                            string TotalPrice = (Convert.ToDecimal(dtlRow["TotalPrice"]) + FlightServiceFee).ToString();
                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["ProviderLocatorCode"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["TicketNumber"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'/>" + dtlRow["FirstName"] + " " + dtlRow["Concats"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + strFlightExclAmt + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["Taxes"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");// Var Fee
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalPrice + "</td>");
                            sbMainrow.Append("</tr>");
                            Flight = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Air Tickets Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + (FlightExclAmt) + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + AirportTaxes + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + (FlihgtClientTotal + FlightServiceFeeTotal) + "</td></tr>");

                    }
                    #endregion AirTicket

                    #region LandArrangement
                    if (objDs.Tables[2].Rows.Count > 0)
                    {
                        decimal LandDtlTotal = 0;
                        foreach (DataRow dtlRow in objDs.Tables[2].Rows)
                        {
                            decimal HotelServiceFee = 0;
                            if (Land == 0)
                            {
                                if (objDs.Tables[1].Rows.Count > 0)
                                {
                                    HotelServiceFee = Convert.ToDecimal(objDs.Tables[3].Rows[0]["ChargeServiceFee"]);
                                    HotelServiceFeeTotal = HotelServiceFeeTotal + HotelServiceFee;
                                }
                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Land Arrangement</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                sbMainrow.Append("<td   style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td   style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            LandClientTotal = LandClientTotal + Convert.ToDecimal(dtlRow["TotalPrice"]);
                            LandExclAmt = LandExclAmt + Convert.ToDecimal(dtlRow["TotalPrice"]);
                            LandVat = LandVat + 0;
                            LandDtlTotal = Convert.ToDecimal(dtlRow["TotalPrice"]) + HotelServiceFee;

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["SupplierReference"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["BookingRefNo"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["Details"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandDtlTotal + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");// Hotel Taxes
                            sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");// Vat
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + LandDtlTotal + "</td>");
                            sbMainrow.Append("</tr>");
                            Land = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + LandExclAmt + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;;text-align:right'>0</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + (LandClientTotal + HotelServiceFeeTotal) + "</td></tr>");

                    }
                    #endregion LandArrangement


                    #region CarLandArrangement
                    if (objDs.Tables[6].Rows.Count > 0)
                    {
                        decimal CarLandDtlTotal = 0;
                        foreach (DataRow dtlRow in objDs.Tables[6].Rows)
                        {

                            if (car == 0)
                            {

                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Car Arrangement</td>");
                                sbMainrow.Append("</tr>");


                                sbMainrow.Append("<tr>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Ser RefNo</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                                sbMainrow.Append("<td   style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                                sbMainrow.Append("<td   style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                                sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                                sbMainrow.Append("</tr>");
                            }
                            CarLandClientTotal = CarLandClientTotal + Convert.ToDecimal(dtlRow["TotalPrice"]);
                            CarLandExclAmt = CarLandExclAmt + Convert.ToDecimal(dtlRow["TotalPrice"]);
                            CarLandVat = CarLandVat + 0;
                            CarLandDtlTotal = Convert.ToDecimal(dtlRow["TotalPrice"]) + 0;

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["CarModel"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["FileNo"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'> Class " + dtlRow["Class"] + "Doors " + dtlRow["CarDoors"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + CarLandDtlTotal + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");// Hotel Taxes
                            sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");// Vat
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + CarLandDtlTotal + "</td>");
                            sbMainrow.Append("</tr>");
                            car = 1;
                        }
                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Land Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + CarLandExclAmt + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;;text-align:right'>0</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + (CarLandClientTotal + 0) + "</td></tr>");

                    }
                    #endregion LandArrangement

                    //#region ServiceFee
                    //if (objDs.Tables[4].Rows.Count > 0)
                    //{
                    //    sbMainrow.Append("<tr>");
                    //    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Service Fee</td>");
                    //    sbMainrow.Append("</tr>");

                    //    sbMainrow.Append("<tr>");
                    //    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                    //    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>SourceRef</td>");
                    //    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                    //    sbMainrow.Append("<td    style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                    //    sbMainrow.Append("<td    style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                    //    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                    //    sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                    //    sbMainrow.Append("</tr>");

                    //    foreach (DataRow dtlRow in objDs.Tables[4].Rows)
                    //    {



                    //        string Clienttotal = string.IsNullOrEmpty(dtlRow["ChargeServiceFee"].ToString().Trim()) ? ".00" : dtlRow["ChargeServiceFee"].ToString().Trim();

                    //        string ExcluAmount = string.IsNullOrEmpty(dtlRow["ServiceFee"].ToString().Trim()) ? ".00" : dtlRow["ServiceFee"].ToString().Trim();

                    //        string VatAmount = string.IsNullOrEmpty(dtlRow["VatFee"].ToString().Trim()) ? ".00" : dtlRow["VatFee"].ToString().Trim();


                    //        ServiceFeeClientTotal = ServiceFeeClientTotal + Convert.ToDecimal(Clienttotal);
                    //        ServiceFeeExclAmt = ServiceFeeExclAmt + Convert.ToDecimal(ExcluAmount);
                    //        ServiceFeeVat = ServiceFeeVat + Convert.ToDecimal(VatAmount);

                    //        sbMainrow.Append("<tr>");
                    //        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service fee on sales</td>");
                    //        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["FileNo"] + "</td>");
                    //        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee On Sales</td>");// Service Fee
                    //        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ServiceFee"] + "</td>");
                    //        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");
                    //        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["VatFee"] + "</td>");
                    //        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["ChargeServiceFee"] + "</td>");
                    //        sbMainrow.Append("</tr>");


                    //    }


                    //    sbMainrow.Append("<tr>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Service Fee Total</td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + ServiceFeeExclAmt + "</td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + ServiceFeeVat + "</td>");
                    //    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + ServiceFeeClientTotal + "</td></tr>");

                    //}
                    //#endregion Service

                    decimal TotalInvExclAmt = FlightExclAmt + LandExclAmt + CarLandExclAmt + ServiceFeeExclAmt + GeneralChargeExclAmt;
                    decimal TotalVat = FlightVat + LandVat + CarLandVat + ServiceFeeVat + GeneralChargeVat;
                    decimal TotalInclAmount = FlihgtClientTotal + CarLandClientTotal + FlightServiceFeeTotal + LandClientTotal + ServiceFeeClientTotal + GeneralChargeClienttotal;

                    TotalInclAmount = Convert.ToDecimal(_objBOUtiltiy.FormatTwoDecimal(TotalInclAmount.ToString()));
                    // Invocie Total desing

                    //sbMainrow.Append("<tr>");
                    //sbMainrow.Append("<td colspan='7'><br/></td>");
                    //sbMainrow.Append("</tr>");


                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'><br/></td>");
                    sbMainrow.Append("</tr>");

                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Invoice Total</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInvExclAmt + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + AirportTaxes + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalVat + "</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");
                    // Balance From you desing
                    sbMainrow.Append("<tr>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;color:blue;'>Total Due</td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + TotalInclAmount + "</td></tr>");

                }
                readFile = readFile.Replace("{MainRows}", sbMainrow.ToString());


                string StrContent = readFile;



                string strFileName = "Invoice_" + " " + strInvocieId;
                string strFileSavePath = Server.MapPath("../PdfDocuments/Invoices/" + strFileName + ".pdf");

                GenerateHTML_TO_PDF(StrContent, true, strFileSavePath, true);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }

    }
    private void GenerateHTML_TO_PDF(string HtmlString, bool ResponseShow, string FileName, bool SaveFileDir)
    {
        try
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


            doc.Save(FileName);


            string FilePath = FileName;
            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }


            //if (FileName != "")
            //    doc.Save(FileName);

            doc.Close();

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Danger", ex.Message);
        }
    }

    private string UKZNCreateInvoice(string FileNo)
    {
        try
        {
            DataSet objDs = _objBALFileManager.GetQuotationMasterByFileNo(FileNo);
            string strMessage = string.Empty;
            int ItemInvoiceNo = 0;
            if (objDs.Tables[0].Rows.Count > 0)
            {

                int CreatedBy = Convert.ToInt32(Session["loginId"]);
                int RoleId = Convert.ToInt32(Session["role_id"]);
                int CompanyId = Convert.ToInt32(Session["CompanyId"]);

                string QuotationRefNo = objDs.Tables[0].Rows[0]["QuotationMasterId"].ToString();
                DataSet objDsQut = _objBOQutation.GetQuotationMasterDetailsWithRefNo(Convert.ToInt32(QuotationRefNo), RoleId, CompanyId, CreatedBy, "");
                TravelRequestProcess objTravelRequest = new TravelRequestProcess();
                List<UKZNInoviceDetails> LstObj = new List<UKZNInoviceDetails>();
                if (objDsQut.Tables[2].Rows.Count > 0)
                {

                    objTravelRequest.supplierno = _strUKZNsupplierno;
                    objTravelRequest.invoicenumber = objDsQut.Tables[2].Rows[0]["QuotationRefNo"].ToString().Replace("QT", "INV");
                    objTravelRequest.totamountincvat = objDsQut.Tables[2].Rows[0]["TripAmount"].ToString();
                    objTravelRequest.invoicedate = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper();
                    objTravelRequest.invoicedescription = "Invoice order no " + objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                    objTravelRequest.ordernumber = objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                    objTravelRequest.costcentre = objDsQut.Tables[2].Rows[0]["cost_center"].ToString();
                }
                if (objDsQut.Tables[0].Rows.Count > 0)// Flight Invoice
                {
                    foreach (DataRow QuotationRow in objDsQut.Tables[0].Rows)
                    {
                        ItemInvoiceNo = ItemInvoiceNo + 1;
                        UKZNInoviceDetails objF = new UKZNInoviceDetails();
                        objF.accountnumber = QuotationRow["AccountCode"].ToString();
                        objF.uniquerequestid = "F_" + QuotationRow["FlightRequestId"].ToString();// QuotationRow["Fromcode"].ToString();
                        objF.invoiceitemnumber = ItemInvoiceNo.ToString();// QuotationRow["FlightRequestId"].ToString();//"F_" + QuotationRow["FlightRequestId"].ToString();
                        objF.invoiceitemdescription = "Flight Item invoice created for file no" + FileNo;
                        objF.invoiceitemnote = "not paid";
                        objF.fullyprocessedyn = "N";//QuotationRow["Fromcode"].ToString();
                        objF.alreadypaidyn = "N";// QuotationRow["Fromcode"].ToString();
                        objF.itemamountincvat = QuotationRow["TotalPrice"].ToString();
                        objF.commentstmc = "Flight invoice created" + QuotationRow["FlightRequestId"].ToString();
                        objF.itemvatamount = _objBOUtiltiy.CalculateVat(Convert.ToDecimal(objF.itemamountincvat), _decVarPercentage).ToString();

                        LstObj.Add(objF);
                    }
                }
                if (objDsQut.Tables[1].Rows.Count > 0)// Hotel Invoice
                {
                    foreach (DataRow QuotationRow in objDsQut.Tables[1].Rows)
                    {
                        ItemInvoiceNo = ItemInvoiceNo + 1;
                        UKZNInoviceDetails objH = new UKZNInoviceDetails();
                        objH.accountnumber = QuotationRow["AccountCode"].ToString();
                        objH.uniquerequestid = "A_" + QuotationRow["HotelRequestId"].ToString();// QuotationRow["Fromcode"].ToString();
                        objH.invoiceitemdescription = "Hotel Item Invoice Created for file no" + FileNo;
                        objH.invoiceitemnumber = ItemInvoiceNo.ToString();//"A_" + QuotationRow["HotelRequestId"].ToString();
                        objH.invoiceitemnote = "not paid";
                        objH.fullyprocessedyn = "N";//QuotationRow["Fromcode"].ToString();
                        objH.alreadypaidyn = "N";// QuotationRow["Fromcode"].ToString();
                        objH.itemamountincvat = QuotationRow["TotalFee"].ToString();
                        objH.commentstmc = "Hotel Invoice Created" + QuotationRow["HotelRequestId"].ToString();
                        objH.itemvatamount = _objBOUtiltiy.CalculateVat(Convert.ToDecimal(objH.itemamountincvat), _decVarPercentage).ToString();

                        LstObj.Add(objH);
                    }
                }
                objTravelRequest.ListObjectUKZNInvoice = LstObj;
                strMessage = _objUKZNTravelRequest.validateinvoice(objTravelRequest);
                if (strMessage == "Success")
                {
                    _objBALFileManager.UpdateFileMaster(FileNo, "Invoice created in integrator");
                }

            }
            else
            {
                lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", "No order find with this order no.");
            }
            return strMessage;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", ex.Message);
            return "";
        }
    }
}