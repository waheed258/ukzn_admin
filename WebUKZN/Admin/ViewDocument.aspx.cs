using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ViewDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["FileName"] != null)
        {
            string strPath = Server.MapPath("FileDocuments/" + Request.QueryString["FileNo"] + "/" + Request.QueryString["FileName"]); ;
            System.IO.FileInfo file = new System.IO.FileInfo(strPath);

            if (file.Exists == true)
            {
                frmDoc.Attributes.Add("src", strPath);
            }
        }



    }
}