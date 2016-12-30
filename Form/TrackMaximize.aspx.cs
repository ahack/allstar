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
using System.Text;
using System.Runtime.InteropServices;

public partial class Form_TrackMaximize : System.Web.UI.Page
{
    GlobalClass gl = new GlobalClass();
    protected void Page_Load(object sender, EventArgs e)
    {
     
      

      
        string pname = "";
       
            userGrid1.Visible = true;
              pname = Request.QueryString["name"];
            string pfdate=Request.QueryString["Pfdate"];
            string ptdate = Request.QueryString["Ptdate"];
           
           
            gl.GetTrackingstatus("slno", userGrid1, pname, pfdate, ptdate);
            if (!Page.IsPostBack)
                
            {
             }
            
    }
   
    #region Export
    protected void LnkUpload_Click(object sender, EventArgs e)
    {
        ExportToExcel(GlobalClass.dattab);
    }
    public void ExportToExcel(DataTable dt)
    {
        if (dt.Rows.Count > 0)
        {
            string filename = "Tracking.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            dgGrid.DataSource = dt;
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            this.EnableViewState = false;
            Response.Write(tw.ToString());
            Response.Flush();
            Response.End();
        }
    }
    #endregion
    //protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    //{

    //}
   
    protected void lnkminimize_Click(object sender, EventArgs e)
    {
        Response.Redirect("Tracking.aspx");
    }
}
