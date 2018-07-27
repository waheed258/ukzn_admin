using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_HotelBooking : System.Web.UI.Page
{
    BAHotelSearch objHotelBookings = new BAHotelSearch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");

        }
        if (!IsPostBack)
        {
            BindHotelBooking();
        }
    }
    #region Private Methods

    private void BindHotelBooking()
    {

        try
        {
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objds = objHotelBookings.GetHotelSearchRequestByCreatedBy(Convert.ToInt32(Session["loginId"]), RoleId, CompanyId);

            if (objds.Tables[0].Rows.Count > 0)
            {
                gdvHotelBooking.DataSource = objds.Tables[0];
                gdvHotelBooking.DataBind();
            }
            else
            {
                gdvHotelBooking.DataSource = null;
                gdvHotelBooking.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void gdvHotelBooking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "PrintInVoice")
        {

            string HotelBookingId = _objBOUtiltiy.Encrypt(e.CommandArgument.ToString(), true);
            string url = "printhotelInvoice.aspx?reqstId=" + HotelBookingId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //Response.Redirect("Customer.aspx?HotelBookingId=" + HotelBookingId);

        }
        else
        {
            string HotelBookingId = _objBOUtiltiy.Encrypt(e.CommandArgument.ToString(), true);
            string url = "../DinoSales/Cancelation.aspx?reqstId=" + HotelBookingId;
            string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

        }
    }
    #endregion Private Methods
    protected void gdvHotelBooking_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfStatusId = e.Row.FindControl("hfStatusId") as HiddenField;

                ImageButton cmdCancelHotel = e.Row.FindControl("cmdCancelHotel") as ImageButton;

                if (hfStatusId.Value == "4")//Booking confirm
                {

                    cmdCancelHotel.Enabled = true;
                    e.Row.Cells[8].ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;

                }
                else
                {
                    cmdCancelHotel.Enabled = false;
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}