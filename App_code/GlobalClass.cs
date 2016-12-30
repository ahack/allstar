using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data;
using MySql.Data.MySqlClient;

/// <summary>
/// Summary description for GlobalClass
/// </summary>
public class GlobalClass
{
    public GlobalClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    Connection con = new Connection();

    #region Variable Declareation

    MySqlDataAdapter mDa;
    MySqlDataReader mDr;
    MySqlParameter[] mParam;
    MySqlCommand cmd;
    DataSet ds = new DataSet();
    DataSet ds1 = new DataSet();
    DataView dataview = new DataView();

    #endregion

    #region Properties

    private string _Admin;
    public string Admin
    {
        get
        {
            if (_Admin == null) { _Admin = "0"; }
            return _Admin;
        }
        set { _Admin = value; }
    }
    private string _Key;
    public string Key
    {
        get
        {
            if (_Key == null) { _Key = "0"; }
            return _Key;
        }
        set { _Key = value; }
    }
    private string _QC;
    public string QC
    {
        get
        {
            if (_QC == null) { _QC = "0"; }
            return _QC;
        }
        set { _QC = value; }
    }
    private string _DU;
    public string DU
    {
        get
        {
            if (_DU == null) { _DU = "0"; }
            return _DU;
        }
        set { _DU = value; }
    }
    private string _Review;
    public string Review
    {
        get
        {
            if (_Review == null) { _Review = "0"; }
            return _Review;
        }
        set { _Review = value; }
    }
    private static DataTable _dattab = null;
    public static DataTable dattab
    {
        get { return _dattab; }
        set { _dattab = value; }
    }
    #endregion

    #region CheckNullDB

    private string checkNullDB(MySqlDataReader mdr, string field)
    {
        if (mdr[field] == DBNull.Value)
        {
            return "";
        }
        else
        {
            return mdr.GetString(field);
        }
    }

    #endregion

    # region ErrorPage
    private static string _error;
    public static string Error
    {
        get { return _error; }
        set { _error = value; }

    }

    public void Errorpage(string errormsg)
    {
        Error = errormsg;
        HttpContext.Current.Response.Redirect("ErrorPage.aspx");
    }
    #endregion

    #region Login Page

    public MySqlDataReader checkLogin(string User, string password)
    {
        mParam = new MySqlParameter[1];
        string query = "select Admin,`Key`,QC,DU,Review from userstatus where Username='" + User + "' and Password=aes_encrypt('" + password + "','String')";
        try
        {
            mDr = con.ExecuteSPReader(query, false, mParam);
            return mDr;
        }
        catch (Exception) { return mDr; }
    }

    #endregion

