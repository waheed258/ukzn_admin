using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PolicyList : System.Web.UI.Page
{
    BAPolicyRules objBAPolicyRules = new BAPolicyRules();
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["ps"] = "10";
            BindPloicy();
        }
    }


    private void BindPloicy()
    {
        gvPolicy.PageSize = int.Parse(ViewState["ps"].ToString());
        DataSet ds = objBAPolicyRules.GetPolicys(0);

        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string sortDirection = "ASC", sortExpression;
            if (ViewState["so"] != null)
            {
                sortDirection = ViewState["so"].ToString();
            }
            if (ViewState["se"] != null)
            {
                sortExpression = ViewState["se"].ToString();
                ds.Tables[0].DefaultView.Sort = sortExpression + " " + sortDirection;
            }

            gvPolicy.DataSource = ds.Tables[0];
            gvPolicy.DataBind();
        }

        else
        {
            gvPolicy.DataSource = null;
            gvPolicy.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Policysetup.aspx");
    }
    protected void gvPolicy_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string PolicyId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Policy")
        {
            Response.Redirect("Policysetup.aspx?PolicyId=" + HttpUtility.UrlEncode(objBOUtiltiy.Encrypt(PolicyId, true)));
        }

        if (e.CommandName == "Delete Policy")
        {
            DeletePolicy(Convert.ToInt32(PolicyId));
            BindPloicy();
        }
    }
    protected void gvPolicy_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPolicy.PageIndex = e.NewPageIndex;
        BindPloicy();
    }
    protected void gvPolicy_Sorting(object sender, GridViewSortEventArgs e)
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
            BindPloicy();
        }
        catch (Exception ex)
        {
            lblMsg.ForeColor = System.Drawing.Color.Red;
            lblMsg.Text = objBOUtiltiy.ShowMessage("error", "Error", ex.Message);
        }
    }


    private void DeletePolicy(int PolicyId)
    {
        try
        {
            int Result = objBAPolicyRules.DeletePolicy(PolicyId);

        }
        catch (Exception ex)
        {
             
        }
    }
}