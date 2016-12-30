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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

public partial class Form_Tracking : System.Web.UI.Page
{

    Connection con = new Connection();
    public string pname;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }

    //    string url = "TrackMaximize.aspx";
    //    LnkMaximize.Attributes.Add("OnClick", "window.open('" + url + "', '_blank', 'height=1500,width=2000,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes,titlebar=no');");
        if (!Page.IsPostBack)
        {
            Lblhead.Text = "Order Status";
            txtfrmdate.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Now);
            txttodate.Text = String.Format("{0:dd-MM-yyyy}", DateTime.Now);
        }
    }

    GlobalClass gl = new GlobalClass();

    #region ShowStatus
    protected void btnshow_Click(object sender, EventArgs e)
    {
        gl.GetTracking("slno", userGrid, txtfrmdate.Text, txttodate.Text,DetailsView1);
        if (userGrid.Rows[0].Cells[1].Text != "&nbsp;")
        {
            LnkUpload.Visible = true;
        }
        else
        {

            LnkUpload.Visible = false;
        }
    }
    protected void DetailsView1_ItemCommand(object sender, DetailsViewCommandEventArgs e)
    {
       SessionHandler.eName = "";
        gl.GetTrackingstatus("slno", userGrid, e.CommandName, txtfrmdate.Text, txttodate.Text);
        string url = "";
        int w = 1000;
        int h= 1000;
        string ot = e.CommandName;
        string pfdate = txtfrmdate.Text;
        string ptdate = txttodate.Text;
        url = "TrackMaximize.aspx?name=" + ot + "&Pfdate=" + pfdate + "&Ptdate=" + ptdate;
        //Response.Redirect(url);
        lnkdemo.NavigateUrl = url;
       
    }
    #endregion

    #region Export
    protected void LnkUpload_Click(object sender, EventArgs e)
    {
        if (userGrid.Rows.Count > 0) GridDecorator.Export("Tracking.xls", userGrid);      
    }
    #endregion
    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }
    protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }

    private string GetretValue(string strord, string struser)
    {
        DataSet dst = new DataSet();
        string strvalue, query = "";
        dst.Reset();
        dst.Dispose();
        if (struser != "") query = "select sec_to_time(sum(time_to_sec(timediff(outtime,intime)))) as 'Break TAT' from other_breakdetails where Order_id='" + strord + "' and Name='" + struser + "' group by Order_no";
        else query = "select sec_to_time(sum(time_to_sec(timediff(outtime,intime)))) as 'Break TAT' from other_breakdetails where Order_id='" + strord + "' group by Order_no";
        dst = con.ExecuteQuery(query);
        if (dst.Tables[0].Rows.Count > 0)
        {
            strvalue = Convert.ToString(dst.Tables[0].Rows[0]["Break TAT"]);
        }
        else strvalue = "00:00:00";

        return strvalue;                                        
    }
    protected void userGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[15].Visible = false;
    }
}
