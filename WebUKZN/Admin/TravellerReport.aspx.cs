using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using System.IO;
using iTextSharp.text.html.simpleparser;

public partial class Admin_TravellerReport : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BORpeort _objBORpeort = new BORpeort();
    BALUserManager _objBALUserManager = new BALUserManager();
    BALUser _objBALUser = new BALUser();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        if (!IsPostBack)
        {
            BindTravellers();
            BindReport();
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void BindReport()
    {
        try
        {
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            int RoleID = Convert.ToInt32(ddlTraveller.SelectedValue);
            int UserId = Convert.ToInt32(Session["loginId"]);

            DataSet objDs = _objBORpeort.GetTravellerReport(UserId, RoleID, CompanyId);

            if (objDs.Tables[0].Rows.Count > 0)
            {
                gvClientReport.DataSource = objDs.Tables[0];
                gvClientReport.DataBind();
            }
            else
            {
                gvClientReport.DataSource = null;
                gvClientReport.DataBind();
            }


        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void BindTravellers()
    {
        try
        {
            ddlTraveller.Items.Clear();
            DataSet objDs = _objBALUser.GetAllTravellers();
            if (objDs.Tables[0].Rows.Count > 0)
            {
                ddlTraveller.DataSource = objDs;
                ddlTraveller.DataValueField = "TravellerId";
                ddlTraveller.DataTextField = "TravellerName";
                ddlTraveller.DataBind();
                ddlTraveller.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
            }
            else
            {
                ddlTraveller.Items.Insert(0, new System.Web.UI.WebControls.ListItem("ALL", "0"));
            }
        }
        catch(Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
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
                    gvClientReport.AllowPaging = false;
                    BindReport();

                    gvClientReport.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ClientDetails.pdf");
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
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {

    }
    protected void ddlTraveller_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}