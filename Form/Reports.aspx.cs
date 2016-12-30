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
using System.IO;
using System.Globalization;

public partial class Form_Reports : System.Web.UI.Page
{
    GlobalClass gl = new GlobalClass();
    Connection db = new Connection();

    DataSet ds = new DataSet();
    DataView dataview;
    string strfrmdate, strtodate = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dt = DateTime.Now;

            ds.Dispose();
            ds.Reset();
            ds = LoadUsername();
            BindUsername(ds);
            lblgridname.Text = "";
            txtfrmdate.Text = dt.ToString("dd-MM-yyyy");
            txttodate.Text = dt.ToString("dd-MM-yyyy");
            userGridreports.Visible = true;
        }
    }

    private DataSet LoadUsername()
    {
        ds.Dispose();
        ds.Reset();
        string strquery = "select Username from userstatus order by Username";
        ds = db.ExecuteQuery(strquery);

        return ds;
    }

    private void BindUsername(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlusername.DataSource = ds;
            ddlusername.DataTextField = "Username";
            ddlusername.DataBind();
            ddlusername.Items.Insert(0, "ALL");
        }
    }

    protected void LnkUpload_Click(object sender, EventArgs e)
    {
        if (userGridreports.Rows.Count > 0)
        {
            if (lblgridname.Text == "EOD Report") GridDecorator.Export("EodReport.xls", userGridreports);
            else if (lblgridname.Text == "Individual Report") GridDecorator.Export("IndividualReport.xls", userGridreports);
            else if (lblgridname.Text == "Break Time Report") GridDecorator.Export("BreakReport.xls", userGridreports);
        }
    }
    protected void LnkEod_Click(object sender, EventArgs e)
    {
        lblgridname.Text = "EOD Report";
        try
        {
            strfrmdate = txtfrmdate.Text;
            strtodate = txttodate.Text;

            if (strfrmdate != "" && strtodate != "")
            {
                ds.Dispose();
                ds.Reset();
                dataview = gl.EODReport(strfrmdate, strtodate);
                userGridreports.DataSource = dataview;
                userGridreports.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void LnkIndividual_Click(object sender, EventArgs e)
    {
        lblgridname.Text = "Individual Report";
        try
        {
            strfrmdate = txtfrmdate.Text;
            strtodate = txttodate.Text;

            if (strfrmdate != "" && strtodate != "")
            {
                if (ddlusername.SelectedItem.Text == "ALL") ds = LoadUsername();
                else
                {
                    ds.Dispose();
                    ds.Reset();
                    ds = GetDataSet();
                }
                ShowGridUtlization(ds, strfrmdate, strtodate);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Lnkotherbreaktime_Click(object sender, EventArgs e)
    {
        lblgridname.Text = "Break Time Report";
        BreakTimeReport("other_breakdetails");
              
    }

    private DataSet GetDataSet()
    {
        DataSet dsset = new DataSet();
        DataTable dtTable = new DataTable();
        DataColumn dcolumn;
        dsset.Dispose();
        dsset.Reset();

        dcolumn = new DataColumn();
        dcolumn.DataType = System.Type.GetType("System.String");
        dcolumn.ColumnName = "Username";
        dcolumn.Caption = "Username";
        dcolumn.ReadOnly = true;
        dtTable.Columns.Add(dcolumn);

        DataRow dtrow = dtTable.NewRow();
        dtrow[0] = ddlusername.SelectedItem.Text.ToString();

        dtTable.Rows.Add(dtrow);
        dsset.Tables.Add(dtTable);

        return dsset;

    }

    public void ShowGridUtlization(DataSet ds, string strfrmdate, string strtodate)
    {
        dataview = new DataView();
        dataview = gl.CovertNewUtlizationDstoDataview(ds, strfrmdate, strtodate);
        userGridreports.DataSource = dataview;
        userGridreports.DataBind();
    }

    private void BreakTimeReport(string tblname)
    {
        try
        {
            DateTime dtt = new DateTime();
            string query, strusrname = "";

            strfrmdate = txtfrmdate.Text;
            strtodate = txttodate.Text;           

            strusrname = ddlusername.SelectedItem.Text;
            if (strfrmdate != "" && strtodate != "")
            {
                ds.Dispose();
                ds.Reset();               
                if (strusrname == "ALL") query = "Select Name,pdate as Date,time(Intime) as 'In Time',time(Outtime) as 'Out Time',Tottime as 'Total Time',Comments from other_breakdetails where Pdate between'" + strfrmdate + "' and '" + strtodate + "'";
                else query = "Select Name,pdate as Date,time(Intime) as 'In Time',time(Outtime) as 'Out Time',Tottime as 'Total Time',Comments from other_breakdetails where Pdate between'" + strfrmdate + "' and '" + strtodate + "' and Name='" + strusrname + "'";
                ds = db.ExecuteQuery(query);
                ShowGridBreaktime(ds);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void ShowGridBreaktime(DataSet ds)
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            userGridreports.DataSource = ds;
            userGridreports.DataBind();
        }
        else
        {
            userGridreports.DataSource = null;
            userGridreports.DataBind();
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        //gl.getreportsindividual(txtfrmdate.Text, txttodate.Text, userGridreports);
    }
}
