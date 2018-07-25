using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_QuotationRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Web.UI.HtmlControls.HtmlGenericControl sidemenudiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("sidemenu");
        sidemenudiv.Style.Add("display", "none");

        System.Web.UI.HtmlControls.HtmlGenericControl footerdiv = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("dvfooter");
        footerdiv.Style.Add("Margin-Left", "1px");

    }
}