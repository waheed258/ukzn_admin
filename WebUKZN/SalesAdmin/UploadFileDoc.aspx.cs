using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using BusinessManager;

public partial class SalesAdmin_UploadFileDoc : System.Web.UI.Page
{
    string strFileNo = string.Empty;
    BALFileManager objBALFileManager = new BALFileManager();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["loginId"] == null)
        //{
        //    Response.Redirect("../Login.aspx");
        //}
        if (Request.QueryString["FileNo"] != null)
        {
            strFileNo = Request.QueryString["FileNo"];
        }
        if (!IsPostBack)
        {
            DataSet objds = objBALFileManager.GetAllBookingsByFileNo(strFileNo);
            if (objds.Tables[0].Rows.Count > 0)
            {
                txtFileNo.Text = strFileNo;
                txtPassengerName.Text = objds.Tables[0].Rows[0]["PaxName"].ToString();
                BindGridview();
            }

        }
    }
    private DataTable DisplayFilesInGridView()
    {
        DataTable dtGvSource = new DataTable();
        dtGvSource.Columns.Add("FileName", typeof(String));
        dtGvSource.Columns.Add("FullPath", typeof(String));
        dtGvSource.Columns.Add("Icon", typeof(String));
        return dtGvSource;
    }
    protected void BindGridview()
    {
        try
        {
            DataTable gvSource = DisplayFilesInGridView();
            DataRow gvRow;

            string dirPath = Server.MapPath("FileDocuments/" + strFileNo + "/");
            string[] filesLoc = Directory.GetFiles(Server.MapPath("FileDocuments/" + strFileNo + "/"));
            // string[] filesLoc = Directory.GetFiles(Server.MapPath(dirPath));
            if (filesLoc.Length == 0)
            {

                gdvUpload.DataSource = null;
                gdvUpload.DataBind();
                return;
            }
            List<ListItem> files = new List<ListItem>();
            string strUrl = "http://localhost:42286/";// _objBOUtiltiy.LogoUrl();//
            foreach (string dirInfo in filesLoc)
            {
                gvRow = gvSource.NewRow();

                string[] finlNameSplit = Path.GetFileName(dirInfo).Split('_');
                if (finlNameSplit.Length > 0)
                {
                    if (finlNameSplit.Length > 1)
                        gvRow["FileName"] = finlNameSplit[1];
                    else
                        gvRow["FileName"] = finlNameSplit[0];
                }
                else
                {
                    gvRow["FileName"] = Path.GetFileName(dirInfo);
                }
                if (Path.GetExtension(dirInfo).ToUpper() == ".PDF")
                    gvRow["Icon"] = "fa fa-file-pdf-o";
                else if (Path.GetExtension(dirInfo).ToUpper() == ".DOCX")
                    gvRow["Icon"] = "fa fa-file-word-o";
                else if (Path.GetExtension(dirInfo).ToUpper() == ".DOC")
                    gvRow["Icon"] = "fa fa-file-word-o";
                else
                    gvRow["FullPath"] = "fa fa-file-text-o";

                gvRow["FullPath"] = strUrl + "Admin/FileDocuments/" + strFileNo + "/" + Path.GetFileName(dirInfo);
                gvSource.Rows.Add(gvRow);
            }
            gdvUpload.DataSource = gvSource;
            gdvUpload.DataBind();
        }
        catch (Exception ex)
        {
            labbel2.Text = ex.Message;
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void gdvUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string FileName = e.CommandArgument.ToString();

        string dirPath = Server.MapPath("FileDocuments/" + strFileNo + "/" + FileName);


        string url = "ViewDocument.aspx?FileNo=" + strFileNo + "&FileName=" + FileName;
        string s = "window.open('" + url + "', 'popup_window', 'width=800,height=1000,left=100,top=100,resizable=yes');";
        ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
    }
    protected void cmdSubmit_Click(object sender, EventArgs e)
    {
        // label1.Text = "<b>uploaded file<b/><br/>";
        //  labbel2.Text = "<b>not uploaded file<b/><br/>";
        label1.Visible = true;
        try
        {
            // Check File Prasent or not  
            if (fileuplaod1.HasFiles)
            {
                int filecount = 0;
                int fileuploadcount = 0;
                //check No of Files Selected  
                filecount = fileuplaod1.PostedFiles.Count();            
                if (filecount <= 10)
                {
                    foreach (HttpPostedFile postfiles in fileuplaod1.PostedFiles)
                    {
                        //Get The File Extension  
                        string filetype = Path.GetExtension(postfiles.FileName);
                        //if (filetype.ToLower() == ".docx" || filetype.ToLower() == ".pdf" || filetype.ToLower() == ".txt" || filetype.ToLower() == ".doc")
                        //{
                        //Get The File Size In Bite  
                        double filesize = postfiles.ContentLength;
                        if (filesize < (6048576))
                        {
                            fileuploadcount++;
                            string serverfolder = string.Empty;
                            string serverpath = string.Empty;
                            // Adding File Into Scecific Folder Depend On his Extension  


                            serverfolder = Server.MapPath("FileDocuments/" + strFileNo + "/");
                            //check Folder avlalible or not  
                            if (!Directory.Exists(serverfolder))
                            {
                                // create Folder  
                                Directory.CreateDirectory(serverfolder);
                            }
                            string GeneratedFileName = DateTime.Now.Ticks.ToString() + "_" + postfiles.FileName.Replace("_", "").Replace(".pdf", "") + "." + Path.GetExtension(postfiles.FileName);
                            serverpath = serverfolder + GeneratedFileName;
                            fileuplaod1.SaveAs(serverpath);
                            // label1.Text += "[" + postfiles.FileName + "]- document file uploaded  successfully<br/>";
                        }
                        else
                        {
                            //labbel2.Text += "[" + postfiles.FileName + "]- files not uploded size is greater then(1)MB.<br/>Your File Size is(" + (filesize / (1024 * 1034)) + ") MB </br>";
                        }
                        //}
                        //else
                        //{
                        //    labbel2.Text += "[" + postfiles.FileName + "]- file type must be .doc or pdf and other<br/>";
                        //}
                    }
                }
                else
                {
                    label1.Visible = false;
                    // labbel2.Text = "you are select(" + filecount + ")files <br/>";
                    // labbel2.Text += "please select Maximum five(10) files !!!";
                }
                label3.Visible = true;
                // label3.Text = "ToTal File =(" + filecount + ")<br/> Uploded file =(" + fileuploadcount + ")<br/> Not Uploaded=(" + (filecount - fileuploadcount) + ")";
            }
            else
            {
                label1.Visible = false;
                label3.Visible = false;
                labbel2.Text = "<b>please select the file for upload !!!</b></br>";
            }
            BindGridview();
        }
        catch (Exception ex)
        {
            labbel2.Text = ex.Message;
        }
    }
}