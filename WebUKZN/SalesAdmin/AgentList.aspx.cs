using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_AgentList : System.Web.UI.Page
{
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../SalesLogin.aspx");
        }

        if (!IsPostBack)
        {

            BindGrid();
        }
    }

    #region PrivateMethods
    private void BindGrid()
    {
        DataSet objDs = _objBALUserManager.GetStaffDetails(0, 2);
        if (objDs.Tables[0].Rows.Count > 0)
        {
            gvAgent.DataSource = objDs;
            gvAgent.DataBind();
        }
        else
        {
            gvAgent.DataSource = null;
            gvAgent.DataBind();
        }
    }
    #endregion PrivateMethods
    protected void gvStaff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string AgentId = e.CommandArgument.ToString();
        if (e.CommandName == "Edit")
        {
            Response.Redirect("CreateAgent.aspx?agentId=" + AgentId);

        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAgent.aspx");
    }
}