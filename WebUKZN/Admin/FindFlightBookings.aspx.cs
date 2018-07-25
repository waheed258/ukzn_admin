using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FindFlightBookings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;


            DataTable dt = FindFlightBooingList();
            GvFindFlightBookingList.DataSource = dt;
            GvFindFlightBookingList.DataBind();
        }
    }


     

    public DataTable FindFlightBooingList()
    {
        DataTable table = new DataTable();

        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Agent", typeof(string));
        table.Columns.Add("PNR", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("PhoneNumber", typeof(string));
        table.Columns.Add("TicketingStatus", typeof(int));

        table.Rows.Add(101, "Satwik", "PQUR51", "Siddu", "9533095350", 1);
        table.Rows.Add(102, "Siddu", "PQUR52", "Nishanth", "9533095351", 2);
        table.Rows.Add(104, "Sravan", "PQUR53", "Sravan", "9533095352", 3);
        table.Rows.Add(105, "Nishanth", "PQUR54", "Siddu", "9533095353", 1);
        table.Rows.Add(106, "Tejaswi", "PQUR55", "Nishanth", "9533095354", 2);
        table.Rows.Add(107, "Sushanth", "PQUR56", "Sravan", "9533095355", 1);
        table.Rows.Add(108, "Swetha", "PQUR57", "Siddu", "9533095356", 3);
        table.Rows.Add(109, "Niraja", "PQUR58", "Nishanth", "9533095357", 2);
        table.Rows.Add(110, "Nisha", "PQUR59", "Sravan", "9533095358", 3);
        table.Rows.Add(111, "Satwik", "PQUR60", "Nishanth", "9533095359", 1);
        table.Rows.Add(112, "Siddu", "PQUR61", "Siddu", "9533095360", 2);
        table.Rows.Add(114, "Sravan", "PQUR62", "Sravan", "9533095362", 1);

        return table;
    }

    protected void GvFindFlightBookingList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvFindFlightBookingList.PageIndex = e.NewPageIndex;
        FindFlightBooingList();
    }
    protected void GvFindFlightBookingList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;

            //e.Row.Cells[1].CssClass = drv["Status"].ToString().Equals("1") ? "Gridlabel Gridlabel-success" : (drv["Status"].ToString().Equals("2")?"Gridlabel  Gridlabel-warning":"");

            if (drv["TicketingStatus"].ToString().Equals("1"))
            {

                e.Row.Cells[5].Text = "<span class='label label-success'>Booked</span>";
            }
            else if (drv["TicketingStatus"].ToString().Equals("2"))
            {
                e.Row.Cells[5].Text = "<span class='label label-warning'>Confirm</span>";
            }
            else if (drv["TicketingStatus"].ToString().Equals("3"))
            {
                e.Row.Cells[5].Text = "<span class='label label-danger'>Cancel</span>";
            }
        }
    }
   
}