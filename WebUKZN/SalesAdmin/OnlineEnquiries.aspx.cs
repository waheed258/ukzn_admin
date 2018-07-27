using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_OnlineEnquiries : System.Web.UI.Page
{
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
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
            int CreatedBy = 393;
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = objBAFlightSearch.GetFlightBookingResponceByCreatedBy(CreatedBy, RoleId, CompanyId);
            if (objDs.Tables[1].Rows.Count > 0)
            {
                gdvFlightBookings.DataSource = objDs.Tables[1];
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    protected void gdvFlights_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FlightRequestId = e.CommandArgument.ToString();
        if (e.CommandName == "BookFlight")
        {

            //string url = "../CustomerSales/PriceConfirmation.aspx?frid=" + FlightRequestId;
            //string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            Response.Redirect("../CustomerSales/PriceConfirmation.aspx?frid=" + FlightRequestId);
        }


    }

    protected void gdvFlightBookings_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfEticketIssue = e.Row.FindControl("hfEticketIssue") as HiddenField;

                ImageButton imgPriceConfirm = e.Row.FindControl("imgPriceConfirm") as ImageButton;


                if (hfEticketIssue.Value == "9")
                {

                    imgPriceConfirm.Enabled = true;

                }
                else
                {
                    imgPriceConfirm.Enabled = false;

                }


            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}