    #region Settings Page
    public void Get_User_Details(string ColumnOrder, GridView MyGridView)
    {
        string Sqlstr = "select * from userstatus where Tag=0 order by " + ColumnOrder;
        ds = con.ExecuteQuery(Sqlstr);
        if (ds.Tables.Count > 0)
        {
            MyGridView.DataSource = ds;
            MyGridView.DataBind();
        }
    }
    public string CheckUsername(string username)
    {
        string query = "select Username from userstatus where Username = '" + username + "' limit 1";
        string result = con.ExecuteScalar(query);
        return result;
    }
    public void InsertUser(string fullname, string username, int Admin, int s1, int sqc, int prod, int qc, int du, int review)
    {
        string query = "sp_Insertuser";

        mParam = new MySqlParameter[9];
        mParam[0] = new MySqlParameter("?$fullname", fullname);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$username", username);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;
        mParam[2] = new MySqlParameter("?$admin", Admin);
        mParam[2].MySqlDbType = MySqlDbType.Int16;
        mParam[3] = new MySqlParameter("?$s1", s1);
        mParam[3].MySqlDbType = MySqlDbType.Int16;
        mParam[4] = new MySqlParameter("?$sqc", sqc);
        mParam[4].MySqlDbType = MySqlDbType.Int16;
        mParam[5] = new MySqlParameter("?$prod", prod);
        mParam[5].MySqlDbType = MySqlDbType.Int16;
        mParam[6] = new MySqlParameter("?$qc", qc);
        mParam[6].MySqlDbType = MySqlDbType.Int16;
        mParam[7] = new MySqlParameter("?$du", du);
        mParam[7].MySqlDbType = MySqlDbType.Int16;
        mParam[8] = new MySqlParameter("?$review", review);
        mParam[8].MySqlDbType = MySqlDbType.Int16;

        int result = con.ExecuteSPScalar(query, true, mParam);
    }
    public void UpdateUser(string username, int Admin,int s1,int sqc, int prod, int qc, int du, int review)
    {
        string query = "sp_Updateuser";

        mParam = new MySqlParameter[8];
        mParam[0] = new MySqlParameter("?$username", username);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$admin", Admin);
        mParam[1].MySqlDbType = MySqlDbType.Int16;

        mParam[2] = new MySqlParameter("?$s1", s1);
        mParam[2].MySqlDbType = MySqlDbType.Int16;

        mParam[3] = new MySqlParameter("?$sqc", sqc);
        mParam[3].MySqlDbType = MySqlDbType.Int16;


        mParam[4] = new MySqlParameter("?$prod", prod);
        mParam[4].MySqlDbType = MySqlDbType.Int16;
        mParam[5] = new MySqlParameter("?$qc", qc);
        mParam[5].MySqlDbType = MySqlDbType.Int16;
        mParam[6] = new MySqlParameter("?$du", du);
        mParam[6].MySqlDbType = MySqlDbType.Int16;
        mParam[7] = new MySqlParameter("?$review", review);
        mParam[7].MySqlDbType = MySqlDbType.Int16;

        int result = con.ExecuteSPScalar(query, true, mParam);
    }
    public void DeleteUser(string username)
    {
        string query = "Delete from userstatus where Username='" + username + "'";
        int result = con.ExecuteSPNonQuery(query);
    }
    public string Fullname(string username)
    {
        string query = "select fullname from userstatus where Username = '" + username + "' limit 1";
        string result = con.ExecuteScalar(query);
        return result;
    }


    public string OldPassWord(string username)
    {
        string query = "select cast(aes_decrypt(Password,'String') as char) from userstatus  where Username = '" + username + "'";
        string result = con.ExecuteScalar(query);
        return result;
    }
    #endregion

