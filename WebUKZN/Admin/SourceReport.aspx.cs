using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SourceReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Sno", typeof(int)),
                            new DataColumn("Source", typeof(string)),
                            new DataColumn("TotalAmount",typeof(string)),
                            });
            dt.Rows.Add(1, "Hotel", "R5430.00" );


            gvSourceReport.DataSource = dt;
            gvSourceReport.DataBind();
        }

    }
    protected void gvSourceReport_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvSourceReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}