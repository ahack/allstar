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
using System.ComponentModel;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.DirectoryServices;


public partial class Form_Settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }
        if (!Page.IsPostBack)
        {
            LoadGrid();
        }
        //ChkProduction.Attributes.Add("onclick", "javascript:Uncheck('" + ChkProduction.ClientID + "','" + Chkqc.ClientID + "','" + Chkreview.ClientID + "','" + ChkDu.ClientID + "');");
        //Chkqc.Attributes.Add("onclick", "javascript:Uncheck('" + Chkqc.ClientID + "','" + ChkProduction.ClientID + "','" + Chkreview.ClientID + "','" + ChkDu.ClientID + "');");
        //ChkDu.Attributes.Add("onclick", "javascript:Uncheck('" + ChkDu.ClientID + "','" + Chkqc.ClientID + "','" + Chkreview.ClientID + "','" + ChkProduction.ClientID + "');");
        //Chkreview.Attributes.Add("onclick", "javascript:Uncheck('" + Chkreview.ClientID + "','" + Chkqc.ClientID + "','" + ChkProduction.ClientID + "','" + ChkDu.ClientID + "');");
    }

    GlobalClass gl = new GlobalClass();

    #region TogglePanel

    private void TogglePanel(Panel sPanel)
    {
        PanelNew.Visible = false;
        PanelGrid.Visible = false;

        sPanel.Visible = true;
    }
    private void ToggleButton(Button sButton)
    {
        btnsave.Visible = false;
        btnupdate.Visible = false;

        sButton.Visible = true;
    }

    #endregion
    #region Sidement
    protected void LnkUser_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void LnkNewuser_Click(object sender, EventArgs e)
    {
        TogglePanel(PanelNew);
        ToggleButton(btnsave);
        ClearFields();
        Lblhead.Text = "New Users";
        ShowMessage("");
    }
    #endregion

    #region UserGrid
    private void LoadGrid()
    {
        try
        {
            TogglePanel(PanelGrid);
            RefreshUserGrid();
            Lblhead.Text = "User Details";
        }
        catch (Exception ex)
        {
            gl.Errorpage(ex.ToString());
        }
    }
    public void RefreshUserGrid()
    {
        gl.Get_User_Details("Username", userGrid);
    }
    protected void userGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            e.Row.Cells[7].Attributes.Add("onClick", "return confirm('Are you sure want to delete the record?');");
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[1].Text == "1") e.Row.Cells[1].Text = "Y";
            else e.Row.Cells[1].Text = "";

            if (e.Row.Cells[2].Text == "1") e.Row.Cells[2].Text = "Y";
            else e.Row.Cells[2].Text = "";

            if (e.Row.Cells[3].Text == "1") e.Row.Cells[3].Text = "Y";
            else e.Row.Cells[3].Text = "";

            if (e.Row.Cells[4].Text == "1") e.Row.Cells[4].Text = "Y";
            else e.Row.Cells[4].Text = "";

            if (e.Row.Cells[5].Text == "1") e.Row.Cells[5].Text = "Y";
            else e.Row.Cells[5].Text = "";

            if (e.Row.Cells[6].Text == "1") e.Row.Cells[6].Text = "Y";
            else e.Row.Cells[6].Text = "";

            if (e.Row.Cells[7].Text == "1") e.Row.Cells[7].Text = "Y";
            else e.Row.Cells[7].Text = "";

        }
    }
    protected void userGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            int rIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow selrow = ((GridView)e.CommandSource).Rows[rIndex];
            string usr = selrow.Cells[0].Text;

            txtusername.Text = usr;
            txtusername.ReadOnly = true;

            txtfullname.Text = gl.Fullname(usr);
            txtfullname.ReadOnly = true;

            if (selrow.Cells[1].Text == "Y") chkadmin.Checked = true;
            else chkadmin.Checked = false;

            if (selrow.Cells[2].Text == "Y") ChkSearch.Checked = true;
            else ChkProduction.Checked = false;

            if (selrow.Cells[3].Text == "Y") ChkSqc.Checked = true;
            else Chkqc.Checked = false;

            if (selrow.Cells[4].Text == "Y") ChkProduction.Checked = true;
            else ChkDu.Checked = false;

            if (selrow.Cells[5].Text == "Y") Chkqc.Checked = true;
            else Chkreview.Checked = false;

            if (selrow.Cells[6].Text == "Y") ChkDu.Checked = true;
            else Chkreview.Checked = false;

            if (selrow.Cells[7].Text == "Y") Chkreview.Checked = true;
            else Chkreview.Checked = false;


            TogglePanel(PanelNew);
            ToggleButton(btnupdate);
            Lblhead.Text = "Update Users";
        }
    }
    protected void userGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void userGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = e.RowIndex;
        GridViewRow selrow = userGrid.Rows[ID];
        gl.DeleteUser(selrow.Cells[0].Text);
        LoadGrid();
    }
    #endregion

    #region New Users
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            InsertNewuser();
        }
        catch (Exception ex)
        {
            gl.Errorpage(ex.ToString());
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            UpdateUser();
        }
        catch (Exception ex)
        {
            gl.Errorpage(ex.ToString());
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    private void InsertNewuser()
    {
        if (!Validation()) { return; }

        //if (!CheckADName(txtusername.Text)) { ShowMessage("UserName Not avaliable in String Domain..!"); return; }
        string User = gl.CheckUsername(txtusername.Text.Trim());
        if (User != "") { ShowMessage("This User Already Exist..!"); return; }

        int Ad = 0, s1 = 0, sqc = 0, prod = 0, qc = 0, du = 0, rv = 0;

        if (chkadmin.Checked) { Ad = 1; }
        if (ChkSearch.Checked) { s1 = 1; }
        if (ChkSqc.Checked) { sqc = 1; }
        if (ChkProduction.Checked) { prod = 1; }
        if (Chkqc.Checked) { qc = 1; }
        if (ChkDu.Checked) { du = 1; }
        if (Chkreview.Checked) { rv = 1; }

        gl.InsertUser(txtfullname.Text, txtusername.Text.ToLower(), Ad, s1, sqc, prod, qc, du, rv);

        ClearFields();
        ShowMessage("Username Added Successfully..!");
    }
    private void UpdateUser()
    {
        if (!Validation()) { return; }

        int Ad = 0, s1 = 0, sqc = 0, prod = 0, qc = 0, du = 0, rv = 0;

        if (chkadmin.Checked) { Ad = 1; }
        if (ChkSearch.Checked) { s1 = 1; }
        if (ChkSqc.Checked) { sqc = 1; }
        if (ChkProduction.Checked) { prod = 1; }
        if (Chkqc.Checked) { qc = 1; }
        if (ChkDu.Checked) { du = 1; }
        if (Chkreview.Checked) { rv = 1; }

        gl.UpdateUser(txtusername.Text.ToLower(), Ad, s1, sqc, prod, qc, du, rv);

        ClearFields();
        LoadGrid();
    }
    private bool Validation()
    {
        if (ChkSearch.Checked == false && ChkSqc.Checked == false && ChkProduction.Checked == false && Chkqc.Checked == false && Chkreview.Checked == false && ChkDu.Checked == false) { Lbluser.Text = "Please Select Production Type (Key or Qc or Review)..!"; return false; }
        return true;
    }
    private bool CheckADName(string UserName)
    {

        DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://stringinfo");
        foreach (DirectoryEntry de in directoryEntry.Children)
        {
            if (de.SchemaClassName == "User")
            {
                if (UserName == de.Name)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void ClearFields()
    {
        txtfullname.Text = "";
        txtusername.Text = "";
        txtusername.ReadOnly = false;
        txtfullname.ReadOnly = false;
        chkadmin.Checked = false;
        ChkProduction.Checked = false;
        Chkqc.Checked = false;
        Chkreview.Checked = false;
        ChkDu.Checked = false;
    }
    private void ShowMessage(string msg)
    {
        Lbluser.Text = msg;
    }
    #endregion
    protected void ChkDu_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDu.Checked == true)
        {
            ChkSearch.Checked = false;
            ChkSqc.Checked = false;
            ChkProduction.Checked = false;
            Chkqc.Checked = false;
            ChkProduction.Checked = false;
            Chkreview.Checked = false;
        }
    }
    protected void ChkProduction_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkProduction.Checked == true)
        {
            ChkDu.Checked = false;
            Chkreview.Checked = false;
            Chkqc.Checked = false;
            ChkSearch.Checked = false;
            ChkSqc.Checked = false;
        }
    }
    protected void chkadmin_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void Chkreview_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkreview.Checked == true)
        {
            ChkSearch.Checked = false;
            ChkSqc.Checked = false;
            ChkProduction.Checked = false;
            Chkqc.Checked = false;
            ChkDu.Checked = false;
        }
    }
    protected void Chkqc_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkqc.Checked == true)
        {
            ChkSearch.Checked = false;
            ChkSqc.Checked = false;
            ChkProduction.Checked = false;
            ChkDu.Checked = false;
            Chkreview.Checked = false;
        }
    }

    protected void ChkSearch_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkSearch.Checked == true)
        {

            ChkSqc.Checked = false;
            ChkProduction.Checked = false;
            Chkqc.Checked = false;
            ChkDu.Checked = false;
            Chkreview.Checked = false;
        }
    }
    protected void ChkSqc_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkSqc.Checked == true)
        {
            ChkSearch.Checked = false;
            ChkProduction.Checked = false;
            Chkqc.Checked = false;
            ChkDu.Checked = false;
            Chkreview.Checked = false;

        }

    }
}
