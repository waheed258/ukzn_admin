using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_OnlineHotelBookings : System.Web.UI.Page
{
    BAHotelSearch _objBAHotelSearch = new BAHotelSearch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
        if (!IsPostBack)
        {
            BindOnlineHotelRequests();
        }
    }
    protected void gdvHotelBookings_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string HotelRequestId = e.CommandArgument.ToString();
        if (e.CommandName == "BookHotel")
        {

            Response.Redirect("../DinoSales/HotelOnlinePaxBooking.aspx?Htlreq=" + HotelRequestId);
        }
    }
    #region PrivateMEthods
    private void BindOnlineHotelRequests()
    {
        try
        {
            DataSet objDs = _objBAHotelSearch.GetAllOnlineHotelSearchRequest();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvHotelBookings.DataSource = objDs;
                gdvHotelBookings.DataBind();
            }
            else
            {
                gdvHotelBookings.DataSource = null;
                gdvHotelBookings.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    #endregion PrivateMethods
}