    #region Assign Job
    public int InsertData_New(string OrderNo, string pdate, string state, string county, string product)
    {
        mParam = new MySqlParameter[5];
        mParam[0] = new MySqlParameter("?$OrderNo", checkStrings(OrderNo.Trim()));
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[0].IsNullable = false;

        mParam[1] = new MySqlParameter("?$pdate", checkStrings(pdate));
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        mParam[2] = new MySqlParameter("?$states", checkStrings(state));
        mParam[2].MySqlDbType = MySqlDbType.VarChar;

        mParam[3] = new MySqlParameter("?$county", checkStrings(county));
        mParam[3].MySqlDbType = MySqlDbType.VarChar;

        mParam[4] = new MySqlParameter("?$product", checkStrings(product));
        mParam[4].MySqlDbType = MySqlDbType.VarChar;


        int result = con.ExecuteSPScalar("sp_InsertOrder", true, mParam);
        return result;
    }
    private string checkStrings(string value)
    {
        if (value == "" || value == null || value == "&nbsp;")
        {
            return "";
        }
        else
        {
            return value;
        }
    }
    public void GetOrders(ListBox ls, string date)
    {
        string query = "";
        query = "Select order_no from tbl_record_status where pdate='" + date + "'";
        mDr = con.ExecuteSPReader(query, false, mParam);
        ls.DataSource = mDr;
        ls.DataTextField = "order_no";
        ls.DataBind();
        mDr.Close();
    }
    public string OrderStatus(string orderno, string datee)
    {
        string query = "";
        string status = "";
        query = ("select s1,sqc,k1,qc,du,review,Lock1,Rejected,Hold from tbl_record_status where order_no='" + orderno + "'");
        mDr = con.ExecuteSPReader(query, false, mParam);
        if (mDr.Read())
        {
            int s1 = Convert.ToInt16(mDr.GetString("s1"));
            int sqc = Convert.ToInt16(mDr.GetString("sqc"));
            int key = Convert.ToInt16(mDr.GetString("k1"));
            int qc = Convert.ToInt16(mDr.GetString("qc"));
            int du = Convert.ToInt16(mDr.GetString("DU"));
            int rv = Convert.ToInt16(mDr.GetString("review"));
            int lk = Convert.ToInt16(mDr.GetString("Lock1"));
            int rj = Convert.ToInt16(mDr.GetString("Rejected"));
            int hd = Convert.ToInt16(mDr.GetString("Hold"));
            if (rj == 1) { status = "Rejected"; }
            else if (lk == 1) { status = "Locked"; }
            else if (hd == 1) { status = "Hold"; }
            //   else if (key == 1 && qc == 0 && du == 0 && rv == 0) { status = "Locked in Production"; }
       
            else if (s1 == 2 && sqc == 0 && key == 0 && qc == 0 && du == 0 && rv == 0) { status = "Search Completed"; }
            else if (s1 == 2 && sqc == 2 && key == 0 && qc == 0 && du == 0 && rv == 0) { status = "Search-QC Completed"; }

            else if (key == 2 && qc == 0 && du == 0 && rv == 0) { status = "Production Completed"; }
          //  else if (key == 2 && qc == 0 && du == 0 && rv == 0) { status = "Production Completed"; }
            else if (s1 == 0 && sqc == 0 &&  key == 0 && qc == 0 && du == 0 && rv == 0) { status = "YTS"; }
            //   else if (key == 2 && qc == 1 && du == 0 && rv == 0) { status = "Locked in QC"; }
            else if (key == 2 && qc == 2 && du == 0 && rv == 0) { status = "QC Completed"; }
            else if (key == 0 && qc == 0 && du == 1 && rv == 0) { status = "DU Started"; }
            else if (key == 2 && qc == 2 && du == 2 && rv == 0) { status = "DU Completed"; }
            //  else if (key == 2 && qc == 2 && du == 0 && rv == 1) { status = "Locked In Review"; }
            else if (key == 2 && qc == 2 && du == 0 && rv == 2) { status = "Review Completed"; }
            //   else if (key == 2 && qc == 2 && du == 2 && rv == 1) { status = "Locked In Review"; }
            else if (key == 2 && qc == 2 && du == 2 && rv == 2) { status = "Review Completed"; }

        }
        mDr.Close();
        return status;
    }
    public void ResetKeyOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set k1=0,qc=0,du=0,review=0,`status`=0,K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);

    }

    public void ResetS1Order(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set S1=0,sqc=0,k1=0,qc=0,du=0,review=0,`status`=0,S1_id=null,S1_tstart=null,S1_tend=null,S1_ttaken=null,SQC_id=null,SQC_tstart=null,SQC_tend=null,SQC_ttaken=null,K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);

    }

    public void ResetSQCOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set sqc=0,k1=0,qc=0,du=0,review=0,`status`=0,SQC_id=null,SQC_tstart=null,SQC_tend=null,SQC_ttaken=null,K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);

    }



    public void ResetQcOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set qc=0,review=0,du=0,`status`=2,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void ResetDUOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set k1=0,qc=0,du=0,rv=0,`status`=0,K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void ResetReviewOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set review=0,`status`=2,Lock1=0,Rejected=0 where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void RejectOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set Rejected='1' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void RejResetOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set K1='0',Qc='0',du='0',status='0',review='0',K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Rejected='0',Lock1='0',Hold='0' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void HoldOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set Hold='0',K1='0',Qc='0',du='0',status='0',review='0',K1_id=null,k1_tstart=null,k1_tend=null,k1_ttaken=null,qc_id=null,qc_tstart=null,qc_tend=null,qc_ttaken=null,Rejected='0',Lock1='0' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void DelOrder(string Ord, string pdate)
    {
        string query = ("delete from tbl_record_status where Order_No ='" + Ord + "' and pDate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void LockOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set Lock1 ='1' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void UnLockOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set Lock1 ='0' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }
    public void PriorOrder(string Ord, string pdate)
    {
        string query = ("update tbl_record_status set hp ='1' where Order_No ='" + Ord + "' and pdate='" + pdate + "'");
        int result = con.ExecuteSPScalar(query);
    }

    #endregion

    #region Production Page

    public int UpdateOrders(string comments, string declaration)
    {
        mParam = new MySqlParameter[4];

        mParam[0] = new MySqlParameter("?$Ord_No", SessionHandler.OrderId);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$pType", SessionHandler.Rights);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;
        mParam[2] = new MySqlParameter("?$comments", comments);
        mParam[2].MySqlDbType = MySqlDbType.VarChar;
        mParam[3] = new MySqlParameter("?$declaration", declaration);
        mParam[3].MySqlDbType = MySqlDbType.VarChar;

        return con.ExecuteSPScalar("sp_UpdateUserKey_new", true, mParam);
    }


    public int Update_declaration(string declaration)
    {
        mParam = new MySqlParameter[2];

        mParam[0] = new MySqlParameter("?$Ord_No", SessionHandler.OrderId);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$declaration", declaration);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        return con.ExecuteSPScalar("sp_Update_declaration", true, mParam);
    }




    public int RejectOrders(string cmds, string pname)
    {
        string query = "";
        if (pname == "KEYING")
        {
            query = ("update tbl_record_status set k1=2,status=2,k1_tend=now(),k_comments='" + cmds + "',Rejected='1' where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname == "DU")
        {
            query = ("update tbl_record_status set k1=2,DU=2,status=2,k1_tend=now(),k_comments='" + cmds + "',Rejected='1' where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname == "QC")
        {
            query = ("update tbl_record_status set qc=2,status=2,qc_tend=now(),qc_comments='" + cmds + "',Rejected='1' where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname == "REVIEW")
        {
            query = ("update tbl_record_status set review=2,status=2,rv_tend=now(),rv_comments='" + cmds + "',Rejected='1' where id ='" + SessionHandler.OrderId + "'");
        }
        return con.ExecuteSPScalar(query);
    }

    public int HoldOrders(string cmds1, string pname1)
    {
        string query = "";
        if (pname1 == "KEYING")
        {
            query = ("update tbl_record_status set k1=2,status=2, k_comments='" + cmds1 + "',k1_tend=now(),Hold ='1',k1_ttaken=timediff(k1_tend,k1_tstart) where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname1 == "DU")
        {
            query = ("update tbl_record_status set k1=2,DU=2,status=2, k_comments='" + cmds1 + "',k1_tend=now(),Hold ='1',k1_ttaken=timediff(k1_tend,k1_tstart) where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname1 == "QC")
        {
            query = ("update tbl_record_status set qc=2,status=2, qc_comments='" + cmds1 + "',qc_tend=now(),Hold ='1',qc_ttaken=timediff(qc_tend,qc_tstart) where id ='" + SessionHandler.OrderId + "'");
        }
        else if (pname1 == "REVIEW")
        {
            query = ("update tbl_record_status set review=2,status=2,rv_comments='" + cmds1 + "',rv_tend=now(),Hold ='1',rv_ttaken=timediff(rv_tend,rv_tstart) where record_id ='" + SessionHandler.OrderId + "'");
        }
        return con.ExecuteSPScalar(query);
    }
    public int UserLogOut()
    {
        mParam = new MySqlParameter[2];

        mParam[0] = new MySqlParameter("?$Ord_No", SessionHandler.OrderId);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$Process", SessionHandler.Rights);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        //return con.ExecuteSPScalar("sp_LogoutUser", true, mParam);
        return con.ExecuteSPScalar("sp_LogoutUser_new", true, mParam);
    }
    #endregion

    #region Tracking Page

    public void GetTracking(string columnOrder, GridView myGridView, string frdate, string todate, DetailsView Dview)
    {
        DataView dataView = new DataView();
        string query = "sp_tracking";
        mParam = new MySqlParameter[2];
        mParam[0] = new MySqlParameter("?$fdate", frdate);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mParam[1] = new MySqlParameter("?$tdate", todate);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        MySqlDataReader myDr = con.ExecuteSPReader(query, true, mParam);
        dataView = ConvertDataReaderToAll(myDr);
        dattab = dataView.ToTable();
        myDr.Close();

        myGridView.DataSource = dataView;
        myGridView.DataBind();

        mParam = new MySqlParameter[2];
        mParam[0] = new MySqlParameter("$fdate", frdate);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[0].IsNullable = false;

        mParam[1] = new MySqlParameter("$tdate", todate);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;
        mParam[1].IsNullable = false;

        Dview.DataSource = con.ExecuteStoredProcedure("sp_getordercounts", true, mParam);
        Dview.DataBind();

    }
    DataView ConvertDataReaderToAll(MySqlDataReader reader)
    {
        DataView dview;
        DataTable schemaTable = new DataTable();
        schemaTable = reader.GetSchemaTable();
        DataTable DtTable = new DataTable();
        DataColumn Dtcolumn;

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.Int32");
        Dtcolumn.ColumnName = "SlNo";
        Dtcolumn.Caption = "SlNo";
        Dtcolumn.ReadOnly = true;
        Dtcolumn.Unique = true;
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "File #";
        Dtcolumn.Caption = "File #";
        DtTable.Columns.Add(Dtcolumn);


        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Date";
        Dtcolumn.Caption = "Date";
        Dtcolumn.ReadOnly = true;
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Status";
        Dtcolumn.Caption = "Status";
        DtTable.Columns.Add(Dtcolumn);


        //_________________________________________________________________________________________________________________________

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "S Name";
        Dtcolumn.Caption = "S Name";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "S Start";
        Dtcolumn.Caption = "S Start";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "S End";
        Dtcolumn.Caption = "S End";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "S TAT";
        Dtcolumn.Caption = "S TAT";
        DtTable.Columns.Add(Dtcolumn);



        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "SQC Name";
        Dtcolumn.Caption = "SQC Name";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "SQC Start";
        Dtcolumn.Caption = "SQC Start";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "SQC End";
        Dtcolumn.Caption = "SQC End";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "SQC TAT";
        Dtcolumn.Caption = "SQC TAT";
        DtTable.Columns.Add(Dtcolumn);


        //_________________________________________________________________________________________________________________________
        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Key Name";
        Dtcolumn.Caption = "Key Name";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Key Start";
        Dtcolumn.Caption = "Key Start";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Key End";
        Dtcolumn.Caption = "Key End";
        DtTable.Columns.Add(Dtcolumn);


        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Key TAT";
        Dtcolumn.Caption = "Key TAT";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "QC Name";
        Dtcolumn.Caption = "QC Name";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Qc Start";
        Dtcolumn.Caption = "Qc Start";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Qc End";
        Dtcolumn.Caption = "Qc End";
        DtTable.Columns.Add(Dtcolumn);


        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Qc TAT";
        Dtcolumn.Caption = "Qc TAT";
        DtTable.Columns.Add(Dtcolumn);


        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "S Comments";
        Dtcolumn.Caption = "S Comments";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "SQC Comments";
        Dtcolumn.Caption = "SQC Comments";
        DtTable.Columns.Add(Dtcolumn);



        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Key Comments";
        Dtcolumn.Caption = "Key Comments";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Qc Comments";
        Dtcolumn.Caption = "Qc Comments";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "TAT";
        Dtcolumn.Caption = "TAT";
        DtTable.Columns.Add(Dtcolumn);


        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Id";
        Dtcolumn.Caption = "Id";
        DtTable.Columns.Add(Dtcolumn);

        Int16 i = 1;
        string Status = "";
        string loc = "";
        string rej = "";
        string hold = "";
        ///<EmptyGridRow>
        ///if data is empty add an emptyrow and return
        ///</EmptyGridRow>
        if (reader.HasRows == false)
        {
            DataRow emptyRow = DtTable.NewRow();
            DtTable.Rows.Add(emptyRow);
            dview = new DataView(DtTable);
            return dview;
        }
        while (reader.Read())
        {
            Status = "";

            DataRow dtRow = DtTable.NewRow();
            dtRow[0] = i;

            dtRow[1] = reader["Order_no"];
            dtRow[2] = reader["pdate"];

            Status = reader["pstatus"].ToString();
            if (Status == "0000") dtRow[3] = "YTS";
            else if (Status == "1000") dtRow[3] = "Keying Started";
            else if (Status == "2000") dtRow[3] = "Key Completed";
            else if (Status == "2100") dtRow[3] = "Qc Started";
            else if (Status == "2200") dtRow[3] = "Completed";
            else if (Status == "2210") dtRow[3] = "Review Started";
            else if (Status == "2220") dtRow[3] = "Review Completed";
            else if (Status == "1001") dtRow[3] = "DU Started";
            else if (Status == "2202") dtRow[3] = "DU Completed";
            else if (Status == "2222") dtRow[3] = "Review Completed";

            loc = reader["Lock1"].ToString();
            if (loc == "1") dtRow[3] = "Locked";
            rej = reader["rejected"].ToString();
            if (rej == "1") dtRow[3] = "Rejected";
            hold = reader["Hold"].ToString();
            if (hold == "1") dtRow[3] = "Hold";


            dtRow[4] = reader["S1_id"];
            dtRow[5] = reader["S1_tstart"];
            dtRow[6] = reader["S1_tend"];
            dtRow[7] = reader["S1_ttaken"];

            dtRow[8] = reader["SQC_id"];
            dtRow[9] = reader["SQC_tstart"];
            dtRow[10] = reader["SQC_tend"];
            dtRow[11] = reader["SQC_ttaken"];


            dtRow[12] = reader["K1_id"];
            dtRow[13] = reader["k1_tstart"];
            dtRow[14] = reader["k1_tend"];
            dtRow[15] = reader["k1_ttaken"];

            dtRow[16] = reader["qc_id"];
            dtRow[17] = reader["qc_tstart"];
            dtRow[18] = reader["qc_tend"];
            dtRow[19] = reader["qc_ttaken"];
            dtRow[20] = reader["k_comments"];
            dtRow[21] = reader["qc_comments"];
            dtRow[22] = reader["s1_comments"];
            dtRow[23] = reader["sqc_comments"];

            dtRow[24] = reader["TAT"];
            dtRow[25] = reader["id"];

            i += 1;
            DtTable.Rows.Add(dtRow);
        }
        dview = new DataView(DtTable);

        return dview;
    }
    public void GetTrackingstatus(string columnOrder, GridView myGridView, string ftype, string frmdate, string todate)
    {
        DataView dataView = new DataView();
        string query = "sp_tracking_status";
        mParam = new MySqlParameter[3];
        mParam[0] = new MySqlParameter("?$ptype", ftype);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mParam[1] = new MySqlParameter("?$fdate", frmdate);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        mParam[2] = new MySqlParameter("?$tdate", todate);
        mParam[2].MySqlDbType = MySqlDbType.VarChar;

        MySqlDataReader myDr = con.ExecuteSPReader(query, true, mParam);
        dataView = ConvertDataReaderToAll(myDr);
        dattab = dataView.ToTable();
        myDr.Close();

        myGridView.DataSource = dataView;
        myGridView.DataBind();

    }

    #endregion

    #region Reports
    public void getreportsindividual(string fdate, string tdate, GridView mygridview)
    {
        DataView dataView = new DataView();
        string query = "sp_indiv";
        mParam = new MySqlParameter[2];
        mParam[0] = new MySqlParameter("?$fdate", fdate);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mParam[1] = new MySqlParameter("?$tdate", tdate);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        MySqlDataReader mdrind = con.ExecuteSPReader(query, true, mParam);
        dataView = ConvertDataReaderToIndividual(mdrind);
        dattab = dataView.ToTable();
        mdrind.Close();
        //DataSet ds = con.ExecuteQuery1(query, false, mParam);

        mygridview.DataSource = dataView;
        mygridview.DataBind();
    }

    DataView ConvertDataReaderToIndividual(MySqlDataReader mreader)
    {
        DataView dview;
        DataTable schemaTable = new DataTable();
        schemaTable = mreader.GetSchemaTable();
        DataTable DtTable = new DataTable();
        DataColumn Dtcolumn;

        //Dtcolumn = new DataColumn();
        //Dtcolumn.DataType = System.Type.GetType("System.Int32");
        //Dtcolumn.ColumnName = "SlNo";
        //Dtcolumn.Caption = "SlNo";
        //Dtcolumn.ReadOnly = true;
        //Dtcolumn.Unique = true;
        //DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "UserName";
        Dtcolumn.Caption = "UserName";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Keying";
        Dtcolumn.Caption = "Keying";
        Dtcolumn.ReadOnly = true;
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "KeyAvgTime";
        Dtcolumn.Caption = "KeyAvgTime";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "QC";
        Dtcolumn.Caption = "QC";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "QCAvgTime";
        Dtcolumn.Caption = "QCAvgTime";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Utilizationofhours";
        Dtcolumn.Caption = "Utilizationofhours";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "EffectivePRatio";
        Dtcolumn.Caption = "EffectivePRatio";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Productivity";
        Dtcolumn.Caption = "Productivity";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "Efficiency";
        Dtcolumn.Caption = "Efficiency";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "EffectiveUtilization";
        Dtcolumn.Caption = "EffectiveUtilization";
        DtTable.Columns.Add(Dtcolumn);

        Dtcolumn = new DataColumn();
        Dtcolumn.DataType = System.Type.GetType("System.String");
        Dtcolumn.ColumnName = "OverallEfficiency";
        Dtcolumn.Caption = "OverallEfficiency";
        DtTable.Columns.Add(Dtcolumn);




        ///<EmptyGridRow>
        ///if data is empty add an emptyrow and return
        ///</EmptyGridRow>
        if (mreader.HasRows == false)
        {
            DataRow emptyRow = DtTable.NewRow();
            DtTable.Rows.Add(emptyRow);
            dview = new DataView(DtTable);
            return dview;
        }
        while (mreader.Read())
        {


            DataRow dtRow = DtTable.NewRow();
            dtRow[0] = mreader["UserName"];

            dtRow[1] = mreader["Keying"];
            dtRow[2] = mreader["KeyAvgTime"];


            dtRow[3] = mreader["QC"];
            dtRow[4] = mreader["QCAvgTime"];

            dtRow[5] = mreader["Utilizationofhours"];
            dtRow[6] = mreader["EffectivePRatio"];
            dtRow[7] = mreader["Productivity"];
            dtRow[8] = mreader["Efficiency"];


            dtRow[9] = mreader["EffectiveUtilization"];
            dtRow[10] = mreader["OverallEfficiency"];

            //i += 1;
            DtTable.Rows.Add(dtRow);
        }
        dview = new DataView(DtTable);

        return dview;
    }


    public DataView CovertNewUtlizationDstoDataview(DataSet ds, string strfrmdate, string strtodate)
    {
        string struser = "";

        DataTable dtTable = new DataTable();
        DataColumn dcolumn;

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Date";
        dcolumn.Caption = "Date";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Name";
        dcolumn.Caption = "Name";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Search Count";
        dcolumn.Caption = "Search Count";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

       
        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Search Avg PT";
        dcolumn.Caption = "Search Avg PT";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "SearchQC Count";
        dcolumn.Caption = "SearchQC Count";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

       

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "SearchQC Avg PT";
        dcolumn.Caption = "SearchQC Avg PT";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Keying Count";
        dcolumn.Caption = "Keying Count";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Keying Avg PT";
        dcolumn.Caption = "Keying Avg PT";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "QC Count";
        dcolumn.Caption = "QC Count";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "QC Avg PT";
        dcolumn.Caption = "QC Avg PT";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

      /*  dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Rejection Count";
        dcolumn.Caption = "Rejection Count";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Rejection Avg PT";
        dcolumn.Caption = "Rejection Avg PT";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);   */

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Utilization of hours";
        dcolumn.Caption = "Utilization of hours";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);


        if (ds.Tables[0].Rows.Count > 0)
        {
            mParam = new MySqlParameter[3];

            mParam[0] = new MySqlParameter("?$fromdate", strfrmdate);
            mParam[0].MySqlDbType = MySqlDbType.VarChar;

            mParam[1] = new MySqlParameter("?$todate", strtodate);
            mParam[1].MySqlDbType = MySqlDbType.VarChar;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                int count = 0;
                struser = ds.Tables[0].Rows[i]["Username"].ToString();

                mParam[2] = new MySqlParameter("?$username", struser);
                mParam[2].MySqlDbType = MySqlDbType.VarChar;


                ds1 = con.ExecuteQuery1("sp_Utilzation", true, mParam);

                for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
                {
                    DataRow dtrow = dtTable.NewRow();
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        dtrow[0] = ds1.Tables[0].Rows[j]["Pdate"];
                        dtrow[1] = struser;

                        dtrow[2] = ds1.Tables[0].Rows[j]["Search Count"];
                        if (dtrow[2].ToString() != "0") dtrow[3] = ds1.Tables[0].Rows[j]["Search Avg PT"];
                        else if (dtrow[2].ToString() == "0") dtrow[3] = "00:00:00";

                        dtrow[4] = ds1.Tables[0].Rows[j]["Keying Count"];
                        if (dtrow[4].ToString() != "0") dtrow[5] = ds1.Tables[0].Rows[j]["Keying Avg PT"];
                        else if (dtrow[4].ToString() == "0") dtrow[5] = "00:00:00";

                        dtrow[6] = ds1.Tables[0].Rows[j]["Keying Count"];
                        if (dtrow[6].ToString() != "0") dtrow[7] = ds1.Tables[0].Rows[j]["Keying Avg PT"];
                        else if (dtrow[6].ToString() == "0") dtrow[7] = "00:00:00";


                        dtrow[8] = ds1.Tables[0].Rows[j]["QC Count"];
                        if (dtrow[8].ToString() != "0") dtrow[9] = ds1.Tables[0].Rows[j]["QC Avg PT"];
                        else if (dtrow[8].ToString() == "0") dtrow[9] = "00:00:00";


                      /*  dtrow[10] = ds1.Tables[0].Rows[j]["Rejected Count"];
                        if (dtrow[10].ToString() != "0") dtrow[11] = ds1.Tables[0].Rows[j]["Rejected Avg PT"];
                        else if (dtrow[10].ToString() == "0") dtrow[11] = "00:00:00";   */


                        dtrow[10] = ds1.Tables[0].Rows[j]["Utilization"];
                    }
                    dtTable.Rows.Add(dtrow);
                }
            }
            dataview = new DataView(dtTable);
        }

        return dataview;
    }

    public DataView EODReport(string strfrmdate, string strtodate)
    {
        mParam = new MySqlParameter[2];

        mParam[0] = new MySqlParameter("?$fdate", strfrmdate);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mParam[1] = new MySqlParameter("?$tdate", strtodate);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;

        ds = con.ExecuteQuery1("sp_EodReport", true, mParam);

        DataTable dtTable = new DataTable();
        DataColumn dcolumn;

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Date";
        dcolumn.Caption = "Date";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Order_no";
        dcolumn.Caption = "Order_no";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Status";
        dcolumn.Caption = "Status";
        dcolumn.ReadOnly = true;
        dcolumn.Unique = false;
        dtTable.Columns.Add(dcolumn);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DataRow dtrow = dtTable.NewRow();

                dtrow[0] = ds.Tables[0].Rows[i]["pdate"];
                dtrow[1] = ds.Tables[0].Rows[i]["Order_no"];
                dtrow[2] = "Completed";

                dtTable.Rows.Add(dtrow);

            }
            dataview = new DataView(dtTable);
        }


        return dataview;
    }

    #endregion


    #region ChangePassword
    public int ChangePassword(string usr, string Pass)
    {
        string query = "update userstatus set Password=aes_encrypt('" + Pass + "','String') where Username='" + usr + "'";
        return con.ExecuteSPNonQuery(query);
    }
    #endregion

    #region output
    public DataSet gettypevalue(string orderno, string query)
    {
        DataSet ds = new DataSet();
        mParam = new MySqlParameter[1];
        mParam[0] = new MySqlParameter("?$OrderNo", orderno);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mDa = con.ExecuteSPAdapter(query, true, mParam);
        mDa.Fill(ds);
        return ds;
    }
    public DataSet GetWriteUp(string query)
    {
        DataSet ds = new DataSet();
        mParam = new MySqlParameter[0];
        // mParam[0] = new MySqlParameter("?$OrderNo", orderno);
        // mParam[0].MySqlDbType = MySqlDbType.VarChar;

        mDa = con.ExecuteSPAdapter(query, false, mParam);
        mDa.Fill(ds);
        return ds;
    }
    #endregion


}
