using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_AirFileManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
        
        if(!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { new DataColumn("Sno", typeof(int)),
                            new DataColumn("FileNo", typeof(string)),
                            new DataColumn("UKZNOrderNo",typeof(string)),
                             new DataColumn("UserRole",typeof(string)),
            new DataColumn("UserName",typeof(string)),
        new DataColumn("PassengerName",typeof(string))});
            dt.Rows.Add(1, "01201800001", "523","UKZN Staff"," Kovilian Nadioo"," Donovan Moodely");
           
           
            gvAirFilesList.DataSource = dt;
            gvAirFilesList.DataBind();
        }
        
    }
    protected void gvAirFilesList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvAirFilesList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
}