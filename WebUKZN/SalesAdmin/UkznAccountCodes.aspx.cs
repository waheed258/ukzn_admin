using BusinessManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UKZNInterface.WebReference;

public partial class SalesAdmin_UkznAccountCodes : System.Web.UI.Page
{
    SqlConnection con;
    string sqlconn;
    G0securitytokentypeUser objSec = new G0securitytokentypeUser();
    TravelManagementUKZN objUKZN = new TravelManagementUKZN();
    string _strUKZNsupplierno = ConfigurationManager.AppSettings["UKZNsupplierno"].ToString();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BOUKZN objBOUKZN = new BOUKZN();
    protected void Page_Load(object sender, EventArgs e)
    {

        objSec.authkey = "TEST";
        objSec.publkey = "3841295F206F704F32CCCD3EF1A4A1C5";
        objSec.version = "1.x";
        objSec.timekey = DateTime.Now.ToString("yyyyMMddHHmmss");
        objBOUKZN.TruncateAccountCodes(1);
        Extractaccounts();
        DataSet objDs = objBOUKZN.TruncateAccountCodes(2);
        if (objDs.Tables[0].Rows.Count > 0)
        {
            gvReport.DataSource = objDs;
            gvReport.DataBind();
        }
        else
        {
            gvReport.DataSource = null;
            gvReport.DataBind();
        }
    }
    #region Extractaccounts
    private void Extractaccounts()
    {
        try
        {
            List<Gna160Extractaccrequest1typeUser> objLstAcc = new List<Gna160Extractaccrequest1typeUser>();
            Gna160Extractaccrequest1typeUser objAcc = new Gna160Extractaccrequest1typeUser();
            objAcc.changedonlyyn = "N";
            objAcc.supplierno = _strUKZNsupplierno;
            objLstAcc.Add(objAcc);
            Gna160Extractaccresponse1typeUser[] objResponce = objUKZN.extractaccounts(objSec, objLstAcc.ToArray());
            DataTable dtEmployee = ToDataTableAccounts(objResponce);
            if (dtEmployee.Rows.Count > 0)
                BatchBulkCopyAccounts(dtEmployee, "ukzn_accountcodes");
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error ", ex.Message);
        }
    }
    public DataTable ToDataTableAccounts(Gna160Extractaccresponse1typeUser[] objData)
    {

        DataTable dt = new DataTable();

        dt.Columns.Add("accId");
        dt.Columns.Add("accountcode");
        dt.Columns.Add("accountcategory");
        dt.Columns.Add("accountname");


        int i = 1;
        foreach (Gna160Extractaccresponse1typeUser inobj in objData)
        {
            DataRow row = dt.NewRow();
            row["accId"] = i;
            row["accountcode"] = inobj.accountcode != null ? inobj.accountcode : "";

            row["accountcategory"] = inobj.accountcategory != null ? inobj.accountcategory : "";
            row["accountname"] = inobj.accountname != null ? inobj.accountname : "";

            i++;
            dt.Rows.Add(row);
        }

        return dt;
    }
    public void BatchBulkCopyAccounts(DataTable dataTable, string DestinationTbl)
    {
        try
        {
            DataTable dtSource = dataTable;
            connection();
            //creating object of SqlBulkCopy  
            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = DestinationTbl;

            // column mappings
            objbulk.ColumnMappings.Add("accId", "accId");
            objbulk.ColumnMappings.Add("accountcode", "accountcode");
            objbulk.ColumnMappings.Add("accountcategory", "accountcategory");
            objbulk.ColumnMappings.Add("accountname", "accountname");
            // Finally write to server
            con.Open();
            objbulk.WriteToServer(dtSource);
            con.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void connection()
    {
        sqlconn = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString; ;
        con = new SqlConnection(sqlconn);

    }
    #endregion Extractaccounts
}