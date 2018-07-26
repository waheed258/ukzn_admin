using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;


public partial class SalesAdmin_DestinationReport : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    string strCurrencycode = "R";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] != null)
        {
            Response.Redirect("../Login.aspx");
        }
        strCurrencycode = _objBOUtiltiy.Currencycode();
        if (!IsPostBack)
        {

            txtStartDate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            BindReport();
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void btnExportPdf_Click(object sender, EventArgs e)
    {

    }

    private void BindReport()
    {
        try
        {
            DataSet objDs = _objBORpeort.GetDestinationReport(txtStartDate.Text, txtTodate.Text, Convert.ToInt32(Session["loginId"]), Convert.ToInt32(Session["role_id"]));

            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvDestination.DataSource = objDs.Tables[0];
                gvDestination.DataBind();

                decimal ExlPrice = objDs.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalPrice"));
                gvDestination.FooterRow.Cells[2].Text = "Total's";
                gvDestination.FooterRow.Cells[2].Font.Bold = true;
                gvDestination.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                gvDestination.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;


                gvDestination.FooterRow.Cells[3].Text = strCurrencycode + " " + ExlPrice.ToString("N2");
                gvDestination.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                gvDestination.FooterRow.Cells[3].Font.Bold = true;
                gvDestination.FooterRow.Cells[3].ForeColor = System.Drawing.Color.White;

            }
            else
            {
                gvDestination.DataSource = null;
                gvDestination.DataBind();
            }

        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
}