using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;

public partial class Form_MISUpdate : System.Web.UI.Page
{
    #region Developer Note

    /// <summary>
    /// Ramesh 24-07-2013
    /// For TAT Report in MIS
    /// </summary>

    #endregion
    #region Declaration
    Connection gl = new Connection();
    MISReports ObjMisReport = new MISReports();
    MySqlConnection mConnection;
    MySqlDataAdapter mDa;
    MySqlCommand mCmd;
    MySqlDataReader mDr;
    MySqlParameter[] mparam;
    DataView dataview = new DataView();
    public string ProductID = "";
    #endregion
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            Response.Redirect("LoginPage.aspx");
        }
        else
        {
            LblError.Text = "";
            if (!Page.IsPostBack)
            {
                //LoadGridTATReport();
                //LoadDUP();
                btnUpdate.Visible = false;
            }
        }
        btnUpdate.Attributes.Add("onclick", "if(confirm('Are You Want To Submit it This Report To MIS For - " + txtDate.Text + "'))return true;else return false");        
    }



    #endregion
    #region Page_preInit
    //protected void Page_preInit(object sender, EventArgs e)
    //{
    //    Page.Theme = SessionHandler.Theme;
    //}
    #endregion
    #region MoveToMIS_Click
    protected void MoveToMIS_Click(object sender, EventArgs e)
    {
        try
        {

            //if (txtComments.Text.Trim() == "")
            //{
            //    LblError.Text = "Please Enter The Comments..!";
            //    return;

            //}
           // string MISConString = "server=10.0.1.17;database=stringreports;uid=root;password=string;pooling=false;";
            string MISConString = "server=192.168.10.13;database=stringreports;uid=root;password=excel90();pooling=false;";
            AddEntries(MISConString);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region LoadGridTATReport
    private void LoadGridTATReport()
    {
        try
        {
            DataTable DtResult = new DataTable();
            string ReportType = "";
            DtResult = ObjMisReport.GetTodayTATReport(ObjMisReport.GetReportDate(txtDate.Text));
            if (DtResult.Rows.Count > 0)
            {
                GridViewResult.DataSource = DtResult;
                GridViewResult.DataBind();

            }
            else
            {
                GridViewResult.DataSource = null;
                GridViewResult.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    #region LoadGridVolumeUpdate
    private void LoadGridVolumeUpdate()
    {

        //try
        //{
        //    DataTable DtResult = new DataTable();
        //    string ReportType = "";
        //    DtResult = ObjMisReport.GetTodayTATVolumeUpdate(ObjMisReport.GetReportDate());
        //    if (DtResult.Rows.Count > 0)
        //    {
        //        GridViewVolumeUpdate.DataSource = DtResult;
        //        GridViewVolumeUpdate.DataBind();
        //    }
        //    else
        //    {
        //        GridViewVolumeUpdate.DataSource = null;
        //        GridViewVolumeUpdate.DataBind();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

    }
    #endregion
    #region LoadGridDownLoadUPloadPattern
    public void LoadGridDownLoadUPloadPattern()
    {
        try
        {
            //string ReportType = "";
            //DataSet DtResult = new DataSet();
            //DtResult = ObjMisReport.GetTodayDUPatternReport();
            //GridView1.DataSource = ConvertThepatternToCustomFormat(DtResult);
            //GridView1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }
    #endregion
    #region ConvertThepatternToCustomFormat
    private DataTable ConvertThepatternToCustomFormat(DataTable DTResult)
    {
        try
        {
            string PrID = "dbb0cacf6a3aa2d769bc19ef5f0dbc7f";

            DataTable DtCustom = new DataTable();
            DtCustom.Columns.Add("Project", typeof(string));
            DtCustom.Columns.Add("Tittle", typeof(string));
            for (int i = 0; i < (DTResult.Rows.Count - 1); i++)
            {
                DtCustom.Columns.Add(DTResult.Rows[i][0].ToString(), typeof(string));
            }

            DataRow Dr = DtCustom.NewRow();

            Dr["Project"] = PrID;
            Dr["Tittle"] = "Download";

            for (int i = 0; i < (DTResult.Rows.Count - 1); i++)
            {
                Dr[DTResult.Rows[i][0].ToString()] = DTResult.Rows[i][1].ToString();
            }
            DtCustom.Rows.Add(Dr);


            DataRow Dr1 = DtCustom.NewRow();
            Dr1["Project"] = PrID;
            Dr1["Tittle"] = "Upload";

            for (int i = 0; i < (DTResult.Rows.Count - 1); i++)
            {
                Dr1[DTResult.Rows[i][0].ToString()] = DTResult.Rows[i][2].ToString();
            }
            DtCustom.Rows.Add(Dr1);
            GridView1.DataSource = DtCustom;
            GridView1.DataBind();
            return DtCustom;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    #endregion
    #region CheckTATUploadStatus
    private bool CheckTATUploadStatus(string MISConstring)
    {
        try
        {
            string PrID = ObjMisReport.GetMISProjectName();
            bool Status = false;
            string StrQueryReport = "select count(id) as Cnt from " + ObjMisReport.GetReportTable() + " r where r.Date='" + ObjMisReport.GetReportDateymd(txtDate.Text) + "';";
            DataSet DSResult = ExecuteQuery(StrQueryReport, MISConstring);

            if (Convert.ToInt32(DSResult.Tables[0].Rows[0]["Cnt"].ToString()) > 0)
            {
                Status = false;
            }
            else if (Convert.ToInt32(DSResult.Tables[0].Rows[0]["Cnt"].ToString()) == 0)
            {
                Status = true;
            }
            return Status;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }


    private bool CheckDUPUploadStatus(string MISConstring)
    {
        try
        {
            string PrID = ObjMisReport.GetMISProjectName();
            bool Status = false;
            string StrQueryReport = "select count(d.id)  as Cnt from downupload_pattern d  where date(d.fdate)='" + ObjMisReport.GetReportDateymd(txtDate.Text) + "' and d.project='" + PrID + "';";
            DataSet DSResult = ExecuteQuery(StrQueryReport, MISConstring);
            if (Convert.ToInt32(DSResult.Tables[0].Rows[0]["Cnt"].ToString()) > 0)
            {
                Status = false;
            }
            else if (Convert.ToInt32(DSResult.Tables[0].Rows[0]["Cnt"].ToString()) == 0)
            {
                Status = true;
            }
            return Status;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    #endregion
    #region DatabaseSideCodings
    #region openConnection
    public MySqlConnection openConnection(string ConnectionString)
    {
        mConnection = new MySqlConnection(ConnectionString);
        if (mConnection.State == ConnectionState.Open)
        {
            mConnection.Close();
        }
        mConnection.Open();
        return mConnection;
    }
    #endregion
    #region closeConnection
    public void closeConnection(string ConnectionString)
    {
        mConnection = new MySqlConnection(ConnectionString);
        if (mConnection.State == ConnectionState.Open)
        {
            mConnection.Close();
        }
        mConnection.Dispose();
    }
    #endregion
    #region ExecuteQuery
    public DataSet ExecuteQuery(string Query, string ConnectionString)
    {
        DataSet ds;
        openConnection(ConnectionString);
        mCmd = new MySqlCommand(Query, mConnection);
        mCmd.CommandTimeout = 400;

        ds = new DataSet();
        mDa = new MySqlDataAdapter(mCmd);
        mDa.Fill(ds);

        mConnection.Close();
        mConnection.Dispose();
        return ds;

    }
    #endregion
    #region ExecuteNonQuery
    public int ExecuteNonQuery(string Query, string ConnectionString)
    {


        try
        {
            int result;
            openConnection(ConnectionString);
            mCmd = new MySqlCommand(Query, mConnection);
            mCmd.CommandTimeout = 400;
            result = mCmd.ExecuteNonQuery();
            return result;
            mConnection.Close();
            mConnection.Dispose();
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }

    }
    #endregion
    #region ExecuteSPAdapter
    public MySqlDataAdapter ExecuteSPAdapter(string query, bool isProcedure, MySqlParameter[] myParams, string ConnectionString)
    {
        openConnection(ConnectionString);
        mCmd = new MySqlCommand(query, mConnection);
        mCmd.CommandTimeout = 400;
        if (isProcedure)
        {
            mCmd.CommandType = CommandType.StoredProcedure;
            if (myParams != null)
            {
                foreach (MySqlParameter param in myParams)
                {
                    mCmd.Parameters.Add(param);
                }
            }
        }
        try
        {
            mDa = new MySqlDataAdapter(mCmd);
            return mDa;
        }
        catch (MySqlException mye)
        {
            return mDa;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }
    #endregion
    #endregion
    #region AddEntries
    private void AddEntries(string MISConnectionString)
    {

        try
        {
            string ReportDate = ObjMisReport.GetReportDate(txtDate.Text);
            //2013-07-31 04:24:29

            string errMsg = "";

            string PrID = ObjMisReport.GetMISProjectName();
            string StrQuery = "";
            if (CheckDUPUploadStatus(MISConnectionString) == true)
            {

                if (GridView1.Rows.Count > 0)
                {
                    StrQuery = "";
                    StrQuery = "Insert Into downupload_pattern( fdate,  tdate,  project,  Hours,  download,  upload) Values";
                    GridView GVDUP = new GridView();
                    GVDUP = GridView1;

                    //for (int i = 1; i < 24; i++)
                    //{
                    //    string Hour = Convert.ToString(i - 1) + " - " + Convert.ToString(i);
                    //    Label Lbldownload = (Label)GVDUP.Rows[0].FindControl("hour" + i);
                    //    Label Lblupload = (Label)GVDUP.Rows[1].FindControl("hour" + i);
                    //    StrQuery += "('" + ObjMisReport.GetReportDateWithTime(txtDate.Text) + "','" + ObjMisReport.GetReportDateWithTime(txtDate.Text) + "','" + PrID + "','" + Hour + "','" + Lbldownload.Text + "','" + Lblupload.Text + "')";
                    //    if (i < 23)
                    //    {
                    //        StrQuery += ",";
                    //    }
                    //}


                    int colCount = GVDUP.Columns.Count;
                    for (int i = 1; i < 25; i++)
                    {
                        string Hour = GVDUP.Columns[i].HeaderText.ToString();
                        Label Lbldownload = (Label)GVDUP.Rows[0].FindControl("hour" + (i - 1));
                        Label Lblupload = (Label)GVDUP.Rows[1].FindControl("hour" + (i - 1));
                        StrQuery += "('" + ObjMisReport.GetReportDateWithTime(txtDate.Text) + "','" + ObjMisReport.GetReportDateWithTime(txtDate.Text) + "','" + PrID + "','" + Hour + "','" + Lbldownload.Text + "','" + Lblupload.Text + "')";

                        if (i < 24)
                        {
                            StrQuery += ",";
                        }
                    }


                    int Result1 = ExecuteNonQuery(StrQuery, MISConnectionString);

                    errMsg = "Today Download Upload Pattern Successfully Submitted MIS..!";
                }

            }
            else
            {
                errMsg += "Today Download Upload Pattern Already Submitted MIS..!";
            }
            if (CheckTATUploadStatus(MISConnectionString) == true)
            {
                if (GridViewResult.Rows.Count > 0)
                {
                    StrQuery = "";
                    StrQuery = "Insert Into " + ObjMisReport.GetReportTable() + "(Date,UserName,KeyCount, AvgKeyTime, QCCount, AVGQCTime, ReviewCount, AvgReviewTime) Values";
                    GridView GVResultTAT = new GridView();
                    GVResultTAT = GridViewResult;
                    int Counter = 1;

                    foreach (GridViewRow R in GVResultTAT.Rows)
                    {
                        StrQuery += "('" + ObjMisReport.GetReportDateymd(txtDate.Text) + "','" + R.Cells[0].Text + "','" + R.Cells[1].Text + "','" + R.Cells[2].Text + "','" + R.Cells[3].Text + "','" + R.Cells[4].Text + "','" + R.Cells[5].Text + "','" + R.Cells[6].Text + "')";

                        if (Counter < GVResultTAT.Rows.Count)
                        {
                            StrQuery += ",";
                        }
                        Counter += 1;
                    }
                    int Result1 = ExecuteNonQuery(StrQuery, MISConnectionString);
                    errMsg += "Today TAT Report Successfully Submitted MIS..!";
                }

            }
            else
            {
                errMsg += "Today TAT Report Already Submitted MIS..!";

            }

            LblError.Text = errMsg;

            //if (GridViewVolumeUpdate.Rows.Count > 0)
            //{
            //    StrQuery = "";
            //    GridView GVResultVU = new GridView();
            //    GVResultVU = GridViewVolumeUpdate;
            //    int Counter = 1;
            //    StrQuery = "Insert Into dailyupdate(ProjectId,  Year,  Month,  RDate,  Received,  Rejected,Delivered,Comments,CreatedBy,DateCreated,RDay,DailyUpdateId) Values";
            //    foreach (GridViewRow R in GVResultVU.Rows)
            //    {
            //        StrQuery += "('" + PrID + "','" + DateTime.Now.Year + "',MONTHNAME(now()),'" + ObjMisReport.GetReportDateymd() + "','" + R.Cells[0].Text + "','" + R.Cells[1].Text + "','" + R.Cells[2].Text + "','" + txtComments.Text + "','" + SessionHandler.UserName + "','" + ObjMisReport.GetReportDateWithTime() + "','" + ObjMisReport.GetReportDateWithTime() + "','" + SessionHandler.UserName.ToString() + "')";
            //        if (Counter != GVResultVU.Rows.Count)
            //        {
            //            StrQuery += ",";
            //        }
            //        Counter += 1;
            //    }
            //    int Result1 = ExecuteNonQuery(StrQuery, MISConnectionString);
            //}

        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    #endregion
    #region LoadDUP
    private void LoadDUP()
    {
        try
        {
            int review = 2;
            DataTable Dt = ObjMisReport.ConvertUploadtoDataview(review, Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy"), Convert.ToDateTime(txtDate.Text).ToString("dd-MM-yyyy"));
            GridView1.DataSource = ConvertThepatternToCustomFormat(Dt);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            LblError.Text = ex.ToString();
        }

    }
    #endregion
    #region cmdShow_Click
    protected void cmdShow_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtDate.Text.Trim() == "")
            {
                LblError.Text = "Please Select The Date..!";
                return;
            }
            LoadGridTATReport();
            LoadDUP();
            btnUpdate.Visible = true;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}
