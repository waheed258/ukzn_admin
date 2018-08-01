using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SwgInvoicePdf : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BAinvoice _objBAinvoice = new BAinvoice();
    string strCurrencyCode = "R";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Session["loginId"] == null)
            {
                Response.Redirect("../Login.aspx");
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

                DataSet objDs = _objBAinvoice.GetSwgAgentInvoiceDetails(strInvocieId);

                StreamReader reader = new StreamReader(Server.MapPath("~/HtmlTemps/InvoicePdf.html"));
                string readFile = reader.ReadToEnd();
                reader.Close();

                StringBuilder sbMainrow = new StringBuilder();

                int Flight = 0;
                int Land = 0;


                if (objDs.Tables.Count > 0)
                {

                    #region Company Deatils
                    if (objDs.Tables[2].Rows.Count > 0)
                    {


                        string strimgLogo = _objBOUtiltiy.LogoUrl(objDs.Tables[2].Rows[0]["communicationlogo"].ToString());
                        readFile = readFile.Replace("{CompanyName}", objDs.Tables[2].Rows[0]["CompanyName"].ToString());
                        readFile = readFile.Replace("{address}", objDs.Tables[2].Rows[0]["CompanyAddress"].ToString());
                        readFile = readFile.Replace("{Image}", "<img   src='" + strimgLogo + "'></img>");
                        readFile = readFile.Replace("{Image3}", " ");

                        readFile = readFile.Replace("{Invoice_No}", objDs.Tables[2].Rows[0]["FileNo"].ToString());
                        readFile = readFile.Replace("{Date}", DateTime.Now.ToString("D"));
                        readFile = readFile.Replace("{Consultant}", objDs.Tables[2].Rows[0]["Consultant"].ToString());
                        readFile = readFile.Replace("{Client1}", objDs.Tables[2].Rows[0]["TravellerFullName"].ToString());
                        readFile = readFile.Replace("{Client}", objDs.Tables[2].Rows[0]["TravellerFullName"].ToString());
                        readFile = readFile.Replace("{Currency}", strCurrencyCode);
                        readFile = readFile.Replace("{ClientAddress}", objDs.Tables[2].Rows[0]["TravellerAddress"].ToString() != "" ? objDs.Tables[2].Rows[0]["TravellerAddress"].ToString() : "Durban");

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
                    decimal FlihgtClientTotal = 0;
                    decimal ServiceFeeClientTotal = 0;
                    decimal GeneralChargeClienttotal = 0;


                    decimal FlightExclAmt = 0;
                    decimal LandExclAmt = 0;
                    decimal ServiceFeeExclAmt = 0;
                    decimal GeneralChargeExclAmt = 0;

                    decimal FlightVat = 0;
                    decimal LandVat = 0;
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
                                    FlightServiceFee = Convert.ToDecimal(objDs.Tables[1].Rows[0]["ChargeServiceFee"]);

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
                                strFlightExclAmt = (Convert.ToDecimal(dtlRow["EquivalentBasePrice"])).ToString();
                                FlightExclAmt = FlightExclAmt + Convert.ToDecimal(strFlightExclAmt);

                            }
                            else
                            {
                                FlightExclAmt = FlightExclAmt + Convert.ToDecimal(dtlRow["BasePrice"]);

                                strFlightExclAmt = (Convert.ToDecimal(dtlRow["BasePrice"])).ToString();
                            }
                            FlightVat = FlightVat + 0;// Vat
                            AirportTaxes = AirportTaxes + Convert.ToDecimal(dtlRow["Taxes"]);
                            string TotalPrice = (Convert.ToDecimal(dtlRow["TotalPrice"])).ToString();
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
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + FlightExclAmt + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + AirportTaxes + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                        sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + FlihgtClientTotal + "</td></tr>");

                    }
                    #endregion AirTicket



                    #region ServiceFee


                    if (objDs.Tables[2].Rows.Count > 0)
                    {


                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td colspan='7' style='background-color:#f5f5f5;border: 1px ridge black;font-weight:bold;padding:3px;color:blue;'>Tickting Fee</td>");
                        sbMainrow.Append("</tr>");


                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Type</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>SourceRef</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Details</td>");
                        sbMainrow.Append("<td    style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Excl Amt</td>");
                        sbMainrow.Append("<td    style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Taxes</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>VAT</td>");
                        sbMainrow.Append("<td style='font-weight:bold;border: 1px ridge black;padding: 5px;background-color: white;border-bottom: 1px ridge black;border-radius:5px;'>Incl Amt</td>");
                        sbMainrow.Append("</tr>");

                        foreach (DataRow dtlRow in objDs.Tables[1].Rows)
                        {



                            string Clienttotal = string.IsNullOrEmpty(dtlRow["AgentTicketFee"].ToString().Trim()) ? ".00" : dtlRow["AgentTicketFee"].ToString().Trim();

                            string ExcluAmount = string.IsNullOrEmpty(dtlRow["AgentTicketFee"].ToString().Trim()) ? ".00" : dtlRow["AgentTicketFee"].ToString().Trim();

                            string VatAmount = string.IsNullOrEmpty(dtlRow["TicketVat"].ToString().Trim()) ? ".00" : dtlRow["TicketVat"].ToString().Trim();


                            ServiceFeeClientTotal = ServiceFeeClientTotal + Convert.ToDecimal(Clienttotal);
                            ServiceFeeExclAmt = ServiceFeeExclAmt + Convert.ToDecimal(ExcluAmount);
                            ServiceFeeVat = ServiceFeeVat + Convert.ToDecimal(VatAmount);

                            sbMainrow.Append("<tr>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Tickting fee on sales</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + dtlRow["FileNo"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Tickting fee on sales</td>");// Service Fee
                            sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AgentTicketFee"] + "</td>");
                            sbMainrow.Append("<td  style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>0</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["TicketVat"] + "</td>");
                            sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + dtlRow["AgentTicketFee"] + "</td>");
                            sbMainrow.Append("</tr>");


                        }


                        sbMainrow.Append("<tr>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>Tickting Fee Total</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'></td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + ServiceFeeExclAmt + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>0</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right;'>" + ServiceFeeVat + "</td>");
                        sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;text-align:right'>" + ServiceFeeClientTotal + "</td></tr>");

                    }


                    #endregion Service

                    decimal TotalInvExclAmt = FlightExclAmt + LandExclAmt + ServiceFeeExclAmt + GeneralChargeExclAmt;
                    decimal TotalVat = FlightVat + LandVat + ServiceFeeVat + GeneralChargeVat;
                    decimal TotalInclAmount = FlihgtClientTotal + LandClientTotal + ServiceFeeClientTotal + GeneralChargeClienttotal;

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
                    sbMainrow.Append("<td style='border: 1px ridge black; font-weight:bold;padding:3px;'>" + AirportTaxes + "</td>");
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

                //GenerateHTML_TO_PDF(StrContent, true, "", false);

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
}