using BusinessManager;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UKZNCostCenterReport : System.Web.UI.Page
{
    BOUKZN _objBOUKZN = new BOUKZN();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindRolesDropDwon();
            txtStartDate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            txtTodate.Text = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
            BindReport();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    protected void ddlCostCenterCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReport();
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindReport();
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
                    gvReport.AllowPaging = false;
                    BindReport();

                    gvReport.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=CostCenterReport" + txtStartDate.Text + "_" + txtTodate.Text + ".pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #region PrivateMethods
    private void BindRolesDropDwon()
    {
        try
        {
            ddlCostCenterCode.Items.Clear();
            DataSet objDs = _objBOUKZN.GetCostCenterAllData();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlCostCenterCode.DataSource = objDs;
                ddlCostCenterCode.DataValueField = "costcentrecode";
                ddlCostCenterCode.DataTextField = "costcenterdesc";
                ddlCostCenterCode.DataBind();
                ddlCostCenterCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
            }
            else
            {
                ddlCostCenterCode.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindReport()
    {
        try
        {
            string CostCenterCode = ddlCostCenterCode.SelectedValue;
            DataSet objDs = _objBOUKZN.GetCostCenterReports(txtStartDate.Text, txtTodate.Text, CostCenterCode);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvReport.DataSource = objDs.Tables[0];
                gvReport.DataBind();
            }
            else
            {
                gvReport.DataSource = null;
                gvReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #endregion PrivateMethods
}