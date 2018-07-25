using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BranchList : System.Web.UI.Page
{
    BALBranch _objBalBranch = new BALBranch();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PageRowSize"] = 10;
            BindBranchList();
        }
    }

    protected void gvBranchList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        string BranchId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Branch")
        {
            Response.Redirect("Branch.aspx?BranchId=" + BranchId);
        }
    }
    protected void gvBranchList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBranchList.PageIndex = e.NewPageIndex;
        BindBranchList();
    }
    protected void gvBranchList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void btnBranchAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Branch.aspx");
    }

    #region PrivateMethods
    private void BindBranchList()
    {
        try
        {
            gvBranchList.PageSize = int.Parse(ViewState["PageRowSize"].ToString());
            DataSet BranchDataset = _objBalBranch.GetBranchList(0);
            if (BranchDataset.Tables[0].Rows.Count >= 1)
            {
                gvBranchList.DataSource = BranchDataset;
                gvBranchList.DataBind();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }


    #endregion Privatemethods
}