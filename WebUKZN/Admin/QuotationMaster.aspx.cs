using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Drawing;
using System.Web.UI.WebControls;

public partial class Admin_QuotationMaster : System.Web.UI.Page
{
    BALQuotation _objBalQuotation = new BALQuotation();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = 10;
            BindQuotationList();
        }
    }

    private void BindQuotationList()
    {
        try
        {
            GvQuotationList.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet QuotationDataset = _objBalQuotation.GetQuotationList();
            if (QuotationDataset.Tables[0].Rows.Count >= 1)
            {
                GvQuotationList.DataSource = QuotationDataset;
                GvQuotationList.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

            // lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    protected void GvQuotationList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;

            //e.Row.Cells[1].CssClass = drv["Status"].ToString().Equals("1") ? "Gridlabel Gridlabel-success" : (drv["Status"].ToString().Equals("2")?"Gridlabel  Gridlabel-warning":"");

            if (drv["Status"].ToString().Equals("1"))
            {

                e.Row.Cells[1].Text = "<span class='label label-success'>Approved</span>";
            }
            else if (drv["Status"].ToString().Equals("2"))
            {
                e.Row.Cells[1].Text = "<span class='label label-warning'>Pending</span>";
            }
            else if (drv["Status"].ToString().Equals("3"))
            {
                e.Row.Cells[1].Text = "<span class='label label-danger'>Reject</span>";
            }
            else if (drv["Status"].ToString().Equals("4"))
            {
                e.Row.Cells[1].Text = "<span class='label label-info'>InProgress</span>";
            }
        }

    }
    protected void GvQuotationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvQuotationList.PageIndex = e.NewPageIndex;
        BindQuotationList();
    }
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        BindQuotationList();
    }
}