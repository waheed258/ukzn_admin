using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
public partial class SalesAdmin_CustomerFlightBookingList : System.Web.UI.Page
{
    BAFlightSearch objBAFlightSearch = new BAFlightSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] != null)
        //{
        //    Response.Redirect("../Login.aspx");

        //}
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
            int CreatedBy = Convert.ToInt32(393);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = objBAFlightSearch.GetFlightBookingResponceByCreatedBy(CreatedBy, RoleId, CompanyId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvFlightBookings.DataSource = objDs.Tables[0];
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
            Response.Redirect("../DinoSales/FlightEticketGenerate.aspx?frid=" + FlightRequestId);
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
    }

    protected void gdvFlightBookings_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hfEticketIssue = e.Row.FindControl("hfEticketIssue") as HiddenField;
                ImageButton cmdPrintTicket = e.Row.FindControl("cmdPrintTicket") as ImageButton;
                ImageButton imgGenerateEticket = e.Row.FindControl("imgGenerateEticket") as ImageButton;
                ImageButton imgBtnCancel = e.Row.FindControl("imgBtnCancel") as ImageButton;
                if (hfEticketIssue.Value == "4")
                {
                    cmdPrintTicket.Enabled = false;
                    imgGenerateEticket.Enabled = true;
                    imgBtnCancel.Enabled = false;
                }
                else if (hfEticketIssue.Value == "5")
                {
                    cmdPrintTicket.Enabled = true;
                    imgGenerateEticket.Enabled = false;
                    imgBtnCancel.Enabled = true;
                }

                else
                {
                    imgGenerateEticket.Enabled = false;
                    cmdPrintTicket.Enabled = true;
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
}