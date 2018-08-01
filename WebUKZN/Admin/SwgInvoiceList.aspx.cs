using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessManager;

public partial class Admin_SwgInvoiceList : System.Web.UI.Page
{
    BALFileManager _objBALFileManager = new BALFileManager();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    #region Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["loginId"] == null)
        {
            Response.Redirect("../Login.aspx");
            return;
        }
        if (!IsPostBack)
        {
            GetFileGrid();
        }
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void gdvAllFileNo_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FileNo = e.CommandArgument.ToString();
        string url = "PrintFile.aspx?FileNo=" + FileNo;
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    #endregion Events

    #region PrivateMethods
    private void BindGrid()
    {
        try
        {
            DataSet objDs = _objBALFileManager.GetFileByTravellerEmailOrPhone(txtSearch.Text);
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    private void GetFileGrid()
    {
        try
        {
            int CreatedBy = Convert.ToInt32(Session["loginId"]);
            int RoleId = Convert.ToInt32(Session["role_id"]);
            int CompanyId = Convert.ToInt32(Session["CompanyId"]);
            DataSet objDs = _objBALFileManager.GetAllBookings(CreatedBy, RoleId, CompanyId);
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
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    #endregion PrivateMethods
}