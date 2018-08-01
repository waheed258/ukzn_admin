using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessManager;

public partial class Admin_Validationerror : System.Web.UI.Page
{
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", "Please contact your requestor to make your travel booking.");
    }
}