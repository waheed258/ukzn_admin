using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using System.Xml.Serialization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.html.simpleparser;

public partial class Admin_FindFlightBookings : System.Web.UI.Page
{
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();

    BOUtiltiy _objBoutility = new BOUtiltiy();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            txtStartDate.Text = _objBoutility.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBoutility.ConvertDateFormat(DateTime.Now.ToString());
            BindFlightBooking();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void BindFlightBooking()
    {
        try
        {
            // DataSet objDs = objBAFlightSearch.GetFlightBookingResponce(0);
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = objBAFlightSearch.GetBookingsByDates(Convert.ToInt32(Session["loginId"]), txtStartDate.Text, txtTodate.Text, RoleId, CompanyId);
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
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {
        try
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gdvFlightBookings.AllowPaging = false;
                    BindFlightBooking();

                    gdvFlightBookings.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SalesReport_" + txtStartDate.Text + "_" + txtTodate.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBoutility.ShowMessage("danger", "Error", ex.Message);
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
            Session["returnurl"] = "../Admin/FindFlightBookings.aspx";
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
        else if (e.CommandName == "ViewDetails")
        {
            string url = "ShowBookinginfo.aspx?frid=" + FlightRequestId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

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
                {
                    objBAFlightSearch.UpdateFlightBookinngResponceCancelStatus(FlightRequestId, resXml, 8, 0, 0, Convert.ToInt32(Session["loginId"]));
                    BindFlightBooking();
                }
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
                    //e.Row.Cells[9].ForeColor = System.Drawing.Color.Black;
                    //e.Row.Cells[9].BackColor = System.Drawing.Color.Yellow;
                    btnStatus.BackColor = System.Drawing.Color.Yellow;
                }
                else if (hfEticketIssue.Value == "5")// Eticket Done
                {
                    cmdPrintTicket.Enabled = true;
                    imgGenerateEticket.Enabled = false;
                    imgBtnCancel.Enabled = true;

                    imgBtnCancel.Visible = true;
                    imgbtnCancelBooking.Visible = false;
                    //e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
                    //e.Row.Cells[9].BackColor = System.Drawing.Color.Green;
                    btnStatus.BackColor = System.Drawing.Color.Green;
                }

                else
                {
                    imgGenerateEticket.Enabled = false;
                    cmdPrintTicket.Enabled = true;
                    imgbtnCancelBooking.Visible = false;

                    imgBtnCancel.Visible = true;
                    imgBtnCancel.Enabled = true;
                    //e.Row.Cells[9].ForeColor = System.Drawing.Color.White;
                    //e.Row.Cells[9].BackColor = System.Drawing.Color.Gray;
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
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindFlightBooking();
    }
}