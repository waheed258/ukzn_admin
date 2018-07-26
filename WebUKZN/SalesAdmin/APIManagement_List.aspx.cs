using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_APIManagement_List : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            ViewState["ps"] = "100";
            BindAPIManagementList();
        }
    }


    private void BindAPIManagementList()
    {
        BALAPIManagement objBALAPIManagement = new BALAPIManagement();
        try
        {
            gdvAPIManagement.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet objds = objBALAPIManagement.GetAPIManagementDetails(0);
            if (objds.Tables[0].Rows.Count > 0)
            {

                string sortDirection = "ASC", sortExpression;
                if (ViewState["so"] != null)
                {
                    sortDirection = ViewState["so"].ToString();
                }
                if (ViewState["se"] != null)
                {
                    sortExpression = ViewState["se"].ToString();
                    objds.Tables[0].DefaultView.Sort = sortExpression + " " + sortDirection;
                }

                gdvAPIManagement.DataSource = objds.Tables[0];
                gdvAPIManagement.DataBind();
            }
            else
            {
                gdvAPIManagement.DataSource = null;
                gdvAPIManagement.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void gdvAPIManagement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string nLineSeqid = e.CommandArgument.ToString();

            Response.Redirect("APIManagement_Add.aspx?LineSeqid=" + nLineSeqid);

        }
    }

    protected void gdvAPIManagement_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvAPIManagement.PageIndex = e.NewPageIndex;
        BindAPIManagementList();
    }

    protected void APIManagement_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        BindAPIManagementList();
    }

    protected void gdvAPIManagement_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            ViewState["se"] = e.SortExpression;
            if (ViewState["so"] == null)
                ViewState["so"] = "ASC";
            if (ViewState["so"].ToString() == "ASC")
                ViewState["so"] = "DESC";
            else
                ViewState["so"] = "ASC";
            BindAPIManagementList();
        }
        catch (Exception ex)
        {
            LabelError.ForeColor = System.Drawing.Color.Red;
            LabelError.Text = _objBOUtility.ShowMessage("error", "Error", ex.Message);
        }
    }



}