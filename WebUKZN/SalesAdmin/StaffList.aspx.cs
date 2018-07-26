using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class SalesAdmin_StaffList : System.Web.UI.Page
{
    private BALUserManager _objBALUserManager = new BALUserManager();
    private BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        if (!IsPostBack)
        {
           // BindGrid();
        }
    }

    #region PrivateMethods
    private void BindGrid()
    {
        DataSet objDs = _objBALUserManager.GetStaffDetails(0, 1);
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
            Response.Redirect("StaffCreation.aspx?stfid=" + StaffDtlId);

        }
        else
        {
            Response.Redirect("TargetSetup.aspx?stfid=" + StaffDtlId);
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("StaffCreation.aspx");
    }
}