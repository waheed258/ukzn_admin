using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using System.Xml.Serialization;


public partial class Admin_FlightBooking : System.Web.UI.Page
{
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBoutility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("~/SalesLogin.aspx");
        }
        if (!IsPostBack)
        {
            BindFlightBooking();
        }
    }
    private void BindFlightBooking()
    {
        try
        {
            // DataSet objDs = objBAFlightSearch.GetFlightBookingResponce(0);
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = objBAFlightSearch.GetFlightBookingResponceByCreatedBy(CreatedBy, RoleId, CompanyId);
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
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
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
                BindFlightBooking();
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
                BindFlightBooking();
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
                ImageButton cmdPrintTicket = e.Row.FindControl("cmdPrintTicket") as ImageButton;
                ImageButton imgGenerateEticket = e.Row.FindControl("imgGenerateEticket") as ImageButton;
                ImageButton imgBtnCancel = e.Row.FindControl("imgBtnCancel") as ImageButton;
                ImageButton imgbtnCancelBooking = e.Row.FindControl("imgbtnCancelBooking") as ImageButton;
                if (hfEticketIssue.Value == "4")//Booking confirm
                {
                    cmdPrintTicket.Enabled = false;
                    imgGenerateEticket.Enabled = true;
                    imgBtnCancel.Enabled = true;
                    imgbtnCancelBooking.Enabled = true;
                    imgbtnCancelBooking.Visible = true;
                    imgBtnCancel.Visible = false;
                }
                else if (hfEticketIssue.Value == "5")// Eticket Done
                {
                    cmdPrintTicket.Enabled = true;
                    imgGenerateEticket.Enabled = false;
                    imgBtnCancel.Enabled = true;

                    imgBtnCancel.Visible = true;
                    imgbtnCancelBooking.Visible = false;
                }

                else
                {
                    imgGenerateEticket.Enabled = false;
                    cmdPrintTicket.Enabled = true;
                    imgbtnCancelBooking.Visible = false;

                    imgBtnCancel.Visible = true;
                    imgBtnCancel.Enabled = true;
                }
                if (Session["role_id"].ToString() == "1")
                {
                    imgGenerateEticket.Visible = true;

                }
                else
                {
                    imgGenerateEticket.Visible = false;

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
}