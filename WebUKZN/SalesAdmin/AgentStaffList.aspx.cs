using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AgentStaffList : System.Web.UI.Page
{
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    #region PrivateMethods
    private void BindGrid()
    {
        DataSet objDs = _objBALUserManager.GetAgentStaffDetails(Convert.ToInt32(Session["loginId"]), 3);
        if (objDs.Tables[0].Rows.Count > 0)
        {
            gvStaff.DataSource = objDs;
            gvStaff.DataBind();
        }
        else
        {
            gvStaff.DataSource = null;
            gvStaff.DataBind();
        }
    }
    #endregion PrivateMethods
    protected void gvStaff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string StaffDtlId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit")
        {
            Response.Redirect("AgentStaffCreation.aspx?stfid=" + StaffDtlId);

        }
        else
        {
            Response.Redirect("TargetSetup.aspx?stfid=" + StaffDtlId);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("AgentStaffCreation.aspx");
    }
}