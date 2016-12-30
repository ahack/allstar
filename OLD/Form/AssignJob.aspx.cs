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

public partial class Form_AssignJob : System.Web.UI.Page
{
    Connection c1 = new Connection();
    MySqlDataAdapter mDa;
    MySqlDataReader mDr;
    MySqlParameter[] mParam;
    MySqlCommand cmd;
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }
        if (!Page.IsPostBack)
        {
            Lblhead.Text = "Upload Orders";
            TogglePanel(PanelAssign);
            Panelstatus.Visible = false;
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
    }

    GlobalClass gl = new GlobalClass();

    #region Variable

    DataSet dExcel;

    #endregion

    #region TogglePanel

    private void TogglePanel(Panel sPanel)
    {
        PanelAssign.Visible =false;
        PanelReset.Visible = false;
        PanelClearData.Visible = false;
        sPanel.Visible = true;
    }
    #endregion
    
    #region SideMenu

    protected void LnkUpload_Click(object sender, EventArgs e)
    {
        TogglePanel(PanelAssign);
        Panelstatus.Visible = false;
        Lblerr.Text = "";
        Lblhead.Text = "Upload Orders";
    }
    protected void LnkReset_Click(object sender, EventArgs e)
    {
        TogglePanel(PanelReset);
        Lslorders.DataSource = null;         
        Lblhead.Text = "Reset Orders";
    }
    protected void LnkClearDatabase_Click(object sender, EventArgs e)
    {
        Panelstatus.Visible = false;
        PanelAssign.Visible = false;
        PanelReset.Visible = false;
        PanelClearData.Visible = true;
        Lblhead.Text = "Clear Database";
    }

    #endregion

    #region Trasmit
    protected void btntransmint_Click(object sender, EventArgs e)
    {
        if (Validate())
        {
            GetOrderDetails();
            Panelstatus.Visible = false;
        }
    }
    private bool Validate()
    {
        if (txtorders.Text == "") {Lblerr.Text = "Field is Blank."; return false;}
        return true;
    }
    private void GetOrderDetails()
    {
        dExcel = new DataSet();        
        string[] row;
        string[] col;
        DataTable dt = new DataTable();
        DataRow dr;         
        dt.Columns.Add("Order NO.");        
        dt.Columns.Add("DATE");
        dt.Columns.Add("state");
        dt.Columns.Add("county");
        dt.Columns.Add("product");

        string pdate = txtdate.Text ;        
        txtorders.Text = txtorders.Text.Trim('\r', '\n');
        row = txtorders.Text.Split('\n');

        foreach (string rowdata in row)
        {
            col = rowdata.Split('\t', '\r');
            dr = dt.NewRow();
            dr[0] = col[0].ToString();            
            dr[1] = txtdate.Text;
            dr[2] = col[1].ToString();
            dr[3] = col[2].ToString();
            dr[4] = col[3].ToString();
            dt.Rows.Add(dr);            
        }
        dExcel.Tables.Add(dt);
        AssignGrid.DataSource = dExcel.Tables[0];
        AssignGrid.DataBind();
        txtorders.Text = "";
        Lblerr.Text = "";
    }

    #endregion

    #region Assign Orders
    protected void btnassign_Click(object sender, EventArgs e)
    {
        try
        {
            if (Validation())
            {
                int count = 0;
                foreach (GridViewRow gr in AssignGrid.Rows)
                {
                    count += gl.InsertData_New(gr.Cells[1].Text, gr.Cells[2].Text, gr.Cells[3].Text, gr.Cells[4].Text, gr.Cells[5].Text);
                    AssignGrid.DataSource = null;
                    AssignGrid.DataBind();
                    Lblstatus.Text = count + " Order(s) uploaded successfully.";
                }
                AssignGrid.DataSource = null;
                AssignGrid.DataBind();
                Panelstatus.Visible = true;
            }
        }
        catch (Exception ex) { gl.Errorpage(ex.ToString()); }
    }
    private bool Validation()
    {
        if (AssignGrid.Rows.Count == 0) return false;
        return true;
    }
    #endregion

    #region ResetOrders
    protected void btnGo_Click(object sender, EventArgs e)
    {
        if (txtrdate.Text == "") { ErrLiteral.Text = "Date is Blank."; return; }
        gl.GetOrders(Lslorders, txtrdate.Text);
        ErrLiteral.Text = "";
    }
    protected void Lslorders_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtrdate.Text != "")
        {
            string Status = "";
            ErrLiteral.Text = "";
            lblorderstatus.Text = "";
            chks1.Checked = false;
            chksqc.Checked = false;
            chkkey.Checked = false;
            chkqc.Checked = false;
            chkreview.Checked = false;
            Status = gl.OrderStatus(Lslorders.SelectedItem.Text, txtrdate.Text);
            lblorderstatus.Text = Status;

            if (Status == "Locked") btnLock.Text = "UnLock";
            else btnLock.Text = "Lock";

            if (Status == "YTS")
            {
                chks1.Enabled = false;
                chksqc.Enabled = false;
                chkkey.Enabled = false;
                chkqc.Enabled = false;
                //chkreview.Enabled = false;
            }

            else if (Status == "Search Completed")
            {
                chks1.Enabled = true;
                chksqc.Enabled = false;
                chkkey.Enabled = false;
                chkqc.Enabled = false;
                //chkreview.Enabled = false;
            }

            else if (Status == "Search-QC Completed")
            {
                chks1.Enabled = true;
                chksqc.Enabled = true;
                chkkey.Enabled = false;
                chkqc.Enabled = false;
                //chkreview.Enabled = false;
            } 



            else if (Status == "Production Completed")
            {
                chks1.Enabled = true;
                chksqc.Enabled = true;
                chkkey.Enabled = true;
                chkqc.Enabled = false;
                //chkreview.Enabled = false;
            }
            else if (Status == "QC Completed" || Status == "DU Completed")
            {
                chks1.Enabled = true;
                chksqc.Enabled = true;
                chkkey.Enabled = true;
                chkqc.Enabled = true;
               // chkreview.Enabled = false;
            }
            else if (Status == "Review Completed")
            {
                chks1.Enabled = true;
                chksqc.Enabled = true;
                chkkey.Enabled = true;
                chkqc.Enabled = true;
               // chkreview.Enabled = true;
            }
        }
    }
    protected void btnreset_Click(object sender, EventArgs e)
    {
        if (chkkey.Checked == true)
        {
           // if (lblorderstatus.Text == "Production Completed")
                ResetOrder("RKset");
        }
        else if(chkqc.Checked == true)
        {
           // else if (lblorderstatus.Text == "QC Completed")
                ResetOrder("RQset");
        }

        else if (chks1.Checked == true)

        {
           
            ResetOrder("RS1set");
        }
        else if (chksqc.Checked == true)
        {
            
            ResetOrder("RSQCset");
        }
             


             

          //  else if (lblorderstatus.Text == "DU Completed")
        else if (chkreview.Checked == true)
        {
           // ResetOrder("RDset");
          //  else if (lblorderstatus.Text == "Review Completed")
                ResetOrder("RRset");
          
        }
        else if (lblorderstatus.Text == "Rejected")
        {
            ResetOrder("RejRset");
        }
        //  else if (lblorderstatus.Text == "Locked in Production") ResetOrder("Hold");
        else if (lblorderstatus.Text == "Hold")
        {
            ResetOrder("Hold");
        }
        //if (lblorderstatus.Text == "Production Completed") ResetOrder("RKset");
        //else if (lblorderstatus.Text == "QC Completed") ResetOrder("RQset");
        //else if (lblorderstatus.Text == "DU Completed") ResetOrder("RDset");
        //else if (lblorderstatus.Text == "Review Completed") ResetOrder("RRset");
        //else if (lblorderstatus.Text == "Rejected") ResetOrder("RejRset");
        ////  else if (lblorderstatus.Text == "Locked in Production") ResetOrder("Hold");
        //else if (lblorderstatus.Text == "Hold") ResetOrder("Hold");
        ErrLiteral.Text = "Reset Successfully..";
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        if (btnLock.Text == "Lock")
        {
            ResetOrder("Lock");
            btnLock.Text = "UnLock";
            ErrLiteral.Text = "Lock Successfully..";
        }
        else if (btnLock.Text == "UnLock")
        {
            ResetOrder("UnLock");
            btnLock.Text = "Lock";
            ErrLiteral.Text = "UnLock Successfully..";
        }
    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {
        ResetOrder("Delete"); 
    }
    protected void BtnPriority_Click(object sender, EventArgs e)
    {
        ResetOrder("HP");
        ErrLiteral.Text = "Assigned priority Successfully..";
    }
    protected void BtnReject_Click(object sender, EventArgs e)
    {
        ResetOrder("Reject");
        ErrLiteral.Text = "Rejected Successfully..";
    }
    protected void btsearch_Click(object sender, EventArgs e)
    {
        Lslorders.ClearSelection();
        string sss = txtsearch.Text.Trim();
        ListItem li = new ListItem(sss);
        if (Lslorders.Items.Contains(li) == true)
        {
            Lslorders.Items.FindByValue(sss).Selected = true;
            Lslorders_SelectedIndexChanged(sender, e);
        }
        else
            ErrLiteral.Text = "OrderNo does not found";
    }
    
    public delegate void Reset(string orderno, string pdate);
    Reset rs;
    public void ResetOrder(string func)
    {
        if (func == "RKset") rs = new Reset(gl.ResetKeyOrder);
        else if (func == "RQset") rs = new Reset(gl.ResetQcOrder);

        else if (func == "RS1set") rs = new Reset(gl.ResetS1Order);
        else if (func == "RSQCset") rs = new Reset(gl.ResetSQCOrder);

        else if (func == "RDset") rs = new Reset(gl.ResetDUOrder);
        else if (func == "RRset") rs = new Reset(gl.ResetReviewOrder);        
        else if (func == "Lock") rs = new Reset(gl.LockOrder);
        else if (func == "UnLock") rs = new Reset(gl.UnLockOrder);
        else if (func == "Delete") rs = new Reset(gl.DelOrder);
        else if (func == "HP") rs = new Reset(gl.PriorOrder);
        else if (func == "Reject") rs = new Reset(gl.RejectOrder);
        else if (func == "Hold") rs = new Reset(gl.HoldOrder);
        else if (func == "RejRset") rs = new Reset(gl.RejResetOrder);
        for (int i = 0; i < Lslorders.Items.Count; i++)
        {
            if (Lslorders.Items[i].Selected == true)
            {
                rs(Lslorders.Items[i].Text, txtrdate.Text);
            }
        }
        gl.GetOrders(Lslorders, txtrdate.Text);
    }
    #endregion      
   
    protected void cmdOK_Click(object sender, EventArgs e)
    {
        if (txtPassword.Text != "")
        {
            int result = DeleteTables(txtPassword.Text);
            if (result < 0)
            { Label5.Text = "Incorrect Password. Please try again..."; return; }
            Label5.Text = "Database cleared successfully.";
        }
        else
        {
            Label5.Text = "Please provide the password...";
        }
    }
    protected void cmdCancel_Click(object sender, EventArgs e)
    {
        txtPassword.Text = "";
        Label5.Text = "";
        txtPassword.Focus();
    }
    #region cleardatabase
    public int DeleteTables(string pwd)
    {
        mParam = new MySqlParameter[1];
        mParam[0] = new MySqlParameter("?$Pwd", pwd);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[0].IsNullable = false;
        MySqlDataReader mdra = c1.ExecuteStoredProcedure("sp_DeleteTables", true, mParam);
        if (mdra.HasRows)
        {
            if (mdra.Read())
            {
                return mdra.GetInt16(0); //Status
            }
        }
        mdra.Close();
        return -1;
    }
    //public MySqlDataReader ExecuteStoredProcedure(string Query, bool isProcedure, MySqlParameter[] myParams)
    //{
    //    getConnectionState();
    //    if (!openConnection()) { return dra; }

    //    cmd = new MySqlCommand(Query, mConnection);

    //    if (isProcedure)
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        if (myParams != null)
    //        {
    //            foreach (MySqlParameter param in myParams)
    //            {
    //                cmd.Parameters.Add(param);
    //            }
    //        }
    //    }
    //    dra = cmd.ExecuteReader();
    //    return dra;
    //}
#endregion
    protected void chkkey_CheckedChanged(object sender, EventArgs e)
    {
        if (chkkey.Checked == true)
        {
            chks1.Checked = false;
            chksqc.Checked = false;
            chkqc.Checked = false;
            chkreview.Checked = false;
        }
    }
    protected void chkqc_CheckedChanged(object sender, EventArgs e)
    {
        if (chkqc.Checked == true)
        {
            chks1.Checked = false;
            chksqc.Checked = false;
            chkkey.Checked = false;
            chkreview.Checked = false;
        }
    }
    protected void chkreview_CheckedChanged(object sender, EventArgs e)
    {
        if (chkreview.Checked == true)
        {
            chks1.Checked = false;
            chksqc.Checked = false;
            chkkey.Checked = false;
            chkqc.Checked = false;
        }
    }
    protected void chks1_CheckedChanged(object sender, EventArgs e)
    {
        if (chks1.Checked == true)
        {
            chksqc.Checked = false;
            chkkey.Checked = false;
            chkqc.Checked = false;
            chkreview.Checked = false;

        }
    }
    protected void chksqc_CheckedChanged(object sender, EventArgs e)
    {
        if (chksqc.Checked == true)
        {
            chks1.Checked = false;
            chkkey.Checked = false;
            chkqc.Checked = false;
            chkreview.Checked = false;
        }
    }
}
