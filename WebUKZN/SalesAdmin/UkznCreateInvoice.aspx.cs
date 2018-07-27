using BusinessManager;
using EntityManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using UKZNInterface;

public partial class SalesAdmin_UkznCreateInvoice : System.Web.UI.Page
{
    BALFileManager objBALFileManager = new BALFileManager();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBoutility = new BOUtiltiy();
    UKZNTravelRequest _objUKZNTravelRequest = new UKZNTravelRequest();
    BOQutation _objBOQutation = new BOQutation();
    string _strUKZNsupplierno = ConfigurationManager.AppSettings["UKZNsupplierno"].ToString();
    decimal _decVarPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["VatPercentage"]);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["FileNo"] != null)
            {
                string FileNo = Request.QueryString["FileNo"];
                BindBookings(FileNo);
            }
        }
    }
    #region ButtonEvents
    protected void btnCreateInvoice_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["FileNo"] != null)
        {
            string FileNo = Request.QueryString["FileNo"];
            UKZNCreateInvoice(FileNo);
        }

    }

    #endregion ButtonEvents

    private void BindBookings(string FileNo)
    {
        try
        {
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = objBALFileManager.GetAllBookingsByFileNoandCreatedBy(FileNo, CreatedBy, RoleId, CompanyId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvFlightBookings.DataSource = objDs;
                gdvFlightBookings.DataBind();
            }
            else
            {
                gdvFlightBookings.DataSource = null;
                gdvFlightBookings.DataBind();
            }

            if (objDs.Tables[1].Rows.Count > 0)
            {
                gdvHotelBooking.DataSource = objDs.Tables[1];
                gdvHotelBooking.DataBind();
                gdvHotelBooking.Visible = true;
                pnlHotelBookings.Visible = true;
            }
            else
            {
                gdvHotelBooking.DataSource = null;
                gdvHotelBooking.DataBind();
                gdvHotelBooking.Visible = false;
                if (!pnlHotelBookings.Visible)
                    pnlHotelBookings.Visible = false;
            }

            if (objDs.Tables[4].Rows.Count > 0)
            {
                gdvOnlineHotelBookings.DataSource = objDs.Tables[4];
                gdvOnlineHotelBookings.DataBind();
                gdvOnlineHotelBookings.Visible = true;
                pnlHotelBookings.Visible = true;
            }
            else
            {
                gdvOnlineHotelBookings.DataSource = null;
                gdvOnlineHotelBookings.DataBind();
                gdvOnlineHotelBookings.Visible = false;
                if (!pnlHotelBookings.Visible)
                    pnlHotelBookings.Visible = false;
            }

            if (objDs.Tables[2].Rows.Count > 0)
            {
                txttotalAmount.Text = objDs.Tables[2].Rows[0]["TotalAmount"].ToString();
            }
            if (objDs.Tables[3].Rows.Count > 0)
            {

                // txtReceivedAmount.Text = objDs.Tables[3].Rows[0]["ResivedAmount"].ToString();
            }
            //if (Session["role_id"].ToString() == "3")
            //    pnlresvamount.Visible = false;
            //else
            //    pnlresvamount.Visible = true;
            if (objDs.Tables[6].Rows.Count > 0)
            {
                txtCostCenterCodeName.Text = objDs.Tables[6].Rows[0]["cost_center"].ToString() + " : " + objDs.Tables[6].Rows[0]["costcentrename"].ToString();
                txtOrderNo.Text = objDs.Tables[6].Rows[0]["OrderNo"].ToString();
            }
            GenerateCars(FileNo);
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #region FlightBookins
    protected void gdvFlightBookings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfEticketIssue = e.Row.FindControl("hfEticketIssue") as HiddenField;

                Button btnStatus = e.Row.FindControl("btnStatus") as Button;


            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void CancelBooking(string BookingRefNo, int FlightRequestId, string TraceId)
    {
        try
        {
            Utilitys.FlightSearchXmlParsing _objFlightSearchXmlParsing = new Utilitys.FlightSearchXmlParsing();
            uAPIClassLib.UniversalReference.UniversalRecordCancelRsp objCancleResponce = _objFlightSearchXmlParsing.AirTicketCancel(BookingRefNo, TraceId);
            if (objCancleResponce != null)
            {
                bool CancelStatus = false;
                string CancelInfoType = string.Empty;
                string CancelMessage = string.Empty;
                foreach (uAPIClassLib.UniversalReference.ProviderReservationStatus objProviderReservationStatus in objCancleResponce.ProviderReservationStatus)
                {
                    CancelStatus = objProviderReservationStatus.Cancelled;
                    CancelInfoType = objProviderReservationStatus.CancelInfo.Type.ToString();
                    CancelMessage = objProviderReservationStatus.CancelInfo.Value;

                }
                string resXml = SerializeObject(objCancleResponce);
                if (CancelStatus)
                    objBAFlightSearch.UpdateFlightBookinngResponceCancelStatus(FlightRequestId, resXml, 8, 0, 0, Convert.ToInt32(Session["loginId"]));
                else
                    lblMsg.Text = _objBoutility.ShowMessage("danger", CancelInfoType, CancelMessage);
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBoutility.ShowMessage("danger", "Error", ex.Message);
        }
    }
    public string SerializeObject(Object toSerialize)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

        using (uAPIClassLib.Utf8StringWriter textWriter = new uAPIClassLib.Utf8StringWriter())
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(textWriter, toSerialize, ns);
            return textWriter.ToString();
        }
    }
    #endregion FlightBookings

    #region HotelBookings
    protected void gdvHotelBooking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatusId = e.Row.FindControl("hfStatusId") as HiddenField;



                if (hfStatusId.Value == "4")//Booking confirm
                {


                    e.Row.Cells[7].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Green;

                }

            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion HotelBookings

    #region CarRentals
    private void GenerateCars(string FileNo)
    {

        BACarResult objCarResult = new BACarResult();
        DataSet objDs = objCarResult.GetCarResult("", "", "", "", 0, FileNo, "");
        if (objDs.Tables[2].Rows.Count > 0)
        {
            gvCarBookings.DataSource = objDs.Tables[2];
            gvCarBookings.DataBind();
            pnlCars.Visible = true;
        }
        else
        {
            pnlCars.Visible = false;
        }


    }
    #endregion CarRentals

    #region UKZNMETHODS
    private string UKZNCreateInvoice(string FileNo)
    {
        try
        {
            decimal TotalAmount = 0;
            DataSet objDs = objBALFileManager.GetQuotationMasterByFileNo(FileNo);
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

                int FlightRequestID = 0;
                int HotelRequestId = 0;
                int CabRequestId = 0;

                if (objDsQut.Tables[0].Rows.Count > 0)// Flight Invoice
                {
                    foreach (GridViewRow row in gdvFlightBookings.Rows)
                    {
                        CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                        if (chk != null && chk.Checked)
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
                                TotalAmount = TotalAmount + Convert.ToDecimal(QuotationRow["TotalPrice"].ToString());
                                objF.commentstmc = "Flight invoice created" + QuotationRow["FlightRequestId"].ToString();
                                objF.itemvatamount = _objBoutility.CalculateVat(Convert.ToDecimal(objF.itemamountincvat), _decVarPercentage).ToString();
                                FlightRequestID = Convert.ToInt32(QuotationRow["FlightRequestId"]);
                                LstObj.Add(objF);
                            }
                        }
                    }
                }
                if (objDsQut.Tables[1].Rows.Count > 0)// Hotel Invoice
                {
                    foreach (DataRow QuotationRow in objDsQut.Tables[1].Rows)
                    {
                        foreach (GridViewRow row in gdvHotelBooking.Rows)
                        {
                            CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                            if (chk != null && chk.Checked)
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
                                objH.itemvatamount = _objBoutility.CalculateVat(Convert.ToDecimal(objH.itemamountincvat), _decVarPercentage).ToString();
                                TotalAmount = TotalAmount + Convert.ToDecimal(QuotationRow["TotalFee"].ToString());
                                HotelRequestId = Convert.ToInt32(QuotationRow["HotelRequestId"]);
                                LstObj.Add(objH);
                            }
                        }
                    }
                }

                if (objDsQut.Tables[3].Rows.Count > 0)// Hotel Invoice
                {
                    foreach (DataRow QuotationRow in objDsQut.Tables[3].Rows)
                    {
                        foreach (GridViewRow row in gvCarBookings.Rows)
                        {
                            CheckBox chk = row.FindControl("chkSelect") as CheckBox;
                            if (chk != null && chk.Checked)
                            {
                                ItemInvoiceNo = ItemInvoiceNo + 1;
                                UKZNInoviceDetails objc = new UKZNInoviceDetails();
                                objc.accountnumber = "30031";
                                objc.uniquerequestid = "C_" + QuotationRow["CarDescId"].ToString();// QuotationRow["Fromcode"].ToString();
                                objc.invoiceitemdescription = "Car Item Invoice Created for file no" + FileNo;
                                objc.invoiceitemnumber = ItemInvoiceNo.ToString();//"A_" + QuotationRow["HotelRequestId"].ToString();
                                objc.invoiceitemnote = "not paid";
                                objc.fullyprocessedyn = "N";//QuotationRow["Fromcode"].ToString();
                                objc.alreadypaidyn = "N";// QuotationRow["Fromcode"].ToString();
                                objc.itemamountincvat = QuotationRow["TotalPrice"].ToString();
                                objc.commentstmc = "Hotel Invoice Created" + QuotationRow["CarDescId"].ToString();
                                objc.itemvatamount = _objBoutility.CalculateVat(Convert.ToDecimal(objc.itemamountincvat), _decVarPercentage).ToString();
                                TotalAmount = TotalAmount + Convert.ToDecimal(QuotationRow["TotalPrice"].ToString());
                                CabRequestId = Convert.ToInt32(QuotationRow["CarDescId"]);
                                LstObj.Add(objc);
                            }
                        }
                    }
                }
                if (TotalAmount > 0)
                {
                    string Invocie = "0";
                    if (objDsQut.Tables[2].Rows.Count > 0)
                    {
                        BALUser objBALUer = new BALUser();
                        int InvoiceNo = objBALUer.GenerateInvocieNo();
                        Invocie = "INV" + InvoiceNo.ToString("000");
                        objTravelRequest.supplierno = _strUKZNsupplierno;
                        objTravelRequest.invoicenumber = objDsQut.Tables[2].Rows[0]["QuotationRefNo"].ToString().Replace("QT", "IN_");// Invocie;
                        objTravelRequest.totamountincvat = TotalAmount.ToString();// objDsQut.Tables[2].Rows[0]["TripAmount"].ToString();
                        objTravelRequest.invoicedate = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper();
                        objTravelRequest.invoicedescription = "Invoice order no " + objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                        objTravelRequest.ordernumber = objDsQut.Tables[2].Rows[0]["ukzn_orderno"].ToString();
                        objTravelRequest.costcentre = objDsQut.Tables[2].Rows[0]["cost_center"].ToString();
                    }
                    objTravelRequest.ListObjectUKZNInvoice = LstObj;
                    strMessage = _objUKZNTravelRequest.validateinvoice(objTravelRequest);
                    if (strMessage == "Success")
                    {
                        objBALFileManager.UpdateFileMaster(FileNo, Invocie);
                        lblMsg.Text = _objBoutility.ShowMessage("succcess", "info ", "Invoice created in integrator");
                    }
                    else
                    {
                        objBALFileManager.UpdateFileMaster(FileNo, strMessage);
                        lblMsg.Text = _objBoutility.ShowMessage("succcess", "info ", strMessage);
                    }
                }
                else
                {
                    lblMsg.Text = _objBoutility.ShowMessage("danger", "Error ", "Please select item");
                }
            }
            else
            {
                lblMsg.Text = _objBoutility.ShowMessage("danger", "Error ", "No order find with this order no.");
            }
            return strMessage;
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBoutility.ShowMessage("danger", "Error ", ex.Message);
            return "";
        }
    }

    private void CancelFullOrder(string FileNo)
    {
        try
        {
            string strMessage = string.Empty;
            DataSet objDs = objBALFileManager.GetQuotationMasterByFileNo(FileNo);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                List<TravelRequestProcess> LstobjTravelRequestProcess = new List<TravelRequestProcess>();
                TravelRequestProcess ItemList = new TravelRequestProcess();
                ItemList.supplierno = _strUKZNsupplierno;
                ItemList.ordernumber = objDs.Tables[0].Rows[0]["ukzn_orderno"].ToString();
                ItemList.travelrequestno = objDs.Tables[0].Rows[0]["QuotationRefNo"].ToString();
                ItemList.totamountincvat = objDs.Tables[0].Rows[0]["TripAmount"].ToString();
                ItemList.cancellationdate = DateTime.Now.ToString("dd-MMM-yyyy").ToUpper();
                LstobjTravelRequestProcess.Add(ItemList);
                strMessage = _objUKZNTravelRequest.validateordcancellation(LstobjTravelRequestProcess);
                if (strMessage == "Success")
                {
                    lblMsg.Text = _objBoutility.ShowMessage("success", "Success ", "Order cancelled in Integrator");
                    objBALFileManager.UpdateFileMaster(FileNo, "Order cancelled in Integrator");
                }
                else
                {
                    lblMsg.Text = _objBoutility.ShowMessage("danger", "Error ", strMessage);
                }
            }
            else
            {
                lblMsg.Text = _objBoutility.ShowMessage("danger", "Info ", "Sorry no records found with this order");
            }
        }
        catch (Exception ex)
        {

        }
    }

    #endregion UKZNMETHODS

    protected void btnPrintInvoice_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["FileNo"] != null)
        {
            string FileNo = Request.QueryString["FileNo"];
            string url = "InvoicePdf.aspx?FileNo=" + FileNo;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
}