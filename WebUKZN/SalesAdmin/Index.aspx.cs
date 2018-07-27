using BusinessManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesAdmin_Index : System.Web.UI.Page
{
    BORpeort objBORpeort = new BORpeort();
    BOUtiltiy _objBOUtiltiy = new BOUtiltiy();
    BALUserManager _objBALUserManager = new BALUserManager();
    string CurrencyCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["loginId"] == null)
            {
                Response.Redirect("../SalesLogin.aspx");
            }
            if (!IsPostBack)
            {
                //DailyReport();
                //MonthlyReport();
                //YearReport();
                //BindTargetPer();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = _objBOUtiltiy.ShowMessage("danger", "Error", ex.Message);
        }
    }
    private void DailyReport()
    {
        lblDayTitle.Text = DateTime.Now.ToString("MMMM dd, yyyy");
        string LoginId = Session["loginId"].ToString();
        string FromDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
        string ToDate = _objBOUtiltiy.ConvertDateFormat(DateTime.Now.ToString());
        DataSet objDayWiseReport = objBORpeort.GetDashboardSourceReport(FromDate, ToDate, LoginId, Convert.ToInt32(Session["role_id"]), Convert.ToInt32(Session["CompanyId"]));
        string strDayReport = string.Empty;
        string strDayReportValues = string.Empty;

        foreach (DataRow dr in objDayWiseReport.Tables[0].Rows)
        {
            strDayReport = strDayReport + "," + dr["BookingSource"].ToString() + "-" + dr["PaymentAmount"].ToString();
            strDayReportValues = strDayReportValues + "," + dr["PaymentAmount"].ToString();

        }
        hfDayReport.Value = strDayReport.TrimStart(',');
        hfDayReportValues.Value = strDayReportValues.TrimStart(',');
    }
    private void MonthlyReport()
    {
        string LoginId = Session["loginId"].ToString();
        DateTime now = DateTime.Now;
        var startDate = new DateTime(now.Year, now.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);
        lblMonthTitle.Text = DateTime.Now.ToString("MMMM, yyyy");
        string MonthlyFromDate = _objBOUtiltiy.ConvertDateFormat(startDate.ToString());
        string MonthlyToDate = _objBOUtiltiy.ConvertDateFormat(endDate.ToString());
        string strMonthReport = string.Empty;
        string strMonthReportValues = string.Empty;
        DataSet objMonthWiseReport = objBORpeort.GetDashboardSourceReport(MonthlyFromDate, MonthlyToDate, LoginId, Convert.ToInt32(Session["role_id"]), Convert.ToInt32(Session["CompanyId"]));
        foreach (DataRow dr in objMonthWiseReport.Tables[0].Rows)
        {
            strMonthReport = strMonthReport + "," + dr["BookingSource"].ToString() + "-" + dr["PaymentAmount"].ToString();
            strMonthReportValues = strMonthReportValues + "," + dr["PaymentAmount"].ToString();
        }
        hfMonthReport.Value = strMonthReport.TrimStart(',');
        hfMonthReportValues.Value = strMonthReportValues.TrimStart(',');
    }
    private void YearReport()
    {
        string LoginId = Session["loginId"].ToString();



        var LastYear = DateTime.Now.AddMonths(-11);
        DateTime now = DateTime.Now;
        var startYearDate = new DateTime(LastYear.Year, LastYear.Month, 1);

        var startDate = new DateTime(now.Year, now.Month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        lblYearTitel.Text = startYearDate.ToString("yyyy") + "-" + endDate.ToString("yyyy");
        string YearlyFromDate = _objBOUtiltiy.ConvertDateFormat(startYearDate.ToString());
        string YearlyToDate = _objBOUtiltiy.ConvertDateFormat(endDate.ToString());
        string strYearReport = string.Empty;
        string strYearReportValues = string.Empty;
        DataSet objMonthWiseReport = objBORpeort.GetDashboardSourceReport(YearlyFromDate, YearlyToDate, LoginId, Convert.ToInt32(Session["role_id"]), Convert.ToInt32(Session["CompanyId"]));

        if (objMonthWiseReport.Tables[1].Rows.Count > 0)
        {
            ReadPreviousMotnhs(objMonthWiseReport.Tables[1]);
        }

    }

    public void ReadPreviousMotnhs(DataTable dt)
    {
        string strYearReport = string.Empty;
        string strYearReportValues = string.Empty;
        for (int i = 0; i < 12; i++)
        {
            string MonthName = DateTime.Now.AddMonths(-i).ToString("MMM");

            string expression = "YearMonthName ='" + MonthName + "'";
            DataRow[] dr = dt.Select(expression);
            strYearReport = strYearReport + "," + dr[0]["YearMonthName"].ToString();
            strYearReportValues = strYearReportValues + "," + dr[0]["Amount"].ToString();
        }
        hfYearMonth.Value = strYearReport.TrimStart(',');
        hfYearValue.Value = strYearReportValues.TrimStart(',');
    }

    public void BindTargetPer()
    {
        try
        {
            StringBuilder strbld = new StringBuilder();
            int Month = DateTime.Today.Month;
            int currentYear = DateTime.Today.Year;
            DataSet objDs = _objBALUserManager.GetTargetTopValues(Month, currentYear, Convert.ToInt32(Session["loginId"]));
            if (objDs.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < objDs.Tables[0].Rows.Count; i++)
                {
                    decimal TargetPer = Convert.ToDecimal(objDs.Tables[0].Rows[i]["TargetCompletePer"]);
                    if (TargetPer > 75)
                    {
                        strbld.Append("<span style=font-size:12px>" + objDs.Tables[0].Rows[i]["UserName"] + ",Target : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["TargetAmount"] + ", Completed : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["CompletedAmount"] + "</span>");
                        strbld.Append("<div class='progress progress-striped light active m-md'>");
                        strbld.Append("<div class='progress-bar progress-bar-primary' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width:" + objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%'>");
                        strbld.Append(objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%</div></div>");

                    }
                    else if (TargetPer > 60 && TargetPer < 75)
                    {
                        strbld.Append("<span style=font-size:12px>" + objDs.Tables[0].Rows[i]["UserName"] + ",Target : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["TargetAmount"] + ", Completed : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["CompletedAmount"] + "</span>");
                        strbld.Append("<div class='progress progress-striped light active m-md'>");
                        strbld.Append("<div class='progress-bar progress-bar-success' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width:" + objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%'>");
                        strbld.Append(objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%</div></div>");
                    }
                    else if (TargetPer > 50 && TargetPer < 60)
                    {
                        strbld.Append("<span style=font-size:12px>" + objDs.Tables[0].Rows[i]["UserName"] + ",Target : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["TargetAmount"] + ", Completed : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["CompletedAmount"] + "</span>");
                        strbld.Append("<div class='progress progress-striped light active m-md'>");
                        strbld.Append("<div class='progress-bar progress-bar-warning' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width:" + objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%'>");
                        strbld.Append(objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%</div></div>");
                    }
                    else if (TargetPer > 40 && TargetPer < 50)
                    {
                        strbld.Append("<span style=font-size:12px>" + objDs.Tables[0].Rows[i]["UserName"] + ",Target : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["TargetAmount"] + ", Completed : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["CompletedAmount"] + "</span>");
                        strbld.Append("<div class='progress progress-striped light active m-md'>");
                        strbld.Append("<div class='progress-bar progress-bar-info' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width:" + objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%'>");
                        strbld.Append(objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%</div></div>");
                    }
                    else if (TargetPer < 40)
                    {
                        strbld.Append("<span style=font-size:12px>" + objDs.Tables[0].Rows[i]["UserName"] + ",Target : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["TargetAmount"] + ", Completed : " + CurrencyCode + " " + objDs.Tables[0].Rows[i]["CompletedAmount"] + "</span>");
                        strbld.Append("<div class='progress progress-striped light active m-md'>");
                        strbld.Append("<div class='progress-bar progress-bar-danger' role='progressbar' aria-valuenow='60' aria-valuemin='0' aria-valuemax='100' style='width:" + objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%'>");
                        strbld.Append(objDs.Tables[0].Rows[i]["TargetCompletePer"] + "%</div></div>");
                    }

                }

            }
            divTarget.InnerHtml = strbld.ToString();
            strbld.Length = 0;
            strbld.Capacity = 0;
        }
        catch (Exception ex)
        {

        }
    }
}