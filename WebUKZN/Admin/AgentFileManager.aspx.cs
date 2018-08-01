using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AgentFileManager : System.Web.UI.Page
{
    BALFileManager _objBALFileManager = new BALFileManager();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        if (!IsPostBack)
        {
            GetFileGrid();
        }
    }

    #region PrivateMethods
    private void GetFileGrid()
    {
        try
        {
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBALFileManager.GetAgentAllBookings(CreatedBy, RoleId, CompanyId);
            if (objDs.Tables[0].Rows.Count > 0)
            {
                gdvAllFileNo.DataSource = objDs;
                gdvAllFileNo.DataBind();
            }
            else
            {
                gdvAllFileNo.DataSource = null;
                gdvAllFileNo.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }
    #endregion PrivateMethods

    protected void gdvAllFileNo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FileNo = e.CommandArgument.ToString();


        string url = "PrintFile.aspx?FileNo=" + FileNo;
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

    }
}