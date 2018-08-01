using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityManager;
using BusinessManager;
using System.Data;

public partial class Admin_CustomerList : System.Web.UI.Page
{
    BOUtiltiy objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            //ViewState["ps"] = "100";
            BindCustomerDetails();
        }
    }
    #region Private Methods
    private void BindCustomerDetails()
    {
        BALUser objBALUser = new BALUser();
        try
        {
            gdvCustomer.PageSize = int.Parse(ViewState["ps"].ToString());
            DataSet objds = objBALUser.GetUserDetails(0);
            Session["dt"] = objds.Tables[0];
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
                gdvCustomer.DataSource = objds.Tables[0];
                gdvCustomer.DataBind();
            }
            else
            {
                gdvCustomer.DataSource = null;
                gdvCustomer.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    #endregion Private Methods
    protected void gdvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            string nuser_master_id = e.CommandArgument.ToString();

            Response.Redirect("Customer.aspx?user_master_id=" + nuser_master_id);

        }
    }
    //protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
    //    BindCustomerDetails();
    //}

    protected void gdvCustomer_Sorting(object sender, GridViewSortEventArgs e)
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
            BindCustomerDetails();
        }
        catch (Exception ex)
        {
            LabelError.ForeColor = System.Drawing.Color.Red;
            LabelError.Text = objBOUtiltiy.ShowMessage("error", "Error", ex.Message);
        }
    }
    protected void gdvCustomer_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvCustomer.PageIndex = e.NewPageIndex;
        BindCustomerDetails();
    }
}