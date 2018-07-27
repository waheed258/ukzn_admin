using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using System.Xml.Serialization;
public partial class SalesAdmin_ViewFileDetails : System.Web.UI.Page
{
    BALFileManager objBALFileManager = new BALFileManager();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBoutility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //    return;
        //}
        if (!IsPostBack)
        {
            if (Request.QueryString["FileNo"] != null)
            {
                string FileNo = Request.QueryString["FileNo"];
                BindBookings(FileNo);
            }
        }
    }

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

                txtReceivedAmount.Text = objDs.Tables[3].Rows[0]["ResivedAmount"].ToString();
            }
            if (Session["role_id"].ToString() == "3")
                pnlresvamount.Visible = false;
            else
                pnlresvamount.Visible = true;
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    #region FlightBookins


    protected void gdvFlights_RowCommand(object sender, GridViewCommandEventArgs e)
    {



        string FlightRequestId = e.CommandArgument.ToString();
        if (e.CommandName == "PrintTicket")
        {
            string url = "../DinoSales/PrintFlightTicket.aspx?frid=" + FlightRequestId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        else if (e.CommandName == "GenerateTicket")
        {
            // Response.Redirect("../DinoSales/FlightEticketGenerate.aspx?frid=" + FlightRequestId);
            Response.Redirect("../DinoSales/PaymentConfirmation.aspx?frid=" + FlightRequestId);
        }
        else if (e.CommandName == "CancelTicket")
        {
            if (Session["role_id"].ToString() == "1")
            {
                Response.Redirect("../DinoSales/FlightCancelDetails.aspx?frid=" + FlightRequestId);
            }
            else
            {
                objBAFlightSearch.UpdateFlightBookinngResponceStatus(Convert.ToInt32(FlightRequestId), 6);
                if (Request.QueryString["FileNo"] != null)
                {
                    string FileNo = Request.QueryString["FileNo"];
                    BindBookings(FileNo);
                }
            }

        }
        else if (e.CommandName == "BookingCancel")
        {
            if (Session["role_id"].ToString() == "1")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                int rowIndex = gvr.RowIndex;
                GridViewRow row = gdvFlightBookings.Rows[rowIndex];
                HiddenField hfBookingRef = (HiddenField)row.FindControl("hfBookingRef");
                HiddenField hfTraceId = (HiddenField)row.FindControl("hfTraceId");

                CancelBooking(hfBookingRef.Value, Convert.ToInt32(FlightRequestId), hfTraceId.Value);
            }
            else
            {
                objBAFlightSearch.UpdateFlightBookinngResponceStatus(Convert.ToInt32(FlightRequestId), 6);
                if (Request.QueryString["FileNo"] != null)
                {
                    string FileNo = Request.QueryString["FileNo"];
                    BindBookings(FileNo);
                }
            }

        }
    }

    protected void gdvFlightBookings_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfEticketIssue = e.Row.FindControl("hfEticketIssue") as HiddenField;

                if (hfEticketIssue.Value == "4")//Booking confirm
                {

                    e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
                }
                else if (hfEticketIssue.Value == "5")// Eticket Done
                {

                    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                }

                else
                {

                    e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Gray;
                }


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


    protected void gdvHotelBooking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintInVoice")
        {

            string HotelBookingId = _objBoutility.Encrypt(e.CommandArgument.ToString(), true);
            string url = "printhotelInvoice.aspx?reqstId=" + HotelBookingId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //Response.Redirect("Customer.aspx?HotelBookingId=" + HotelBookingId);

        }
        else
        {
            string HotelBookingId = _objBoutility.Encrypt(e.CommandArgument.ToString(), true);
            string url = "../DinoSales/Cancelation.aspx?reqstId=" + HotelBookingId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
    }
    #endregion HotelBookings
    protected void gdvOnlineHotelBookings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string HotelRequestId = e.CommandArgument.ToString();
        if (e.CommandName == "BookHotel")
        {

            Response.Redirect("../DinoSales/HotelOnlinePaxBooking.aspx?Htlreq=" + HotelRequestId);
        }
    }
    protected void gdvHotelBooking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatusId = e.Row.FindControl("hfStatusId") as HiddenField;




            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}