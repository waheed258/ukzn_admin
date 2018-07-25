using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmployeeList : System.Web.UI.Page
{
    BALUserUKZN objBalUser = new BALUserUKZN();
    BOUtiltiyUKZN _objBOUtiltiy = new BOUtiltiyUKZN();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PageRowSize"] = 10;
            GirdUserList();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("Employee.aspx");
    }

    #region PrivateMethods
    private void GirdUserList()
    {
        try
        {
            gvUserList.PageSize = int.Parse(ViewState["PageRowSize"].ToString());
            DataSet ds = objBalUser.GetUserList(0);
            gvUserList.DataSource = ds;
            gvUserList.DataBind();
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #endregion Privatemethods
    protected void DropPage_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void gvUserList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string UserId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit User")
        {
            Response.Redirect("Employee.aspx?UserId=" + UserId);
        }

    }
    protected void gvUserList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvUserList.PageIndex = e.NewPageIndex;
        GirdUserList();
       // SearchItemFromList(txtSearch.Text.Trim());
    }
    protected void gvUserList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void imgsearch_Click(object sender, ImageClickEventArgs e)
    {

    }
}