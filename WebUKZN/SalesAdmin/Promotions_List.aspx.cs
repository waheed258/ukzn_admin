using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using System.Data;
public partial class SalesAdmin_Promotions_List : System.Web.UI.Page
{
    BOUtiltiy _objBOUtility = new BOUtiltiy();
    BALPromotions objBALPromotions = new BALPromotions();
    public string _sortDirection;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }
        if (!IsPostBack)
        {

            ViewState["ps"] = "100";
            BindPromotionsList();

        }
    }

    protected void gdvPromotions_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string npromotionId = e.CommandArgument.ToString();

            Response.Redirect("Promotions_Add.aspx?promotionId=" + npromotionId);

        }
    }
    
    protected void gdvPromotions_Sorting(object sender, GridViewSortEventArgs e)
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
            BindPromotionsList();
        }
        catch (Exception ex)
        {
            LabelError.ForeColor = System.Drawing.Color.Red;
            LabelError.Text = _objBOUtility.ShowMessage("error", "Error", ex.Message);
        }
    }
    protected void gdvPromotions_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvPromotions.PageIndex = e.NewPageIndex;
        BindPromotionsList();
    }
    protected void gdvPromotions_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string imgAsc = @" <img src='../images/sort_asc.png' border='0' title='Ascending' />";
        string imgDes = @" <img src='../images/sort_sesc.png' border='0' title='Descendng' />";
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //foreach (TableCell cell in e.Row.Cells)
            //{
            //    HyperLink lnkbtn = (HyperLink)cell.Controls[1];
            //    if (lnkbtn.Text == GridViewRecord.SortExpression)
            //    {
            //        if (GridViewRecord.SortDirection == SortDirection.Ascending)
            //        {
            //            lnkbtn.Text += imgAsc;
            //        }
            //        else
            //            lnkbtn.Text += imgDes;
            //    }
            //}
        }
    }

    //protected void cmdSearch_Click(object sender, EventArgs e)
    //{
    //    SearchItemFromList(txtSearch.Text.Trim());
    //}
    private void BindPromotionsList()
    {

        try
        {
            gdvPromotions.PageSize = int.Parse(ViewState["ps"].ToString());

            DataSet objds = objBALPromotions.GetPromotionsDetails(0);
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
                gdvPromotions.DataSource = objds.Tables[0];
                gdvPromotions.DataBind();
            }
            else
            {
                gdvPromotions.DataSource = null;
                gdvPromotions.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void Promotions_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["ps"] = DropPage.SelectedItem.ToString().Trim();
        BindPromotionsList();
    }

    void SearchItemFromList(string SearchText)
    {
        try
        {
            if (Session["dt"] != null)
            {
                DataTable dt = (DataTable)Session["dt"];
                DataRow[] dr = dt.Select(
                    "promotionId='" + SearchText +
                    "' OR promotionCategory LIKE '%" + SearchText +
                    "%' OR promotionRelavance LIKE '%" + SearchText +
                    "%' OR promotionType LIKE '%" + SearchText +
                    "%' OR promotionCode LIKE '%" + SearchText +
                    "%' OR DiscountType LIKE '%" + SearchText +
                    "%' OR percentage LIKE '%" + SearchText +
                    "%' OR amount LIKE '%" + SearchText +
                    "%' OR startDate LIKE '%" + SearchText +
                    "%' OR endDate LIKE '%" + SearchText + "%'");
                if (dr.Count() > 0)
                {
                    gdvPromotions.DataSource = dr.CopyToDataTable();
                    gdvPromotions.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            LabelError.Text = _objBOUtility.ShowMessage("error", "Error", ex.Message);
        }
    }
}