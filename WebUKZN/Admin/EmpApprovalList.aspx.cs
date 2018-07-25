using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;
using EntityManager;
using System.Data;

public partial class Admin_EmpApprovalList : System.Web.UI.Page
{
    BALUserUKZN _objBalUser = new BALUserUKZN();
    BOUtiltiyUKZN _objBoUtility = new BOUtiltiyUKZN();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["PageRowSize"] = 10;
            BindEmpApprovalList();

        }
    }

    protected void btnEmpApprovalAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmpApproval.aspx");
    }
    protected void gvEmpApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string EmpId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit Employee Approvals")
        {
            Response.Redirect("EmpApproval.aspx?EmpId=" + EmpId);
        }
    }

    private void BindEmpApprovalList()
    {
        try
        {
            gvEmpApprovalList.PageSize = int.Parse(ViewState["PageRowSize"].ToString());
            DataSet EmpApprovalList = _objBalUser.EmpApprovalCheckGet(0, 0);
            if (EmpApprovalList.Tables[0].Rows.Count > 0)
            {
                gvEmpApprovalList.DataSource = EmpApprovalList;
                gvEmpApprovalList.DataBind();
            }
            else
            {
                gvEmpApprovalList.DataSource = null;
                gvEmpApprovalList.DataBind();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    protected void gvEmpApprovalList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvEmpApprovalList.PageIndex = e.NewPageIndex;
        BindEmpApprovalList();
    }
}