using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for MISReports
/// </summary>
public class MISReports
{

    MySqlParameter[] mparam;
    DataView dataview = new DataView();
    Connection db = new Connection();

    public MISReports()
    {
        //
        // TODO: Add constructor logic here
        //
    }



    public DataTable GetTodayTATReport(string ReportDate)
    {
        try
        {
            //string DtSearchDate = "";
            //int hour = DateTime.Now.Hour;
            //if (hour >= 17)
            //{
            //    DtSearchDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //}
            //else if (hour <= 17)
            //{
            //    DtSearchDate = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            //}
            DataSet DSresult = new DataSet();
            MySqlDataAdapter mda;
            mparam = new MySqlParameter[1];
            mparam[0] = new MySqlParameter("?$Date", ReportDate);
            mparam[0].MySqlDbType = MySqlDbType.VarChar;

            mda = db.ExecuteSPAdapter("SP_Get_MIS_Data", true, mparam);

            mda.Fill(DSresult, "Table0");

            return DSresult.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public DataSet GetTodayDUPatternReport(string ReportDate)
    {
        try
        {
            //string DtSearchDate = "";
            //int hour = DateTime.Now.Hour;
            //if (hour >= 17)
            //{
            //    DtSearchDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //}
            //else if (hour <= 17)
            //{
            //    DtSearchDate = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            //}

            //string DtSearchDate = DateTime.Now.ToString("yyyy-MM-dd");
            DataSet DSresult = new DataSet();
            MySqlDataAdapter mda;
            mparam = new MySqlParameter[1];
            mparam[0] = new MySqlParameter("?$Date", ReportDate);
            mparam[0].MySqlDbType = MySqlDbType.VarChar;
            mda = db.ExecuteSPAdapter("SP_GetDowNloadAndUploadPattern", true, mparam);
            mda.Fill(DSresult, "Table0");
            return DSresult;
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }

    public DataTable GetTodayTATVolumeUpdate(string ReportDate)
    {
        try
        {
            //string DtSearchDate = "";
            //int hour = DateTime.Now.Hour;
            //if (hour >= 17)
            //{
            //    DtSearchDate = DateTime.Now.ToString("dd-MMM-yyyy");
            //}
            //else if (hour <= 17)
            //{
            //    DtSearchDate = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            //}

            // string DtSearchDate1 = DateTime.Now.ToString("dd-MMM-yyyy");

            DataSet DSresult = new DataSet();
            MySqlDataAdapter mda;
            mparam = new MySqlParameter[1];
            mparam[0] = new MySqlParameter("?$Date", ReportDate);
            mparam[0].MySqlDbType = MySqlDbType.VarChar;
            mda = db.ExecuteSPAdapter("Sp_Get_Volume_Update", true, mparam);
            mda.Fill(DSresult, "Table0");
            return DSresult.Tables[0];
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }


    public DataTable ConvertUploadtoDataview(int review, string strfrmdate, string strtodate)
    {
        string strfromtime = string.Empty;
        string strtotime = string.Empty;
        DataSet ds = new DataSet();
        DataTable dtTable = new DataTable();
        DataColumn dcolumn;
        try
        {



            dcolumn = new DataColumn();
            dcolumn.DataType = System.Type.GetType("System.String");
            dcolumn.ColumnName = "Timing";
            dcolumn.Caption = "Timing";
            dcolumn.ReadOnly = true;
            dcolumn.Unique = false;
            dtTable.Columns.Add(dcolumn);

            dcolumn = new DataColumn();
            dcolumn.DataType = System.Type.GetType("System.String");
            dcolumn.ColumnName = "Download";
            dcolumn.Caption = "Download";
            dcolumn.ReadOnly = true;
            dcolumn.Unique = false;
            dtTable.Columns.Add(dcolumn);

            dcolumn = new DataColumn();
            dcolumn.DataType = System.Type.GetType("System.String");
            dcolumn.ColumnName = "Upload";
            dcolumn.Caption = "Upload";
            dcolumn.ReadOnly = true;
            dcolumn.Unique = false;
            dtTable.Columns.Add(dcolumn);

            mparam = new MySqlParameter[5];
            mparam[0] = new MySqlParameter("?$review", review);
            mparam[0].MySqlDbType = MySqlDbType.Int32;
            mparam[1] = new MySqlParameter("?$fromdate", strfrmdate);
            mparam[1].MySqlDbType = MySqlDbType.VarChar;
            mparam[2] = new MySqlParameter("?$todate", strtodate);
            mparam[2].MySqlDbType = MySqlDbType.VarChar;

            int h = 18;
            int h1 = 19;
            int downloadtotal = 0, uploadtotal = 0;
            string downtime = "", uptime = "";

            for (int i = 0; i <= 24; i++)
            {
                DataRow dtrow = dtTable.NewRow();
                if (i != 24)
                {
                    if (h <= 9) { strfromtime = "0" + h + ":00:00"; }
                    else if (h <= 24 && h >= 9) { strfromtime = h + ":00:00"; }
                    if (h1 <= 9) { strtotime = "0" + h1 + ":00:00"; }
                    else if (h1 <= 24 && h1 >= 9) { strtotime = h1 + ":00:00"; }

                    mparam[3] = new MySqlParameter("?$fromtiming", strfromtime);
                    mparam[3].MySqlDbType = MySqlDbType.VarChar;
                    mparam[4] = new MySqlParameter("?$totiming", strtotime = strfromtime == "23:00:00" ? "24:00:00" : strtotime);
                    mparam[4].MySqlDbType = MySqlDbType.VarChar;

                    ds = db.ExecuteQuery("sp_uploadpattern_MIS", true, mparam);

                    string[] frmtime = strfromtime.Split(':');
                    string[] totime = strtotime.Split(':');
                    downtime = frmtime[0] + ":" + frmtime[1];
                    uptime = totime[0] + ":" + totime[1];
                    dtrow[0] = downtime + " - " + uptime;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        dtrow[1] = ds.Tables[0].Rows[0]["DownloadTime"];
                        downloadtotal += Convert.ToInt16(dtrow[1].ToString());
                        dtrow[2] = ds.Tables[1].Rows[0]["UploadTime"];
                        uploadtotal += Convert.ToInt16(dtrow[2].ToString());
                    }

                    h = h + 1;
                    h1 = h1 + 1;
                    if (h == 24) { h = 0; }
                    if (h1 == 24) { h1 = 0; }
                }
                else
                {
                    dtrow[0] = "Total";
                    dtrow[1] = downloadtotal.ToString();
                    dtrow[2] = uploadtotal.ToString();
                }
                dtTable.Rows.Add(dtrow);
            }


            return dtTable;

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }



    #region GetReportDate()

    public string GetReportDate(string date)
    {
        try
        {
            string DayofWeek = DateTime.Now.DayOfWeek.ToString().ToLower();
            DateTime ReportDate = Convert.ToDateTime(date);
            //int ProcessTime = DateTime.Now.Hour;

            //if (ProcessTime >= 17)
            //{
            //    ReportDate = DateTime.Now.ToString("dd-MM-yyyy");
            //}
            //else if (ProcessTime <= 17)
            //{
            //    ReportDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            //}
            //ReportDate = DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy");

            return ReportDate.ToString("dd-MM-yyyy");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string GetReportDateWithTime(string Date)
    {
        try
        {
            DateTime DT = Convert.ToDateTime(Date);
            return DT.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("hh:mm:ss");

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetReportDateymd(string Date)
    {
        try
        {
            DateTime ReportDate = Convert.ToDateTime(Date);
            return ReportDate.ToString("yyyy-MM-dd");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    public string GetReportTable()
    {
        string Tblname = "report_hfs";
        return Tblname;
    }

    #region GetMISProjectName
    //public string GetServernameValue(string ServerName)
    public string GetMISProjectName()
    {
        try
        {
            string Servername = "931be245f7f6b60355c7e2a48cb1adf1";
            return Servername;
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }
    #endregion


}
