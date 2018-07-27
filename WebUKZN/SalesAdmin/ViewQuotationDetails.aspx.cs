using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using uAPIClassLib;
using uAPIClassLib.AirReference;
using uAPIClassLib.UniversalReference;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using BusinessManager;
using System.Xml;
using Utilitys;
using System.Data;
using System.Configuration;
using EntityManager;


public partial class SalesAdmin_ViewQuotationDetails : System.Web.UI.Page
{
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    SearchUtility objSearchUtility = new SearchUtility();
    BALUser objBalUser = new BALUser();
    BOQutation objBOQutation = new BOQutation();
    BAHotelSearch _objBAHotelSearch = new BAHotelSearch();
    FlightSearchXmlParsing objFlightSearchXmlParsing = new FlightSearchXmlParsing();
    decimal decServiceFee = 0;
    string strCurrencyCode = "R";

    int TripType = 0;
    int TotalPax = 0;
    DataTable dtQuotation = new DataTable();
    DataTable dtHotelQuotation = new DataTable();
    DataSet objDsAirlinecode = null;
    decimal VarPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["VatPercentage"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //if (Session["loginId"] == null)
            //{
            //    Response.Redirect("../Login.aspx");
            //}
            strCurrencyCode = objBOUtiltiy.Currencycode();
            if (!IsPostBack)
            {
                Session.Remove("returnurl");
                if (Session["FileNo"] == null)
                {
                    Session["FileNo"] = 0;
                }

                string QuotationRefNo = Request.QueryString["QutRef"];
                objDsAirlinecode = objBAFlightSearch.GetAirticketDetailsByFlightRequestId(0);


                int CreatedBy = Convert.ToInt32(Session["loginId"]);
                int RoleId = Convert.ToInt32(Session["role_id"]);
                int CompanyId = Convert.ToInt32(Session["CompanyId"]);


                DataSet objDsQut = objBOQutation.GetAllQuotationByQuotationMaster(Convert.ToInt32(QuotationRefNo), RoleId, CompanyId, CreatedBy);
                if (objDsQut.Tables[2].Rows.Count > 0)
                {

                    lblOrderNo.Text = "UKZN Order No : " + objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                    Session["UKZNORDERNO"] = objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                    Session["CarResult"] = 0;
                }
                if (objDsQut.Tables[0].Rows.Count > 0)
                {
                    QuotationConfirmation(objDsQut);
                    //pnlFlight.Visible = true;
                }
                else
                {
                    //pnlFlight.Visible = false;
                }
                if (objDsQut.Tables[1].Rows.Count > 0)
                {
                    GenerateHotelQuotation(objDsQut);
                    // pnlHotel.Visible = true;
                }
                else
                {
                    // pnlHotel.Visible = false;
                }
                if (objDsQut.Tables[3].Rows.Count > 0)
                {
                    rptCars.DataSource = objDsQut.Tables[3];
                    rptCars.DataBind();
                }
                else
                {

                }

            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #region PrivateMethods
    private void QuotationConfirmation(DataSet objDsQut)
    {
        try
        {
            List<OneWayRoundTripOptinKey> lstobjOneWayRoundTripOptinKey = new List<OneWayRoundTripOptinKey>();

            string strFlightRequestId = "0";



            if (objDsQut.Tables[0].Rows.Count > 0)
            {


                // lblQuotationRef.Text = objDsQut.Tables[0].Rows[0]["FlightQuotationReference"].ToString();
                strFlightRequestId = objDsQut.Tables[0].Rows[0]["FlightRequestId"].ToString();
                foreach (DataRow QuotationRow in objDsQut.Tables[0].Rows)
                {

                    OneWayRoundTripOptinKey objOneWayRoundTripOptinKey = new OneWayRoundTripOptinKey();
                    string strSplitKeys = QuotationRow["HtmlText"].ToString();
                    var strKeys = strSplitKeys.Split('~');
                    objOneWayRoundTripOptinKey.OptionKey = strKeys[0];
                    objOneWayRoundTripOptinKey.RetrunOptionKey = strKeys[1];
                    objOneWayRoundTripOptinKey.FromCode = QuotationRow["Fromcode"].ToString();
                    objOneWayRoundTripOptinKey.Tocode = QuotationRow["ToCode"].ToString();
                    objOneWayRoundTripOptinKey.FlightDate = QuotationRow["FlightDate"].ToString();
                    objOneWayRoundTripOptinKey.RoundTrip = strKeys[1] != "" ? true : false;
                    objOneWayRoundTripOptinKey.status = QuotationRow["status"].ToString();
                    objOneWayRoundTripOptinKey.TotalFlightPrice = QuotationRow["TotalPrice"].ToString();
                    objOneWayRoundTripOptinKey.ServiceFee = QuotationRow["ServiceFee"].ToString();
                    objOneWayRoundTripOptinKey.ApprovalStatus = QuotationRow["ApprovalStatus"].ToString();
                    objOneWayRoundTripOptinKey.TravellerId = QuotationRow["TravellerId"].ToString();
                    objOneWayRoundTripOptinKey.FlightRequestId = QuotationRow["FlightRequestId"].ToString();

                    lstobjOneWayRoundTripOptinKey.Add(objOneWayRoundTripOptinKey);
                }

            }

            if (lstobjOneWayRoundTripOptinKey.Count == 0)
            {
                lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Info", "No quotations found");
                return;
            }

            int FlightRequestId = Convert.ToInt32(strFlightRequestId);
            DataSet objDs = objBAFlightSearch.GetFlightRequestDetails(FlightRequestId);
            string strOrigincode = string.Empty;
            string strDestinationCode = string.Empty;
            string strDate = string.Empty;
            int Adults = 0;
            int Childs = 0;
            int Infrants = 0;
            if (objDs.Tables[0].Rows.Count > 0)
            {
                strOrigincode = objDs.Tables[0].Rows[0]["Fromcode"].ToString();
                // lblFrom.Text = objDs.Tables[0].Rows[0]["FlightFrom"].ToString();
                strDestinationCode = objDs.Tables[0].Rows[0]["ToCode"].ToString();
                //  lblTo.Text = objDs.Tables[0].Rows[0]["FlightTo"].ToString();
                strDate = objDs.Tables[0].Rows[0]["FlightDate"].ToString();
                Adults = Convert.ToInt32(objDs.Tables[0].Rows[0]["Adults"]);
                Childs = Convert.ToInt32(objDs.Tables[0].Rows[0]["Child"]);
                Infrants = Convert.ToInt32(objDs.Tables[0].Rows[0]["Infrants"]);
            }
            TotalPax = Adults + Childs + Infrants;

            string FileName = FlightRequestId + "_" + strOrigincode + "_" + strDestinationCode + "_" + objBOUtiltiy.ReverseConvertDateFormat(strDate, "yyyy-MM-dd"); ;
            string XmlFilePath = Server.MapPath("/DinoSales/Xml/Air/" + FileName + ".xml");

            AirSearchRsp objAirSearchRsp = ReadXmlFile(XmlFilePath);
            if (lstobjOneWayRoundTripOptinKey.Count > 0)
            {
                dtQuotation.Clear();
                dtQuotation.Columns.Add("OptionText");
                dtQuotation.Columns.Add("FlightNo");
                dtQuotation.Columns.Add("AirLineCode");
                dtQuotation.Columns.Add("FromCode");

                dtQuotation.Columns.Add("ToCode");
                dtQuotation.Columns.Add("DeptDateAndTime");
                dtQuotation.Columns.Add("ArrivalDateAndtime");
                dtQuotation.Columns.Add("TotalPrice");
                dtQuotation.Columns.Add("TrClass");
                dtQuotation.Columns.Add("CabinClass");
                dtQuotation.Columns.Add("OptionKeys");
                dtQuotation.Columns.Add("airlineimgsrc");
                dtQuotation.Columns.Add("Quotationkeys");
                dtQuotation.Columns.Add("CurrencyCode");
                dtQuotation.Columns.Add("ServiceFee");
                dtQuotation.Columns.Add("Status");

                dtQuotation.Columns.Add("FlightRequestId");
                dtQuotation.Columns.Add("TrvellerId");
                int Optioncount = 1;
                foreach (var loopQuotation in lstobjOneWayRoundTripOptinKey)
                {
                    string OptionText = "Proceed for booking";
                    string strKey = loopQuotation.OptionKey;
                    string strRetKey = loopQuotation.RetrunOptionKey;
                    GenerateQuotationHtml(objAirSearchRsp, strKey, strRetKey, OptionText, loopQuotation.TotalFlightPrice, loopQuotation.ServiceFee, loopQuotation.status,
                        loopQuotation.FlightRequestId, loopQuotation.TravellerId);
                    Optioncount++;
                }
                if (dtQuotation.Rows.Count > 0)
                {
                    rptQuotation.DataSource = dtQuotation;
                    rptQuotation.DataBind();
                }
                else
                {
                    rptQuotation.DataSource = null;
                    rptQuotation.DataBind();
                    lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Info", "No quotations found");
                }
            }
            else
            {
                lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Info", "No quotations found");
                return;
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private AirSearchRsp ReadXmlFile(string FilePath)
    {

        try
        {

            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "LowFareSearchRsp";
            XmlSerializer deserializer = new XmlSerializer(typeof(AirSearchRsp), xRoot);
            TextReader textReader = new StreamReader(FilePath);
            AirSearchRsp Responce;
            Responce = (AirSearchRsp)deserializer.Deserialize(textReader);
            return Responce;
        }
        catch
        {
            try
            {

                XmlRootAttribute xRoot = new XmlRootAttribute();
                xRoot.ElementName = "AirSearchRsp";
                XmlSerializer deserializer = new XmlSerializer(typeof(AirSearchRsp), xRoot);
                TextReader textReader = new StreamReader(FilePath);
                AirSearchRsp Responce;
                Responce = (AirSearchRsp)deserializer.Deserialize(textReader);
                return Responce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    public void GenerateQuotationHtml(uAPIClassLib.AirReference.AirSearchRsp objresp, string Key, string retkey, string OptionText, string FlightTotalPrice, string ServiceFee,
        string Status, string strFlightRequestId, string strTravellerId)
    {

        string TotalPrice = "0";
        string KeyConfig = string.Empty;
        string strBothKeys = string.Empty;

        string FareInfoRef = string.Empty;
        List<uAPIClassLib.AirReference.typeBaseAirSegment> segments = new List<uAPIClassLib.AirReference.typeBaseAirSegment>();
        List<uAPIClassLib.AirReference.SearchPassenger> passengers = new List<uAPIClassLib.AirReference.SearchPassenger>();
        List<uAPIClassLib.AirReference.Connection> Connections = new List<uAPIClassLib.AirReference.Connection>();
        int StopCount = 0;
        foreach (uAPIClassLib.AirReference.AirPricePointList Itemsin in objresp.Items)
        {
            if (StopCount == 0)
            {
                foreach (var AirPricePointin in Itemsin.AirPricePoint)
                {
                    TotalPrice = AirPricePointin.TotalPrice;
                    decimal FlightPrice = Convert.ToDecimal(objBOUtiltiy.RemovecurrencyCode(TotalPrice));
                    foreach (var AirPricingInfoin in AirPricePointin.AirPricingInfo)
                    {
                        uAPIClassLib.AirReference.FlightOption[] objFlightOption = AirPricingInfoin.FlightOptionsList.Where(fol => fol.LegRef == Key).ToArray();
                        uAPIClassLib.AirReference.FlightOption[] objFlightOptionReturnCal = AirPricingInfoin.FlightOptionsList.Where(fol => fol.LegRef == retkey).ToArray();

                        int RetrunStopsPriceCal = 0;
                        int OnwayTotalStops = 0;
                        string strCabinClass = string.Empty;
                        string strBookingcode = string.Empty;
                        foreach (var returnoptioninCal in objFlightOptionReturnCal)
                        {
                            foreach (var retOptioninCal in returnoptioninCal.Option)
                            {
                                RetrunStopsPriceCal = retOptioninCal.BookingInfo.Length;
                            }
                        }
                        foreach (var optioninCal in objFlightOption)
                        {
                            foreach (var OptioninCal in optioninCal.Option)
                            {
                                OnwayTotalStops = OptioninCal.BookingInfo.Length;
                            }
                        }
                        foreach (var FlightOptionsListin in AirPricingInfoin.FlightOptionsList)
                        {
                            #region ForwardFlightConfirm
                            List<uAPIClassLib.AirReference.Option> objOptins = FlightOptionsListin.Option.Where(opt => opt.Key == Key).ToList();

                            if (objOptins != null)
                            {
                                for (int opt = 0; opt < objOptins.Count; opt++)
                                {
                                    int BookingInfoFirstElement = 0;
                                    string CalculatedTotalPrice = "0";
                                    string strclass = "border_bottom1";
                                    uAPIClassLib.AirReference.BookingInfo[] objBookingInfo = objOptins[opt].BookingInfo;
                                    foreach (var objBookingInfoin in objBookingInfo)
                                    {
                                        strCabinClass = objBookingInfoin.CabinClass;
                                        strBookingcode = objBookingInfoin.BookingCode;
                                        uAPIClassLib.AirReference.typeBaseAirSegment[] objAirSegment = objresp.AirSegmentList.Where(seg => seg.Key == objBookingInfoin.SegmentRef).ToArray();
                                        if (objAirSegment != null)
                                        {
                                            foreach (var inAirSegment in objAirSegment)
                                            {
                                                uAPIClassLib.AirReference.typeBaseAirSegment segment = new uAPIClassLib.AirReference.typeBaseAirSegment();

                                                if (BookingInfoFirstElement == 0)
                                                {
                                                    CalculatedTotalPrice = FlightTotalPrice;
                                                    strclass = "border_bottom";
                                                    if (retkey != "")
                                                    {
                                                        KeyConfig = "?optkey=" + Key + "&optkeyret=" + retkey;
                                                        strBothKeys = Key + "~" + retkey;
                                                    }
                                                    else
                                                    {
                                                        KeyConfig = "?optkey=" + Key;
                                                        strBothKeys = Key + "~";
                                                    }
                                                }
                                                else
                                                {
                                                    OptionText = "";
                                                    CalculatedTotalPrice = "";
                                                    strclass = "border_bottom1";
                                                }
                                                BookingInfoFirstElement = 1;
                                                DataRow Segment1 = dtQuotation.NewRow();
                                                Segment1["OptionText"] = OptionText;
                                                Segment1["FlightNo"] = inAirSegment.FlightNumber;


                                                Segment1["FlightRequestId"] = strFlightRequestId;
                                                Segment1["TrvellerId"] = strTravellerId;

                                                string strAirLineName = string.Empty;
                                                DataRow[] drAirLineList = objDsAirlinecode.Tables[2].Select("airline_code='" + inAirSegment.Carrier + "'");
                                                if (drAirLineList.Length > 0)
                                                {
                                                    strAirLineName = drAirLineList[0]["airline_name"].ToString();
                                                }
                                                else
                                                {
                                                    strAirLineName = segment.Carrier;
                                                }
                                                Segment1["AirLineCode"] = strAirLineName;

                                                Segment1["airlineimgsrc"] = objBOUtiltiy.LogoUrl() + "DinoSales/airline_logo/" + inAirSegment.Carrier + ".gif";
                                                string OriginDesc = string.Empty;
                                                string DestinationDesc = string.Empty;

                                                DataRow[] drOrgGdsCodes;
                                                drOrgGdsCodes = objDsAirlinecode.Tables[3].Select("GdsCode='" + inAirSegment.Origin + "'");
                                                if (drOrgGdsCodes.Length > 0)
                                                {
                                                    OriginDesc = drOrgGdsCodes[0]["GdsCodeDescription"].ToString();
                                                }
                                                else
                                                {
                                                    OriginDesc = inAirSegment.Origin;
                                                }
                                                Segment1["FromCode"] = OriginDesc;
                                                DataRow[] drDestGdsCodes;
                                                drDestGdsCodes = objDsAirlinecode.Tables[3].Select("GdsCode='" + inAirSegment.Destination + "'");
                                                if (drDestGdsCodes.Length > 0)
                                                {
                                                    DestinationDesc = drDestGdsCodes[0]["GdsCodeDescription"].ToString();
                                                }
                                                else
                                                {
                                                    DestinationDesc = inAirSegment.Destination;
                                                }
                                                Segment1["ToCode"] = DestinationDesc;
                                                Segment1["DeptDateAndTime"] = objBOUtiltiy.ConvertDateString(inAirSegment.DepartureTime);
                                                Segment1["ArrivalDateAndtime"] = objBOUtiltiy.ConvertDateString(inAirSegment.ArrivalTime);
                                                Segment1["TotalPrice"] = CalculatedTotalPrice;
                                                Segment1["ServiceFee"] = ServiceFee;
                                                Segment1["Status"] = Status;
                                                Segment1["TrClass"] = strclass;
                                                Segment1["CabinClass"] = strCabinClass;
                                                Segment1["OptionKeys"] = KeyConfig;
                                                Segment1["Quotationkeys"] = strBothKeys;

                                                Segment1["CurrencyCode"] = CalculatedTotalPrice != "" ? strCurrencyCode : "";
                                                dtQuotation.Rows.Add(Segment1);


                                            }
                                        }
                                    }
                                }
                            }
                            #endregion ForwardFlightConfirm


                            if (retkey != "")
                            {
                                #region ReturnFlightConfirm
                                if (retkey != "")
                                {

                                    List<uAPIClassLib.AirReference.Option> objReturnOptins = FlightOptionsListin.Option.Where(opt => opt.Key == retkey).ToList();
                                    if (objOptins != null)
                                    {
                                        for (int opt = 0; opt < objReturnOptins.Count; opt++)
                                        {

                                            uAPIClassLib.AirReference.BookingInfo[] objBookingInfo = objReturnOptins[opt].BookingInfo;
                                            foreach (var objBookingInfoin in objBookingInfo)
                                            {
                                                strCabinClass = objBookingInfoin.CabinClass;
                                                strBookingcode = objBookingInfoin.BookingCode;
                                                uAPIClassLib.AirReference.typeBaseAirSegment[] objAirSegment = objresp.AirSegmentList.Where(seg => seg.Key == objBookingInfoin.SegmentRef).ToArray();
                                                if (objAirSegment != null)
                                                {
                                                    foreach (var inAirSegment in objAirSegment)
                                                    {
                                                        DataRow Segment1 = dtQuotation.NewRow();
                                                        Segment1["OptionText"] = "";
                                                        Segment1["FlightNo"] = inAirSegment.FlightNumber;

                                                        string strAirLineName = string.Empty;
                                                        DataRow[] drAirLineList = objDsAirlinecode.Tables[2].Select("airline_code='" + inAirSegment.Carrier + "'");
                                                        if (drAirLineList.Length > 0)
                                                        {
                                                            strAirLineName = drAirLineList[0]["airline_name"].ToString();
                                                        }
                                                        else
                                                        {
                                                            strAirLineName = inAirSegment.Carrier;
                                                        }
                                                        Segment1["AirLineCode"] = strAirLineName;

                                                        Segment1["airlineimgsrc"] = objBOUtiltiy.LogoUrl() + "DinoSales/airline_logo/" + inAirSegment.Carrier + ".gif";
                                                        string OriginDesc = string.Empty;
                                                        string DestinationDesc = string.Empty;

                                                        DataRow[] drOrgGdsCodes;
                                                        drOrgGdsCodes = objDsAirlinecode.Tables[3].Select("GdsCode='" + inAirSegment.Origin + "'");
                                                        if (drOrgGdsCodes.Length > 0)
                                                        {
                                                            OriginDesc = drOrgGdsCodes[0]["GdsCodeDescription"].ToString();
                                                        }
                                                        else
                                                        {
                                                            OriginDesc = inAirSegment.Origin;
                                                        }
                                                        Segment1["FromCode"] = OriginDesc;
                                                        DataRow[] drDestGdsCodes;
                                                        drDestGdsCodes = objDsAirlinecode.Tables[3].Select("GdsCode='" + inAirSegment.Destination + "'");
                                                        if (drDestGdsCodes.Length > 0)
                                                        {
                                                            DestinationDesc = drDestGdsCodes[0]["GdsCodeDescription"].ToString();
                                                        }
                                                        else
                                                        {
                                                            DestinationDesc = inAirSegment.Destination;
                                                        }

                                                        Segment1["ToCode"] = DestinationDesc;
                                                        Segment1["DeptDateAndTime"] = objBOUtiltiy.ConvertDateString(inAirSegment.DepartureTime);

                                                        Segment1["ArrivalDateAndtime"] = objBOUtiltiy.ConvertDateString(inAirSegment.ArrivalTime);
                                                        Segment1["TotalPrice"] = "";
                                                        Segment1["TrClass"] = "border_bottom1";
                                                        Segment1["CabinClass"] = strCabinClass;
                                                        Segment1["OptionKeys"] = "";
                                                        Segment1["Quotationkeys"] = "";
                                                        Segment1["CurrencyCode"] = "";
                                                        dtQuotation.Rows.Add(Segment1);
                                                    }
                                                }
                                            }
                                        }
                                    }

                                }
                                #endregion ReturnFlightConfirm
                            }
                        }
                    }
                }

            }
            StopCount = 1;
        }


    }
    private void SetServiceFeeSessions(decimal ServiceFee)
    {
        try
        {
            decimal VatAmount = CalculateVatAmount(ServiceFee, VarPercentage);

            Session["ServiceFee"] = ServiceFee - VatAmount;
            Session["VatAmount"] = VatAmount;
        }
        catch
        {

            Session["ServiceFee"] = 0;
            Session["VatAmount"] = 0;
        }
    }
    private decimal CalculateVatAmount(decimal ServiceFee, decimal VatPer)
    {
        decimal vatFee = 0;
        try
        {

            vatFee = (ServiceFee * VatPer / 100);

            return decimal.Round(vatFee, 2);
        }
        catch (Exception ex)
        {
            return vatFee;
        }
    }
    protected void rptQuotation_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string BookingKeys = e.CommandArgument.ToString();
        HiddenField hfServiceFee = (HiddenField)e.Item.FindControl("hfServiceFee");
        HiddenField hfFlightRequestId = (HiddenField)e.Item.FindControl("hfFlightRequestId");
        HiddenField hfTrvellerId = (HiddenField)e.Item.FindControl("hfTrvellerId");
        Session["TrvellerId"] = hfTrvellerId.Value;
        Session["FlightRequestId"] = hfFlightRequestId.Value;
        if (e.CommandName == "Proceed for booking")
        {
            SetServiceFeeSessions(Convert.ToDecimal(hfServiceFee.Value));
            //Session["returnurl"] = "../Admin/ViewQuotationDetails.aspx?QutRef=" + Request.QueryString["QutRef"];
            Session["returnurl"] = "../Admin/ViewQuotationDetails.aspx?QutRef=" + Request.QueryString["QutRef"];
            Response.Redirect("../DinoSales/BookingPriceConfirmation.aspx" + BookingKeys);
        }
    }

    #endregion PrivateMethods

    #region HotelQuotation
    private void GenerateHotelQuotation(DataSet objDs)
    {

        if (objDs.Tables[1].Rows.Count > 0)
        {
            dtHotelQuotation.Clear();
            dtHotelQuotation.Columns.Add("HotelQuotationId");
            dtHotelQuotation.Columns.Add("HotelOptionNo");
            dtHotelQuotation.Columns.Add("HotelOptionText");
            dtHotelQuotation.Columns.Add("PropertyName");
            dtHotelQuotation.Columns.Add("HotelAddress");
            dtHotelQuotation.Columns.Add("NoPax");
            dtHotelQuotation.Columns.Add("HotelNoNights");
            dtHotelQuotation.Columns.Add("HotelRoomType");
            dtHotelQuotation.Columns.Add("MealDesc");

            dtHotelQuotation.Columns.Add("HotelTotalRooms");
            dtHotelQuotation.Columns.Add("HotelCostFare");
            dtHotelQuotation.Columns.Add("HotelServiceFee");

            dtHotelQuotation.Columns.Add("HotelBookingText");
            dtHotelQuotation.Columns.Add("HotelStatus");
            dtHotelQuotation.Columns.Add("ApprovalStatus");

            dtHotelQuotation.Columns.Add("HotelRequestId");
            dtHotelQuotation.Columns.Add("TrvellerId");
            GeneratehotelQuotation(objDs);
        }
        else
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "Info", "No hotel quotations found");
            return;
        }
        //  Session.Remove("HotelQuotationCart");
    }
    private DataSet ReadHotelXmlFile(string FilePath)
    {
        string myXMLfile = FilePath;
        DataSet ds = new DataSet();

        System.IO.FileStream fsReadXml = new System.IO.FileStream
            (myXMLfile, System.IO.FileMode.Open);
        try
        {
            ds.ReadXml(fsReadXml);
            return ds;
        }
        catch (Exception ex)
        {
            return null;
        }
        finally
        {
            fsReadXml.Close();
        }
    }
    private void GeneratehotelQuotation(DataSet objDs)
    {
        try
        {

            string FileName = "AgentId_Res" + objDs.Tables[1].Rows[0]["HotelRequestId"].ToString();
            string GetFilePath = Server.MapPath("/DinoSales/Xml/Hotel/" + FileName + ".xml");


            DataSet objDsHotelResult = ReadHotelXmlFile(GetFilePath);
            if (objDsHotelResult.Tables.Count > 0)
            {

                DataSet objdsSearch = _objBAHotelSearch.GetHotelSearchRequest(Convert.ToInt32(objDs.Tables[1].Rows[0]["HotelRequestId"]));

                int OptionCount = 1;
                foreach (DataRow drHotel in objDs.Tables[1].Rows)
                {
                    DataRow hotelRow = dtHotelQuotation.NewRow();

                    DataRow[] drPropertyResult;
                    drPropertyResult = objDsHotelResult.Tables["PropertyResult"].Select("PropertyID='" + drHotel["PropertyID"].ToString() + "'", "PropertyID", DataViewRowState.CurrentRows);

                    foreach (DataRow drPropertyResultin in objDsHotelResult.Tables["PropertyResult"].Rows)
                    {

                        hotelRow["HotelServiceFee"] = drHotel["ServiceFee"].ToString();
                        hotelRow["HotelBookingText"] = drHotel["QuotationText"].ToString();
                        hotelRow["HotelStatus"] = drHotel["Status"].ToString();
                        hotelRow["HotelRequestId"] = drHotel["HotelRequestId"].ToString();
                        hotelRow["ApprovalStatus"] = drHotel["ApprovalStatus"].ToString();
                        hotelRow["TrvellerId"] = drHotel["TravellerId"].ToString();
                        if (drPropertyResultin["PropertyID"].ToString() == drHotel["PropertyID"].ToString())
                        {
                            DataSet objDsHotelProperites = new DataSet();
                            objDsHotelProperites = _objBAHotelSearch.GetHotelsProperites(drPropertyResultin["PropertyReferenceID"].ToString().TrimEnd(','));
                            if (objDsHotelProperites.Tables[0].Rows.Count > 0)
                            {
                                hotelRow["HotelOptionNo"] = "Proceed for booking";

                                //if (drHotel["Status"].ToString() == "2")
                                //    hotelRow["HotelOptionNo"] = "Approved";
                                //else
                                //    hotelRow["HotelOptionNo"] = "Need to approve";
                                hotelRow["HotelQuotationId"] = drHotel["HotelQuotationId"].ToString();
                                OptionCount++;
                                hotelRow["PropertyName"] = objDsHotelProperites.Tables[0].Rows[0]["PropertyName"].ToString();
                                hotelRow["HotelAddress"] = objDsHotelProperites.Tables[0].Rows[0]["Address1"].ToString() + "  " +
                                    objDsHotelProperites.Tables[0].Rows[0]["Address2"].ToString()
                                    + " " + objDsHotelProperites.Tables[0].Rows[0]["TownCity"].ToString() + " " + objDsHotelProperites.Tables[0].Rows[0]["County"].ToString()
                                    + "  " + objDsHotelProperites.Tables[0].Rows[0]["PostcodeZip"].ToString();

                            }
                        }

                    }

                    if (objDsHotelResult.Tables["RoomType"].Rows.Count > 0)
                    {
                        DataSet objPreBookingResult = new DataSet();
                        DataRow[] drRoomTypes;
                        string PreBookingReq = string.Empty;
                        if (drHotel["PropertyRoomTypeID"].ToString() != "0")
                        {

                            drRoomTypes = objDsHotelResult.Tables["RoomType"].Select("PropertyRoomTypeID='" + drHotel["PropertyRoomTypeID"].ToString() + "'", "PropertyRoomTypeID", DataViewRowState.CurrentRows);
                            if (drRoomTypes.Length > 0)
                            {
                                hotelRow["HotelRoomType"] = drRoomTypes[0]["RoomtypeDesc"].ToString();
                                hotelRow["MealDesc"] = drRoomTypes[0]["MealBasis"].ToString();
                                hotelRow["HotelOptionText"] = "PropertyID=" + objBOUtiltiy.Encrypt(drHotel["PropertyID"].ToString(), true) + "&PropertyRoomTypeID="
                                                   + objBOUtiltiy.Encrypt(drHotel["PropertyRoomTypeID"].ToString(), true);
                            }
                        }
                        else
                        {
                            drRoomTypes = objDsHotelResult.Tables["RoomType"].Select("BookingToken='" + drHotel["BookingToken"].ToString().Replace(" ", "+") + "'", "BookingToken", DataViewRowState.CurrentRows);
                            hotelRow["HotelRoomType"] = drRoomTypes[0]["RoomtypeDesc"].ToString();
                            hotelRow["MealDesc"] = drRoomTypes[0]["MealBasis"].ToString();
                            hotelRow["HotelOptionText"] = "PropertyID=" + objBOUtiltiy.Encrypt(drHotel["PropertyID"].ToString(), true) + "&Bookingtoken="
                                                   + drHotel["BookingToken"].ToString().Replace(" ", "+");
                        }

                        int RequestId = Convert.ToInt32(Session["HotelRequestId"]);
                        DataSet objds = _objBAHotelSearch.GetHotelSearchRequest(RequestId);


                        if (objdsSearch.Tables[0].Rows.Count > 0)
                        {

                            hotelRow["HotelNoNights"] = objdsSearch.Tables[0].Rows[0]["Duration"].ToString();
                            int HotelRooms = Convert.ToInt32(objdsSearch.Tables[0].Rows[0]["NoRooms"].ToString());
                            decimal HotelTotalFare = Convert.ToDecimal(drHotel["TotalFee"]);

                            hotelRow["HotelTotalRooms"] = objdsSearch.Tables[0].Rows[0]["NoRooms"].ToString();
                            hotelRow["HotelCostFare"] = strCurrencyCode + " " + HotelTotalFare.ToString();


                            //  lblHotelCheckin.Text = objBOUtiltiy.ReverseConvertDateFormat(objdsSearch.Tables[0].Rows[0]["ArrivalDate"].ToString(), "MMMM dd, yyyy");


                            //  lblHotelCheckOut.Text = objBOUtiltiy.ReverseConvertDateFormat(objdsSearch.Tables[0].Rows[0]["CheckOutDate"].ToString(), "MMMM dd, yyyy");
                        }
                        if (objdsSearch.Tables[1].Rows.Count > 0)
                        {
                            int Adult = 0;
                            int Child = 0;
                            int Infant = 0;
                            int TotalPax = 0;
                            foreach (DataRow drPax in objdsSearch.Tables[1].Rows)
                            {
                                Adult = Adult + Convert.ToInt32(drPax["Adults"]);
                                Child = Child + Convert.ToInt32(drPax["Children"]);
                                Infant = Infant + Convert.ToInt32(drPax["Infants"]);
                            }
                            TotalPax = Adult + Child + Infant;

                            hotelRow["NoPax"] = TotalPax.ToString();
                        }

                    }

                    dtHotelQuotation.Rows.Add(hotelRow);
                }
                if (dtHotelQuotation.Rows.Count > 0)
                {
                    rptHotelQuotation.DataSource = dtHotelQuotation;
                    rptHotelQuotation.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "System Error", ex.Message);
        }
    }
    protected void rptHotelQuotation_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string QutRefNo = e.CommandArgument.ToString();

        HiddenField hfServiceFee = (HiddenField)e.Item.FindControl("hfServiceFee");
        HiddenField hfTrvellerId = (HiddenField)e.Item.FindControl("hfTrvellerId");
        HiddenField hfHotelRequestId = (HiddenField)e.Item.FindControl("hfHotelRequestId");

        Session["HotelRequestId"] = hfHotelRequestId.Value;
        Session["TrvellerId"] = hfTrvellerId.Value;
        if (e.CommandName == "Proceed for booking")
        {
            SetServiceFeeSessions(Convert.ToDecimal(hfServiceFee.Value));
            Session["returnurl"] = "../Admin/ViewQuotationDetails.aspx?QutRef=" + Request.QueryString["QutRef"];
            Response.Redirect("../DinoSales/HotelPaxBooking.aspx?" + QutRefNo);
        }
    }
    #endregion HotelQuotation

    #region CarRentals
    private void CarRentals()
    {
        try
        {
            if (Session["CarResult"] != null)
            {
                BACarResult objCarResult = new BACarResult();
                DataSet objDs = objCarResult.GetCarResult("", "", "", "", Convert.ToInt32(Session["CarResult"]), "", "");

                if (objDs.Tables[0].Rows.Count > 0)
                {
                    rptCars.DataSource = objDs;
                    rptCars.DataBind();
                }


            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = objBOUtiltiy.ShowMessage("danger", "System Error", ex.Message);
        }
    }
    #endregion CarRentals

    protected void rptCars_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string UKZNOrderNo = string.Empty;
        if (Session["UKZNORDERNO"] != null)
            UKZNOrderNo = Session["UKZNORDERNO"].ToString();
        string FileNo = "000";
        if (Session["FileNo"].ToString() == "0")
        {
            FileNo = objBOUtiltiy.GenerateFileNo(Convert.ToInt32(Session["BranchId"]));
            BALFileManager _objBALFileManager = new BALFileManager();

            _objBALFileManager.CreateFileNo(FileNo, Convert.ToInt32(Session["loginId"].ToString()), Convert.ToInt32(Session["TrvellerId"]), UKZNOrderNo);
            Session["FileNo"] = FileNo;
        }
        else
        {
            FileNo = Session["FileNo"].ToString();
        }

        BACarResult objCarResult = new BACarResult();
        int QuotationRefNo = Convert.ToInt32(Request.QueryString["QutRef"]);
        objCarResult.UpdateCarResultFile(QuotationRefNo, FileNo, UKZNOrderNo);
        Response.Redirect("ViewQuotationDetails.aspx?QutRef=" + QuotationRefNo);

    }
}