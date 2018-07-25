using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LoginHistory : System.Web.UI.Page
{
    BALUserUKZN objBalUser = new BALUserUKZN();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridLoginUserHistory();
        }
    }

    protected void gvLoginEmpList_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void gvLoginEmpList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void gvLoginEmpList_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    #region PrivateMethods
    private void BindGridLoginUserHistory()
    {
        try
        {
            DataSet LoginHistory = objBalUser.UserLoginHistoryGet();
            if (LoginHistory.Tables[0].Rows.Count > 1)
            {
                gvLoginEmpList.DataSource = LoginHistory;
                gvLoginEmpList.DataBind();
            }
            else
            {
                gvLoginEmpList.DataSource = "";
                gvLoginEmpList.DataBind();

            }
        }
        catch (Exception ex)
        {

            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }

    #endregion Privatemethods
}