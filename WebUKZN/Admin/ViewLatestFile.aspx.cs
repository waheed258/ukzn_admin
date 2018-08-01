using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using System.Xml.Serialization;

public partial class Admin_ViewLatestFile : System.Web.UI.Page
{
    BALFileManager objBALFileManager = new BALFileManager();
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBoutility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../login.aspx");
        }
        if (!IsPostBack)
        {
            //if (Session["loginId"] == null)
            //{
            //    Response.Redirect("../Login.aspx");
            //}
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            BindBookings(CreatedBy);

            //if (Session["role_id"].ToString() == "3")
            //{
            //    lblAgentMessage.Visible = true;
            //    lblAgentMessage.Text = "Note : Please contact your IATA license provider for ticketing your bookings";
            //}

        }
    }
    private void BindBookings(int CreatedBy)
    {
        try
        {
            DataSet objDsLatestFile = objBALFileManager.GetLatestFileNo(CreatedBy);
            string FileNo = string.Empty;
            if (objDsLatestFile.Tables[0].Rows.Count > 0)
            {
                FileNo = objDsLatestFile.Tables[0].Rows[0]["FileNo"].ToString();
                lblLatestFileNo.Text = FileNo;
                lblOrderNo.Text = objDsLatestFile.Tables[0].Rows[0]["OrderNo"].ToString();
                hfTravellerId.Value = objDsLatestFile.Tables[0].Rows[0]["TravellerId"].ToString();
                hfFileNo.Value = objDsLatestFile.Tables[0].Rows[0]["FileNo"].ToString();

                DataSet objDs = objBALFileManager.GetAllBookingsByFileNo(FileNo);
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
                }
                else
                {
                    gdvHotelBooking.DataSource = null;
                    gdvHotelBooking.DataBind();
                }
                if (objDs.Tables[2].Rows.Count > 0)
                {
                    txttotalAmount.Text = (Convert.ToDecimal(objDs.Tables[2].Rows[0]["TotalAmount"].ToString()) + 5000).ToString();
                }
                if (objDs.Tables[3].Rows.Count > 0)
                {
                    txtReceivedAmount.Text = objDs.Tables[3].Rows[0]["ResivedAmount"].ToString();
                }

            }
            else
            {
                btnGenerateOtherBookings.Enabled = false;
            }

            GenerateCars(FileNo);

        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    protected void btnCloseFile_Click(object sender, EventArgs e)
    {
        Session.Remove("TrvellerId");
        Session.Remove("FileNo");
        Response.Redirect("Index.aspx");
    }
    protected void btnInvoice_Click(object sender, EventArgs e)
    {
        string url = "InvoicePdf.aspx?FileNo=" + hfFileNo.Value;
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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
            Session["returnurl"] = "../Admin/ViewLatestFile.aspx";
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

                int CreatedBy = Convert.ToInt32(Session["loginId"]);
                BindBookings(CreatedBy);

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

                int CreatedBy = Convert.ToInt32(Session["loginId"]);
                BindBookings(CreatedBy);
            }

        }

        else if (e.CommandName == "ViewDetails")
        {
            string url = "ShowBookinginfo.aspx?frid=" + FlightRequestId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

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
                Button btnStatus = e.Row.FindControl("btnStatus") as Button;

                if (hfEticketIssue.Value == "4")//Booking confirm
                {
                    cmdPrintTicket.Enabled = false;
                    imgGenerateEticket.Enabled = true;
                    imgBtnCancel.Enabled = true;
                    imgbtnCancelBooking.Enabled = true;
                    imgbtnCancelBooking.Visible = true;
                    imgBtnCancel.Visible = false;
                    //e.Row.Cells[10].ForeColor = System.Drawing.Color.Black;
                    //e.Row.Cells[10].BackColor = System.Drawing.Color.Yellow;
                    btnStatus.BackColor = System.Drawing.Color.Yellow;

                }
                else if (hfEticketIssue.Value == "5")// Eticket Done
                {
                    cmdPrintTicket.Enabled = true;
                    imgGenerateEticket.Enabled = false;
                    imgBtnCancel.Enabled = true;

                    imgBtnCancel.Visible = true;
                    imgbtnCancelBooking.Visible = false;
                    //e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                    //e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                    btnStatus.BackColor = System.Drawing.Color.Green;
                }

                else
                {
                    imgGenerateEticket.Enabled = false;
                    cmdPrintTicket.Enabled = true;
                    imgbtnCancelBooking.Visible = false;
                    imgBtnCancel.Enabled = false;

                    imgBtnCancel.Visible = true;

                    //e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                    //e.Row.Cells[10].BackColor = System.Drawing.Color.Gray;
                    btnStatus.BackColor = System.Drawing.Color.Gray;
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
    protected void gdvHotelBooking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatusId = e.Row.FindControl("hfStatusId") as HiddenField;
                Button btnStatus = e.Row.FindControl("btnStatus") as Button;
                ImageButton cmdCancelHotel = e.Row.FindControl("cmdCancelHotel") as ImageButton;

                if (hfStatusId.Value == "4")//Booking confirm
                {

                    cmdCancelHotel.Enabled = true;
                    //e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
                    //e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    btnStatus.BackColor = System.Drawing.Color.Green;

                }
                else
                {
                    cmdCancelHotel.Enabled = false;
                    btnStatus.BackColor = System.Drawing.Color.Gray;
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
        }


    }
    #endregion CarRentals
  
    protected void gvCarBookings_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string FlightRequestId = e.CommandArgument.ToString();
        if (e.CommandName == "PrintInVoice")
        {
            string url = "../DinoSales/PrintCarTicket .aspx?reqstId=" + FlightRequestId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
    }
    protected void btnGenerateOtherBookings_Click(object sender, EventArgs e)
    {
        Session["TrvellerId"] = hfTravellerId.Value;
        Session["FileNo"] = hfFileNo.Value;
        Response.Redirect("../DinoSales/Search.aspx");
    }
}