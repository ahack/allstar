using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.IO;
using System.Globalization;
//using Word = Microsoft.Office.Interop.Word;
//using Microsoft.Office.Core;
//using Microsoft.Office.Interop.Word;
using System.Windows;


public partial class Form_productionology : System.Web.UI.Page
{
    Connection cons = new Connection();
    GlobalClass gls = new GlobalClass();
    global gl = new global();
    DBConnection objconnection = new DBConnection();
    DataTable dt = new DataTable();
    DataSet dstaxass = new DataSet();
    static string ID;
    string ordno = string.Empty;
    string types = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }
        if (!Page.IsPostBack)
        {
            txt_orderno.ReadOnly = true;

            AllotProcess();
            gettypes();
            drp_mortgage_SelectedIndexChanged(sender, e);
            drp_judgement_SelectedIndexChanged(sender, e);
            drp_others_SelectedIndexChanged(sender, e);
            get_clientinfo();
            get_taxass();
            grid_deed_show();
            grid_mortgage_show();
            grid_others_show();
            getgridpreviewshow();
        }
    }
    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        int index = Int32.Parse(e.Item.Value);

        MultiView1.ActiveViewIndex = index;
        if (index == 6)
        {
            getgridpreviewshow();
        }
    }

    protected void gettypes()
    {
        DataSet ds_mortgage = new DataSet();
        DataSet ds_judg = new DataSet();
        DataSet ds_others = new DataSet();

        ds_mortgage = objconnection.ExecuteDataset("select mortgage from tbl_mortgage_types");
        ds_judg = objconnection.ExecuteDataset("select Judgment from tbl_judgment_types");
        ds_others = objconnection.ExecuteDataset("select others from tbl_others_types");

        drp_mortgage.DataSource = ds_mortgage;
        drp_mortgage.DataValueField = "mortgage";
        drp_mortgage.DataTextField = "mortgage";
        drp_mortgage.DataBind();

        drp_judgement.DataSource = ds_judg;
        drp_judgement.DataValueField = "Judgment";
        drp_judgement.DataTextField = "Judgment";
        drp_judgement.DataBind();

        drp_others.DataSource = ds_others;
        drp_others.DataValueField = "others";
        drp_others.DataTextField = "others";
        drp_others.DataBind();
    }

    #region selecttable

    protected void judg_selecttable()
    {
        DataSet ds_tblno_judge = new DataSet();
        ds_tblno_judge = objconnection.ExecuteDataset("select tableno from tbl_judgment_types where Judgment='" + drp_judgement.Text + "'");
        string str = ds_tblno_judge.Tables[0].Rows[0][0].ToString();
        int res1 = Convert.ToInt32(str);
        int res = grd__judgement.Rows.Count;
        int res2 = res + res1;
        txt_judg_tableno.Text = res2.ToString();


    }


    protected void other_selecttable()
    {
        DataSet ds_tblno_other = new DataSet();
        ds_tblno_other = objconnection.ExecuteDataset("select tableno from tbl_others_types where Others='" + drp_others.Text + "'");
        string str = ds_tblno_other.Tables[0].Rows[0][0].ToString();
        int res1 = Convert.ToInt32(str);
        int res = grd__judgement.Rows.Count;
        int res2 = res + res1;
        txt_other_tableno.Text = res2.ToString();

    }


    protected void mortgage_selecttable()
    {
        DataSet ds_tblno_mortgage = new DataSet();
        ds_tblno_mortgage = objconnection.ExecuteDataset("select tableno from tbl_mortgage_types where Mortgage='" + drp_mortgage.Text + "'");
        string str = ds_tblno_mortgage.Tables[0].Rows[0][0].ToString();
        int res1 = Convert.ToInt32(str);
        int res = grd_mortgage.Rows.Count;
        int res2 = res + res1;
        txt_mrg_tableno.Text = res2.ToString();


    }

    protected void deed_selecttable()
    {

        int res1 = 7;
        int res = GridView1.Rows.Count;
        int res2 = res + res1;
        txt_deed_tableno.Text = res2.ToString();

    }




    #endregion selecttable
    #region dropdownlist
    protected void drp_mortgage_SelectedIndexChanged(object sender, EventArgs e)
    {
        mortgage_clear();
        txt_mrg_type.Text = drp_mortgage.SelectedItem.Text;
        if (drp_mortgage.SelectedItem.Text == "AFFIDAVIT OF LOST ASSIGNMENT" || drp_mortgage.SelectedItem.Text == "ASSIGNMENT")
        {

            lbl_mrg_assignee.Visible = true;
            txt_mrg_assignee.Visible = true;

            lbl_mrg_assignor.Visible = true;
            txt_mrg_assignor.Visible = true;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = false;
            txt_mrg_amount.Visible = false;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = false;
            txt_mrg_grantor.Visible = false;

            lbl_mrg_payableto.Visible = false;
            txt_mrg_payableto.Visible = false;

            lbl_mrg_trustee.Visible = false;
            txt_mrg_trustee.Visible = false;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }

        else if (drp_mortgage.SelectedItem.Text == "APPOINTMENT OF SUBSTITUTE TRUSTEE")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = false;
            txt_mrg_amount.Visible = false;

            lbl_mrg_appointed.Visible = true;
            txt_mrg_appointed.Visible = true;

            lbl_mrg_exeby.Visible = true;
            txt_mrg_exeby.Visible = true;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = false;
            txt_mrg_grantor.Visible = false;

            lbl_mrg_payableto.Visible = false;
            txt_mrg_payableto.Visible = false;

            lbl_mrg_trustee.Visible = false;
            txt_mrg_trustee.Visible = false;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }
        else if (drp_mortgage.SelectedItem.Text == "ASSIGNMENT OF RENTS")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = false;
            txt_mrg_amount.Visible = false;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = true;
            txt_mrg_lender.Visible = true;

            lbl_mrg_grantor.Visible = true;
            txt_mrg_grantor.Visible = true;

            lbl_mrg_payableto.Visible = false;
            txt_mrg_payableto.Visible = false;

            lbl_mrg_trustee.Visible = false;
            txt_mrg_trustee.Visible = false;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }
        else if (drp_mortgage.SelectedItem.Text == "DEED OF TRUST OR MORTGAGE")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = true;
            txt_mrg_amount.Visible = true;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = true;
            txt_mrg_grantor.Visible = true;

            lbl_mrg_payableto.Visible = true;
            txt_mrg_payableto.Visible = true;

            lbl_mrg_trustee.Visible = true;
            txt_mrg_trustee.Visible = true;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }
        else if (drp_mortgage.SelectedItem.Text == "UCC FINANCING STATEMENT")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = false;
            txt_mrg_amount.Visible = false;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = false;
            txt_mrg_grantor.Visible = false;

            lbl_mrg_payableto.Visible = false;
            txt_mrg_payableto.Visible = false;

            lbl_mrg_trustee.Visible = false;
            txt_mrg_trustee.Visible = false;

            lbl_mrg_secparty.Visible = true;
            txt_mrg_secparty.Visible = true;

            lbl_mrg_debtor.Visible = true;
            txt_mrg_debtor.Visible = true;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }
        else if (drp_mortgage.SelectedItem.Text == "LOAN MODIFICATION")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = false;
            txt_mrg_amount.Visible = false;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = false;
            txt_mrg_grantor.Visible = false;

            lbl_mrg_payableto.Visible = false;
            txt_mrg_payableto.Visible = false;

            lbl_mrg_trustee.Visible = false;
            txt_mrg_trustee.Visible = false;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = true;
            txt_mrg_byandbeet.Visible = true;

        }
        else if (drp_mortgage.SelectedItem.Text == "SUBORDINATE DEED OF TRUST")
        {
            lbl_mrg_assignee.Visible = false;
            txt_mrg_assignee.Visible = false;

            lbl_mrg_assignor.Visible = false;
            txt_mrg_assignor.Visible = false;

            lbl_mrg_notes.Visible = true;
            txt_mrg_notes.Visible = true;

            lbl_mrg_amount.Visible = true;
            txt_mrg_amount.Visible = true;

            lbl_mrg_appointed.Visible = false;
            txt_mrg_appointed.Visible = false;

            lbl_mrg_exeby.Visible = false;
            txt_mrg_exeby.Visible = false;

            lbl_mrg_lender.Visible = false;
            txt_mrg_lender.Visible = false;

            lbl_mrg_grantor.Visible = true;
            txt_mrg_grantor.Visible = true;

            lbl_mrg_payableto.Visible = true;
            txt_mrg_payableto.Visible = true;

            lbl_mrg_trustee.Visible = true;
            txt_mrg_trustee.Visible = true;

            lbl_mrg_secparty.Visible = false;
            txt_mrg_secparty.Visible = false;

            lbl_mrg_debtor.Visible = false;
            txt_mrg_debtor.Visible = false;

            lbl_mrg_byandbeet.Visible = false;
            txt_mrg_byandbeet.Visible = false;

        }
        grid_mortgage_show();
        mortgage_selecttable();


    }
    protected void drp_judgement_SelectedIndexChanged(object sender, EventArgs e)
    {
        judgement_clear();
        txt_judg_type.Text = drp_judgement.SelectedItem.Text;
        if (drp_judgement.SelectedItem.Value == "ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION" || drp_judgement.SelectedItem.Text == "STATE TAX LIEN")
        {
            lbl_judg_address.Visible = true;
            txt_judg_address.Visible = true;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;



            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;


            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;

            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;


            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = true;
            txt_judg_taxpayer.Visible = true;

            lbl_judg_taxpayerid.Visible = true;
            txt_judg_taxpayerid.Visible = true;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;


        }
        else if (drp_judgement.SelectedItem.Text == "ABSTRACT OF JUDGMENT")
        {

            lbl_judg_address.Visible = true;
            txt_judg_address.Visible = true;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = true;
            txt_judg_atty.Visible = true;


            lbl_judg_cause.Visible = true;
            txt_judg_cause.Visible = true;


            lbl_judg_cost.Visible = true;
            txt_judg_cost.Visible = true;


            lbl_judg_dated.Visible = true;
            txt_judg_dated.Visible = true;


            lbl_judg_defendant.Visible = true;
            txt_judg_defendant.Visible = true;

            lbl_judg_filed.Visible = true;
            txt_judg_filed.Visible = true;


            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;

            lbl_judg_inst.Visible = true;
            txt_judg_inst.Visible = true;


            lbl_judg_int.Visible = true;
            txt_judg_int.Visible = true;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;


            lbl_judg_plaintiff.Visible = true;
            txt_judg_plaintiff.Visible = true;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;


        }
        else if (drp_judgement.SelectedItem.Text == "AFFIDAVIT TO FIX LIEN")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;


            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;


            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = true;
            txt_judg_grantor.Visible = true;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = true;
            txt_judg_owner.Visible = true;



            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }
        else if (drp_judgement.SelectedItem.Text == "FEDERAL TAX LIEN")
        {
            lbl_judg_address.Visible = true;
            txt_judg_address.Visible = true;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;


            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = true;
            txt_judg_taxpayer.Visible = true;

            lbl_judg_taxpayerid.Visible = true;
            txt_judg_taxpayerid.Visible = true;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;


        }



        else if (drp_judgement.SelectedItem.Text == "NOTICE OF ASSESSMENT LIEN")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = false;
            txt_judg_amount.Visible = false;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = true;
            txt_judg_grantor.Visible = true;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = true;
            txt_judg_notes.Visible = true;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = true;
            txt_judg_owner.Visible = true;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }


        else if (drp_judgement.SelectedItem.Text == "LIEN CLAIM AFFIDAVIT")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = true;
            txt_judg_grantor.Visible = true;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = true;
            txt_judg_notes.Visible = true;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = true;
            txt_judg_owner.Visible = true;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }

        else if (drp_judgement.SelectedItem.Text == "AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = true;
            txt_judg_grantor.Visible = true;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = true;
            txt_judg_owner.Visible = true;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }





        else if (drp_judgement.SelectedItem.Text == "NOTICE OF CHILD SUPPORT LIEN")
        {
            lbl_judg_address.Visible = true;
            txt_judg_address.Visible = true;


            lbl_judg_amount.Visible = true;
            txt_judg_amount.Visible = true;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = true;
            txt_judg_obligor.Visible = true;

            lbl_judg_obligee.Visible = true;
            txt_judg_obligee.Visible = true;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = true;
            txt_judg_ssn.Visible = true;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = true;
            txt_judg_tribunal.Visible = true;

        }


        else if (drp_judgement.SelectedItem.Text == "NOTICE OF FORECLOSURE")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = false;
            txt_judg_amount.Visible = false;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = true;
            txt_judg_grantor.Visible = true;

            lbl_judg_grantee.Visible = true;
            txt_judg_grantee.Visible = true;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = true;
            txt_judg_notes.Visible = true;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }

        else if (drp_judgement.SelectedItem.Text == "NOTICE OF TRUSTEE SALE")
        {
            lbl_judg_address.Visible = false;
            txt_judg_address.Visible = false;


            lbl_judg_amount.Visible = false;
            txt_judg_amount.Visible = false;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = false;
            txt_judg_cause.Visible = false;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = false;
            txt_judg_defendant.Visible = false;

            lbl_judg_from.Visible = true;
            txt_judg_from.Visible = true;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = true;
            txt_judg_notes.Visible = true;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;

            lbl_judg_plaintiff.Visible = false;
            txt_judg_plaintiff.Visible = false;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = true;
            txt_judg_to.Visible = true;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }

        else if (drp_judgement.SelectedItem.Text == "ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE")
        {
            lbl_judg_address.Visible = true;
            txt_judg_address.Visible = true;


            lbl_judg_amount.Visible = false;
            txt_judg_amount.Visible = false;


            lbl_judg_atty.Visible = false;
            txt_judg_atty.Visible = false;


            lbl_judg_cause.Visible = true;
            txt_judg_cause.Visible = true;


            lbl_judg_cost.Visible = false;
            txt_judg_cost.Visible = false;

            lbl_judg_defendant.Visible = true;
            txt_judg_defendant.Visible = true;

            lbl_judg_from.Visible = false;
            txt_judg_from.Visible = false;

            lbl_judg_grantor.Visible = false;
            txt_judg_grantor.Visible = false;

            lbl_judg_grantee.Visible = false;
            txt_judg_grantee.Visible = false;


            lbl_judg_int.Visible = false;
            txt_judg_int.Visible = false;

            lbl_judg_notes.Visible = false;
            txt_judg_notes.Visible = false;


            lbl_judg_obligor.Visible = false;
            txt_judg_obligor.Visible = false;

            lbl_judg_obligee.Visible = false;
            txt_judg_obligee.Visible = false;


            lbl_judg_owner.Visible = false;
            txt_judg_owner.Visible = false;

            lbl_judg_plaintiff.Visible = true;
            txt_judg_plaintiff.Visible = true;


            lbl_judg_ssn.Visible = false;
            txt_judg_ssn.Visible = false;


            lbl_judg_taxpayer.Visible = false;
            txt_judg_taxpayer.Visible = false;

            lbl_judg_taxpayerid.Visible = false;
            txt_judg_taxpayerid.Visible = false;


            lbl_judg_to.Visible = false;
            txt_judg_to.Visible = false;

            lbl_judg_tribunal.Visible = false;
            txt_judg_tribunal.Visible = false;

        }
        grid_judg_show();
        judg_selecttable();

    }
    protected void drp_others_SelectedIndexChanged(object sender, EventArgs e)
    {
        others_clear();

        txt_other_type.Text = drp_others.SelectedItem.Text;
        if (drp_others.SelectedItem.Text == "AFFIDAVIT AND AGREEMENT")
        {

            lbl_other_cause.Visible = false;
            txt_other_cause.Visible = false;

            lbl_other_to.Visible = false;
            txt_other_to.Visible = false;

            lbl_other_grantee.Visible = true;
            txt_other_grantee.Visible = true;

            lbl_other_grantor.Visible = true;
            txt_other_grantor.Visible = true;

            lbl_other_manufacturer.Visible = false;
            txt_other_manufacturer.Visible = false;

            lbl_other_owner.Visible = false;
            txt_other_owner.Visible = false;

            lbl_other_petitioner.Visible = false;
            txt_other_petitioner.Visible = false;

            lbl_other_re.Visible = false;
            txt_other_re.Visible = false;

            lbl_other_respondent.Visible = false;
            txt_other_respondent.Visible = false;

            lbl_others_notes.Visible = true;
            txt_others_notes.Visible = true;

            lbl_others_dated.Visible = true;
            txt_others_dated.Visible = true;

            lbl_others_inst.Visible = true;
            txt_others_inst.Visible = true;

            lbl_others_pg.Visible = true;
            txt_others_pg.Visible = true;

            lbl_others_vol.Visible = true;
            txt_others_vol.Visible = true;

        }

        if (drp_others.SelectedItem.Text == "DIVORCE NOT EXAMINED")
        {

            lbl_other_cause.Visible = true;
            txt_other_cause.Visible = true;

            lbl_other_to.Visible = false;
            txt_other_to.Visible = false;

            lbl_other_grantee.Visible = false;
            txt_other_grantee.Visible = false;

            lbl_other_grantor.Visible = false;
            txt_other_grantor.Visible = false;

            lbl_other_manufacturer.Visible = false;
            txt_other_manufacturer.Visible = false;

            lbl_other_owner.Visible = false;
            txt_other_owner.Visible = false;

            lbl_other_petitioner.Visible = true;
            txt_other_petitioner.Visible = true;

            lbl_other_re.Visible = false;
            txt_other_re.Visible = false;

            lbl_other_respondent.Visible = true;
            txt_other_respondent.Visible = true;

            lbl_others_notes.Visible = false;
            txt_others_notes.Visible = false;

            lbl_others_dated.Visible = false;
            txt_others_dated.Visible = false;

            lbl_others_inst.Visible = false;
            txt_others_inst.Visible = false;

            lbl_others_pg.Visible = false;
            txt_others_pg.Visible = false;

            lbl_others_vol.Visible = false;
            txt_others_vol.Visible = false;

            lbl_others_filed.Visible = true;
            txt_others_filed.Visible = true;



        }

        if (drp_others.SelectedItem.Text == "GENERAL POWER OF ATTORNEY" || drp_others.SelectedItem.Text == "REINSTATEMENT AGREEMENT")
        {

            lbl_other_cause.Visible = false;
            txt_other_cause.Visible = false;

            lbl_other_to.Visible = false;
            txt_other_to.Visible = false;

            lbl_other_grantee.Visible = false;
            txt_other_grantee.Visible = false;

            lbl_other_grantor.Visible = false;
            txt_other_grantor.Visible = false;

            lbl_other_manufacturer.Visible = false;
            txt_other_manufacturer.Visible = false;

            lbl_other_owner.Visible = false;
            txt_other_owner.Visible = false;

            lbl_other_petitioner.Visible = false;
            txt_other_petitioner.Visible = false;

            lbl_other_re.Visible = false;
            txt_other_re.Visible = false;

            lbl_other_respondent.Visible = false;
            txt_other_respondent.Visible = false;

            lbl_others_notes.Visible = true;
            txt_others_notes.Visible = true;

            lbl_others_dated.Visible = true;
            txt_others_dated.Visible = true;

            lbl_others_inst.Visible = true;
            txt_others_inst.Visible = true;

            lbl_others_pg.Visible = true;
            txt_others_pg.Visible = true;

            lbl_others_vol.Visible = true;
            txt_others_vol.Visible = true;

        }

        if (drp_others.SelectedItem.Text == "PROBATE - NOT EXAMINED")
        {

            lbl_other_cause.Visible = true;
            txt_other_cause.Visible = true;

            lbl_other_to.Visible = false;
            txt_other_to.Visible = false;

            lbl_other_grantee.Visible = false;
            txt_other_grantee.Visible = false;

            lbl_other_grantor.Visible = false;
            txt_other_grantor.Visible = false;

            lbl_other_manufacturer.Visible = false;
            txt_other_manufacturer.Visible = false;

            lbl_other_owner.Visible = false;
            txt_other_owner.Visible = false;

            lbl_other_petitioner.Visible = false;
            txt_other_petitioner.Visible = false;

            lbl_other_re.Visible = true;
            txt_other_re.Visible = true;

            lbl_other_respondent.Visible = false;
            txt_other_respondent.Visible = false;

            lbl_others_notes.Visible = false;
            txt_others_notes.Visible = false;

            lbl_others_dated.Visible = false;
            txt_others_dated.Visible = false;

            lbl_others_inst.Visible = false;
            txt_others_inst.Visible = false;

            lbl_others_pg.Visible = false;
            txt_others_pg.Visible = false;

            lbl_others_vol.Visible = false;
            txt_others_vol.Visible = false;

        }

        if (drp_others.SelectedItem.Text == "STATEMENT OF OWNERSHIP AND LOCATION")
        {

            lbl_other_cause.Visible = false;
            txt_other_cause.Visible = false;

            lbl_other_to.Visible = false;
            txt_other_to.Visible = false;

            lbl_other_grantee.Visible = false;
            txt_other_grantee.Visible = false;

            lbl_other_grantor.Visible = false;
            txt_other_grantor.Visible = false;

            lbl_other_manufacturer.Visible = true;
            txt_other_manufacturer.Visible = true;

            lbl_other_owner.Visible = true;
            txt_other_owner.Visible = true;

            lbl_other_petitioner.Visible = false;
            txt_other_petitioner.Visible = false;

            lbl_other_re.Visible = false;
            txt_other_re.Visible = false;

            lbl_other_respondent.Visible = false;
            txt_other_respondent.Visible = false;

            lbl_others_notes.Visible = true;
            txt_others_notes.Visible = true;

            lbl_others_dated.Visible = true;
            txt_others_dated.Visible = true;

            lbl_others_inst.Visible = true;
            txt_others_inst.Visible = true;

            lbl_others_pg.Visible = true;
            txt_others_pg.Visible = true;

            lbl_others_vol.Visible = true;
            txt_others_vol.Visible = true;

        }

        if (drp_others.SelectedItem.Text == "SPECIAL POWER OF ATTORNEY")
        {

            lbl_other_cause.Visible = false;
            txt_other_cause.Visible = false;

            lbl_other_to.Visible = true;
            txt_other_to.Visible = true;

            lbl_other_grantee.Visible = false;
            txt_other_grantee.Visible = false;

            lbl_other_grantor.Visible = true;
            txt_other_grantor.Visible = true;

            lbl_other_manufacturer.Visible = false;
            txt_other_manufacturer.Visible = false;

            lbl_other_owner.Visible = false;
            txt_other_owner.Visible = false;

            lbl_other_petitioner.Visible = false;
            txt_other_petitioner.Visible = false;

            lbl_other_re.Visible = false;
            txt_other_re.Visible = false;

            lbl_other_respondent.Visible = false;
            txt_other_respondent.Visible = false;

            lbl_others_notes.Visible = true;
            txt_others_notes.Visible = true;

            lbl_others_dated.Visible = true;
            txt_others_dated.Visible = true;

            lbl_others_inst.Visible = true;
            txt_others_inst.Visible = true;

            lbl_others_pg.Visible = true;
            txt_others_pg.Visible = true;

            lbl_others_vol.Visible = true;
            txt_others_vol.Visible = true;

        }


        grid_others_show();
        other_selecttable();



    }
    #endregion dropdownlist
    #region btn_save
    protected void btn_mrg_save_Click(object sender, EventArgs e)
    {
        string pp = string.Empty;
        if (txt_mrg_amount.Text != "")
        {
          //  var cultureInfo = new System.Globalization.CultureInfo("en-US");
          //  double plain = Double.Parse(txt_mrg_amount.Text, cultureInfo);
          //  double numba = plain;
            double numba =Convert .ToDouble (txt_mrg_amount.Text);
            pp = String.Format("{0:N}", Convert.ToInt32(numba));
        }

        gl.insertmortgage(lbl_orderno.Text, drp_mortgage.Text.ToUpper(), txt_mrg_type.Text, txt_mrg_assignee.Text.ToUpper(), txt_mrg_assignor.Text.ToUpper(), txt_mrg_appointed.Text.ToUpper(), txt_mrg_exeby.Text.ToUpper(), txt_mrg_lender.Text.ToUpper(), txt_mrg_grantor.Text.ToUpper(), txt_mrg_payableto.Text.ToUpper(), txt_mrg_trustee.Text.ToUpper(), txt_mrg_secparty.Text.ToUpper(), txt_mrg_debtor.Text.ToUpper(), txt_mrg_byandbeet.Text.ToUpper(), txt_mrg_dated.Text.ToUpper(), txt_mrg_filed.Text.ToUpper(), txt_mrg_vol.Text.ToUpper(), txt_mrg_pg.Text.ToUpper(), txt_mrg_inst.Text.ToUpper(), pp, txt_mrg_notes.Text.ToUpper(), txt_mrg_tableno.Text.Trim());
        grid_mortgage_show();
        mortgage_selecttable();

    }
    protected void btn_others_save_Click(object sender, EventArgs e)
    {


        gl.insertothers(lbl_orderno.Text, drp_others.Text.ToUpper(), txt_other_type.Text.ToUpper(), txt_other_grantee.Text.ToUpper(), txt_other_grantor.Text.ToUpper(), txt_other_petitioner.Text.ToUpper(), txt_other_to.Text.ToUpper(), txt_other_respondent.Text.ToUpper(), txt_other_owner.Text.ToUpper(), txt_other_re.Text.ToUpper(), txt_other_manufacturer.Text.ToUpper(), txt_others_dated.Text.ToUpper(), txt_others_filed.Text.ToUpper(), txt_others_vol.Text.ToUpper(), txt_others_pg.Text.ToUpper(), txt_others_inst.Text.ToUpper(), txt_other_cause.Text.ToUpper(), txt_others_notes.Text.ToUpper(), txt_other_tableno.Text.Trim());
        grid_others_show();

    }
    protected void btn_judg_save_Click(object sender, EventArgs e)
    {
         string pp = string.Empty;
         if (txt_judg_amount.Text != "")
         {
            // var cultureInfo = new System.Globalization.CultureInfo("en-US");
            // double plain = Double.Parse(txt_judg_amount.Text, cultureInfo);
           //  double numba = plain;
           double numba =Convert .ToDouble (txt_judg_amount.Text);
              pp = String.Format("{0:N}", Convert.ToInt32(numba));
         }


        gl.insertjudgement(lbl_orderno.Text.ToUpper(), drp_judgement.Text.ToUpper(), txt_judg_type.Text, txt_judg_taxpayer.Text.ToUpper(), txt_judg_address.Text.ToUpper(), txt_judg_taxpayerid.Text.ToUpper(), txt_judg_defendant.Text.ToUpper(), txt_judg_plaintiff.Text.ToUpper(), txt_judg_owner.Text.ToUpper(), txt_judg_grantor.Text.ToUpper(), txt_judg_grantee.Text.ToUpper(), txt_judg_obligor.Text.ToUpper(), txt_judg_ssn.Text.ToUpper(), txt_judg_obligee.Text.ToUpper(), txt_judg_tribunal.Text.ToUpper(), txt_judg_to.Text.ToUpper(), txt_judg_from.Text.ToUpper(), txt_judg_dated.Text.ToUpper(), txt_judg_filed.Text.ToUpper(), txt_judg_vol.Text.ToUpper(), txt_judg_pg.Text.ToUpper(), txt_judg_inst.Text.ToUpper(), txt_judg_cost.Text.ToUpper(), txt_judg_int.Text.ToUpper(), txt_judg_atty.Text.ToUpper(), pp, txt_judg_cause.Text.ToUpper(), txt_judg_notes.Text.ToUpper(), txt_judg_tableno.Text);
        grid_judg_show();

    }

    #endregion btn_save


    #region Deed
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_save_wardeed.Visible = false;
        btn_save_deedupdate.Visible = true;
        ID = GridView1.SelectedRow.Cells[2].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        ordno = GridView1.SelectedRow.Cells[3].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_type.Text = GridView1.SelectedRow.Cells[4].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtgrantee.Text = GridView1.SelectedRow.Cells[5].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtgrantor.Text = GridView1.SelectedRow.Cells[6].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtdated.Text = GridView1.SelectedRow.Cells[7].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtfield.Text = GridView1.SelectedRow.Cells[8].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtvol.Text = GridView1.SelectedRow.Cells[9].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtpg.Text = GridView1.SelectedRow.Cells[10].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtinst.Text = GridView1.SelectedRow.Cells[11].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txtnotes.Text = GridView1.SelectedRow.Cells[12].Text != "&nbsp;" ? GridView1.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ID = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
        string pp = "delete from tbl_deed where id='" + ID + "'";
        int del = objconnection.ExecuteNonQuery(pp);
        if (del > 0)
        {
            grid_deed_show();
        }

    }
    protected void btn_deed_cancel_Click(object sender, EventArgs e)
    {
        grid_deed_show();
        btn_save_wardeed.Visible = true;
        btn_save_deedupdate.Visible = false;
    }
    protected void btn_save_deedupdate_Click(object sender, EventArgs e)
    {

        gl.updatedeed(ID, lbl_orderno.Text, txt_deed_type.Text, txtgrantee.Text.ToUpper(), txtgrantor.Text.ToUpper(), txtdated.Text.ToUpper(), txtfield.Text.ToUpper(), txtvol.Text.ToUpper(), txtpg.Text.ToUpper(), txtinst.Text.ToUpper(), txtnotes.Text.ToUpper());
        ID = string.Empty;
        btn_save_deedupdate.Visible = false;
        btn_save_wardeed.Visible = true;
        grid_deed_show();
    }
    protected void btn_save_wardeed_Click(object sender, EventArgs e)
    {
        string ddtypt = txt_deed_type.Text.Replace("'", "''");
        gl.insertdeed(lbl_orderno.Text.ToUpper(), ddtypt, txtgrantee.Text.ToUpper(), txtgrantor.Text.ToUpper(), txtdated.Text.ToUpper(), txtfield.Text.ToUpper(), txtvol.Text.ToUpper(), txtpg.Text.ToUpper(), txtinst.Text.ToUpper(), txtnotes.Text.Replace("'s", "\\'").ToUpper(), txt_deed_tableno.Text);
        grid_deed_show();
        deed_selecttable();

    }
    protected void get_clientinfo()
    {
        DataSet ds = new DataSet();
        ds = gl.showclientinfo(lbl_orderno.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_client.Text = ds.Tables[0].Rows[0]["client"].ToString();
            txt_date.Text = ds.Tables[0].Rows[0]["pdate"].ToString();
            txt_address.Text = ds.Tables[0].Rows[0]["address"].ToString();
            txt_city_zip.Text = ds.Tables[0].Rows[0]["city_zip"].ToString();
            txt_ref.Text = ds.Tables[0].Rows[0]["ref"].ToString();
            txt_attention.Text = ds.Tables[0].Rows[0]["attention"].ToString();
            txt_certdate.Text = ds.Tables[0].Rows[0]["conformdate"].ToString();
            txt_owner.Text = ds.Tables[0].Rows[0]["owner"].ToString();
            txt_county.Text = ds.Tables[0].Rows[0]["propaddress"].ToString();
            txt_propaddress.Text = ds.Tables[0].Rows[0]["city"].ToString();
            txt_state.Text = ds.Tables[0].Rows[0]["state"].ToString();
            txt_zip.Text = ds.Tables[0].Rows[0]["zip"].ToString();
            txt_city.Text = ds.Tables[0].Rows[0]["county"].ToString();
            txt_legalinfo.Text = ds.Tables[0].Rows[0]["legalinfo"].ToString();
            txt_ownerofrec.Text = ds.Tables[0].Rows[0]["ownerofrec"].ToString();
            btn_client_save.Visible = false;
            btn_client_update.Visible = true;

        }

    }
    protected void grid_deed_show()
    {
        DataSet ds = new DataSet();
        ds = gl.showdeed(lbl_orderno.Text);
        GridView1.DataSource = ds;
        GridView1.DataBind();
        txt_deed_type.Text = "";
        txtgrantee.Text = "";
        txtgrantor.Text = "";
        txtdated.Text = "";
        txtfield.Text = "";
        txtvol.Text = "";
        txtpg.Text = "";
        txtinst.Text = "";
        txtnotes.Text = "";
        deed_selecttable();



    }

    protected void get_taxass()
    {
        DataSet ds = new DataSet();
        ds = gl.showtaxass(lbl_orderno.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_parcelid.Text = ds.Tables[0].Rows[0]["parcel_id"].ToString();
            txt_taxyear.Text = ds.Tables[0].Rows[0]["tax_year"].ToString();
            txt_land.Text = ds.Tables[0].Rows[0]["land"].ToString();
            txt_improv.Text = ds.Tables[0].Rows[0]["improvements"].ToString();
            txt_total.Text = ds.Tables[0].Rows[0]["total"].ToString();
            txt_taxes.Text = ds.Tables[0].Rows[0]["taxes"].ToString();
            txt_duepaid.Text = ds.Tables[0].Rows[0]["due_paid"].ToString();
            txt_assessnotes.Text = ds.Tables[0].Rows[0]["notes"].ToString();
            btn_assess_save.Visible = false;
            btn_assess_update.Visible = true;
        }

    }
    #endregion Deed
    #region clear/show
    protected void grid_mortgage_show()
    {
        DataSet ds = new DataSet();
        ds = gl.showmortgage(lbl_orderno.Text, drp_mortgage.Text);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        grd_mortgage.Visible = true;
        grd_mortgage.DataSource = ds;
        grd_mortgage.DataBind();
        //}
        //else
        //{
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        grd_mortgage.DataSource = null;
        //        grd_mortgage.Visible = false;
        //    }
        //}
        txt_mrg_assignee.Text = "";
        txt_mrg_assignor.Text = "";
        txt_mrg_notes.Text = "";
        txt_mrg_amount.Text = "";
        txt_mrg_appointed.Text = "";
        txt_mrg_exeby.Text = "";
        txt_mrg_lender.Text = "";
        txt_mrg_grantor.Text = "";
        txt_mrg_payableto.Text = "";
        txt_mrg_trustee.Text = "";
        txt_mrg_secparty.Text = "";
        txt_mrg_debtor.Text = "";
        txt_mrg_byandbeet.Text = "";
        txt_mrg_dated.Text = "";
        txt_mrg_filed.Text = "";
        txt_mrg_inst.Text = "";
        txt_mrg_pg.Text = "";
        txt_mrg_secparty.Text = "";
        txt_mrg_vol.Text = "";
        //txt_mrg_type.Text = "";

    }
    protected void grid_others_show()
    {
        DataSet ds = new DataSet();
        ds = gl.showothers(lbl_orderno.Text, drp_others.Text);
        
        grd_others.Visible = true;
        grd_others.DataSource = ds;
        grd_others.DataBind();

        txt_other_grantee.Text = "";
        txt_other_to.Text = "";
        txt_other_grantor.Text = "";
        txt_other_manufacturer.Text = "";
        txt_other_owner.Text = "";
        txt_other_petitioner.Text = "";
        txt_other_re.Text = "";
        txt_other_respondent.Text = "";
        txt_others_dated.Text = "";
        txt_others_filed.Text = "";
        txt_others_inst.Text = "";
        txt_others_notes.Text = "";
        txt_others_pg.Text = "";
        txt_others_vol.Text = "";
        txt_other_cause.Text = "";
        txt_other_to.Text = "";


    }
    protected void grid_judg_show()
    {
        DataSet ds = new DataSet();
        ds = gl.showjudgement(lbl_orderno.Text, drp_judgement.Text);
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    grd__judgement.Visible = true;
        //    grd__judgement.DataSource = null;
        grd__judgement.DataSource = ds;
        grd__judgement.DataBind();
        //}
        //else
        //{
        //    if (ds.Tables[0].Rows.Count == 0)
        //    {
        //        grd__judgement.Visible = false;
        //    }

        //}
        txt_judg_taxpayer.Text = "";
        txt_judg_address.Text = "";
        txt_judg_taxpayerid.Text = "";
        txt_judg_defendant.Text = "";
        txt_judg_plaintiff.Text = "";
        txt_judg_owner.Text = "";
        txt_judg_grantor.Text = "";
        txt_judg_grantee.Text = "";
        txt_judg_obligor.Text = "";
        txt_judg_ssn.Text = "";
        txt_judg_obligee.Text = "";
        txt_judg_tribunal.Text = "";
        txt_judg_to.Text = "";
        txt_judg_from.Text = "";
        txt_judg_dated.Text = "";
        txt_judg_filed.Text = "";
        txt_judg_pg.Text = "";
        txt_judg_vol.Text = "";
        txt_judg_int.Text = "";
        txt_judg_cost.Text = "";
        txt_judg_inst.Text = "";
        txt_judg_atty.Text = "";
        txt_judg_cause.Text = "";
        txt_judg_amount.Text = "";
        txt_judg_notes.Text = "";

    }

    protected void mortgage_clear()
    {
        txt_mrg_type.Text = "";
        txt_mrg_assignee.Text = "";
        txt_mrg_assignor.Text = "";
        txt_mrg_notes.Text = "";
        txt_mrg_amount.Text = "";
        txt_mrg_appointed.Text = "";
        txt_mrg_exeby.Text = "";
        txt_mrg_lender.Text = "";
        txt_mrg_grantor.Text = "";
        txt_mrg_payableto.Text = "";
        txt_mrg_trustee.Text = "";
        txt_mrg_secparty.Text = "";
        txt_mrg_debtor.Text = "";
        txt_mrg_byandbeet.Text = "";
        txt_mrg_dated.Text = "";
        txt_mrg_filed.Text = "";
        txt_mrg_vol.Text = "";
        txt_mrg_pg.Text = "";
        txt_mrg_inst.Text = "";
    }
    protected void judgement_clear()
    {
        txt_judg_type.Text = "";
        txt_judg_address.Text = "";
        txt_judg_amount.Text = "";
        txt_judg_atty.Text = "";
        txt_judg_cause.Text = "";
        txt_judg_cost.Text = "";
        txt_judg_defendant.Text = "";
        txt_judg_from.Text = "";
        txt_judg_grantor.Text = "";
        txt_judg_grantee.Text = "";
        txt_judg_int.Text = "";
        txt_judg_notes.Text = "";
        txt_judg_obligor.Text = "";
        txt_judg_obligee.Text = "";
        txt_judg_owner.Text = "";
        txt_judg_plaintiff.Text = "";
        txt_judg_ssn.Text = "";
        txt_judg_taxpayer.Text = "";
        txt_judg_taxpayerid.Text = "";
        txt_judg_to.Text = "";
        txt_judg_tribunal.Text = "";
        txt_judg_dated.Text = "";
        txt_judg_filed.Text = "";
        txt_judg_inst.Text = "";
        txt_judg_pg.Text = "";
        txt_judg_vol.Text = "";

    }
    protected void others_clear()
    {
        txt_other_type.Text = "";
        txt_other_cause.Text = "";
        txt_other_grantee.Text = "";
        txt_other_grantor.Text = "";
        txt_other_manufacturer.Text = "";
        txt_other_owner.Text = "";
        txt_other_petitioner.Text = "";
        txt_other_re.Text = "";
        txt_other_respondent.Text = "";
        txt_others_notes.Text = "";
        txt_others_dated.Text = "";
        txt_others_filed.Text = "";
        txt_others_vol.Text = "";
        txt_others_pg.Text = "";
        txt_others_inst.Text = "";

    }
    protected void client_clear()
    {
        txt_orderno.Text = "";
        txt_client.Text = "";
        txt_date.Text = "";
        txt_address.Text = "";
        txt_city_zip.Text = "";
        txt_ref.Text = "";
        txt_attention.Text = "";
        txt_certdate.Text = "";
        txt_owner.Text = "";
        txt_propaddress.Text = "";
        txt_city.Text = "";
        txt_state.Text = "";
        txt_zip.Text = "";
        txt_county.Text = "";
        txt_legalinfo.Text = "";
        txt_ownerofrec.Text = "";
    }
    protected void clear_assessment()
    {
        txt_orderno.Text = "";
        txt_parcelid.Text = "";
        txt_taxyear.Text = "";
        txt_land.Text = "";
        txt_improv.Text = "";
        txt_total.Text = "";
        txt_taxes.Text = "";
        txt_duepaid.Text = "";
        txt_assessnotes.Text = "";
    }
    #endregion clear/show
    #region mortgage
    protected void grd_mortgage_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid_mortgage_show();
        btn_mrg_save.Visible = false;
        btn_mrg_update.Visible = true;
        ID = grd_mortgage.SelectedRow.Cells[2].Text;
        if (drp_mortgage.Text == "DEED OF TRUST OR MORTGAGE")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[12].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_payableto.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_trustee.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[13].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[13].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_mortgage.Text == "AFFIDAVIT OF LOST ASSIGNMENT")
        {
            txt_mrg_assignee.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_assignor.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = "";
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }

        else if (drp_mortgage.Text == "APPOINTMENT OF SUBSTITUTE TRUSTEE")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_exeby.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = "";
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_mortgage.Text == "ASSIGNMENT OF RENTS")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_grantor.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }

        else if (drp_mortgage.Text == "ASSIGNMENT")
        {
            txt_mrg_assignee.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_assignor.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = "";
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }

        else if (drp_mortgage.Text == "LOAN MODIFICATION")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = "";
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }
        else if (drp_mortgage.Text == "SUBORDINATE DEED OF TRUST")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = "";
            txt_mrg_amount.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_payableto.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_trustee.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_secparty.Text = "";
            txt_mrg_debtor.Text = "";
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[12].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }

        else if (drp_mortgage.Text == "UCC FINANCING STATEMENT")
        {
            txt_mrg_assignee.Text = "";
            txt_mrg_assignor.Text = "";
            txt_mrg_notes.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_amount.Text = "";
            txt_mrg_appointed.Text = "";
            txt_mrg_exeby.Text = "";
            txt_mrg_lender.Text = "";
            txt_mrg_grantor.Text = "";
            txt_mrg_payableto.Text = "";
            txt_mrg_trustee.Text = "";
            txt_mrg_secparty.Text = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_debtor.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_byandbeet.Text = "";
            txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_filed.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_vol.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_inst.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_mrg_type.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }


    }
    protected void grd_mortgage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void grd_mortgage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ID = grd_mortgage.DataKeys[e.RowIndex].Values["ID"].ToString();
        string pp = "delete from tbl_mortgage where id='" + ID + "'";
        int del = objconnection.ExecuteNonQuery(pp);


        string pp2 = "delete from tbl_sequence where orderno='" + SessionHandler.OrderNo + "' and type='Mortgage' and header='" + drp_mortgage.Text + "'";
        int del2 = objconnection.ExecuteNonQuery(pp2);


        if (del > 0)
        {
            grid_mortgage_show();
            mortgage_selecttable();
        }
    }
    protected void btn_mrg_cancel_Click(object sender, EventArgs e)
    {
        btn_mrg_save.Visible = true;
        btn_mrg_update.Visible = false;
        mortgage_clear();
        grid_judg_show();
    }
    protected void btn_mrg_update_Click(object sender, EventArgs e)
    {
         string pp = string.Empty;
         if (txt_mrg_amount.Text != "")
         {
            // var cultureInfo = new System.Globalization.CultureInfo("en-US");
            // double plain = Double.Parse(txt_mrg_amount.Text, cultureInfo);
            // double numba = plain;
             double numba =Convert .ToDouble (txt_mrg_amount.Text);
             pp = String.Format("{0:N}", Convert.ToInt32(numba));
         }
        gl.updatemortgage(ID, lbl_orderno.Text, drp_mortgage.Text.ToUpper(), txt_mrg_type.Text, txt_mrg_assignee.Text.ToUpper(), txt_mrg_assignor.Text.ToUpper(), txt_mrg_appointed.Text.ToUpper(), txt_mrg_exeby.Text.ToUpper(), txt_mrg_lender.Text.ToUpper(), txt_mrg_grantor.Text.ToUpper(), txt_mrg_payableto.Text.ToUpper(), txt_mrg_trustee.Text.ToUpper(), txt_mrg_secparty.Text.ToUpper(), txt_mrg_debtor.Text.ToUpper().ToUpper(), txt_mrg_byandbeet.Text.ToUpper(), txt_mrg_dated.Text.ToUpper(), txt_mrg_filed.Text.ToUpper(), txt_mrg_vol.Text.ToUpper(), txt_mrg_pg.Text.ToUpper(), txt_mrg_inst.Text.ToUpper(),pp, txt_mrg_notes.Text.ToUpper());
        ID = string.Empty;
        btn_mrg_update.Visible = false;
        btn_mrg_save.Visible = true;
        grid_mortgage_show();
        mortgage_selecttable();

    }
    #endregion mortgage
    #region others
    protected void grd_others_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid_others_show();
        btn_others_save.Visible = false;
        btn_others_update.Visible = true;
        ID = grd_others.SelectedRow.Cells[2].Text;

        if (drp_others.Text == "AFFIDAVIT AND AGREEMENT")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_grantee.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_grantor.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_manufacturer.Text = "";
            txt_other_to.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = "";
            txt_other_re.Text = "";
            txt_other_respondent.Text = "";
            txt_other_cause.Text = "";
            txt_others_dated.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_filed.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_vol.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_inst.Text = grd_others.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = grd_others.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_notes.Text = grd_others.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_others.Text == "DIVORCE NOT EXAMINED")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_cause.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_grantee.Text = "";
            txt_other_to.Text = "";
            txt_other_grantor.Text = "";
            txt_other_manufacturer.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_re.Text = "";
            txt_other_respondent.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_notes.Text = "";
            txt_others_dated.Text = "";
            txt_others_inst.Text = "";
            txt_others_pg.Text = "";
            txt_others_vol.Text = "";
            txt_others_filed.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_others.Text == "GENERAL POWER OF ATTORNEY")
        {
            txt_other_cause.Text = grd_others.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_grantee.Text = "";
            txt_other_to.Text = "";
            txt_other_grantor.Text = "";
            txt_other_manufacturer.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = "";
            txt_other_re.Text = "";
            txt_other_respondent.Text = "";
            txt_others_notes.Text = grd_others.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_dated.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_filed.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_vol.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_inst.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }

        else if (drp_others.Text == "SPECIAL POWER OF ATTORNEY")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_cause.Text = "";
            txt_other_grantee.Text = "";
            txt_other_to.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_grantor.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_manufacturer.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = "";
            txt_other_re.Text = "";
            txt_other_respondent.Text = "";
            txt_others_notes.Text = grd_others.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_dated.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_filed.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_vol.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_inst.Text = grd_others.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = grd_others.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }


        else if (drp_others.Text == "PROBATE - NOT EXAMINED")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_cause.Text = "";
            txt_other_grantee.Text = "";
            txt_other_to.Text = "";
            txt_other_grantor.Text = "";
            txt_other_manufacturer.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = "";
            txt_other_re.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_respondent.Text = "";
            txt_others_notes.Text = "";
            txt_others_dated.Text = "";
            txt_others_filed.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = "";
            txt_others_vol.Text = "";
            txt_others_inst.Text = "";
        }

        else if (drp_others.Text == "REINSTATEMENT AGREEMENT")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_cause.Text = "";
            txt_other_grantee.Text = "";
            txt_other_to.Text = "";
            txt_other_grantor.Text = "";
            txt_other_manufacturer.Text = "";
            txt_other_owner.Text = "";
            txt_other_petitioner.Text = "";
            txt_other_re.Text = "";
            txt_other_respondent.Text = "";
            txt_others_notes.Text = grd_others.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_dated.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_filed.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_vol.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_inst.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }
        else if (drp_others.Text == "STATEMENT OF OWNERSHIP AND LOCATION")
        {
            txt_other_type.Text = grd_others.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_cause.Text = "";
            txt_other_grantee.Text = "";
            txt_other_to.Text = "";
            txt_other_grantor.Text = "";
            txt_other_manufacturer.Text = grd_others.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_owner.Text = grd_others.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_other_petitioner.Text = "";
            txt_other_re.Text = "";
            txt_other_respondent.Text = "";
            txt_others_notes.Text = grd_others.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_dated.Text = grd_others.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_filed.Text = grd_others.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_vol.Text = grd_others.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_pg.Text = grd_others.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_others_inst.Text = grd_others.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_others.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }



    }
    protected void grd_others_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ID = grd_others.DataKeys[e.RowIndex].Values["ID"].ToString();
        string pp = "delete from tbl_others where id='" + ID + "'";
        int del = objconnection.ExecuteNonQuery(pp);

        string pp2 = "delete from tbl_sequence where orderno='" + SessionHandler.OrderNo + "' and type='Others' and header='" + drp_others.Text + "'";
        int del2 = objconnection.ExecuteNonQuery(pp2);


        if (del > 0)
        {
            grid_others_show();
        }
    }
    protected void grd_others_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void btn_others_cancel_Click(object sender, EventArgs e)
    {
        btn_others_save.Visible = true;
        btn_others_update.Visible = false;
        others_clear();
        grid_others_show();
    }
    protected void btn_others_update_Click(object sender, EventArgs e)
    {
        gl.updateothers(ID, lbl_orderno.Text, drp_others.Text.ToUpper(), txt_other_type.Text.ToUpper(), txt_other_grantee.Text.ToUpper(), txt_other_grantor.Text.ToUpper(), txt_other_petitioner.Text.ToUpper(), txt_other_to.Text.ToUpper(), txt_other_respondent.Text.ToUpper(), txt_other_owner.Text.ToUpper(), txt_other_re.Text.ToUpper(), txt_other_manufacturer.Text.ToUpper(), txt_others_dated.Text.ToUpper(), txt_others_filed.Text.ToUpper(), txt_others_vol.Text.ToUpper(), txt_others_pg.Text.ToUpper(), txt_others_inst.Text.ToUpper(), txt_other_cause.Text.ToUpper(), txt_others_notes.Text.ToUpper());
        ID = string.Empty;
        btn_others_save.Visible = true;
        btn_others_update.Visible = false;
        grid_others_show();

    }
    #endregion others
    #region judgement
    protected void grd__judgement_SelectedIndexChanged(object sender, EventArgs e)
    {
        grid_judg_show();
        btn_judg_update.Visible = true;
        btn_judg_save.Visible = false;
        ID = grd__judgement.SelectedRow.Cells[2].Text;
        if (drp_judgement.Text == "ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION")
        {
            txt_judg_taxpayer.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_int.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "ABSTRACT OF JUDGMENT")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_plaintiff.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_atty.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_int.Text = grd__judgement.SelectedRow.Cells[13].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[13].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cause.Text = grd__judgement.SelectedRow.Cells[14].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[14].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[15].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[15].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[16].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[16].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }
        else if (drp_judgement.Text == "AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantor.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }

        else if (drp_judgement.Text == "AFFIDAVIT TO FIX LIEN")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantor.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = "";
            txt_judg_notes.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }

        else if (drp_judgement.Text == "FEDERAL TAX LIEN")
        {
            txt_judg_taxpayer.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "LIEN CLAIM AFFIDAVIT")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantor.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "NOTICE OF ASSESSMENT LIEN OR HOA")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantor.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "NOTICE OF CHILD SUPPORT LIEN")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_ssn.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_obligee.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_tribunal.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[13].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[13].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[14].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[14].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "NOTICE OF FORECLOSURE")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_grantee.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = "";
            txt_judg_notes.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }

        else if (drp_judgement.Text == "NOTICE OF TRUSTEE SALE")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = "";
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_from.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = "";
            txt_judg_notes.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE")
        {
            txt_judg_taxpayer.Text = "";
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = "";
            txt_judg_defendant.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_plaintiff.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_amount.Text = "";
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        }
        else if (drp_judgement.Text == "STATE TAX LIEN")
        {
            txt_judg_taxpayer.Text = grd__judgement.SelectedRow.Cells[3].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_address.Text = grd__judgement.SelectedRow.Cells[4].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_taxpayerid.Text = grd__judgement.SelectedRow.Cells[5].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_defendant.Text = "";
            txt_judg_plaintiff.Text = "";
            txt_judg_owner.Text = "";
            txt_judg_grantor.Text = "";
            txt_judg_grantee.Text = "";
            txt_judg_obligor.Text = "";
            txt_judg_ssn.Text = "";
            txt_judg_obligee.Text = "";
            txt_judg_tribunal.Text = "";
            txt_judg_to.Text = "";
            txt_judg_from.Text = "";
            txt_judg_dated.Text = grd__judgement.SelectedRow.Cells[6].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_filed.Text = grd__judgement.SelectedRow.Cells[7].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_vol.Text = grd__judgement.SelectedRow.Cells[8].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_pg.Text = grd__judgement.SelectedRow.Cells[9].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_inst.Text = grd__judgement.SelectedRow.Cells[10].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_cost.Text = "";
            txt_judg_atty.Text = "";
            txt_judg_int.Text = "";
            txt_judg_cause.Text = "";
            txt_judg_amount.Text = grd__judgement.SelectedRow.Cells[11].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
            txt_judg_notes.Text = "";
            txt_judg_type.Text = grd__judgement.SelectedRow.Cells[12].Text != "&nbsp;" ? grd__judgement.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

        }

    }
    protected void grd__judgement_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ID = grd__judgement.DataKeys[e.RowIndex].Values["ID"].ToString();
        string pp = "delete from tbl_judgment where id='" + ID + "'";
        int del = objconnection.ExecuteNonQuery(pp);

        string pp2 = "delete from tbl_sequence where orderno='" + SessionHandler.OrderNo + "' and type='judgement' and header='" + drp_judgement.Text + "'";
        int del2 = objconnection.ExecuteNonQuery(pp2);
        if (del > 0)
        {
            grid_judg_show();
        }
    }
    protected void grd__judgement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void btn_judg_cancel_Click(object sender, EventArgs e)
    {
        judgement_clear();
        btn_judg_save.Visible = true;
        btn_judg_update.Visible = false;
        grid_judg_show();
    }
    protected void btn_judg_update_Click(object sender, EventArgs e)
    {
         string pp = string.Empty;
         if (txt_judg_amount.Text != "")
         {

             //var cultureInfo = new System.Globalization.CultureInfo("en-US");
             //double plain = Double.Parse(txt_judg_amount.Text, cultureInfo);
            // double numba = plain;
             double numba =Convert .ToDouble (txt_judg_amount.Text);
             pp = String.Format("{0:N}", Convert.ToInt32(numba));
         }
        gl.updatejudgement(ID, lbl_orderno.Text, drp_judgement.Text.ToUpper(), txt_judg_type.Text, txt_judg_taxpayer.Text.ToUpper(), txt_judg_address.Text.ToUpper(), txt_judg_taxpayerid.Text.ToUpper(), txt_judg_defendant.Text.ToUpper(), txt_judg_plaintiff.Text.ToUpper(), txt_judg_owner.Text.ToUpper(), txt_judg_grantor.Text.ToUpper(), txt_judg_grantee.Text.ToUpper(), txt_judg_obligor.Text.ToUpper(), txt_judg_ssn.Text, txt_judg_obligee.Text.ToUpper(), txt_judg_tribunal.Text.ToUpper(), txt_judg_to.Text.ToUpper(), txt_judg_from.Text.ToUpper(), txt_judg_dated.Text.ToUpper(), txt_judg_filed.Text.ToUpper(), txt_judg_vol.Text.ToUpper(), txt_judg_pg.Text.ToUpper(), txt_judg_inst.Text, txt_judg_cost.Text, txt_judg_int.Text.ToUpper(), txt_judg_atty.Text.ToUpper(),pp , txt_judg_cause.Text.ToUpper(), txt_judg_notes.Text.ToUpper());
        ID = string.Empty;
        btn_judg_save.Visible = true;
        btn_judg_update.Visible = false;
        grid_judg_show();
    }
    #endregion judgement

    protected void btn_client_save_Click(object sender, EventArgs e)
    {
        int res = gl.insertclient(txt_orderno.Text, txt_client.Text, txt_date.Text, txt_address.Text, txt_city_zip.Text, txt_ref.Text, txt_attention.Text, txt_certdate.Text, txt_owner.Text, txt_propaddress.Text, txt_city.Text, txt_state.Text, txt_zip.Text, txt_county.Text, txt_legalinfo.Text, txt_ownerofrec.Text);
        if (res > 0)
        {
            lbl_client_show.Text = "Saved Successfully....!!";
            //client_clear();
        }
    }

    protected void btn_assess_save_Click(object sender, EventArgs e)
    {
        int res = gl.insertassessmaent(txt_orderno.Text, txt_parcelid.Text, txt_taxyear.Text, txt_land.Text, txt_improv.Text, txt_total.Text, txt_taxes.Text, txt_duepaid.Text, txt_assessnotes.Text);

        if (res > 0)
        {
            lbl_assess_show.Text = "Saved Successfully....!!";
            clear_assessment();
        }

    }


    #region new
    private void AllotProcess()
    {
        autoProduction();
    }
    private void autoProduction()
    {


        if (!FillData())
        {
            SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
            SessionHandler.RedirectPage("~/Form/HomePage.aspx?status=No Orders to Process.");
        }
    }
    private bool FillData()
    {
        MySqlParameter[] mparam = new MySqlParameter[1];

        mparam[0] = new MySqlParameter("?$User_id", SessionHandler.UserName);
        mparam[0].MySqlDbType = MySqlDbType.VarChar;

        MySqlDataReader mdr = cons.ExecuteSPReader("sp_CheckUserLockNew_new", true, mparam);
        if (mdr.HasRows)
        {
            if (mdr.Read())
            {

                SessionHandler.OrderId = CheckNull(mdr, 0);

                string Orderno = CheckNull(mdr, 1);
                lbl_orderno.Text = Orderno;




                SessionHandler.OrderNo = lbl_orderno.Text;
                txt_orderno.Text = lbl_orderno.Text;
                LblDate.Text = CheckNull(mdr, 2);
                txt_date.Text = CheckNull(mdr, 2);
                lbl_processname.Text = SessionHandler.UserName;
                lbl_pros_name.Text = CheckNull(mdr, 3);

                // Session["Timepro"] = DateTime.Now;KEYING
                if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU")
                {
                    lbl_qc_comments.Visible = false;
                    txt_qc_comments.Visible = false;

                }


                SessionHandler.Rights = lbl_pros_name.Text;
                if (lbl_pros_name.Text == "QC")
                {

                    txt_keying_commend.Text = CheckNull(mdr, 4);
                    txt_keying_commend.ReadOnly = true;
                    lbl_keying.Text = CheckNull(mdr, 5);
                    lbl_keying.Visible = true;
                    lbl_keyingtext.Visible = true;
                    // PanelQC.Visible = true;
                }

            }
            return true;
        }
        mdr.Close();
        return false;
    }
    private string CheckNull(MySqlDataReader myDr, int Index)
    {
        return myDr[Index] == DBNull.Value ? "" : myDr[Index].ToString();
    }
    #endregion new

    #region Completed
    protected void btn_order_save_Click(object sender, EventArgs e)
    {


        string comments = "";
        if (ValidateComments())
        {
            if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU") comments = txt_keying_commend.Text;
            else if (lbl_pros_name.Text == "QC") comments = txt_qc_comments.Text;
            int rest = gls.Update_declaration(txt_declaration.Text);
            OutputWriteUp(lbl_orderno.Text);

            int result = gls.UpdateOrders(comments, txt_declaration.Text);

            SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
            SessionHandler.RedirectPage("~/Form/HomePage.aspx");

        }

    }
    private bool ValidateComments()
    {
        if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU")
        {
            if (txt_keying_commend.Text == "")
            { LblError.Text = "Please Fill the Keycomments."; return false; }
        }
        if (lbl_pros_name.Text == "QC")
        {
            if (txt_qc_comments.Text == "")
            { LblError.Text = "Please Fill the QCcomments."; return false; }
        }
        if (txt_declaration.Text == "") { LblError.Text = "Please Fill the Declaration Comments..."; return false; }

        return true;
    }
    #endregion Completed
    protected void btnpreview_Click(object sender, EventArgs e)
    {

    }
    protected void getgridpreviewshow()
    {
        DataSet ds = new DataSet();
        ds = gl.getshowpreviewall(lbl_orderno.Text);


        ds.Tables[0].Merge(ds.Tables[1]);
        ds.Tables[0].Merge(ds.Tables[2]);
        ds.Tables[0].Merge(ds.Tables[3]);

        if (ds.Tables[0].Rows.Count > 0)
        {
            gridpreview.Visible = true;
            gridpreview.DataSource = null;
            gridpreview.DataSource = ds.Tables[0];
            gridpreview.DataBind();
        }
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
            {
                gridpreview.Visible = false;
            }

        }


    }




    protected void gridpreview_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsquenceupdate_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < gridpreview.Rows.Count; i++)
        {
            string type = gridpreview.Rows[i].Cells[1].Text;
            string header = gridpreview.Rows[i].Cells[2].Text;
            TextBox txtseque = (TextBox)gridpreview.Rows[i].FindControl("txtsequence");
            string seque = txtseque.Text;
            string query = "";
            string query2 = "";
            if (type == "Deed")
            {
                query = "update tbl_deed set sequence='" + seque + "' where Deed_type='" + header + "' and orderno='" + lbl_orderno.Text.Trim() + "'";
                query2 = "update tbl_sequence set sequence='" + seque + "' where orderno='" + lbl_orderno.Text.Trim() + "' and  type='Deed' and header='" + header + "'";

            }
            else if (type == "Mortgage")
            {
                query = "update tbl_mortgage set sequence='" + seque + "' where mortgage_type_2='" + header + "' and orderno='" + lbl_orderno.Text.Trim() + "'";
                query2 = "update tbl_sequence set sequence='" + seque + "' where orderno='" + lbl_orderno.Text.Trim() + "' and  type='Mortgage' and header='" + header + "'";
            }
            else if (type == "Judgment")
            {
                query = "update tbl_judgment set sequence='" + seque + "' where judgement_type_2='" + header + "' and orderno='" + lbl_orderno.Text.Trim() + "'";
                query2 = "update tbl_sequence set sequence='" + seque + "' where orderno='" + lbl_orderno.Text.Trim() + "' and  type='judgement' and header='" + header + "'";
            }
            else if (type == "Others")
            {
                query = "update tbl_others set sequence='" + seque + "' where others_type_2='" + header + "' and orderno='" + lbl_orderno.Text.Trim() + "'";
                query2 = "update tbl_sequence set sequence='" + seque + "' where orderno='" + lbl_orderno.Text.Trim() + "' and  type='Others' and header='" + header + "'";
            }

            int result = objconnection.ExecuteNonQuery(query);
            int res = objconnection.ExecuteNonQuery(query2);

        }

        string message = "Updated..!";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload=function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

    }


    private string outputath;
    private bool OutputWriteUp(string order_no)
    {
        #region old
        object missing = System.Type.Missing;

        DataSet dswriteup = new DataSet();
        string query = "select roughcopy,Template from master_path";
        dswriteup = gls.GetWriteUp(query);

        string sourcePath = dswriteup.Tables[0].Rows[0]["Template"].ToString();
        outputath = dswriteup.Tables[0].Rows[0]["roughcopy"].ToString();

        string docname = "";
        // outputath = getfullpath1(query);
        docname = "Consolidated" + ".docx";
        string[] fileArray = Directory.GetFiles(sourcePath, docname);

        string fileName = null, target = null;
        for (int i = 0; i < fileArray.Length; i++)
        {
            fileName = Path.GetFileName(fileArray[i]);
            target = outputath + "\\" + order_no.Trim() + "_" + fileName;
            File.Delete(target);


            File.Copy(fileArray[i], target, true);
        }

        if (BindWriteUp(dswriteup, target) == false)
        {
            // MessageBox.Show("Writeup Output generation failed", "String Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // return false;
        }


        #endregion old


        return true;
    }



    private bool BindWriteUp(DataSet dswriteup, string target)
    {
        Regex id_client = new Regex("@client");
        Regex id_date = new Regex("@date");
        Regex id_address = new Regex("@address");
        Regex id_orderno = new Regex("@order");
        Regex id_cityst = new Regex("@city");
        Regex id_ref = new Regex("@ref");
        Regex id_attention = new Regex("@attention");
        Regex id_cdate = new Regex("@cdate");
        Regex id_owner = new Regex("@owner");
        Regex id_paddress = new Regex("@address");
        Regex id_city = new Regex("@city");
        Regex id_state = new Regex("@state");
        Regex id_zip = new Regex("@zip");
        Regex id_county = new Regex("@county");
        Regex id_legal = new Regex("@legal");
        Regex id_ownerofrecord = new Regex("@owners");

        #region General ID
        //TAX ASSESSMENT INFORMATION1
        Regex id_tsparcel1 = new Regex("@tsparcel1");
        Regex id_tstaxyear1 = new Regex("@tstaxyear1");
        Regex id_tsland1 = new Regex("@tsland1");
        Regex id_tsimprove1 = new Regex("@tsimprove1");
        Regex id_tstotal1 = new Regex("@tstotal1");
        Regex id_tstaxes1 = new Regex("@tstaxes1");
        Regex id_tsdue1 = new Regex("@tsdue1");
        Regex id_tsnotes1 = new Regex("@tsnotes1");

        //TAX ASSESSMENT INFORMATION2
        Regex id_tsparcel2 = new Regex("@tsparcel2");
        Regex id_tstaxyear2 = new Regex("@tstaxyear2");
        Regex id_tsland2 = new Regex("@tsland2");
        Regex id_tsimprove2 = new Regex("@tsimprove2");
        Regex id_tstotal2 = new Regex("@tstotal2");
        Regex id_tstaxes2 = new Regex("@tstaxes2");
        Regex id_tsdue2 = new Regex("@tsdue2");
        Regex id_tsnotes2 = new Regex("@tsnotes2");

        //TAX ASSESSMENT INFORMATION3
        Regex id_tsparcel3 = new Regex("@tsparcel3");
        Regex id_tstaxyear3 = new Regex("@tstaxyear3");
        Regex id_tsland3 = new Regex("@tsland3");
        Regex id_tsimprove3 = new Regex("@tsimprove3");
        Regex id_tstotal3 = new Regex("@tstotal3");
        Regex id_tstaxes3 = new Regex("@tstaxes3");
        Regex id_tsdue3 = new Regex("@tsdue3");
        Regex id_tsnotes3 = new Regex("@tsnotes3");

        //TAX ASSESSMENT INFORMATION4
        Regex id_tsparcel4 = new Regex("@tsparcel4");
        Regex id_tstaxyear4 = new Regex("@tstaxyear4");
        Regex id_tsland4 = new Regex("@tsland4");
        Regex id_tsimprove4 = new Regex("@tsimprove4");
        Regex id_tstotal4 = new Regex("@tstotal4");
        Regex id_tstaxes4 = new Regex("@tstaxes4");
        Regex id_tsdue4 = new Regex("@tsdue4");
        Regex id_tsnotes4 = new Regex("@tsnotes4");

        //TAX ASSESSMENT INFORMATION5
        Regex id_tsparcel5 = new Regex("@tsparcel5");
        Regex id_tstaxyear5 = new Regex("@tstaxyear5");
        Regex id_tsland5 = new Regex("@tsland5");
        Regex id_tsimprove5 = new Regex("@tsimprove5");
        Regex id_tstotal5 = new Regex("@tstotal5");
        Regex id_tstaxes5 = new Regex("@tstaxes5");
        Regex id_tsdue5 = new Regex("@tsdue5");
        Regex id_tsnotes5 = new Regex("@tsnotes5");


        #endregion


        #region Deed ID

        //Deed1
        Regex id_deed1 = new Regex("@deed1");
        Regex id_dgrantee1 = new Regex("@dgrantee1");
        Regex id_dgrantor1 = new Regex("@dgrantor1");
        Regex id_ddated1 = new Regex("@ddated1");
        Regex id_dfiled1 = new Regex("@dfiled1");
        Regex id_dvol1 = new Regex("@dvol1");
        Regex id_dpg1 = new Regex("@dpg1");
        Regex id_dinst1 = new Regex("@dinst1");
        Regex id_dnotes1 = new Regex("@dnotes1");

        //deed2
        Regex id_deed2 = new Regex("@deed2");
        Regex id_dgrantee2 = new Regex("@dgrantee2");
        Regex id_dgrantor2 = new Regex("@dgrantor2");
        Regex id_ddated2 = new Regex("@ddated2");
        Regex id_dfiled2 = new Regex("@dfiled2");
        Regex id_dvol2 = new Regex("@dvol2");
        Regex id_dpg2 = new Regex("@dpg2");
        Regex id_dinst2 = new Regex("@dinst2");
        Regex id_dnotes2 = new Regex("@dnotes2");

        //deed3

        Regex id_deed3 = new Regex("@deed3");
        Regex id_dgrantee3 = new Regex("@dgrantee3");
        Regex id_dgrantor3 = new Regex("@dgrantor3");
        Regex id_ddated3 = new Regex("@ddated3");
        Regex id_dfiled3 = new Regex("@dfiled3");
        Regex id_dvol3 = new Regex("@dvol3");
        Regex id_dpg3 = new Regex("@dpg3");
        Regex id_dinst3 = new Regex("@dinst3");
        Regex id_dnotes3 = new Regex("@dnotes3");

        //Deed4
        Regex id_deed4 = new Regex("@deed4");
        Regex id_dgrantee4 = new Regex("@dgrantee4");
        Regex id_dgrantor4 = new Regex("@dgrantor4");
        Regex id_ddated4 = new Regex("@ddated4");
        Regex id_dfiled4 = new Regex("@dfiled4");
        Regex id_dvol4 = new Regex("@dvol4");
        Regex id_dpg4 = new Regex("@dpg4");
        Regex id_dinst4 = new Regex("@dinst4");
        Regex id_dnotes4 = new Regex("@dnotes4");

        //Deed5

        Regex id_deed5 = new Regex("@deed5");
        Regex id_dgrantee5 = new Regex("@dgrantee5");
        Regex id_dgrantor5 = new Regex("@dgrantor5");
        Regex id_ddated5 = new Regex("@ddated5");
        Regex id_dfiled5 = new Regex("@dfiled5");
        Regex id_dvol5 = new Regex("@dvol5");
        Regex id_dpg5 = new Regex("@dpg5");
        Regex id_dinst5 = new Regex("@dinst5");
        Regex id_dnotes5 = new Regex("@dnotes5");
        #endregion

        #region Mortgage ID

        // AFFIDAVIT OF LOST ASSIGNMENT 1
        Regex id_affoflostassign1 = new Regex("hellos");
        Regex id_affoflostassignee1 = new Regex("@affoflostassignee1");
        Regex id_affoflostassignor1 = new Regex("@affoflostassignor1");
        Regex id_affmda1 = new Regex("@affmda1");
        Regex id_affmf1 = new Regex("@affmf1");
        Regex id_affmv1 = new Regex("@affmv1");
        Regex id_affmpg1 = new Regex("@affmpg1");
        Regex id_affminst1 = new Regex("@affminst1");
        Regex id_affmnotes1 = new Regex("@affmnotes1");

        // AFFIDAVIT OF LOST ASSIGNMENT 2
        Regex id_affoflostassign2 = new Regex("@affoflostassign2");
        Regex id_affoflostassignee2 = new Regex("@affoflostassignee2");
        Regex id_affoflostassignor2 = new Regex("@affoflostassignor2");
        Regex id_affmda2 = new Regex("@affmda2");
        Regex id_affmf2 = new Regex("@affmf2");
        Regex id_affmv2 = new Regex("@affmv2");
        Regex id_affmpg2 = new Regex("@affmpg2");
        Regex id_affminst2 = new Regex("@affminst2");
        Regex id_affmnotes2 = new Regex("@affmnotes2");

        // AFFIDAVIT OF LOST ASSIGNMENT 3
        Regex id_affoflostassign3 = new Regex("@affoflostassign3");
        Regex id_affoflostassignee3 = new Regex("@affoflostassignee3");
        Regex id_affoflostassignor3 = new Regex("@affoflostassignor3");
        Regex id_affmda3 = new Regex("@affmda3");
        Regex id_affmf3 = new Regex("@affmf3");
        Regex id_affmv3 = new Regex("@affmv3");
        Regex id_affmpg3 = new Regex("@affmpg3");
        Regex id_affminst3 = new Regex("@affminst3");
        Regex id_affmnotes3 = new Regex("@affmnotes3");

        // AFFIDAVIT OF LOST ASSIGNMENT 4
        Regex id_affoflostassign4 = new Regex("@affoflostassign4");
        Regex id_affoflostassignee4 = new Regex("@affoflostassignee4");
        Regex id_affoflostassignor4 = new Regex("@affoflostassignor4");
        Regex id_affmda4 = new Regex("@affmda4");
        Regex id_affmf4 = new Regex("@affmf4");
        Regex id_affmv4 = new Regex("@affmv4");
        Regex id_affmpg4 = new Regex("@affmpg4");
        Regex id_affminst4 = new Regex("@affminst4");
        Regex id_affmnotes4 = new Regex("@affmnotes4");

        // AFFIDAVIT OF LOST ASSIGNMENT 5
        Regex id_affoflostassign5 = new Regex("@affoflostassign5");
        Regex id_affoflostassignee5 = new Regex("@affoflostassignee5");
        Regex id_affoflostassignor5 = new Regex("@affoflostassignor5");
        Regex id_affmda5 = new Regex("@affmda5");
        Regex id_affmf5 = new Regex("@affmf5");
        Regex id_affmv5 = new Regex("@affmv5");
        Regex id_affmpg5 = new Regex("@affmpg5");
        Regex id_affminst5 = new Regex("@affminst5");
        Regex id_affmnotes5 = new Regex("@affmnotes5");

        //APPOINTMENT OF SUBSTITUTE TRUSTEE 1

        Regex id_appofsubtrus1 = new Regex("@appofsub1");
        Regex id_appointed1 = new Regex("@appointed1");
        Regex id_executedby1 = new Regex("@executedby1");
        Regex id_appda1 = new Regex("@appda1");
        Regex id_appf1 = new Regex("@appf1");
        Regex id_appv1 = new Regex("@appv1");
        Regex id_appp1 = new Regex("@appp1");
        Regex id_appins1 = new Regex("@appins1");
        Regex id_appnotes1 = new Regex("@appnotes1");

        //APPOINTMENT OF SUBSTITUTE TRUSTEE 2

        Regex id_appofsubtrus2 = new Regex("@appofsubtrus2");
        Regex id_appointed2 = new Regex("@appointed2");
        Regex id_executedby2 = new Regex("@executedby2");
        Regex id_appda2 = new Regex("@appda2");
        Regex id_appf2 = new Regex("@appf2");
        Regex id_appv2 = new Regex("@appv2");
        Regex id_appp2 = new Regex("@appp2");
        Regex id_appins2 = new Regex("@appins2");
        Regex id_appnotes2 = new Regex("@appnotes2");

        //APPOINTMENT OF SUBSTITUTE TRUSTEE 3

        Regex id_appofsubtrus3 = new Regex("@appofsubtrus3");
        Regex id_appointed3 = new Regex("@appointed3");
        Regex id_executedby3 = new Regex("@executedby3");
        Regex id_appda3 = new Regex("@appda3");
        Regex id_appf3 = new Regex("@appf3");
        Regex id_appv3 = new Regex("@appv3");
        Regex id_appp3 = new Regex("@appp3");
        Regex id_appins3 = new Regex("@appins3");
        Regex id_appnotes3 = new Regex("@appnotes3");

        //APPOINTMENT OF SUBSTITUTE TRUSTEE 4

        Regex id_appofsubtrus4 = new Regex("@appofsubtrus4");
        Regex id_appointed4 = new Regex("@appointed4");
        Regex id_executedby4 = new Regex("@executedby4");
        Regex id_appda4 = new Regex("@appda4");
        Regex id_appf4 = new Regex("@appf4");
        Regex id_appv4 = new Regex("@appv4");
        Regex id_appp4 = new Regex("@appp4");
        Regex id_appins4 = new Regex("@appins4");
        Regex id_appnotes4 = new Regex("@appnotes4");

        //APPOINTMENT OF SUBSTITUTE TRUSTEE 5

        Regex id_appofsubtrus5 = new Regex("@appofsubtrus5");
        Regex id_appointed5 = new Regex("@appointed5");
        Regex id_executedby5 = new Regex("@executedby5");
        Regex id_appda5 = new Regex("@appda5");
        Regex id_appf5 = new Regex("@appf5");
        Regex id_appv5 = new Regex("@appv5");
        Regex id_appp5 = new Regex("@appp5");
        Regex id_appins5 = new Regex("@appins5");
        Regex id_appnotes5 = new Regex("@appnotes5");



        //ASSIGNMENT OF RENTS 1

        Regex id_assofrents = new Regex("@ofrents1s");
        Regex id_lender1 = new Regex("@lender1");
        Regex id_assgrantor1 = new Regex("assgrantor1s");
        Regex id_arda1 = new Regex("@arda1");
        Regex id_arf1 = new Regex("@arf1");
        Regex id_arv1 = new Regex("@arv1");
        Regex id_arp1 = new Regex("@arp1");
        Regex id_arins1 = new Regex("@arins1");
        Regex id_arnotes1 = new Regex("@arnotes1");

        //ASSIGNMENT OF RENTS 2

        Regex id_assofrents2 = new Regex("@ofrents2");
        Regex id_lender2 = new Regex("@lender2");
        Regex id_assgrantor2 = new Regex("@assgrantor2");
        Regex id_arda2 = new Regex("@arda2");
        Regex id_arf2 = new Regex("@arf2");
        Regex id_arv2 = new Regex("@arv2");
        Regex id_arp2 = new Regex("@arp2");
        Regex id_arins2 = new Regex("@arins2");
        Regex id_arnotes2 = new Regex("@arnotes2");

        //ASSIGNMENT OF RENTS 3

        Regex id_assofrents3 = new Regex("@ofrents3");
        Regex id_lender3 = new Regex("@lender3");
        Regex id_assgrantor3 = new Regex("@assgrantor3");
        Regex id_arda3 = new Regex("@arda3");
        Regex id_arf3 = new Regex("@arf3");
        Regex id_arv3 = new Regex("@arv3");
        Regex id_arp3 = new Regex("@arp3");
        Regex id_arins3 = new Regex("@arins3");
        Regex id_arnotes3 = new Regex("@arnotes3");

        //ASSIGNMENT OF RENTS 4

        Regex id_assofrents4 = new Regex("@ofrents4");
        Regex id_lender4 = new Regex("@lender4");
        Regex id_assgrantor4 = new Regex("@assgrantor4");
        Regex id_arda4 = new Regex("@arda4");
        Regex id_arf4 = new Regex("@arf4");
        Regex id_arv4 = new Regex("@arv4");
        Regex id_arp4 = new Regex("@arp4");
        Regex id_arins4 = new Regex("@arins4");
        Regex id_arnotes4 = new Regex("@arnotes4");

        //ASSIGNMENT OF RENTS 5

        Regex id_assofrents5 = new Regex("@ofrents5");
        Regex id_lender5 = new Regex("@lender5");
        Regex id_assgrantor5 = new Regex("@assgrantor5");
        Regex id_arda5 = new Regex("@arda5");
        Regex id_arf5 = new Regex("@arf5");
        Regex id_arv5 = new Regex("@arv5");
        Regex id_arp5 = new Regex("@arp5");
        Regex id_arins5 = new Regex("@arins5");
        Regex id_arnotes5 = new Regex("@arnotes5");



        //ASSIGNMENT 1

        Regex id_ass = new Regex("ass1s");
        Regex id_aassignee1 = new Regex("@aassignee1");
        Regex id_aassignor1 = new Regex("@aassignor1");
        Regex id_ada1 = new Regex("@ada1");
        Regex id_af1 = new Regex("@af1");
        Regex id_av1 = new Regex("@av1");
        Regex id_ap1 = new Regex("@ap1");
        Regex id_ains1 = new Regex("@ains1");
        Regex id_anotes1 = new Regex("@anotes1");

        //ASSIGNMENT 2

        Regex id_ass2 = new Regex("@ass2");
        Regex id_aassignee2 = new Regex("@aassignee2");
        Regex id_aassignor2 = new Regex("@aassignor2");
        Regex id_ada2 = new Regex("@ada2");
        Regex id_af2 = new Regex("@af2");
        Regex id_av2 = new Regex("@av2");
        Regex id_ap2 = new Regex("@ap2");
        Regex id_ains2 = new Regex("@ains2");
        Regex id_anotes2 = new Regex("@anotes2");

        //ASSIGNMENT 3

        Regex id_ass3 = new Regex("@ass3");
        Regex id_aassignee3 = new Regex("@aassignee3");
        Regex id_aassignor3 = new Regex("@aassignor3");
        Regex id_ada3 = new Regex("@ada3");
        Regex id_af3 = new Regex("@af3");
        Regex id_av3 = new Regex("@av3");
        Regex id_ap3 = new Regex("@ap3");
        Regex id_ains3 = new Regex("@ains3");
        Regex id_anotes3 = new Regex("@anotes3");

        //ASSIGNMENT 4

        Regex id_ass4 = new Regex("@ass4");
        Regex id_aassignee4 = new Regex("@aassignee4");
        Regex id_aassignor4 = new Regex("@aassignor4");
        Regex id_ada4 = new Regex("@ada4");
        Regex id_af4 = new Regex("@af4");
        Regex id_av4 = new Regex("@av4");
        Regex id_ap4 = new Regex("@ap4");
        Regex id_ains4 = new Regex("@ains4");
        Regex id_anotes4 = new Regex("@anotes4");

        //ASSIGNMENT 5

        Regex id_ass5 = new Regex("@ass5");
        Regex id_aassignee5 = new Regex("@aassignee5");
        Regex id_aassignor5 = new Regex("@aassignor5");
        Regex id_ada5 = new Regex("@ada5");
        Regex id_af5 = new Regex("@af5");
        Regex id_av5 = new Regex("@av5");
        Regex id_ap5 = new Regex("@ap5");
        Regex id_ains5 = new Regex("@ains5");
        Regex id_anotes5 = new Regex("@anotes5");





        //DEED OF TRUST 

        Regex id_deedoftrust1 = new Regex("@oftrust1");
        Regex id_dotpayable1 = new Regex("@dotpayable1");
        Regex id_dotgrantor1 = new Regex("@dotgrantor1");
        Regex id_dottrustee1 = new Regex("@dottrustee1");
        Regex id_dotda1 = new Regex("@dotda1");
        Regex id_dotf1 = new Regex("@dotf1");
        Regex id_dotv1 = new Regex("@dotv1");
        Regex id_dotp1 = new Regex("@dotp1");
        Regex id_dotins1 = new Regex("@dotins1");
        Regex id_dotamount1 = new Regex("@dotamount1");

        //DEED OF TRUST 2

        Regex id_deedoftrust2 = new Regex("@oftrust2");
        Regex id_dotpayable2 = new Regex("@dotpayable2");
        Regex id_dotgrantor2 = new Regex("@dotgrantor2");
        Regex id_dottrustee2 = new Regex("@dottrustee2");
        Regex id_dotda2 = new Regex("@dotda2");
        Regex id_dotf2 = new Regex("@dotf2");
        Regex id_dotv2 = new Regex("@dotv2");
        Regex id_dotp2 = new Regex("@dotp2");
        Regex id_dotins2 = new Regex("@dotins2");
        Regex id_dotamount2 = new Regex("@dotamount2");

        //DEED OF TRUST 3

        Regex id_deedoftrust3 = new Regex("@oftrust3");
        Regex id_dotpayable3 = new Regex("@dotpayable3");
        Regex id_dotgrantor3 = new Regex("@dotgrantor3");
        Regex id_dottrustee3 = new Regex("@dottrustee3");
        Regex id_dotda3 = new Regex("@dotda3");
        Regex id_dotf3 = new Regex("@dotf3");
        Regex id_dotv3 = new Regex("@dotv3");
        Regex id_dotp3 = new Regex("@dotp3");
        Regex id_dotins3 = new Regex("@dotins3");
        Regex id_dotamount3 = new Regex("@dotamount3");

        //DEED OF TRUST 4

        Regex id_deedoftrust4 = new Regex("@oftrust4");
        Regex id_dotpayable4 = new Regex("@dotpayable4");
        Regex id_dotgrantor4 = new Regex("@dotgrantor4");
        Regex id_dottrustee4 = new Regex("@dottrustee4");
        Regex id_dotda4 = new Regex("@dotda4");
        Regex id_dotf4 = new Regex("@dotf4");
        Regex id_dotv4 = new Regex("@dotv4");
        Regex id_dotp4 = new Regex("@dotp4");
        Regex id_dotins4 = new Regex("@dotins4");
        Regex id_dotamount4 = new Regex("@dotamount4");

        //DEED OF TRUST 5

        Regex id_deedoftrust5 = new Regex("@oftrust5");
        Regex id_dotpayable5 = new Regex("@dotpayable5");
        Regex id_dotgrantor5 = new Regex("@dotgrantor5");
        Regex id_dottrustee5 = new Regex("@dottrustee5");
        Regex id_dotda5 = new Regex("@dotda5");
        Regex id_dotf5 = new Regex("@dotf5");
        Regex id_dotv5 = new Regex("@dotv5");
        Regex id_dotp5 = new Regex("@dotp5");
        Regex id_dotins5 = new Regex("@dotins5");
        Regex id_dotamount5 = new Regex("@dotamount5");



        //LOAN MODIFICATION 1

        Regex id_loanmodify = new Regex("@loanmodify1");
        Regex id_modifybtwn = new Regex("@modifybtwn1");
        Regex id_mda1 = new Regex("@mda1");
        Regex id_mf1 = new Regex("@mf1");
        Regex id_mv1 = new Regex("@mv1");
        Regex id_mp1 = new Regex("@mp1");
        Regex id_mins1 = new Regex("@mins1");
        Regex id_mnotes1 = new Regex("@mnotes1");


        //LOAN MODIFICATION 2

        Regex id_loanmodify2 = new Regex("@loanmodify2");
        Regex id_modifybtwn2 = new Regex("@modifybtwn2");
        Regex id_mda2 = new Regex("@mda12");
        Regex id_mf2 = new Regex("@mf2");
        Regex id_mv2 = new Regex("@mv2");
        Regex id_mp2 = new Regex("@mp2");
        Regex id_mins2 = new Regex("@mins2");
        Regex id_mnotes2 = new Regex("@mnotes2");

        //LOAN MODIFICATION 3

        Regex id_loanmodify3 = new Regex("@loanmodify3");
        Regex id_modifybtwn3 = new Regex("@modifybtwn3");
        Regex id_mda3 = new Regex("@mda3");
        Regex id_mf3 = new Regex("@mf3");
        Regex id_mv3 = new Regex("@mv3");
        Regex id_mp3 = new Regex("@mp3");
        Regex id_mins3 = new Regex("@mins3");
        Regex id_mnotes3 = new Regex("@mnotes3");

        //LOAN MODIFICATION 4

        Regex id_loanmodify4 = new Regex("@loanmodify4");
        Regex id_modifybtwn4 = new Regex("@modifybtwn4");
        Regex id_mda4 = new Regex("@mda4");
        Regex id_mf4 = new Regex("@mf4");
        Regex id_mv4 = new Regex("@mv4");
        Regex id_mp4 = new Regex("@mp4");
        Regex id_mins4 = new Regex("@mins4");
        Regex id_mnotes4 = new Regex("@mnotes4");

        //LOAN MODIFICATION 5

        Regex id_loanmodify5 = new Regex("@loanmodify5");
        Regex id_modifybtwn5 = new Regex("@modifybtwn5");
        Regex id_mda5 = new Regex("@mda5");
        Regex id_mf5 = new Regex("@mf5");
        Regex id_mv5 = new Regex("@mv5");
        Regex id_mp5 = new Regex("@mp5");
        Regex id_mins5 = new Regex("@mins5");
        Regex id_mnotes5 = new Regex("@mnotes5");



        //SUBORDINATE DEED OF TRUST  1

        Regex id_subdee = new Regex("subdeed1s");
        Regex id_sdotpayable1 = new Regex("@sdotpayable1");
        Regex id_sdotgrantor1 = new Regex("@sdotgrantor1");
        Regex id_sdottrustee1 = new Regex("@sdottrustee1");
        Regex id_sdda1 = new Regex("@sdda1");
        Regex id_sdf1 = new Regex("@sdf1");
        Regex id_sdv1 = new Regex("@sdv1");
        Regex id_sdp1 = new Regex("@sdp1");
        Regex id_sdins1 = new Regex("@sdins1");
        Regex id_sdotamount1 = new Regex("@sdotamount1");


        //SUBORDINATE DEED OF TRUST  2

        Regex id_subdee2 = new Regex("@subdeed2");
        Regex id_sdotpayable2 = new Regex("@sdotpayable2");
        Regex id_sdotgrantor2 = new Regex("@sdotgrantor2");
        Regex id_sdottrustee2 = new Regex("@sdottrustee2");
        Regex id_sdda2 = new Regex("@sdda2");
        Regex id_sdf2 = new Regex("@sdf2");
        Regex id_sdv2 = new Regex("@sdv2");
        Regex id_sdp2 = new Regex("@sdp2");
        Regex id_sdins2 = new Regex("@sdins2");
        Regex id_sdotamount2 = new Regex("@sdotamount2");

        //SUBORDINATE DEED OF TRUST  3

        Regex id_subdee3 = new Regex("@subdeed3");
        Regex id_sdotpayable3 = new Regex("@sdotpayable3");
        Regex id_sdotgrantor3 = new Regex("@sdotgrantor3");
        Regex id_sdottrustee3 = new Regex("@sdottrustee3");
        Regex id_sdda3 = new Regex("@sdda3");
        Regex id_sdf3 = new Regex("@sdf3");
        Regex id_sdv3 = new Regex("@sdv3");
        Regex id_sdp3 = new Regex("@sdp3");
        Regex id_sdins3 = new Regex("@sdins3");
        Regex id_sdotamount3 = new Regex("@sdotamount3");

        //SUBORDINATE DEED OF TRUST  4

        Regex id_subdee4 = new Regex("@subdeed4");
        Regex id_sdotpayable4 = new Regex("@sdotpayable4");
        Regex id_sdotgrantor4 = new Regex("@sdotgrantor4");
        Regex id_sdottrustee4 = new Regex("@sdottrustee4");
        Regex id_sdda4 = new Regex("@sdda4");
        Regex id_sdf4 = new Regex("@sdf4");
        Regex id_sdv4 = new Regex("@sdv4");
        Regex id_sdp4 = new Regex("@sdp4");
        Regex id_sdins4 = new Regex("@sdins4");
        Regex id_sdotamount4 = new Regex("@sdotamount4");

        //SUBORDINATE DEED OF TRUST  5

        Regex id_subdee5 = new Regex("@subdeed5");
        Regex id_sdotpayable5 = new Regex("@sdotpayable5");
        Regex id_sdotgrantor5 = new Regex("@sdotgrantor5");
        Regex id_sdottrustee5 = new Regex("@sdottrustee5");
        Regex id_sdda5 = new Regex("@sdda5");
        Regex id_sdf5 = new Regex("@sdf5");
        Regex id_sdv5 = new Regex("@sdv5");
        Regex id_sdp5 = new Regex("@sdp5");
        Regex id_sdins5 = new Regex("@sdins5");
        Regex id_sdotamount5 = new Regex("@sdotamount5");



        //FINANCING STATEMENT 1

        Regex id_finanstate = new Regex("@finanstate1");
        Regex id_finsecured = new Regex("@finsecured1");
        Regex id_findebtor = new Regex("@findebtor1");
        Regex id_fda1 = new Regex("@fda1");
        Regex id_ff1 = new Regex("@ff1");
        Regex id_fv1 = new Regex("@fv1");
        Regex id_fp1 = new Regex("@fp1");
        Regex id_fins1 = new Regex("@fins1");
        Regex id_fnotes1 = new Regex("@fnotes1");

        //FINANCING STATEMENT 2

        Regex id_finanstate2 = new Regex("@finanstate2");
        Regex id_finsecured2 = new Regex("@finsecured2");
        Regex id_findebtor2 = new Regex("@findebtor2");
        Regex id_fda2 = new Regex("@fda2");
        Regex id_ff2 = new Regex("@ff2");
        Regex id_fv2 = new Regex("@fv2");
        Regex id_fp2 = new Regex("@fp2");
        Regex id_fins2 = new Regex("@fins2");
        Regex id_fnotes2 = new Regex("@fnotes2");

        //FINANCING STATEMENT 3

        Regex id_finanstate3 = new Regex("@finanstate3");
        Regex id_finsecured3 = new Regex("@finsecured3");
        Regex id_findebtor3 = new Regex("@findebtor3");
        Regex id_fda3 = new Regex("@fda3");
        Regex id_ff3 = new Regex("@ff3");
        Regex id_fv3 = new Regex("@fv3");
        Regex id_fp3 = new Regex("@fp3");
        Regex id_fins3 = new Regex("@fins3");
        Regex id_fnotes3 = new Regex("@fnotes3");

        //FINANCING STATEMENT 4

        Regex id_finanstate4 = new Regex("@finanstate4");
        Regex id_finsecured4 = new Regex("@finsecured4");
        Regex id_findebtor4 = new Regex("@findebtor4");
        Regex id_fda4 = new Regex("@fda4");
        Regex id_ff4 = new Regex("@ff4");
        Regex id_fv4 = new Regex("@fv4");
        Regex id_fp4 = new Regex("@fp4");
        Regex id_fins4 = new Regex("@fins4");
        Regex id_fnotes4 = new Regex("@fnotes4");

        //FINANCING STATEMENT 5

        Regex id_finanstate5 = new Regex("@finanstate5");
        Regex id_finsecured5 = new Regex("@finsecured5");
        Regex id_findebtor5 = new Regex("@findebtor5");
        Regex id_fda5 = new Regex("@fda5");
        Regex id_ff5 = new Regex("@ff5");
        Regex id_fv5 = new Regex("@fv5");
        Regex id_fp5 = new Regex("@fp5");
        Regex id_fins5 = new Regex("@fins5");
        Regex id_fnotes5 = new Regex("@fnotes5");



        #endregion

        #region Judgement ID

        // ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION 1
        Regex id_abstractofasstax1 = new Regex("abstractofasstax1s");
        Regex id_jaataxpayer1 = new Regex("@jaataxpayer1");
        Regex id_jaaadress1 = new Regex("@jaaadress1");
        Regex id_jaataxpayerid1 = new Regex("@jaataxpayerid1");
        Regex id_jaada1 = new Regex("@jaada1");
        Regex id_jaaf1 = new Regex("@jaaf1");
        Regex id_jaav1 = new Regex("@jaav1");
        Regex id_jaap1 = new Regex("@jaap1");
        Regex id_jaains1 = new Regex("@jaains1");
        Regex id_jaanotes1 = new Regex("@jaanotes1");

        // ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION 2
        Regex id_abstractofasstax2 = new Regex("@abstractofasstax2");
        Regex id_jaataxpayer2 = new Regex("@jaataxpayer2");
        Regex id_jaaadress2 = new Regex("@jaaadress2");
        Regex id_jaataxpayerid2 = new Regex("@jaataxpayerid2");
        Regex id_jaada2 = new Regex("@jaada2");
        Regex id_jaaf2 = new Regex("@jaaf2");
        Regex id_jaav2 = new Regex("@jaav2");
        Regex id_jaap2 = new Regex("@jaap2");
        Regex id_jaains2 = new Regex("@jaains2");
        Regex id_jaanotes2 = new Regex("@jaanotes2");

        // ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION 3
        Regex id_abstractofasstax3 = new Regex("@abstractofasstax3");
        Regex id_jaataxpayer3 = new Regex("@jaataxpayer3");
        Regex id_jaaadress3 = new Regex("@jaaadress3");
        Regex id_jaataxpayerid3 = new Regex("@jaataxpayerid3");
        Regex id_jaada3 = new Regex("@jaada3");
        Regex id_jaaf3 = new Regex("@jaaf3");
        Regex id_jaav3 = new Regex("@jaav3");
        Regex id_jaap3 = new Regex("@jaap3");
        Regex id_jaains3 = new Regex("@jaains3");
        Regex id_jaanotes3 = new Regex("@jaanotes3");

        // ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION 4
        Regex id_abstractofasstax4 = new Regex("@abstractofasstax4");
        Regex id_jaataxpayer4 = new Regex("@jaataxpayer4");
        Regex id_jaaadress4 = new Regex("@jaaadress4");
        Regex id_jaataxpayerid4 = new Regex("@jaataxpayerid4");
        Regex id_jaada4 = new Regex("@jaada4");
        Regex id_jaaf4 = new Regex("@jaaf4");
        Regex id_jaav4 = new Regex("@jaav4");
        Regex id_jaap4 = new Regex("@jaap4");
        Regex id_jaains4 = new Regex("@jaains4");
        Regex id_jaanotes4 = new Regex("@jaanotes4");

        //// ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION 5
        //Regex id_abstractofasstax5 = new Regex("@abstractofasstax5");
        //Regex id_jaataxpayer5 = new Regex("@jaataxpayer5");
        //Regex id_jaaadress5 = new Regex("@jaaadress5");
        //Regex id_jaataxpayerid5 = new Regex("@jaataxpayerid5");
        //Regex id_jaada5 = new Regex("@jaada5");
        //Regex id_jaaf5 = new Regex("@jaaf5");
        //Regex id_jaav5 = new Regex("@jaav5");
        //Regex id_jaap5 = new Regex("@jaap5");
        //Regex id_jaains5 = new Regex("@jaains5");
        //Regex id_jaanotes5 = new Regex("@jaanotes5");



        // ABSTRACT OF JUDGMENT 1
        Regex id_abstractofjudg1 = new Regex("abstractofjudg1s");
        Regex id_jajdeffendant1 = new Regex("@jajdeffendant1");
        Regex id_jajaddress1 = new Regex("@jajaddress1");
        Regex id_jajplaintiff1 = new Regex("@jajplaintiff1");
        Regex id_jajda1 = new Regex("@jajda1");
        Regex id_jajf1 = new Regex("@jajf1");
        Regex id_jajv1 = new Regex("@jajv1");
        Regex id_jajp1 = new Regex("@jajp1");
        Regex id_jajins1 = new Regex("@jajins1");
        Regex id_jajam1 = new Regex("@jajam1");
        Regex id_jajc1 = new Regex("@jajc1");
        Regex id_jajat1 = new Regex("@jajat1");
        Regex id_jajint1 = new Regex("@jajint1");
        Regex id_jajcause1 = new Regex("@jajcause1");

        // ABSTRACT OF JUDGMENT 2
        Regex id_abstractofjudg2 = new Regex("@abstractofjudg2");
        Regex id_jajdeffendant2 = new Regex("@jajdeffendant2");
        Regex id_jajaddress2 = new Regex("@jajaddress2");
        Regex id_jajplaintiff2 = new Regex("@jajplaintiff2");
        Regex id_jajda2 = new Regex("@jajda2");
        Regex id_jajf2 = new Regex("@jajf2");
        Regex id_jajv2 = new Regex("@jajv2");
        Regex id_jajp2 = new Regex("@jajp2");
        Regex id_jajins2 = new Regex("@jajins2");
        Regex id_jajam2 = new Regex("@jajam2");
        Regex id_jajc2 = new Regex("@jajc2");
        Regex id_jajat2 = new Regex("@jajat2");
        Regex id_jajint2 = new Regex("@jajint2");
        Regex id_jajcause2 = new Regex("@jajcause2");

        // ABSTRACT OF JUDGMENT 3
        Regex id_abstractofjudg3 = new Regex("@abstractofjudg3");
        Regex id_jajdeffendant3 = new Regex("@jajdeffendant3");
        Regex id_jajaddress3 = new Regex("@jajaddress3");
        Regex id_jajplaintiff3 = new Regex("@jajplaintiff3");
        Regex id_jajda3 = new Regex("@jajda3");
        Regex id_jajf3 = new Regex("@jajf3");
        Regex id_jajv3 = new Regex("@jajv3");
        Regex id_jajp3 = new Regex("@jajp3");
        Regex id_jajins3 = new Regex("@jajins3");
        Regex id_jajam3 = new Regex("@jajam3");
        Regex id_jajc3 = new Regex("@jajc3");
        Regex id_jajat3 = new Regex("@jajat3");
        Regex id_jajint3 = new Regex("@jajint3");
        Regex id_jajcause3 = new Regex("@jajcause3");

        // ABSTRACT OF JUDGMENT 4
        Regex id_abstractofjudg4 = new Regex("@abstractofjudg4");
        Regex id_jajdeffendant4 = new Regex("@jajdeffendant4");
        Regex id_jajaddress4 = new Regex("@jajaddress4");
        Regex id_jajplaintiff4 = new Regex("@jajplaintiff4");
        Regex id_jajda4 = new Regex("@jajda4");
        Regex id_jajf4 = new Regex("@jajf4");
        Regex id_jajv4 = new Regex("@jajv4");
        Regex id_jajp4 = new Regex("@jajp4");
        Regex id_jajins4 = new Regex("@jajins4");
        Regex id_jajam4 = new Regex("@jajam4");
        Regex id_jajc4 = new Regex("@jajc4");
        Regex id_jajat4 = new Regex("@jajat4");
        Regex id_jajint4 = new Regex("@jajint4");
        Regex id_jajcause4 = new Regex("@jajcause4");

        //// ABSTRACT OF JUDGMENT 5
        //Regex id_abstractofjudg5 = new Regex("@abstractofjudg5");
        //Regex id_jajdeffendant5 = new Regex("@jajdeffendant5");
        //Regex id_jajaddress5 = new Regex("@jajaddress5");
        //Regex id_jajplaintiff5 = new Regex("@jajplaintiff5");
        //Regex id_jajda5 = new Regex("@jajda5");
        //Regex id_jajf5 = new Regex("@jajf5");
        //Regex id_jajv5 = new Regex("@jajv5");
        //Regex id_jajp5 = new Regex("@jajp5");
        //Regex id_jajins5 = new Regex("@jajins5");
        //Regex id_jajam5 = new Regex("@jajam5");
        //Regex id_jajc5 = new Regex("@jajc5");
        //Regex id_jajat5 = new Regex("@jajat5");
        //Regex id_jajint5 = new Regex("@jajint5");
        //Regex id_jajcause5 = new Regex("@jajcause5");



        // AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN  1
        Regex id_affofdelinquent1 = new Regex("@affofdelinquent1");
        Regex id_jadowner1 = new Regex("@jadowner1");
        Regex id_jadgrantor1 = new Regex("@jadgrantor1");
        Regex id_jadda1 = new Regex("@jadda1");
        Regex id_jadf1 = new Regex("@jadf1");
        Regex id_jadv1 = new Regex("@jadv1");
        Regex id_jadp1 = new Regex("@jadp1");
        Regex id_jadins1 = new Regex("@jadins1");
        Regex id_jadamount1 = new Regex("@jadamount1");



        // AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN  2
        Regex id_affofdelinquent2 = new Regex("@affofdelinquent2");
        Regex id_jadowner2 = new Regex("@jadowner2");
        Regex id_jadgrantor2 = new Regex("@jadgrantor2");
        Regex id_jadda2 = new Regex("@jadda2");
        Regex id_jadf2 = new Regex("@jadf2");
        Regex id_jadv2 = new Regex("@jadv2");
        Regex id_jadp2 = new Regex("@jadp2");
        Regex id_jadins2 = new Regex("@jadins2");
        Regex id_jadamount2 = new Regex("@jadamount2");

        // AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN  3
        Regex id_affofdelinquent3 = new Regex("@affofdelinquent3");
        Regex id_jadowner3 = new Regex("@jadowner3");
        Regex id_jadgrantor3 = new Regex("@jadgrantor3");
        Regex id_jadda3 = new Regex("@jadda3");
        Regex id_jadf3 = new Regex("@jadf3");
        Regex id_jadv3 = new Regex("@jadv3");
        Regex id_jadp3 = new Regex("@jadp3");
        Regex id_jadins3 = new Regex("@jadins3");
        Regex id_jadamount3 = new Regex("@jadamount3");

        // AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN  4
        Regex id_affofdelinquent4 = new Regex("@affofdelinquent4");
        Regex id_jadowner4 = new Regex("@jadowner4");
        Regex id_jadgrantor4 = new Regex("@jadgrantor4");
        Regex id_jadda4 = new Regex("@jadda4");
        Regex id_jadf4 = new Regex("@jadf4");
        Regex id_jadv4 = new Regex("@jadv4");
        Regex id_jadp4 = new Regex("@jadp4");
        Regex id_jadins4 = new Regex("@jadins4");
        Regex id_jadamount4 = new Regex("@jadamount4");

        //// AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN  5
        //Regex id_affofdelinquent5 = new Regex("@affofdelinquent5");
        //Regex id_jadowner5 = new Regex("@jadowner5");
        //Regex id_jadgrantor5 = new Regex("@jadgrantor5");
        //Regex id_jadda5 = new Regex("@jadda5");
        //Regex id_jadf5 = new Regex("@jadf5");
        //Regex id_jadv5 = new Regex("@jadv5");
        //Regex id_jadp5 = new Regex("@jadp5");
        //Regex id_jadins5 = new Regex("@jadins5");
        //Regex id_jadamount5 = new Regex("@jadamount5");


        // AFFIDAVIT TO FIX LIEN   1
        Regex id_afftofixlien1 = new Regex("@afftofixlien1");
        Regex id_jalowner1 = new Regex("@jalowner1");
        Regex id_jalgrantor1 = new Regex("@jalgrantor1");
        Regex id_jalda1 = new Regex("@jalda1");
        Regex id_jalf1 = new Regex("@jalf1");
        Regex id_jalv1 = new Regex("@jalv1");
        Regex id_jalp1 = new Regex("@jalp1");
        Regex id_jalins1 = new Regex("@jalins1");
        Regex id_jalamount1 = new Regex("@jalamount1");

        // AFFIDAVIT TO FIX LIEN   2
        Regex id_afftofixlien2 = new Regex("@afftofixlien2");
        Regex id_jalowner2 = new Regex("@jalowner2");
        Regex id_jalgrantor2 = new Regex("@jalgrantor2");
        Regex id_jalda2 = new Regex("@jalda2");
        Regex id_jalf2 = new Regex("@jalf2");
        Regex id_jalv2 = new Regex("@jalv2");
        Regex id_jalp2 = new Regex("@jalp2");
        Regex id_jalins2 = new Regex("@jalins2");
        Regex id_jalamount2 = new Regex("@jalamount2");

        // AFFIDAVIT TO FIX LIEN   3
        Regex id_afftofixlien3 = new Regex("@afftofixlien3");
        Regex id_jalowner3 = new Regex("@jalowner3");
        Regex id_jalgrantor3 = new Regex("@jalgrantor3");
        Regex id_jalda3 = new Regex("@jalda3");
        Regex id_jalf3 = new Regex("@jalf3");
        Regex id_jalv3 = new Regex("@jalv3");
        Regex id_jalp3 = new Regex("@jalp3");
        Regex id_jalins3 = new Regex("@jalins3");
        Regex id_jalamount3 = new Regex("@jalamount3");

        // AFFIDAVIT TO FIX LIEN   4
        Regex id_afftofixlien4 = new Regex("@afftofixlien4");
        Regex id_jalowner4 = new Regex("@jalowner4");
        Regex id_jalgrantor4 = new Regex("@jalgrantor4");
        Regex id_jalda4 = new Regex("@jalda4");
        Regex id_jalf4 = new Regex("@jalf4");
        Regex id_jalv4 = new Regex("@jalv4");
        Regex id_jalp4 = new Regex("@jalp4");
        Regex id_jalins4 = new Regex("@jalins4");
        Regex id_jalamount4 = new Regex("@jalamount4");

        //// AFFIDAVIT TO FIX LIEN   5
        //Regex id_afftofixlien5 = new Regex("@afftofixlien5");
        //Regex id_jalowner5 = new Regex("@jalowner5");
        //Regex id_jalgrantor5 = new Regex("@jalgrantor5");
        //Regex id_jalda5 = new Regex("@jalda5");
        //Regex id_jalf5 = new Regex("@jalf5");
        //Regex id_jalv5 = new Regex("@jalv5");
        //Regex id_jalp5 = new Regex("@jalp5");
        //Regex id_jalins5 = new Regex("@jalins5");
        //Regex id_jalamount5 = new Regex("@jalamount5");



        // FEDERAL TAX LIEN  1
        Regex id_federaltaxlien1 = new Regex("@federaltaxlien1");
        Regex id_jfltaxpayer1 = new Regex("@jfltaxpayer1");
        Regex id_jfladdress1 = new Regex("@jfladdress1");
        Regex id_jfltaxpayerid1 = new Regex("@jaataxpayerid1");
        Regex id_jflda1 = new Regex("@jflda1");
        Regex id_jflf1 = new Regex("@jflf1");
        Regex id_jflv1 = new Regex("@jflv1");
        Regex id_jflp1 = new Regex("@jflp1");
        Regex id_jflins1 = new Regex("@jflins1");
        Regex id_jflamount1 = new Regex("@jflamount1");


        // FEDERAL TAX LIEN  2
        Regex id_federaltaxlien2 = new Regex("@federaltaxlien2");
        Regex id_jfltaxpayer2 = new Regex("@jfltaxpayer2");
        Regex id_jfladdress2 = new Regex("@jfladdress2");
        Regex id_jfltaxpayerid2 = new Regex("@jaataxpayerid2");
        Regex id_jflda2 = new Regex("@jflda2");
        Regex id_jflf2 = new Regex("@jflf2");
        Regex id_jflv2 = new Regex("@jflv2");
        Regex id_jflp2 = new Regex("@jflp2");
        Regex id_jflins2 = new Regex("@jflins2");
        Regex id_jflamount2 = new Regex("@jflamount2");


        // FEDERAL TAX LIEN  3
        Regex id_federaltaxlien3 = new Regex("@federaltaxlien3");
        Regex id_jfltaxpayer3 = new Regex("@jfltaxpayer3");
        Regex id_jfladdress3 = new Regex("@jfladdress3");
        Regex id_jfltaxpayerid3 = new Regex("@jaataxpayerid3");
        Regex id_jflda3 = new Regex("@jflda3");
        Regex id_jflf3 = new Regex("@jflf3");
        Regex id_jflv3 = new Regex("@jflv3");
        Regex id_jflp3 = new Regex("@jflp3");
        Regex id_jflins3 = new Regex("@jflins3");
        Regex id_jflamount3 = new Regex("@jflamount3");


        // FEDERAL TAX LIEN  4
        Regex id_federaltaxlien4 = new Regex("@federaltaxlien4");
        Regex id_jfltaxpayer4 = new Regex("@jfltaxpayer4");
        Regex id_jfladdress4 = new Regex("@jfladdress4");
        Regex id_jfltaxpayerid4 = new Regex("@jaataxpayerid4");
        Regex id_jflda4 = new Regex("@jflda4");
        Regex id_jflf4 = new Regex("@jflf4");
        Regex id_jflv4 = new Regex("@jflv4");
        Regex id_jflp4 = new Regex("@jflp4");
        Regex id_jflins4 = new Regex("@jflins4");
        Regex id_jflamount4 = new Regex("@jflamount4");

        //// FEDERAL TAX LIEN  5
        //Regex id_federaltaxlien5 = new Regex("@federaltaxlien5");
        //Regex id_jfltaxpayer5 = new Regex("@jfltaxpayer5");
        //Regex id_jfladdress5 = new Regex("@jfladdress5");
        //Regex id_jfltaxpayerid5 = new Regex("@jaataxpayerid5");
        //Regex id_jflda5 = new Regex("@jflda5");
        //Regex id_jflf5 = new Regex("@jflf5");
        //Regex id_jflv5 = new Regex("@jflv5");
        //Regex id_jflp5 = new Regex("@jflp5");
        //Regex id_jflins5 = new Regex("@jflins5");
        //Regex id_jflamount5 = new Regex("@jflamount5");




        // LIEN CLAIM AFFIDAVIT  1
        Regex id_lienclaim1 = new Regex("@lienclaim1");
        Regex id_jlaowner1 = new Regex("@jlaowner1");
        Regex id_jlagrantor1 = new Regex("@jlagrantor1");
        Regex id_jlada1 = new Regex("@jlada1");
        Regex id_jlaf1 = new Regex("@jlaf1");
        Regex id_jlav1 = new Regex("@jlav1");
        Regex id_jlap1 = new Regex("@jlap1");
        Regex id_jlains1 = new Regex("@jlains1");
        Regex id_jlaamount1 = new Regex("@jlaamount1");
        Regex id_jlanotes1 = new Regex("@jlanotes1");


        // LIEN CLAIM AFFIDAVIT  2
        Regex id_lienclaim2 = new Regex("@lienclaim2");
        Regex id_jlaowner2 = new Regex("@jlaowner2");
        Regex id_jlagrantor2 = new Regex("@jlagrantor2");
        Regex id_jlada2 = new Regex("@jlada2");
        Regex id_jlaf2 = new Regex("@jlaf2");
        Regex id_jlav2 = new Regex("@jlav2");
        Regex id_jlap2 = new Regex("@jlap2");
        Regex id_jlains2 = new Regex("@jlains2");
        Regex id_jlaamount2 = new Regex("@jlaamount2");
        Regex id_jlanotes2 = new Regex("@jlanotes2");


        // LIEN CLAIM AFFIDAVIT  3
        Regex id_lienclaim3 = new Regex("@lienclaim3");
        Regex id_jlaowner3 = new Regex("@jlaowner3");
        Regex id_jlagrantor3 = new Regex("@jlagrantor3");
        Regex id_jlada3 = new Regex("@jlada3");
        Regex id_jlaf3 = new Regex("@jlaf3");
        Regex id_jlav3 = new Regex("@jlav3");
        Regex id_jlap3 = new Regex("@jlap3");
        Regex id_jlains3 = new Regex("@jlains3");
        Regex id_jlaamount3 = new Regex("@jlaamount3");
        Regex id_jlanotes3 = new Regex("@jlanotes3");


        // LIEN CLAIM AFFIDAVIT  4
        Regex id_lienclaim4 = new Regex("@lienclaim4");
        Regex id_jlaowner4 = new Regex("@jlaowner4");
        Regex id_jlagrantor4 = new Regex("@jlagrantor4");
        Regex id_jlada4 = new Regex("@jlada4");
        Regex id_jlaf4 = new Regex("@jlaf4");
        Regex id_jlav4 = new Regex("@jlav4");
        Regex id_jlap4 = new Regex("@jlap4");
        Regex id_jlains4 = new Regex("@jlains4");
        Regex id_jlaamount4 = new Regex("@jlaamount4");
        Regex id_jlanotes4 = new Regex("@jlanotes4");


        //// LIEN CLAIM AFFIDAVIT  5
        //Regex id_lienclaim5 = new Regex("@lienclaim5");
        //Regex id_jlaowner5 = new Regex("@jlaowner5");
        //Regex id_jlagrantor5 = new Regex("@jlagrantor5");
        //Regex id_jlada5 = new Regex("@jlada5");
        //Regex id_jlaf5 = new Regex("@jlaf5");
        //Regex id_jlav5 = new Regex("@jlav5");
        //Regex id_jlap5 = new Regex("@jlap5");
        //Regex id_jlains5 = new Regex("@jlains5");
        //Regex id_jlaamount5 = new Regex("@jlaamount5");
        //Regex id_jlanotes5 = new Regex("@jlanotes5");





        // NOTICE OF ASSESSMENT LIEN  1
        Regex id_noticeofass1 = new Regex("@noticeofass1");
        Regex id_jnalowner1 = new Regex("@jnalowner1");
        Regex id_jnalgrantor1 = new Regex("@jnalgrantor1");
        Regex id_jnalda1 = new Regex("@jnalda1");
        Regex id_jnalf1 = new Regex("@jnalf1");
        Regex id_jnalv1 = new Regex("@jnalv1");
        Regex id_jnalp1 = new Regex("@jnalp1");
        Regex id_jnalins1 = new Regex("@jnalins1");
        Regex id_jnalamount1 = new Regex("@jnalamount1");

        // NOTICE OF ASSESSMENT LIEN  2
        Regex id_noticeofass2 = new Regex("@noticeofass2");
        Regex id_jnalowner2 = new Regex("@jnalowner2");
        Regex id_jnalgrantor2 = new Regex("@jnalgrantor2");
        Regex id_jnalda2 = new Regex("@jnalda2");
        Regex id_jnalf2 = new Regex("@jnalf2");
        Regex id_jnalv2 = new Regex("@jnalv2");
        Regex id_jnalp2 = new Regex("@jnalp2");
        Regex id_jnalins2 = new Regex("@jnalins2");
        Regex id_jnalamount2 = new Regex("@jnalamount2");

        // NOTICE OF ASSESSMENT LIEN  3
        Regex id_noticeofass3 = new Regex("@noticeofass3");
        Regex id_jnalowner3 = new Regex("@jnalowner3");
        Regex id_jnalgrantor3 = new Regex("@jnalgrantor3");
        Regex id_jnalda3 = new Regex("@jnalda3");
        Regex id_jnalf3 = new Regex("@jnalf3");
        Regex id_jnalv3 = new Regex("@jnalv3");
        Regex id_jnalp3 = new Regex("@jnalp3");
        Regex id_jnalins3 = new Regex("@jnalins3");
        Regex id_jnalamount3 = new Regex("@jnalamount3");

        // NOTICE OF ASSESSMENT LIEN  4
        Regex id_noticeofass4 = new Regex("@noticeofass4");
        Regex id_jnalowner4 = new Regex("@jnalowner4");
        Regex id_jnalgrantor4 = new Regex("@jnalgrantor4");
        Regex id_jnalda4 = new Regex("@jnalda4");
        Regex id_jnalf4 = new Regex("@jnalf4");
        Regex id_jnalv4 = new Regex("@jnalv4");
        Regex id_jnalp4 = new Regex("@jnalp4");
        Regex id_jnalins4 = new Regex("@jnalins4");
        Regex id_jnalamount4 = new Regex("@jnalamount4");

        //// NOTICE OF ASSESSMENT LIEN  5
        //Regex id_noticeofass5 = new Regex("@noticeofass5");
        //Regex id_jnalowner5 = new Regex("@jnalowner5");
        //Regex id_jnalgrantor5 = new Regex("@jnalgrantor5");
        //Regex id_jnalda5 = new Regex("@jnalda5");
        //Regex id_jnalf5 = new Regex("@jnalf5");
        //Regex id_jnalv5 = new Regex("@jnalv5");
        //Regex id_jnalp5 = new Regex("@jnalp5");
        //Regex id_jnalins5 = new Regex("@jnalins5");
        //Regex id_jnalamount5 = new Regex("@jnalamount5");



        // NOTICE OF CHILD SUPPORT LIEN  1
        Regex id_noticeofchild1 = new Regex("@noticeofchild1");
        Regex id_jnclobligor1 = new Regex("@jnclobligor1");
        Regex id_jncladdress1 = new Regex("@jncladdress1");
        Regex id_jnclssn1 = new Regex("@jnclssn1");
        Regex id_jnclobligee1 = new Regex("@jnclobligee1");
        Regex id_jncltribunal1 = new Regex("@jncltribunal1");
        Regex id_jnclda1 = new Regex("@jnclda1");
        Regex id_jnclf1 = new Regex("@jnclf1");
        Regex id_jnclv1 = new Regex("@jnclv1");
        Regex id_jnclp1 = new Regex("@jnclp1");
        Regex id_jnclins1 = new Regex("@jnclins1");
        Regex id_jnclamount1 = new Regex("@jnclamount1");


        // NOTICE OF CHILD SUPPORT LIEN  2
        Regex id_noticeofchild2 = new Regex("@noticeofchild2");
        Regex id_jnclobligor2 = new Regex("@jnclobligor2");
        Regex id_jncladdress2 = new Regex("@jncladdress2");
        Regex id_jnclssn2 = new Regex("@jnclssn2");
        Regex id_jnclobligee2 = new Regex("@jnclobligee2");
        Regex id_jncltribunal2 = new Regex("@jncltribunal2");
        Regex id_jnclda2 = new Regex("@jnclda2");
        Regex id_jnclf2 = new Regex("@jnclf2");
        Regex id_jnclv2 = new Regex("@jnclv2");
        Regex id_jnclp2 = new Regex("@jnclp2");
        Regex id_jnclins2 = new Regex("@jnclins2");
        Regex id_jnclamount2 = new Regex("@jnclamount2");


        // NOTICE OF CHILD SUPPORT LIEN  3
        Regex id_noticeofchild3 = new Regex("@noticeofchild3");
        Regex id_jnclobligor3 = new Regex("@jnclobligor3");
        Regex id_jncladdress3 = new Regex("@jncladdress3");
        Regex id_jnclssn3 = new Regex("@jnclssn3");
        Regex id_jnclobligee3 = new Regex("@jnclobligee3");
        Regex id_jncltribunal3 = new Regex("@jncltribunal3");
        Regex id_jnclda3 = new Regex("@jnclda3");
        Regex id_jnclf3 = new Regex("@jnclf3");
        Regex id_jnclv3 = new Regex("@jnclv3");
        Regex id_jnclp3 = new Regex("@jnclp3");
        Regex id_jnclins3 = new Regex("@jnclins3");
        Regex id_jnclamount3 = new Regex("@jnclamount3");


        // NOTICE OF CHILD SUPPORT LIEN  4
        Regex id_noticeofchild4 = new Regex("@noticeofchild4");
        Regex id_jnclobligor4 = new Regex("@jnclobligor4");
        Regex id_jncladdress4 = new Regex("@jncladdress4");
        Regex id_jnclssn4 = new Regex("@jnclssn4");
        Regex id_jnclobligee4 = new Regex("@jnclobligee4");
        Regex id_jncltribunal4 = new Regex("@jncltribunal4");
        Regex id_jnclda4 = new Regex("@jnclda4");
        Regex id_jnclf4 = new Regex("@jnclf4");
        Regex id_jnclv4 = new Regex("@jnclv4");
        Regex id_jnclp4 = new Regex("@jnclp4");
        Regex id_jnclins4 = new Regex("@jnclins4");
        Regex id_jnclamount4 = new Regex("@jnclamount4");

        //// NOTICE OF CHILD SUPPORT LIEN  5
        //Regex id_noticeofchild5 = new Regex("@noticeofchild5");
        //Regex id_jnclobligor5 = new Regex("@jnclobligor5");
        //Regex id_jncladdress5 = new Regex("@jncladdress5");
        //Regex id_jnclssn5 = new Regex("@jnclssn5");
        //Regex id_jnclobligee5 = new Regex("@jnclobligee5");
        //Regex id_jncltribunal5 = new Regex("@jncltribunal5");
        //Regex id_jnclda5 = new Regex("@jnclda5");
        //Regex id_jnclf5 = new Regex("@jnclf5");
        //Regex id_jnclv5 = new Regex("@jnclv5");
        //Regex id_jnclp5 = new Regex("@jnclp5");
        //Regex id_jnclins5 = new Regex("@jnclins5");
        //Regex id_jnclamount5 = new Regex("@jnclamount5");



        // NOTICE OF FORECLOSURE  1
        Regex id_noticeoffore1 = new Regex("@noticeoffore1");
        Regex id_jnfgrantee1 = new Regex("@jnfgrantee1");
        Regex id_jnfgrantor1 = new Regex("@jnfgrantor1");
        Regex id_jnfda1 = new Regex("@jnfda1");
        Regex id_jnff1 = new Regex("@jnff1");
        Regex id_jnfv1 = new Regex("@jnfv1");
        Regex id_jnfp1 = new Regex("@jnfp1");
        Regex id_jnfins1 = new Regex("@jnfins1");
        Regex id_jnfnotes1 = new Regex("@jnfnotes1");

        // NOTICE OF FORECLOSURE  2
        Regex id_noticeoffore2 = new Regex("@noticeoffore2");
        Regex id_jnfgrantee2 = new Regex("@jnfgrantee2");
        Regex id_jnfgrantor2 = new Regex("@jnfgrantor2");
        Regex id_jnfda2 = new Regex("@jnfda2");
        Regex id_jnff2 = new Regex("@jnff2");
        Regex id_jnfv2 = new Regex("@jnfv2");
        Regex id_jnfp2 = new Regex("@jnfp2");
        Regex id_jnfins2 = new Regex("@jnfins2");
        Regex id_jnfnotes2 = new Regex("@jnfnotes2");

        // NOTICE OF FORECLOSURE  3
        Regex id_noticeoffore3 = new Regex("@noticeoffore3");
        Regex id_jnfgrantee3 = new Regex("@jnfgrantee3");
        Regex id_jnfgrantor3 = new Regex("@jnfgrantor3");
        Regex id_jnfda3 = new Regex("@jnfda3");
        Regex id_jnff3 = new Regex("@jnff3");
        Regex id_jnfv3 = new Regex("@jnfv3");
        Regex id_jnfp3 = new Regex("@jnfp3");
        Regex id_jnfins3 = new Regex("@jnfins3");
        Regex id_jnfnotes3 = new Regex("@jnfnotes3");

        // NOTICE OF FORECLOSURE  4
        Regex id_noticeoffore4 = new Regex("@noticeoffore4");
        Regex id_jnfgrantee4 = new Regex("@jnfgrantee4");
        Regex id_jnfgrantor4 = new Regex("@jnfgrantor4");
        Regex id_jnfda4 = new Regex("@jnfda4");
        Regex id_jnff4 = new Regex("@jnff4");
        Regex id_jnfv4 = new Regex("@jnfv4");
        Regex id_jnfp4 = new Regex("@jnfp4");
        Regex id_jnfins4 = new Regex("@jnfins4");
        Regex id_jnfnotes4 = new Regex("@jnfnotes4");

        //// NOTICE OF FORECLOSURE  5
        //Regex id_noticeoffore5 = new Regex("@noticeoffore5");
        //Regex id_jnfgrantee5 = new Regex("@jnfgrantee5");
        //Regex id_jnfgrantor5 = new Regex("@jnfgrantor5");
        //Regex id_jnfda5 = new Regex("@jnfda5");
        //Regex id_jnff5 = new Regex("@jnff5");
        //Regex id_jnfv5 = new Regex("@jnfv5");
        //Regex id_jnfp5 = new Regex("@jnfp5");
        //Regex id_jnfins5 = new Regex("@jnfins5");
        //Regex id_jnfnotes5 = new Regex("@jnfnotes5");



        // NOTICE OF TRUSTEE SALE  1
        Regex id_noticeoftrus1 = new Regex("noticeoftrus1s");
        Regex id_jntsto1 = new Regex("@jntsto1");
        Regex id_jntsfrom1 = new Regex("@jntsfrom1");
        Regex id_jntsda1 = new Regex("@jntsda1");
        Regex id_jntsf1 = new Regex("@jntsf1");
        Regex id_jntsv1 = new Regex("@jntsv1");
        Regex id_jntsp1 = new Regex("@jntsp1");
        Regex id_jntsins1 = new Regex("@jntsins1");
        Regex id_jntsnotes1 = new Regex("@jntsnotes1");

        // NOTICE OF TRUSTEE SALE  2
        Regex id_noticeoftrus2 = new Regex("Noticeoftrus2s");
        Regex id_jntsto2 = new Regex("@jntsto2");
        Regex id_jntsfrom2 = new Regex("@jntsfrom2");
        Regex id_jntsda2 = new Regex("@jntsda2");
        Regex id_jntsf2 = new Regex("@jntsf2");
        Regex id_jntsv2 = new Regex("@jntsv2");
        Regex id_jntsp2 = new Regex("@jntsp2");
        Regex id_jntsins2 = new Regex("@jntsins2");
        Regex id_jntsnotes2 = new Regex("@jntsnotes2");

        // NOTICE OF TRUSTEE SALE  3
        Regex id_noticeoftrus3 = new Regex("Noticeoftrus3s");
        Regex id_jntsto3 = new Regex("@jntsto3");
        Regex id_jntsfrom3 = new Regex("@jntsfrom3");
        Regex id_jntsda3 = new Regex("@jntsda3");
        Regex id_jntsf3 = new Regex("@jntsf3");
        Regex id_jntsv3 = new Regex("@jntsv3");
        Regex id_jntsp3 = new Regex("@jntsp3");
        Regex id_jntsins3 = new Regex("@jntsins3");
        Regex id_jntsnotes3 = new Regex("@jntsnotes3");

        // NOTICE OF TRUSTEE SALE  4
        Regex id_noticeoftrus4 = new Regex("@noticeoftrus4");
        Regex id_jntsto4 = new Regex("@jntsto4");
        Regex id_jntsfrom4 = new Regex("@jntsfrom4");
        Regex id_jntsda4 = new Regex("@jntsda4");
        Regex id_jntsf4 = new Regex("@jntsf4");
        Regex id_jntsv4 = new Regex("@jntsv4");
        Regex id_jntsp4 = new Regex("@jntsp4");
        Regex id_jntsins4 = new Regex("@jntsins4");
        Regex id_jntsnotes4 = new Regex("@jntsnotes4");

        //// NOTICE OF TRUSTEE SALE  5
        //Regex id_noticeoftrus5 = new Regex("@noticeoftrus5");
        //Regex id_jntsto5 = new Regex("@jntsto5");
        //Regex id_jntsfrom5 = new Regex("@jntsfrom5");
        //Regex id_jntsda5 = new Regex("@jntsda5");
        //Regex id_jntsf5 = new Regex("@jntsf5");
        //Regex id_jntsv5 = new Regex("@jntsv5");
        //Regex id_jntsp5 = new Regex("@jntsp5");
        //Regex id_jntsins5 = new Regex("@jntsins5");
        //Regex id_jntsnotes5 = new Regex("@jntsnotes5");



        // ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE  1
        Regex id_ordertoproceed1 = new Regex("@ordertoproceed1");
        Regex id_jofdeffendant1 = new Regex("@jofdeffendant1");
        Regex id_jofaddress1 = new Regex("@jofaddress1");
        Regex id_jofpalintiff1 = new Regex("@jofpalintiff1");
        Regex id_jofda1 = new Regex("@jofda1");
        Regex id_joff1 = new Regex("@joff1");
        Regex id_jofv1 = new Regex("@jofv1");
        Regex id_jofp1 = new Regex("@jofp1");
        Regex id_jofins1 = new Regex("@jofins1");
        Regex id_jofcause1 = new Regex("@jofcause1");

        // ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE  2
        Regex id_ordertoproceed2 = new Regex("@ordertoproceed2");
        Regex id_jofdeffendant2 = new Regex("@jofdeffendant2");
        Regex id_jofaddress2 = new Regex("@jofaddress2");
        Regex id_jofpalintiff2 = new Regex("@jofpalintiff2");
        Regex id_jofda2 = new Regex("@jofda2");
        Regex id_joff2 = new Regex("@joff2");
        Regex id_jofv2 = new Regex("@jofv2");
        Regex id_jofp2 = new Regex("@jofp2");
        Regex id_jofins2 = new Regex("@jofins2");
        Regex id_jofcause2 = new Regex("@jofcause2");

        // ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE  3
        Regex id_ordertoproceed3 = new Regex("@ordertoproceed3");
        Regex id_jofdeffendant3 = new Regex("@jofdeffendant3");
        Regex id_jofaddress3 = new Regex("@jofaddress3");
        Regex id_jofpalintiff3 = new Regex("@jofpalintiff3");
        Regex id_jofda3 = new Regex("@jofda3");
        Regex id_joff3 = new Regex("@joff3");
        Regex id_jofv3 = new Regex("@jofv3");
        Regex id_jofp3 = new Regex("@jofp3");
        Regex id_jofins3 = new Regex("@jofins3");
        Regex id_jofcause3 = new Regex("@jofcause3");

        // ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE  4
        Regex id_ordertoproceed4 = new Regex("@ordertoproceed4");
        Regex id_jofdeffendant4 = new Regex("@jofdeffendant4");
        Regex id_jofaddress4 = new Regex("@jofaddress4");
        Regex id_jofpalintiff4 = new Regex("@jofpalintiff4");
        Regex id_jofda4 = new Regex("@jofda4");
        Regex id_joff4 = new Regex("@joff4");
        Regex id_jofv4 = new Regex("@jofv4");
        Regex id_jofp4 = new Regex("@jofp4");
        Regex id_jofins4 = new Regex("@jofins4");
        Regex id_jofcause4 = new Regex("@jofcause4");

        //// ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE  5
        //Regex id_ordertoproceed5 = new Regex("@ordertoproceed5");
        //Regex id_jofdeffendant5 = new Regex("@jofdeffendant5");
        //Regex id_jofaddress5 = new Regex("@jofaddress5");
        //Regex id_jofpalintiff5 = new Regex("@jofpalintiff5");
        //Regex id_jofda5 = new Regex("@jofda5");
        //Regex id_joff5 = new Regex("@joff5");
        //Regex id_jofv5 = new Regex("@jofv5");
        //Regex id_jofp5 = new Regex("@jofp5");
        //Regex id_jofins5 = new Regex("@jofins5");
        //Regex id_jofcause5 = new Regex("@jofcause5");



        // STATE TAX LIEN   1
        Regex id_statetaxlien1 = new Regex("@taxlien1");
        Regex id_jstltaxpayer1 = new Regex("@jstltaxpayer1");
        Regex id_jstladdress1 = new Regex("@jstladdress1");
        Regex id_jstltaxpayerid1 = new Regex("@jstltaxpayerid1");
        Regex id_jstlda1 = new Regex("@jstlda1");
        Regex id_jstlf1 = new Regex("@jstlf1");
        Regex id_jstlv1 = new Regex("@jstlv1");
        Regex id_jstlp1 = new Regex("@jstlp1");
        Regex id_jstlins1 = new Regex("@jstlins1");
        Regex id_jstlamount1 = new Regex("jstlamount1");


        // STATE TAX LIEN   2
        Regex id_statetaxlien2 = new Regex("@taxlien2");
        Regex id_jstltaxpayer2 = new Regex("@jstltaxpayer2");
        Regex id_jstladdress2 = new Regex("@jstladdress2");
        Regex id_jstltaxpayerid2 = new Regex("@jstltaxpayerid2");
        Regex id_jstlda2 = new Regex("@jstlda2");
        Regex id_jstlf2 = new Regex("@jstlf2");
        Regex id_jstlv2 = new Regex("@jstlv2");
        Regex id_jstlp2 = new Regex("@jstlp2");
        Regex id_jstlins2 = new Regex("@jstlins2");
        Regex id_jstlamount2 = new Regex("jstlamount2");

        // STATE TAX LIEN   3
        Regex id_statetaxlien3 = new Regex("@taxlien3");
        Regex id_jstltaxpayer3 = new Regex("@jstltaxpayer3");
        Regex id_jstladdress3 = new Regex("@jstladdress3");
        Regex id_jstltaxpayerid3 = new Regex("@jstltaxpayerid3");
        Regex id_jstlda3 = new Regex("@jstlda3");
        Regex id_jstlf3 = new Regex("@jstlf3");
        Regex id_jstlv3 = new Regex("@jstlv3");
        Regex id_jstlp3 = new Regex("@jstlp3");
        Regex id_jstlins3 = new Regex("@jstlins3");
        Regex id_jstlamount3 = new Regex("jstlamount3");

        // STATE TAX LIEN   4
        Regex id_statetaxlien4 = new Regex("@taxlien4");
        Regex id_jstltaxpayer4 = new Regex("@jstltaxpayer4");
        Regex id_jstladdress4 = new Regex("@jstladdress4");
        Regex id_jstltaxpayerid4 = new Regex("@jstltaxpayerid4");
        Regex id_jstlda4 = new Regex("@jstlda4");
        Regex id_jstlf4 = new Regex("@jstlf4");
        Regex id_jstlv4 = new Regex("@jstlv4");
        Regex id_jstlp4 = new Regex("@jstlp4");
        Regex id_jstlins4 = new Regex("@jstlins4");
        Regex id_jstlamount4 = new Regex("jstlamount4");

        //// STATE TAX LIEN   5
        //Regex id_statetaxlien5 = new Regex("@statetaxlien5");
        //Regex id_jstltaxpayer5 = new Regex("@jstltaxpayer5");
        //Regex id_jstladdress5 = new Regex("@jstladdress5");
        //Regex id_jstltaxpayerid5 = new Regex("@jstltaxpayerid5");
        //Regex id_jstlda5 = new Regex("@jstlda5");
        //Regex id_jstlf5 = new Regex("@jstlf5");
        //Regex id_jstlv5 = new Regex("@jstlv5");
        //Regex id_jstlp5 = new Regex("@jstlp5");
        //Regex id_jstlins5 = new Regex("@jstlins5");
        //Regex id_jstlamount5 = new Regex("jstlamount5");




        #endregion

        #region Others ID


        //AFFIDAVIT AND AGREEMENT 1

        Regex id_affandagree1 = new Regex("affandagree1s");
        Regex id_aagrantee1 = new Regex("@aagrantee1");
        Regex id_aagrantor1 = new Regex("@aagrantor1");
        Regex id_aada1 = new Regex("@aada1");
        Regex id_aaf1 = new Regex("@aaf1");
        Regex id_aav1 = new Regex("@aav1");
        Regex id_aap1 = new Regex("@aap1");
        Regex id_aains1 = new Regex("@aains1");
        Regex id_aanotes1 = new Regex("@aanotes1");


        //AFFIDAVIT AND AGREEMENT 2

        Regex id_affandagree2 = new Regex("affandagree2s");
        Regex id_aagrantee2 = new Regex("@aagrantee2");
        Regex id_aagrantor2 = new Regex("@aagrantor2");
        Regex id_aada2 = new Regex("@aada2");
        Regex id_aaf2 = new Regex("@aaf2");
        Regex id_aav2 = new Regex("@aav2");
        Regex id_aap2 = new Regex("@aap2");
        Regex id_aains2 = new Regex("@aains2");
        Regex id_aanotes2 = new Regex("@aanotes2");

        //AFFIDAVIT AND AGREEMENT 3

        Regex id_affandagree3 = new Regex("affandagree3s");
        Regex id_aagrantee3 = new Regex("@aagrantee3");
        Regex id_aagrantor3 = new Regex("@aagrantor3");
        Regex id_aada3 = new Regex("@aada3");
        Regex id_aaf3 = new Regex("@aaf3");
        Regex id_aav3 = new Regex("@aav3");
        Regex id_aap3 = new Regex("@aap3");
        Regex id_aains3 = new Regex("@aains3");
        Regex id_aanotes3 = new Regex("@aanotes3");

        //AFFIDAVIT AND AGREEMENT 4

        Regex id_affandagree4 = new Regex("affandagree4s");
        Regex id_aagrantee4 = new Regex("@aagrantee4");
        Regex id_aagrantor4 = new Regex("@aagrantor4");
        Regex id_aada4 = new Regex("@aada4");
        Regex id_aaf4 = new Regex("@aaf4");
        Regex id_aav4 = new Regex("@aav4");
        Regex id_aap4 = new Regex("@aap4");
        Regex id_aains4 = new Regex("@aains4");
        Regex id_aanotes4 = new Regex("@aanotes4");

        //AFFIDAVIT AND AGREEMENT 5

        //Regex id_affandagree5 = new Regex("@affandagree5");
        //Regex id_aagrantee5 = new Regex("@aagrantee5");
        //Regex id_aagrantor5 = new Regex("@aagrantor5");
        //Regex id_aada5 = new Regex("@aada5");
        //Regex id_aaf5 = new Regex("@aaf5");
        //Regex id_aav5 = new Regex("@aav5");
        //Regex id_aap5 = new Regex("@aap5");
        //Regex id_aains5 = new Regex("@aains5");
        //Regex id_aanotes5 = new Regex("@aanotes5");


        //DIVORCE – NOT EXAMINED  1

        Regex id_divorce1 = new Regex("@divorce1");
        Regex id_dipetitioner1 = new Regex("@dipetitioner1");
        Regex id_direspondent1 = new Regex("@direspondent1");
        Regex id_dif1 = new Regex("@dif1");
        Regex id_dicause1 = new Regex("@dicause1");

        //DIVORCE – NOT EXAMINED  2

        Regex id_divorce2 = new Regex("@divorce2");
        Regex id_dipetitioner2 = new Regex("@dipetitioner2");
        Regex id_direspondent2 = new Regex("@direspondent2");
        Regex id_dif2 = new Regex("@dif2");
        Regex id_dicause2 = new Regex("@dicause2");

        //DIVORCE – NOT EXAMINED  3

        Regex id_divorce3 = new Regex("@divorce3");
        Regex id_dipetitioner3 = new Regex("@dipetitioner3");
        Regex id_direspondent3 = new Regex("@direspondent3");
        Regex id_dif3 = new Regex("@dif3");
        Regex id_dicause3 = new Regex("@dicause3");

        //DIVORCE – NOT EXAMINED  4

        Regex id_divorce4 = new Regex("@divorce4");
        Regex id_dipetitioner4 = new Regex("@dipetitioner4");
        Regex id_direspondent4 = new Regex("@direspondent4");
        Regex id_dif4 = new Regex("@dif4");
        Regex id_dicause4 = new Regex("@dicause4");

        ////DIVORCE – NOT EXAMINED  5

        //Regex id_divorce5 = new Regex("@divorce5");
        //Regex id_dipetitioner5 = new Regex("@dipetitioner5");
        //Regex id_direspondent5 = new Regex("@direspondent5");
        //Regex id_dif5 = new Regex("@dif5");
        //Regex id_dicause5 = new Regex("@dicause5");



        //GENERAL POWER OF ATTORNEY 1

        Regex id_genepower1 = new Regex("@genepower1");
        Regex id_gpda1 = new Regex("@gpda1");
        Regex id_gpf1 = new Regex("@gpf1");
        Regex id_gpv1 = new Regex("@gpv1");
        Regex id_gpp1 = new Regex("@gpp1");
        Regex id_gpins1 = new Regex("@gpins1");
        Regex id_gpnotes1 = new Regex("@gpnotes1");


        //GENERAL POWER OF ATTORNEY 2

        Regex id_genepower2 = new Regex("@genepower2");
        Regex id_gpda2 = new Regex("@gpda2");
        Regex id_gpf2 = new Regex("@gpf2");
        Regex id_gpv2 = new Regex("@gpv2");
        Regex id_gpp2 = new Regex("@gpp2");
        Regex id_gpins2 = new Regex("@gpins2");
        Regex id_gpnotes2 = new Regex("@gpnotes2");

        //GENERAL POWER OF ATTORNEY 3

        Regex id_genepower3 = new Regex("@genepower3");
        Regex id_gpda3 = new Regex("@gpda3");
        Regex id_gpf3 = new Regex("@gpf3");
        Regex id_gpv3 = new Regex("@gpv3");
        Regex id_gpp3 = new Regex("@gpp3");
        Regex id_gpins3 = new Regex("@gpins3");
        Regex id_gpnotes3 = new Regex("@gpnotes3");

        //GENERAL POWER OF ATTORNEY 4

        Regex id_genepower4 = new Regex("@genepower4");
        Regex id_gpda4 = new Regex("@gpda4");
        Regex id_gpf4 = new Regex("@gpf4");
        Regex id_gpv4 = new Regex("@gpv4");
        Regex id_gpp4 = new Regex("@gpp4");
        Regex id_gpins4 = new Regex("@gpins4");
        Regex id_gpnotes4 = new Regex("@gpnotes4");

        ////GENERAL POWER OF ATTORNEY 5

        //Regex id_genepower5 = new Regex("@genepower5");
        //Regex id_gpda5 = new Regex("@gpda5");
        //Regex id_gpf5 = new Regex("@gpf5");
        //Regex id_gpv5 = new Regex("@gpv5");
        //Regex id_gpp5 = new Regex("@gpp5");
        //Regex id_gpins5 = new Regex("@gpins5");
        //Regex id_gpnotes5 = new Regex("@gpnotes5");



        //PROBATE – NOT EXAMINED   1

        Regex id_probate1 = new Regex("probate1s");
        Regex id_prore1 = new Regex("@prore1");
        Regex id_prof1 = new Regex("@prof1");
        Regex id_procause1 = new Regex("@procause1");


        //PROBATE – NOT EXAMINED   2

        Regex id_probate2 = new Regex("probate2s");
        Regex id_prore2 = new Regex("@prore2");
        Regex id_prof2 = new Regex("@prof2");
        Regex id_procause2 = new Regex("@procause2");


        //PROBATE – NOT EXAMINED   3

        Regex id_probate3 = new Regex("probate3s");
        Regex id_prore3 = new Regex("@prore3");
        Regex id_prof3 = new Regex("@prof3");
        Regex id_procause3 = new Regex("@procause3");


        //PROBATE – NOT EXAMINED   4

        Regex id_probate4 = new Regex("probate4s");
        Regex id_prore4 = new Regex("@prore4");
        Regex id_prof4 = new Regex("@prof4");
        Regex id_procause4 = new Regex("@procause4");



        ////PROBATE – NOT EXAMINED   5

        //Regex id_probate5 = new Regex("@probate5");
        //Regex id_prore5 = new Regex("@prore5");
        //Regex id_prof5 = new Regex("@prof5");
        //Regex id_procause5 = new Regex("@procause5");






        //REINSTATEMENT AGREEMENT 1
        Regex id_reinstatement1 = new Regex("@reinstatement1");
        Regex id_rada1 = new Regex("@rada1");
        Regex id_raf1 = new Regex("@raf1");
        Regex id_rav1 = new Regex("@rav1");
        Regex id_rap1 = new Regex("@rap1");
        Regex id_rains1 = new Regex("@rains1");
        Regex id_ranotes1 = new Regex("@ranotes1");


        //REINSTATEMENT AGREEMENT 2
        Regex id_reinstatement2 = new Regex("@reinstatement2");
        Regex id_rada2 = new Regex("@rada2");
        Regex id_raf2 = new Regex("@raf2");
        Regex id_rav2 = new Regex("@rav2");
        Regex id_rap2 = new Regex("@rap2");
        Regex id_rains2 = new Regex("@rains2");
        Regex id_ranotes2 = new Regex("@ranotes2");


        //REINSTATEMENT AGREEMENT 3
        Regex id_reinstatement3 = new Regex("@reinstatement3");
        Regex id_rada3 = new Regex("@rada3");
        Regex id_raf3 = new Regex("@raf3");
        Regex id_rav3 = new Regex("@rav3");
        Regex id_rap3 = new Regex("@rap3");
        Regex id_rains3 = new Regex("@rains3");
        Regex id_ranotes3 = new Regex("@ranotes3");


        //REINSTATEMENT AGREEMENT 4
        Regex id_reinstatement4 = new Regex("@reinstatement4");
        Regex id_rada4 = new Regex("@rada4");
        Regex id_raf4 = new Regex("@raf4");
        Regex id_rav4 = new Regex("@rav4");
        Regex id_rap4 = new Regex("@rap4");
        Regex id_rains4 = new Regex("@rains4");
        Regex id_ranotes4 = new Regex("@ranotes4");


        ////REINSTATEMENT AGREEMENT 5
        //Regex id_reinstatement5 = new Regex("@reinstatement5");
        //Regex id_rada5 = new Regex("@rada5");
        //Regex id_raf5 = new Regex("@raf5");
        //Regex id_rav5 = new Regex("@rav5");
        //Regex id_rap5 = new Regex("@rap5");
        //Regex id_rains5 = new Regex("@rains5");
        //Regex id_ranotes5 = new Regex("@ranotes5");



        //STATEMENT OF OWNERSHIP AND LOCATION 1

        Regex id_stateofowner1 = new Regex("@ofowner1s");
        Regex id_soowner1 = new Regex("@soowner1");
        Regex id_somanufacturer1 = new Regex("@somanufacturer1");
        Regex id_soda1 = new Regex("@soda1");
        Regex id_sof1 = new Regex("@sof1");
        Regex id_sov1 = new Regex("@sov1");
        Regex id_sop1 = new Regex("@sop1");
        Regex id_soins1 = new Regex("@soins1");
        Regex id_sonotes1 = new Regex("@sonotes1");

        //STATEMENT OF OWNERSHIP AND LOCATION 2

        Regex id_stateofowner2 = new Regex("@ofowner2s");
        Regex id_soowner2 = new Regex("@soowner2");
        Regex id_somanufacturer2 = new Regex("@somanufacturer2");
        Regex id_soda2 = new Regex("@soda2");
        Regex id_sof2 = new Regex("@sof2");
        Regex id_sov2 = new Regex("@sov2");
        Regex id_sop2 = new Regex("@sop2");
        Regex id_soins2 = new Regex("@soins2");
        Regex id_sonotes2 = new Regex("@sonotes2");

        //STATEMENT OF OWNERSHIP AND LOCATION 3

        Regex id_stateofowner3 = new Regex("@ofowner3s");
        Regex id_soowner3 = new Regex("@soowner3");
        Regex id_somanufacturer3 = new Regex("@somanufacturer3");
        Regex id_soda3 = new Regex("@soda3");
        Regex id_sof3 = new Regex("@sof3");
        Regex id_sov3 = new Regex("@sov3");
        Regex id_sop3 = new Regex("@sop3");
        Regex id_soins3 = new Regex("@soins3");
        Regex id_sonotes3 = new Regex("@sonotes3");

        //STATEMENT OF OWNERSHIP AND LOCATION 4

        Regex id_stateofowner4 = new Regex("@ofowner4s");
        Regex id_soowner4 = new Regex("@soowner4");
        Regex id_somanufacturer4 = new Regex("@somanufacturer4");
        Regex id_soda4 = new Regex("@soda4");
        Regex id_sof4 = new Regex("@sof4");
        Regex id_sov4 = new Regex("@sov4");
        Regex id_sop4 = new Regex("@sop4");
        Regex id_soins4 = new Regex("@soins4");
        Regex id_sonotes4 = new Regex("@sonotes4");

        ////STATEMENT OF OWNERSHIP AND LOCATION 5

        //Regex id_stateofowner5 = new Regex("@stateofowner5");
        //Regex id_soowner5 = new Regex("@soowner5");
        //Regex id_somanufacturer5 = new Regex("@somanufacturer5");
        //Regex id_soda5 = new Regex("@soda5");
        //Regex id_sof5 = new Regex("@sof5");
        //Regex id_sov5 = new Regex("@sov5");
        //Regex id_sop5 = new Regex("@sop5");
        //Regex id_soins5 = new Regex("@soins5");
        //Regex id_sonotes5 = new Regex("@sonotes5");



        //SPECIAL POWER OF ATTORNEY 1

        Regex id_spoaid1 = new Regex("@spoa1");
        Regex id_spoato1 = new Regex("@spoato1");
        Regex id_spoagrantor1 = new Regex("@spoagrantor1");
        Regex id_spoadate1 = new Regex("@spoadate1");
        Regex id_spoafiled1 = new Regex("@spoafiled1");
        Regex id_spoavol1 = new Regex("@sav1");
        Regex id_spoapg1 = new Regex("@sap1");
        Regex id_spoainst1 = new Regex("@sai1");
        Regex id_spoanote1 = new Regex("@spoanote1");


        //SPECIAL POWER OF ATTORNEY 2
        Regex id_spoaid2 = new Regex("@spoa2");
        Regex id_spoato2 = new Regex("@spoato2");
        Regex id_spoagrantor2 = new Regex("@spoagrantor2");
        Regex id_spoadate2 = new Regex("@spoadate2");
        Regex id_spoafiled2 = new Regex("@spoafiled2");
        Regex id_spoavol2 = new Regex("@sav2");
        Regex id_spoapg2 = new Regex("@sap2");
        Regex id_spoainst2 = new Regex("@sai2");
        Regex id_spoanote2 = new Regex("@spoanote2");

        //SPECIAL POWER OF ATTORNEY 3
        Regex id_spoaid3 = new Regex("@spoa3");
        Regex id_spoato3 = new Regex("@spoato3");
        Regex id_spoagrantor3 = new Regex("@spoagrantor3");
        Regex id_spoadate3 = new Regex("@spoadate3");
        Regex id_spoafiled3 = new Regex("@spoafiled3");
        Regex id_spoavol3 = new Regex("@sav3");
        Regex id_spoapg3 = new Regex("@sap3");
        Regex id_spoainst3 = new Regex("@sai3");
        Regex id_spoanote3 = new Regex("@spoanote3");

        //SPECIAL POWER OF ATTORNEY 4
        Regex id_spoaid4 = new Regex("@spoa4");
        Regex id_spoato4 = new Regex("@spoato4");
        Regex id_spoagrantor4 = new Regex("@spoagrantor4");
        Regex id_spoadate4 = new Regex("@spoadate4");
        Regex id_spoafiled4 = new Regex("@spoafiled4");
        Regex id_spoavol4 = new Regex("@sav4");
        Regex id_spoapg4 = new Regex("@sap4");
        Regex id_spoainst4 = new Regex("@sai4");
        Regex id_spoanote4 = new Regex("@spoanote4");


        #endregion

        #region declaration

        Regex id_declaration = new Regex("@declaration");
        #endregion declaration



        #region wordodc


        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(target, true))
        {
            string docText = null;


            using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
            {
                docText = sr.ReadToEnd();

            }

            #region general
            DataSet dsgeneral = new DataSet();
            dsgeneral = gls.gettypevalue(lbl_orderno.Text, "sp_sel_client_output");
            if (dsgeneral.Tables[0].Rows.Count > 0)
            {
                docText = id_client.Replace(docText, dsgeneral.Tables[0].Rows[0]["client"].ToString());
                docText = id_date.Replace(docText, dsgeneral.Tables[0].Rows[0]["pdate"].ToString());
                docText = id_address.Replace(docText, dsgeneral.Tables[0].Rows[0]["address"].ToString());
                docText = id_orderno.Replace(docText, dsgeneral.Tables[0].Rows[0]["orderno"].ToString());
                docText = id_cityst.Replace(docText, dsgeneral.Tables[0].Rows[0]["city_zip"].ToString());
                docText = id_ref.Replace(docText, dsgeneral.Tables[0].Rows[0]["ref"].ToString());
                docText = id_attention.Replace(docText, dsgeneral.Tables[0].Rows[0]["attention"].ToString());
                docText = id_cdate.Replace(docText, dsgeneral.Tables[0].Rows[0]["conformdate"].ToString());
                docText = id_owner.Replace(docText, dsgeneral.Tables[0].Rows[0]["owner"].ToString());
                docText = id_paddress.Replace(docText, dsgeneral.Tables[0].Rows[0]["propaddress"].ToString());
                docText = id_city.Replace(docText, dsgeneral.Tables[0].Rows[0]["city"].ToString());
                docText = id_state.Replace(docText, dsgeneral.Tables[0].Rows[0]["state"].ToString());
                docText = id_zip.Replace(docText, dsgeneral.Tables[0].Rows[0]["zip"].ToString());
                docText = id_county.Replace(docText, dsgeneral.Tables[0].Rows[0]["county"].ToString());
                docText = id_legal.Replace(docText, dsgeneral.Tables[0].Rows[0]["legalinfo"].ToString());
                docText = id_ownerofrecord.Replace(docText, dsgeneral.Tables[0].Rows[0]["ownerofrec"].ToString());
            }
            //else
            //{
            //    docText = id_client.Replace(docText, string.Empty);
            //    docText = id_date.Replace(docText, string.Empty);
            //    docText = id_address.Replace(docText, string.Empty);
            //    docText = id_orderno.Replace(docText, string.Empty);
            //    docText = id_cityst.Replace(docText, string.Empty);
            //    docText = id_ref.Replace(docText, string.Empty);
            //    docText = id_attention.Replace(docText, string.Empty);
            //    docText = id_cdate.Replace(docText, string.Empty);
            //    docText = id_owner.Replace(docText, string.Empty);
            //    docText = id_paddress.Replace(docText, string.Empty);
            //    docText = id_city.Replace(docText, string.Empty);
            //    docText = id_state.Replace(docText, string.Empty);
            //    docText = id_zip.Replace(docText, string.Empty);
            //    docText = id_county.Replace(docText, string.Empty);
            //    docText = id_legal.Replace(docText, string.Empty);
            //    docText = id_ownerofrecord.Replace(docText, string.Empty);
            //}
            #endregion

            #region tax assesment

            dstaxass = gls.gettypevalue(lbl_orderno.Text, "sp_sel_tax_assessment_output");
            if (dstaxass.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dstaxass.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_tsparcel1.Replace(docText, dstaxass.Tables[0].Rows[i]["parcel_id"].ToString());
                        docText = id_tsland1.Replace(docText, dstaxass.Tables[0].Rows[i]["land"].ToString());
                        docText = id_tstaxes1.Replace(docText, dstaxass.Tables[0].Rows[i]["taxes"].ToString());
                        docText = id_tsimprove1.Replace(docText, dstaxass.Tables[0].Rows[i]["improvements"].ToString());
                        docText = id_tstotal1.Replace(docText, dstaxass.Tables[0].Rows[i]["total"].ToString());
                        docText = id_tstaxyear1.Replace(docText, dstaxass.Tables[0].Rows[i]["tax_year"].ToString());
                        docText = id_tsdue1.Replace(docText, dstaxass.Tables[0].Rows[i]["due_paid"].ToString());
                        docText = id_tsnotes1.Replace(docText, dstaxass.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 1)
                    {

                        docText = id_tsparcel2.Replace(docText, dstaxass.Tables[0].Rows[i]["parcel_id"].ToString());
                        docText = id_tsland2.Replace(docText, dstaxass.Tables[0].Rows[i]["land"].ToString());
                        docText = id_tstaxes2.Replace(docText, dstaxass.Tables[0].Rows[i]["taxes"].ToString());
                        docText = id_tsimprove2.Replace(docText, dstaxass.Tables[0].Rows[i]["improvements"].ToString());
                        docText = id_tstotal2.Replace(docText, dstaxass.Tables[0].Rows[i]["total"].ToString());
                        docText = id_tstaxyear2.Replace(docText, dstaxass.Tables[0].Rows[i]["tax_year"].ToString());
                        docText = id_tsdue2.Replace(docText, dstaxass.Tables[0].Rows[i]["due_paid"].ToString());
                        docText = id_tsnotes2.Replace(docText, dstaxass.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 2)
                    {
                        docText = id_tsparcel3.Replace(docText, dstaxass.Tables[0].Rows[i]["parcel_id"].ToString());
                        docText = id_tsland3.Replace(docText, dstaxass.Tables[0].Rows[i]["land"].ToString());
                        docText = id_tstaxes3.Replace(docText, dstaxass.Tables[0].Rows[i]["taxes"].ToString());
                        docText = id_tsimprove3.Replace(docText, dstaxass.Tables[0].Rows[i]["improvements"].ToString());
                        docText = id_tstaxyear3.Replace(docText, dstaxass.Tables[0].Rows[i]["tax_year"].ToString());
                        docText = id_tstotal3.Replace(docText, dstaxass.Tables[0].Rows[i]["total"].ToString());
                        docText = id_tsdue3.Replace(docText, dstaxass.Tables[0].Rows[i]["due_paid"].ToString());
                        docText = id_tsnotes3.Replace(docText, dstaxass.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 3)
                    {
                        docText = id_tsparcel4.Replace(docText, dstaxass.Tables[0].Rows[i]["PARCELID"].ToString());
                        docText = id_tsland4.Replace(docText, dstaxass.Tables[0].Rows[i]["LAND"].ToString());
                        docText = id_tstaxes4.Replace(docText, dstaxass.Tables[0].Rows[i]["TAXES"].ToString());
                        docText = id_tsimprove4.Replace(docText, dstaxass.Tables[0].Rows[i]["IMPROVEMENTS"].ToString());
                        docText = id_tstaxyear4.Replace(docText, dstaxass.Tables[0].Rows[i]["TAXYEAR"].ToString());
                        docText = id_tstotal4.Replace(docText, dstaxass.Tables[0].Rows[i]["TOTAL"].ToString());
                        docText = id_tsdue4.Replace(docText, dstaxass.Tables[0].Rows[i]["DUE/PAID"].ToString());
                        docText = id_tsnotes4.Replace(docText, dstaxass.Tables[0].Rows[i]["NOTES"].ToString());
                    }
                    if (i == 4)
                    {
                        docText = id_tsparcel5.Replace(docText, dstaxass.Tables[0].Rows[i]["PARCELID"].ToString());
                        docText = id_tsland5.Replace(docText, dstaxass.Tables[0].Rows[i]["LAND"].ToString());
                        docText = id_tstaxes5.Replace(docText, dstaxass.Tables[0].Rows[i]["TAXES"].ToString());
                        docText = id_tsimprove5.Replace(docText, dstaxass.Tables[0].Rows[i]["IMPROVEMENTS"].ToString());
                        docText = id_tstaxyear5.Replace(docText, dstaxass.Tables[0].Rows[i]["TAXYEAR"].ToString());
                        docText = id_tstotal5.Replace(docText, dstaxass.Tables[0].Rows[i]["TOTAL"].ToString());
                        docText = id_tsdue5.Replace(docText, dstaxass.Tables[0].Rows[i]["DUE/PAID"].ToString());
                        docText = id_tsnotes5.Replace(docText, dstaxass.Tables[0].Rows[i]["NOTES"].ToString());
                    }

                }
            }
            #endregion

            #region Deed
            //Deed
            DataSet dsdeed = new DataSet();
            dsdeed = gls.gettypevalue(lbl_orderno.Text, "sp_sel_deed_output");
            if (dsdeed.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < dsdeed.Tables[0].Rows.Count; i++)
                {

                    if (i == 0)
                    {
                        docText = id_deed1.Replace(docText, dsdeed.Tables[0].Rows[i]["Deed_Type"].ToString());
                        docText = id_dgrantee1.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_dgrantor1.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_ddated1.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_dfiled1.Replace(docText, dsdeed.Tables[0].Rows[i]["FILED"].ToString());
                        docText = id_dvol1.Replace(docText, dsdeed.Tables[0].Rows[i]["VOL"].ToString());
                        docText = id_dpg1.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_dinst1.Replace(docText, dsdeed.Tables[0].Rows[i]["INST"].ToString());
                        docText = id_dnotes1.Replace(docText, dsdeed.Tables[0].Rows[i]["NOTES"].ToString());
                    }
                    if (i == 1)
                    {
                        docText = id_deed2.Replace(docText, dsdeed.Tables[0].Rows[i]["Deed_Type"].ToString());
                        docText = id_dgrantee2.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_dgrantor2.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_ddated2.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_dfiled2.Replace(docText, dsdeed.Tables[0].Rows[i]["FILED"].ToString());
                        docText = id_dvol2.Replace(docText, dsdeed.Tables[0].Rows[i]["VOL"].ToString());
                        docText = id_dpg2.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_dinst2.Replace(docText, dsdeed.Tables[0].Rows[i]["INST"].ToString());
                        docText = id_dnotes2.Replace(docText, dsdeed.Tables[0].Rows[i]["NOTES"].ToString());
                    }
                    if (i == 2)
                    {

                        docText = id_deed3.Replace(docText, dsdeed.Tables[0].Rows[i]["Deed_Type"].ToString());
                        docText = id_dgrantee3.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_dgrantor3.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_ddated3.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_dfiled3.Replace(docText, dsdeed.Tables[0].Rows[i]["FILED"].ToString());
                        docText = id_dvol3.Replace(docText, dsdeed.Tables[0].Rows[i]["VOL"].ToString());
                        docText = id_dpg3.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_dinst3.Replace(docText, dsdeed.Tables[0].Rows[i]["INST"].ToString());
                        docText = id_dnotes3.Replace(docText, dsdeed.Tables[0].Rows[i]["NOTES"].ToString());
                    }
                    if (i == 3)
                    {

                        docText = id_deed4.Replace(docText, dsdeed.Tables[0].Rows[i]["Deed_Type"].ToString());
                        docText = id_dgrantee4.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_dgrantor4.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_ddated4.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_dfiled4.Replace(docText, dsdeed.Tables[0].Rows[i]["FILED"].ToString());
                        docText = id_dvol4.Replace(docText, dsdeed.Tables[0].Rows[i]["VOL"].ToString());
                        docText = id_dpg4.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_dinst4.Replace(docText, dsdeed.Tables[0].Rows[i]["INST"].ToString());
                        docText = id_dnotes4.Replace(docText, dsdeed.Tables[0].Rows[i]["NOTES"].ToString());
                    }
                    if (i == 4)
                    {
                        docText = id_deed5.Replace(docText, dsdeed.Tables[0].Rows[i]["Deed_Type"].ToString());
                        docText = id_dgrantee5.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_dgrantor5.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_ddated5.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_dfiled5.Replace(docText, dsdeed.Tables[0].Rows[i]["FILED"].ToString());
                        docText = id_dvol5.Replace(docText, dsdeed.Tables[0].Rows[i]["VOL"].ToString());
                        docText = id_dpg5.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_dinst5.Replace(docText, dsdeed.Tables[0].Rows[i]["INST"].ToString());
                        docText = id_dnotes5.Replace(docText, dsdeed.Tables[0].Rows[i]["NOTES"].ToString());
                    }

                }
            }

            #endregion

            #region Mortgage
            // Mortgage

            DataSet dsmortgage = new DataSet();
            dsmortgage = gls.gettypevalue(lbl_orderno.Text, "sp_sel_mortgage_ouput");
            if (dsmortgage.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsmortgage.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_affoflostassign1.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgage_type"].ToString());
                        docText = id_affoflostassignee1.Replace(docText, dsmortgage.Tables[0].Rows[i]["assigne"].ToString());
                        docText = id_affoflostassignor1.Replace(docText, dsmortgage.Tables[0].Rows[i]["assignor"].ToString());
                        docText = id_affmda1.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_affmf1.Replace(docText, dsmortgage.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_affmv1.Replace(docText, dsmortgage.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_affmpg1.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_affminst1.Replace(docText, dsmortgage.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_affmnotes1.Replace(docText, dsmortgage.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 1)
                    {
                        docText = id_affoflostassign2.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgage_type"].ToString());
                        docText = id_affoflostassignee2.Replace(docText, dsmortgage.Tables[0].Rows[i]["assigne"].ToString());
                        docText = id_affoflostassignor2.Replace(docText, dsmortgage.Tables[0].Rows[i]["assignor"].ToString());
                        docText = id_affmda2.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_affmf2.Replace(docText, dsmortgage.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_affmv2.Replace(docText, dsmortgage.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_affmpg2.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_affminst2.Replace(docText, dsmortgage.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_affmnotes2.Replace(docText, dsmortgage.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 2)
                    {

                        docText = id_affoflostassign3.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgage_type"].ToString());
                        docText = id_affoflostassignee3.Replace(docText, dsmortgage.Tables[0].Rows[i]["assigne"].ToString());
                        docText = id_affoflostassignor3.Replace(docText, dsmortgage.Tables[0].Rows[i]["assignor"].ToString());
                        docText = id_affmda3.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_affmf3.Replace(docText, dsmortgage.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_affmv3.Replace(docText, dsmortgage.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_affmpg3.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_affminst3.Replace(docText, dsmortgage.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_affmnotes3.Replace(docText, dsmortgage.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 3)
                    {

                        docText = id_affoflostassign4.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgage_type"].ToString());
                        docText = id_affoflostassignee4.Replace(docText, dsmortgage.Tables[0].Rows[i]["assigne"].ToString());
                        docText = id_affoflostassignor4.Replace(docText, dsmortgage.Tables[0].Rows[i]["assignor"].ToString());
                        docText = id_affmda4.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_affmf4.Replace(docText, dsmortgage.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_affmv4.Replace(docText, dsmortgage.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_affmpg4.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_affminst4.Replace(docText, dsmortgage.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_affmnotes4.Replace(docText, dsmortgage.Tables[0].Rows[i]["notes"].ToString());
                    }
                    if (i == 4)
                    {
                        docText = id_affoflostassign5.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgage_type"].ToString());
                        docText = id_affoflostassignee5.Replace(docText, dsmortgage.Tables[0].Rows[i]["assigne"].ToString());
                        docText = id_affoflostassignor5.Replace(docText, dsmortgage.Tables[0].Rows[i]["assignor"].ToString());
                        docText = id_affmda5.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_affmf5.Replace(docText, dsmortgage.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_affmv5.Replace(docText, dsmortgage.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_affmpg5.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_affminst5.Replace(docText, dsmortgage.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_affmnotes5.Replace(docText, dsmortgage.Tables[0].Rows[i]["notes"].ToString());
                    }

                }
            }

            if (dsmortgage.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dsmortgage.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_appofsubtrus1.Replace(docText, dsmortgage.Tables[1].Rows[0]["mortgage_type"].ToString());
                        docText = id_appointed1.Replace(docText, dsmortgage.Tables[1].Rows[0]["appointed"].ToString());
                        docText = id_executedby1.Replace(docText, dsmortgage.Tables[1].Rows[0]["executed_by"].ToString());
                        docText = id_appda1.Replace(docText, dsmortgage.Tables[1].Rows[0]["dated"].ToString());
                        docText = id_appf1.Replace(docText, dsmortgage.Tables[1].Rows[0]["filed"].ToString());
                        docText = id_appv1.Replace(docText, dsmortgage.Tables[1].Rows[0]["vol"].ToString());
                        docText = id_appp1.Replace(docText, dsmortgage.Tables[1].Rows[0]["pg"].ToString());
                        docText = id_appins1.Replace(docText, dsmortgage.Tables[1].Rows[0]["inst"].ToString());
                        docText = id_appnotes1.Replace(docText, dsmortgage.Tables[1].Rows[0]["notes"].ToString());
                    }
                    if (i == 1)
                    {
                        docText = id_appofsubtrus2.Replace(docText, dsmortgage.Tables[1].Rows[1]["mortgage_type"].ToString());
                        docText = id_appointed2.Replace(docText, dsmortgage.Tables[1].Rows[1]["appointed"].ToString());
                        docText = id_executedby2.Replace(docText, dsmortgage.Tables[1].Rows[1]["executed_by"].ToString());
                        docText = id_appda2.Replace(docText, dsmortgage.Tables[1].Rows[1]["dated"].ToString());
                        docText = id_appf2.Replace(docText, dsmortgage.Tables[1].Rows[1]["filed"].ToString());
                        docText = id_appv2.Replace(docText, dsmortgage.Tables[1].Rows[1]["vol"].ToString());
                        docText = id_appp2.Replace(docText, dsmortgage.Tables[1].Rows[1]["pg"].ToString());
                        docText = id_appins2.Replace(docText, dsmortgage.Tables[1].Rows[1]["inst"].ToString());
                        docText = id_appnotes2.Replace(docText, dsmortgage.Tables[1].Rows[1]["notes"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_appofsubtrus3.Replace(docText, dsmortgage.Tables[1].Rows[2]["mortgage_type"].ToString());
                        docText = id_appointed3.Replace(docText, dsmortgage.Tables[1].Rows[2]["appointed"].ToString());
                        docText = id_executedby3.Replace(docText, dsmortgage.Tables[1].Rows[2]["executed_by"].ToString());
                        docText = id_appda3.Replace(docText, dsmortgage.Tables[1].Rows[2]["dated"].ToString());
                        docText = id_appf3.Replace(docText, dsmortgage.Tables[1].Rows[2]["filed"].ToString());
                        docText = id_appv3.Replace(docText, dsmortgage.Tables[1].Rows[2]["vol"].ToString());
                        docText = id_appp3.Replace(docText, dsmortgage.Tables[1].Rows[2]["pg"].ToString());
                        docText = id_appins3.Replace(docText, dsmortgage.Tables[1].Rows[2]["inst"].ToString());
                        docText = id_appnotes3.Replace(docText, dsmortgage.Tables[1].Rows[2]["notes"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_appofsubtrus4.Replace(docText, dsmortgage.Tables[1].Rows[3]["mortgage_type"].ToString());
                        docText = id_appointed4.Replace(docText, dsmortgage.Tables[1].Rows[3]["appointed"].ToString());
                        docText = id_executedby4.Replace(docText, dsmortgage.Tables[1].Rows[3]["executed_by"].ToString());
                        docText = id_appda4.Replace(docText, dsmortgage.Tables[1].Rows[3]["dated"].ToString());
                        docText = id_appf4.Replace(docText, dsmortgage.Tables[1].Rows[3]["filed"].ToString());
                        docText = id_appv4.Replace(docText, dsmortgage.Tables[1].Rows[3]["vol"].ToString());
                        docText = id_appp4.Replace(docText, dsmortgage.Tables[1].Rows[3]["pg"].ToString());
                        docText = id_appins4.Replace(docText, dsmortgage.Tables[1].Rows[3]["inst"].ToString());
                        docText = id_appnotes4.Replace(docText, dsmortgage.Tables[1].Rows[3]["notes"].ToString());
                    }

                    if (i == 4)
                    {
                        docText = id_appofsubtrus5.Replace(docText, dsmortgage.Tables[1].Rows[4]["mortgage_type"].ToString());
                        docText = id_appointed5.Replace(docText, dsmortgage.Tables[1].Rows[4]["appointed"].ToString());
                        docText = id_executedby5.Replace(docText, dsmortgage.Tables[1].Rows[4]["executed_by"].ToString());
                        docText = id_appda5.Replace(docText, dsmortgage.Tables[1].Rows[4]["dated"].ToString());
                        docText = id_appf5.Replace(docText, dsmortgage.Tables[1].Rows[4]["filed"].ToString());
                        docText = id_appv5.Replace(docText, dsmortgage.Tables[1].Rows[4]["vol"].ToString());
                        docText = id_appp5.Replace(docText, dsmortgage.Tables[1].Rows[4]["pg"].ToString());
                        docText = id_appins5.Replace(docText, dsmortgage.Tables[1].Rows[4]["inst"].ToString());
                        docText = id_appnotes5.Replace(docText, dsmortgage.Tables[1].Rows[4]["notes"].ToString());
                    }

                }
            }



            if (dsmortgage.Tables[2].Rows.Count > 0)
            {
                for (int i = 0; i < dsmortgage.Tables[2].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_assofrents.Replace(docText, dsmortgage.Tables[2].Rows[0]["mortgage_type"].ToString());
                        docText = id_lender1.Replace(docText, dsmortgage.Tables[2].Rows[0]["lender"].ToString());
                        docText = id_assgrantor1.Replace(docText, dsmortgage.Tables[2].Rows[0]["grantor"].ToString());
                        docText = id_arda1.Replace(docText, dsmortgage.Tables[2].Rows[0]["dated"].ToString());
                        docText = id_arf1.Replace(docText, dsmortgage.Tables[2].Rows[0]["filed"].ToString());
                        docText = id_arv1.Replace(docText, dsmortgage.Tables[2].Rows[0]["vol"].ToString());
                        docText = id_arp1.Replace(docText, dsmortgage.Tables[2].Rows[0]["pg"].ToString());
                        docText = id_arins1.Replace(docText, dsmortgage.Tables[2].Rows[0]["inst"].ToString());
                        docText = id_arnotes1.Replace(docText, dsmortgage.Tables[2].Rows[0]["notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_assofrents2.Replace(docText, dsmortgage.Tables[2].Rows[1]["mortgage_type"].ToString());
                        docText = id_lender2.Replace(docText, dsmortgage.Tables[2].Rows[1]["lender"].ToString());
                        docText = id_assgrantor2.Replace(docText, dsmortgage.Tables[2].Rows[1]["grantor"].ToString());
                        docText = id_arda2.Replace(docText, dsmortgage.Tables[2].Rows[1]["dated"].ToString());
                        docText = id_arf2.Replace(docText, dsmortgage.Tables[2].Rows[1]["filed"].ToString());
                        docText = id_arv2.Replace(docText, dsmortgage.Tables[2].Rows[1]["vol"].ToString());
                        docText = id_arp2.Replace(docText, dsmortgage.Tables[2].Rows[1]["pg"].ToString());
                        docText = id_arins2.Replace(docText, dsmortgage.Tables[2].Rows[1]["inst"].ToString());
                        docText = id_arnotes2.Replace(docText, dsmortgage.Tables[2].Rows[1]["notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_assofrents3.Replace(docText, dsmortgage.Tables[2].Rows[i]["mortgage_type"].ToString());
                        docText = id_lender3.Replace(docText, dsmortgage.Tables[2].Rows[i]["lender"].ToString());
                        docText = id_assgrantor3.Replace(docText, dsmortgage.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_arda3.Replace(docText, dsmortgage.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_arf3.Replace(docText, dsmortgage.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_arv3.Replace(docText, dsmortgage.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_arp3.Replace(docText, dsmortgage.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_arins3.Replace(docText, dsmortgage.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_arnotes3.Replace(docText, dsmortgage.Tables[2].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_assofrents4.Replace(docText, dsmortgage.Tables[2].Rows[i]["mortgage_type"].ToString());
                        docText = id_lender4.Replace(docText, dsmortgage.Tables[2].Rows[i]["lender"].ToString());
                        docText = id_assgrantor4.Replace(docText, dsmortgage.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_arda4.Replace(docText, dsmortgage.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_arf4.Replace(docText, dsmortgage.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_arv4.Replace(docText, dsmortgage.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_arp4.Replace(docText, dsmortgage.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_arins4.Replace(docText, dsmortgage.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_arnotes4.Replace(docText, dsmortgage.Tables[2].Rows[i]["notes"].ToString());
                    }


                    if (i == 4)
                    {

                        docText = id_assofrents5.Replace(docText, dsmortgage.Tables[2].Rows[i]["mortgage_type"].ToString());
                        docText = id_lender5.Replace(docText, dsmortgage.Tables[2].Rows[i]["lender"].ToString());
                        docText = id_assgrantor5.Replace(docText, dsmortgage.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_arda5.Replace(docText, dsmortgage.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_arf5.Replace(docText, dsmortgage.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_arv5.Replace(docText, dsmortgage.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_arp5.Replace(docText, dsmortgage.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_arins5.Replace(docText, dsmortgage.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_arnotes5.Replace(docText, dsmortgage.Tables[2].Rows[i]["notes"].ToString());
                    }

                }
            }






            if (dsmortgage.Tables[3].Rows.Count > 0)
            {


                for (int i = 0; i < dsmortgage.Tables[3].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_ass.Replace(docText, dsmortgage.Tables[3].Rows[0]["mortgage_type"].ToString());
                        docText = id_aassignee1.Replace(docText, dsmortgage.Tables[3].Rows[0]["assigne"].ToString());
                        docText = id_aassignor1.Replace(docText, dsmortgage.Tables[3].Rows[0]["assignor"].ToString());
                        docText = id_ada1.Replace(docText, dsmortgage.Tables[3].Rows[0]["dated"].ToString());
                        docText = id_af1.Replace(docText, dsmortgage.Tables[3].Rows[0]["filed"].ToString());
                        docText = id_av1.Replace(docText, dsmortgage.Tables[3].Rows[0]["vol"].ToString());
                        docText = id_ap1.Replace(docText, dsmortgage.Tables[3].Rows[0]["pg"].ToString());
                        docText = id_ains1.Replace(docText, dsmortgage.Tables[3].Rows[0]["inst"].ToString());
                        docText = id_anotes1.Replace(docText, dsmortgage.Tables[3].Rows[0]["notes"].ToString());

                    }

                    if (i == 1)
                    {

                        docText = id_ass2.Replace(docText, dsmortgage.Tables[3].Rows[i]["mortgage_type"].ToString());
                        docText = id_aassignee2.Replace(docText, dsmortgage.Tables[3].Rows[i]["assigne"].ToString());
                        docText = id_aassignor2.Replace(docText, dsmortgage.Tables[3].Rows[i]["assignor"].ToString());
                        docText = id_ada2.Replace(docText, dsmortgage.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_af2.Replace(docText, dsmortgage.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_av2.Replace(docText, dsmortgage.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_ap2.Replace(docText, dsmortgage.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_ains2.Replace(docText, dsmortgage.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_anotes2.Replace(docText, dsmortgage.Tables[3].Rows[i]["notes"].ToString());

                    }

                    if (i == 2)
                    {

                        docText = id_ass3.Replace(docText, dsmortgage.Tables[3].Rows[i]["mortgage_type"].ToString());
                        docText = id_aassignee3.Replace(docText, dsmortgage.Tables[3].Rows[i]["assigne"].ToString());
                        docText = id_aassignor3.Replace(docText, dsmortgage.Tables[3].Rows[i]["assignor"].ToString());
                        docText = id_ada3.Replace(docText, dsmortgage.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_af3.Replace(docText, dsmortgage.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_av3.Replace(docText, dsmortgage.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_ap3.Replace(docText, dsmortgage.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_ains3.Replace(docText, dsmortgage.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_anotes3.Replace(docText, dsmortgage.Tables[3].Rows[i]["notes"].ToString());

                    }

                    if (i == 3)
                    {

                        docText = id_ass4.Replace(docText, dsmortgage.Tables[3].Rows[i]["mortgage_type"].ToString());
                        docText = id_aassignee4.Replace(docText, dsmortgage.Tables[3].Rows[i]["assigne"].ToString());
                        docText = id_aassignor4.Replace(docText, dsmortgage.Tables[3].Rows[i]["assignor"].ToString());
                        docText = id_ada4.Replace(docText, dsmortgage.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_af4.Replace(docText, dsmortgage.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_av4.Replace(docText, dsmortgage.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_ap4.Replace(docText, dsmortgage.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_ains4.Replace(docText, dsmortgage.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_anotes4.Replace(docText, dsmortgage.Tables[3].Rows[i]["notes"].ToString());

                    }

                    if (i == 4)
                    {

                        docText = id_ass5.Replace(docText, dsmortgage.Tables[3].Rows[i]["mortgage_type"].ToString());
                        docText = id_aassignee5.Replace(docText, dsmortgage.Tables[3].Rows[i]["assigne"].ToString());
                        docText = id_aassignor5.Replace(docText, dsmortgage.Tables[3].Rows[i]["assignor"].ToString());
                        docText = id_ada5.Replace(docText, dsmortgage.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_af5.Replace(docText, dsmortgage.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_av5.Replace(docText, dsmortgage.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_ap5.Replace(docText, dsmortgage.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_ains5.Replace(docText, dsmortgage.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_anotes5.Replace(docText, dsmortgage.Tables[3].Rows[i]["notes"].ToString());

                    }





                }
            }


            if (dsmortgage.Tables[4].Rows.Count > 0)
            {

                for (int i = 0; i < dsmortgage.Tables[4].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_deedoftrust1.Replace(docText, dsmortgage.Tables[4].Rows[i]["mortgage_type"].ToString());
                        docText = id_dotpayable1.Replace(docText, dsmortgage.Tables[4].Rows[i]["payable_to"].ToString());
                        docText = id_dotgrantor1.Replace(docText, dsmortgage.Tables[4].Rows[i]["grantor"].ToString());
                        docText = id_dottrustee1.Replace(docText, dsmortgage.Tables[4].Rows[i]["trustee"].ToString());
                        docText = id_dotda1.Replace(docText, dsmortgage.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_dotf1.Replace(docText, dsmortgage.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_dotv1.Replace(docText, dsmortgage.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_dotp1.Replace(docText, dsmortgage.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_dotins1.Replace(docText, dsmortgage.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_dotamount1.Replace(docText, dsmortgage.Tables[4].Rows[i]["amount"].ToString());

                    }

                    if (i == 1)
                    {

                        docText = id_deedoftrust2.Replace(docText, dsmortgage.Tables[4].Rows[i]["mortgage_type"].ToString());
                        docText = id_dotpayable2.Replace(docText, dsmortgage.Tables[4].Rows[i]["payable_to"].ToString());
                        docText = id_dotgrantor2.Replace(docText, dsmortgage.Tables[4].Rows[i]["grantor"].ToString());
                        docText = id_dottrustee2.Replace(docText, dsmortgage.Tables[4].Rows[i]["trustee"].ToString());
                        docText = id_dotda2.Replace(docText, dsmortgage.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_dotf2.Replace(docText, dsmortgage.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_dotv2.Replace(docText, dsmortgage.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_dotp2.Replace(docText, dsmortgage.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_dotins2.Replace(docText, dsmortgage.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_dotamount2.Replace(docText, dsmortgage.Tables[4].Rows[i]["amount"].ToString());

                    }

                    if (i == 2)
                    {

                        docText = id_deedoftrust3.Replace(docText, dsmortgage.Tables[4].Rows[i]["mortgage_type"].ToString());
                        docText = id_dotpayable3.Replace(docText, dsmortgage.Tables[4].Rows[i]["payable_to"].ToString());
                        docText = id_dotgrantor3.Replace(docText, dsmortgage.Tables[4].Rows[i]["grantor"].ToString());
                        docText = id_dottrustee3.Replace(docText, dsmortgage.Tables[4].Rows[i]["trustee"].ToString());
                        docText = id_dotda3.Replace(docText, dsmortgage.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_dotf3.Replace(docText, dsmortgage.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_dotv3.Replace(docText, dsmortgage.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_dotp3.Replace(docText, dsmortgage.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_dotins3.Replace(docText, dsmortgage.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_dotamount3.Replace(docText, dsmortgage.Tables[4].Rows[i]["amount"].ToString());

                    }

                    if (i == 3)
                    {

                        docText = id_deedoftrust4.Replace(docText, dsmortgage.Tables[4].Rows[i]["mortgage_type"].ToString());
                        docText = id_dotpayable4.Replace(docText, dsmortgage.Tables[4].Rows[i]["payable_to"].ToString());
                        docText = id_dotgrantor4.Replace(docText, dsmortgage.Tables[4].Rows[i]["grantor"].ToString());
                        docText = id_dottrustee4.Replace(docText, dsmortgage.Tables[4].Rows[i]["trustee"].ToString());
                        docText = id_dotda4.Replace(docText, dsmortgage.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_dotf4.Replace(docText, dsmortgage.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_dotv4.Replace(docText, dsmortgage.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_dotp4.Replace(docText, dsmortgage.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_dotins4.Replace(docText, dsmortgage.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_dotamount4.Replace(docText, dsmortgage.Tables[4].Rows[i]["amount"].ToString());

                    }

                    if (i == 4)
                    {

                        docText = id_deedoftrust5.Replace(docText, dsmortgage.Tables[4].Rows[i]["mortgage_type"].ToString());
                        docText = id_dotpayable5.Replace(docText, dsmortgage.Tables[4].Rows[i]["payable_to"].ToString());
                        docText = id_dotgrantor5.Replace(docText, dsmortgage.Tables[4].Rows[i]["grantor"].ToString());
                        docText = id_dottrustee5.Replace(docText, dsmortgage.Tables[4].Rows[i]["trustee"].ToString());
                        docText = id_dotda5.Replace(docText, dsmortgage.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_dotf5.Replace(docText, dsmortgage.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_dotv5.Replace(docText, dsmortgage.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_dotp5.Replace(docText, dsmortgage.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_dotins5.Replace(docText, dsmortgage.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_dotamount5.Replace(docText, dsmortgage.Tables[4].Rows[i]["amount"].ToString());

                    }


                }

            }




            if (dsmortgage.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < dsmortgage.Tables[5].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_loanmodify.Replace(docText, dsmortgage.Tables[5].Rows[i]["mortgage_type"].ToString());
                        docText = id_modifybtwn.Replace(docText, dsmortgage.Tables[5].Rows[i]["by_and_between"].ToString());
                        docText = id_mda1.Replace(docText, dsmortgage.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_mf1.Replace(docText, dsmortgage.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_mv1.Replace(docText, dsmortgage.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_mp1.Replace(docText, dsmortgage.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_mins1.Replace(docText, dsmortgage.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_mnotes1.Replace(docText, dsmortgage.Tables[5].Rows[i]["notes"].ToString());
                    }


                    if (i == 1)
                    {

                        docText = id_loanmodify2.Replace(docText, dsmortgage.Tables[5].Rows[i]["mortgage_type"].ToString());
                        docText = id_modifybtwn2.Replace(docText, dsmortgage.Tables[5].Rows[i]["by_and_between"].ToString());
                        docText = id_mda2.Replace(docText, dsmortgage.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_mf2.Replace(docText, dsmortgage.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_mv2.Replace(docText, dsmortgage.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_mp2.Replace(docText, dsmortgage.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_mins2.Replace(docText, dsmortgage.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_mnotes2.Replace(docText, dsmortgage.Tables[5].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_loanmodify3.Replace(docText, dsmortgage.Tables[5].Rows[i]["mortgage_type"].ToString());
                        docText = id_modifybtwn3.Replace(docText, dsmortgage.Tables[5].Rows[i]["by_and_between"].ToString());
                        docText = id_mda3.Replace(docText, dsmortgage.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_mf3.Replace(docText, dsmortgage.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_mv3.Replace(docText, dsmortgage.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_mp3.Replace(docText, dsmortgage.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_mins3.Replace(docText, dsmortgage.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_mnotes3.Replace(docText, dsmortgage.Tables[5].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_loanmodify4.Replace(docText, dsmortgage.Tables[5].Rows[i]["mortgage_type"].ToString());
                        docText = id_modifybtwn4.Replace(docText, dsmortgage.Tables[5].Rows[i]["by_and_between"].ToString());
                        docText = id_mda4.Replace(docText, dsmortgage.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_mf4.Replace(docText, dsmortgage.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_mv4.Replace(docText, dsmortgage.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_mp4.Replace(docText, dsmortgage.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_mins4.Replace(docText, dsmortgage.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_mnotes4.Replace(docText, dsmortgage.Tables[5].Rows[i]["notes"].ToString());
                    }

                    if (i == 4)
                    {

                        docText = id_loanmodify5.Replace(docText, dsmortgage.Tables[5].Rows[i]["mortgage_type"].ToString());
                        docText = id_modifybtwn5.Replace(docText, dsmortgage.Tables[5].Rows[i]["by_and_between"].ToString());
                        docText = id_mda5.Replace(docText, dsmortgage.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_mf5.Replace(docText, dsmortgage.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_mv5.Replace(docText, dsmortgage.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_mp5.Replace(docText, dsmortgage.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_mins5.Replace(docText, dsmortgage.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_mnotes5.Replace(docText, dsmortgage.Tables[5].Rows[i]["notes"].ToString());
                    }


                }
            }




            if (dsmortgage.Tables[6].Rows.Count > 0)
            {

                for (int i = 0; i < dsmortgage.Tables[6].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_subdee.Replace(docText, dsmortgage.Tables[6].Rows[i]["mortgage_type"].ToString());
                        docText = id_sdotpayable1.Replace(docText, dsmortgage.Tables[6].Rows[i]["payable_to"].ToString());
                        docText = id_sdotgrantor1.Replace(docText, dsmortgage.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_sdottrustee1.Replace(docText, dsmortgage.Tables[6].Rows[i]["trustee"].ToString());
                        docText = id_sdda1.Replace(docText, dsmortgage.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_sdf1.Replace(docText, dsmortgage.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_sdv1.Replace(docText, dsmortgage.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_sdp1.Replace(docText, dsmortgage.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_sdins1.Replace(docText, dsmortgage.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_sdotamount1.Replace(docText, dsmortgage.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_subdee2.Replace(docText, dsmortgage.Tables[6].Rows[i]["mortgage_type"].ToString());
                        docText = id_sdotpayable2.Replace(docText, dsmortgage.Tables[6].Rows[i]["payable_to"].ToString());
                        docText = id_sdotgrantor2.Replace(docText, dsmortgage.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_sdottrustee2.Replace(docText, dsmortgage.Tables[6].Rows[i]["trustee"].ToString());
                        docText = id_sdda2.Replace(docText, dsmortgage.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_sdf2.Replace(docText, dsmortgage.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_sdv2.Replace(docText, dsmortgage.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_sdp2.Replace(docText, dsmortgage.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_sdins2.Replace(docText, dsmortgage.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_sdotamount2.Replace(docText, dsmortgage.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_subdee3.Replace(docText, dsmortgage.Tables[6].Rows[i]["mortgage_type"].ToString());
                        docText = id_sdotpayable3.Replace(docText, dsmortgage.Tables[6].Rows[i]["payable_to"].ToString());
                        docText = id_sdotgrantor3.Replace(docText, dsmortgage.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_sdottrustee3.Replace(docText, dsmortgage.Tables[6].Rows[i]["trustee"].ToString());
                        docText = id_sdda3.Replace(docText, dsmortgage.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_sdf3.Replace(docText, dsmortgage.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_sdv3.Replace(docText, dsmortgage.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_sdp3.Replace(docText, dsmortgage.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_sdins3.Replace(docText, dsmortgage.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_sdotamount3.Replace(docText, dsmortgage.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_subdee4.Replace(docText, dsmortgage.Tables[6].Rows[i]["mortgage_type"].ToString());
                        docText = id_sdotpayable4.Replace(docText, dsmortgage.Tables[6].Rows[i]["payable_to"].ToString());
                        docText = id_sdotgrantor4.Replace(docText, dsmortgage.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_sdottrustee4.Replace(docText, dsmortgage.Tables[6].Rows[i]["trustee"].ToString());
                        docText = id_sdda4.Replace(docText, dsmortgage.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_sdf4.Replace(docText, dsmortgage.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_sdv4.Replace(docText, dsmortgage.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_sdp4.Replace(docText, dsmortgage.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_sdins4.Replace(docText, dsmortgage.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_sdotamount4.Replace(docText, dsmortgage.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 4)
                    {

                        docText = id_subdee5.Replace(docText, dsmortgage.Tables[6].Rows[i]["mortgage_type"].ToString());
                        docText = id_sdotpayable5.Replace(docText, dsmortgage.Tables[6].Rows[i]["payable_to"].ToString());
                        docText = id_sdotgrantor5.Replace(docText, dsmortgage.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_sdottrustee5.Replace(docText, dsmortgage.Tables[6].Rows[i]["trustee"].ToString());
                        docText = id_sdda5.Replace(docText, dsmortgage.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_sdf5.Replace(docText, dsmortgage.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_sdv5.Replace(docText, dsmortgage.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_sdp5.Replace(docText, dsmortgage.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_sdins5.Replace(docText, dsmortgage.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_sdotamount5.Replace(docText, dsmortgage.Tables[6].Rows[i]["amount"].ToString());
                    }


                }
            }



            if (dsmortgage.Tables[7].Rows.Count > 0)
            {


                for (int i = 0; i < dsmortgage.Tables[7].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_finanstate.Replace(docText, dsmortgage.Tables[7].Rows[i]["mortgage_type"].ToString());
                        docText = id_finsecured.Replace(docText, dsmortgage.Tables[7].Rows[i]["secured_party"].ToString());
                        docText = id_findebtor.Replace(docText, dsmortgage.Tables[7].Rows[i]["deptor"].ToString());
                        docText = id_fda1.Replace(docText, dsmortgage.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_ff1.Replace(docText, dsmortgage.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_fv1.Replace(docText, dsmortgage.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_fp1.Replace(docText, dsmortgage.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_fins1.Replace(docText, dsmortgage.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_fnotes1.Replace(docText, dsmortgage.Tables[7].Rows[i]["notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_finanstate2.Replace(docText, dsmortgage.Tables[7].Rows[i]["mortgage_type"].ToString());
                        docText = id_finsecured2.Replace(docText, dsmortgage.Tables[7].Rows[i]["secured_party"].ToString());
                        docText = id_findebtor2.Replace(docText, dsmortgage.Tables[7].Rows[i]["deptor"].ToString());
                        docText = id_fda2.Replace(docText, dsmortgage.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_ff2.Replace(docText, dsmortgage.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_fv2.Replace(docText, dsmortgage.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_fp2.Replace(docText, dsmortgage.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_fins2.Replace(docText, dsmortgage.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_fnotes2.Replace(docText, dsmortgage.Tables[7].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_finanstate3.Replace(docText, dsmortgage.Tables[7].Rows[i]["mortgage_type"].ToString());
                        docText = id_finsecured3.Replace(docText, dsmortgage.Tables[7].Rows[i]["secured_party"].ToString());
                        docText = id_findebtor3.Replace(docText, dsmortgage.Tables[7].Rows[i]["deptor"].ToString());
                        docText = id_fda3.Replace(docText, dsmortgage.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_ff3.Replace(docText, dsmortgage.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_fv3.Replace(docText, dsmortgage.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_fp3.Replace(docText, dsmortgage.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_fins3.Replace(docText, dsmortgage.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_fnotes3.Replace(docText, dsmortgage.Tables[7].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_finanstate4.Replace(docText, dsmortgage.Tables[7].Rows[i]["mortgage_type"].ToString());
                        docText = id_finsecured4.Replace(docText, dsmortgage.Tables[7].Rows[i]["secured_party"].ToString());
                        docText = id_findebtor4.Replace(docText, dsmortgage.Tables[7].Rows[i]["deptor"].ToString());
                        docText = id_fda4.Replace(docText, dsmortgage.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_ff4.Replace(docText, dsmortgage.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_fv4.Replace(docText, dsmortgage.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_fp4.Replace(docText, dsmortgage.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_fins4.Replace(docText, dsmortgage.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_fnotes4.Replace(docText, dsmortgage.Tables[7].Rows[i]["notes"].ToString());
                    }

                    if (i == 4)
                    {

                        docText = id_finanstate5.Replace(docText, dsmortgage.Tables[7].Rows[i]["mortgage_type"].ToString());
                        docText = id_finsecured5.Replace(docText, dsmortgage.Tables[7].Rows[i]["secured_party"].ToString());
                        docText = id_findebtor5.Replace(docText, dsmortgage.Tables[7].Rows[i]["deptor"].ToString());
                        docText = id_fda5.Replace(docText, dsmortgage.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_ff5.Replace(docText, dsmortgage.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_fv5.Replace(docText, dsmortgage.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_fp5.Replace(docText, dsmortgage.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_fins5.Replace(docText, dsmortgage.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_fnotes5.Replace(docText, dsmortgage.Tables[7].Rows[i]["notes"].ToString());
                    }


                }
            }

            #endregion

            #region Judgement
            DataSet dsjudgement = new DataSet();
            dsjudgement = gls.gettypevalue(lbl_orderno.Text, "sp_sel_judgement_output");
            if (dsjudgement.Tables[0].Rows.Count > 0) // ABSTRACT OF ASSESSMENT TEXAS WORKFORCE COMMISSION
            {

                for (int i = 0; i < dsjudgement.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {


                        docText = id_abstractofasstax1.Replace(docText, dsjudgement.Tables[0].Rows[0]["judgement_type"].ToString());
                        docText = id_jaataxpayer1.Replace(docText, dsjudgement.Tables[0].Rows[0]["Taxpayer"].ToString());
                        docText = id_jaaadress1.Replace(docText, dsjudgement.Tables[0].Rows[0]["Address"].ToString());
                        docText = id_jaataxpayerid1.Replace(docText, dsjudgement.Tables[0].Rows[0]["Taxpayerid"].ToString());
                        docText = id_jaada1.Replace(docText, dsjudgement.Tables[0].Rows[0]["dated"].ToString());
                        docText = id_jaaf1.Replace(docText, dsjudgement.Tables[0].Rows[0]["filed"].ToString());
                        docText = id_jaav1.Replace(docText, dsjudgement.Tables[0].Rows[0]["vol"].ToString());
                        docText = id_jaap1.Replace(docText, dsjudgement.Tables[0].Rows[0]["pg"].ToString());
                        docText = id_jaains1.Replace(docText, dsjudgement.Tables[0].Rows[0]["inst"].ToString());
                        docText = id_jaanotes1.Replace(docText, dsjudgement.Tables[0].Rows[0]["amount"].ToString());
                    }

                    if (i == 1)
                    {


                        docText = id_abstractofasstax2.Replace(docText, dsjudgement.Tables[0].Rows[i]["judgement_type"].ToString());
                        docText = id_jaataxpayer2.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayer"].ToString());
                        docText = id_jaaadress2.Replace(docText, dsjudgement.Tables[0].Rows[i]["Address"].ToString());
                        docText = id_jaataxpayerid2.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jaada2.Replace(docText, dsjudgement.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_jaaf2.Replace(docText, dsjudgement.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_jaav2.Replace(docText, dsjudgement.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_jaap2.Replace(docText, dsjudgement.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_jaains2.Replace(docText, dsjudgement.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_jaanotes2.Replace(docText, dsjudgement.Tables[0].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {


                        docText = id_abstractofasstax3.Replace(docText, dsjudgement.Tables[0].Rows[i]["judgement_type"].ToString());
                        docText = id_jaataxpayer3.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayer"].ToString());
                        docText = id_jaaadress3.Replace(docText, dsjudgement.Tables[0].Rows[i]["Address"].ToString());
                        docText = id_jaataxpayerid3.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jaada3.Replace(docText, dsjudgement.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_jaaf3.Replace(docText, dsjudgement.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_jaav3.Replace(docText, dsjudgement.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_jaap3.Replace(docText, dsjudgement.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_jaains3.Replace(docText, dsjudgement.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_jaanotes3.Replace(docText, dsjudgement.Tables[0].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {


                        docText = id_abstractofasstax4.Replace(docText, dsjudgement.Tables[0].Rows[i]["judgement_type"].ToString());
                        docText = id_jaataxpayer4.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayer"].ToString());
                        docText = id_jaaadress4.Replace(docText, dsjudgement.Tables[0].Rows[i]["Address"].ToString());
                        docText = id_jaataxpayerid4.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jaada4.Replace(docText, dsjudgement.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_jaaf4.Replace(docText, dsjudgement.Tables[0].Rows[i]["filed"].ToString());
                        docText = id_jaav4.Replace(docText, dsjudgement.Tables[0].Rows[i]["vol"].ToString());
                        docText = id_jaap4.Replace(docText, dsjudgement.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_jaains4.Replace(docText, dsjudgement.Tables[0].Rows[i]["inst"].ToString());
                        docText = id_jaanotes4.Replace(docText, dsjudgement.Tables[0].Rows[i]["amount"].ToString());
                    }

                    //if (i == 4)
                    //{


                    //    docText = id_abstractofasstax5.Replace(docText, dsjudgement.Tables[0].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jaataxpayer5.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayer"].ToString());
                    //    docText = id_jaaadress5.Replace(docText, dsjudgement.Tables[0].Rows[i]["Address"].ToString());
                    //    docText = id_jaataxpayerid5.Replace(docText, dsjudgement.Tables[0].Rows[i]["Taxpayerid"].ToString());
                    //    docText = id_jaada5.Replace(docText, dsjudgement.Tables[0].Rows[i]["dated"].ToString());
                    //    docText = id_jaaf5.Replace(docText, dsjudgement.Tables[0].Rows[i]["filed"].ToString());
                    //    docText = id_jaav5.Replace(docText, dsjudgement.Tables[0].Rows[i]["vol"].ToString());
                    //    docText = id_jaap5.Replace(docText, dsjudgement.Tables[0].Rows[i]["pg"].ToString());
                    //    docText = id_jaains5.Replace(docText, dsjudgement.Tables[0].Rows[i]["inst"].ToString());
                    //    docText = id_jaanotes5.Replace(docText, dsjudgement.Tables[0].Rows[i]["amount"].ToString());
                    //}


                }
            }



            if (dsjudgement.Tables[1].Rows.Count > 0) // ABSTRACT OF JUDGMENT
            {

                for (int i = 0; i < dsjudgement.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_abstractofjudg1.Replace(docText, dsjudgement.Tables[1].Rows[0]["judgement_type"].ToString());
                        docText = id_jajdeffendant1.Replace(docText, dsjudgement.Tables[1].Rows[0]["Address"].ToString());
                        docText = id_jajaddress1.Replace(docText, dsjudgement.Tables[1].Rows[0]["defendant"].ToString());
                        docText = id_jajplaintiff1.Replace(docText, dsjudgement.Tables[1].Rows[0]["paintiff"].ToString());
                        docText = id_jajda1.Replace(docText, dsjudgement.Tables[1].Rows[0]["dated"].ToString());
                        docText = id_jajf1.Replace(docText, dsjudgement.Tables[1].Rows[0]["filed"].ToString());
                        docText = id_jajv1.Replace(docText, dsjudgement.Tables[1].Rows[0]["vol"].ToString());
                        docText = id_jajp1.Replace(docText, dsjudgement.Tables[1].Rows[0]["pg"].ToString());
                        docText = id_jajins1.Replace(docText, dsjudgement.Tables[1].Rows[0]["inst"].ToString());
                        docText = id_jajam1.Replace(docText, dsjudgement.Tables[1].Rows[0]["amount"].ToString());
                        docText = id_jajc1.Replace(docText, dsjudgement.Tables[1].Rows[0]["cost"].ToString());
                        docText = id_jajat1.Replace(docText, dsjudgement.Tables[1].Rows[0]["atty"].ToString());
                        docText = id_jajint1.Replace(docText, dsjudgement.Tables[1].Rows[0]["intt"].ToString());
                        docText = id_jajcause1.Replace(docText, dsjudgement.Tables[1].Rows[0]["cause"].ToString());
                    }

                    if (i == 1)
                    {
                        docText = id_abstractofjudg2.Replace(docText, dsjudgement.Tables[1].Rows[i]["judgement_type"].ToString());
                        docText = id_jajdeffendant2.Replace(docText, dsjudgement.Tables[1].Rows[i]["Address"].ToString());
                        docText = id_jajaddress2.Replace(docText, dsjudgement.Tables[1].Rows[i]["defendant"].ToString());
                        docText = id_jajplaintiff2.Replace(docText, dsjudgement.Tables[1].Rows[i]["paintiff"].ToString());
                        docText = id_jajda2.Replace(docText, dsjudgement.Tables[1].Rows[i]["dated"].ToString());
                        docText = id_jajf2.Replace(docText, dsjudgement.Tables[1].Rows[i]["filed"].ToString());
                        docText = id_jajv2.Replace(docText, dsjudgement.Tables[1].Rows[i]["vol"].ToString());
                        docText = id_jajp2.Replace(docText, dsjudgement.Tables[1].Rows[i]["pg"].ToString());
                        docText = id_jajins2.Replace(docText, dsjudgement.Tables[1].Rows[i]["inst"].ToString());
                        docText = id_jajam2.Replace(docText, dsjudgement.Tables[1].Rows[i]["amount"].ToString());
                        docText = id_jajc2.Replace(docText, dsjudgement.Tables[1].Rows[i]["cost"].ToString());
                        docText = id_jajat2.Replace(docText, dsjudgement.Tables[1].Rows[i]["atty"].ToString());
                        docText = id_jajint2.Replace(docText, dsjudgement.Tables[1].Rows[i]["intt"].ToString());
                        docText = id_jajcause2.Replace(docText, dsjudgement.Tables[1].Rows[i]["cause"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_abstractofjudg3.Replace(docText, dsjudgement.Tables[1].Rows[i]["judgement_type"].ToString());
                        docText = id_jajdeffendant3.Replace(docText, dsjudgement.Tables[1].Rows[i]["Address"].ToString());
                        docText = id_jajaddress3.Replace(docText, dsjudgement.Tables[1].Rows[i]["defendant"].ToString());
                        docText = id_jajplaintiff3.Replace(docText, dsjudgement.Tables[1].Rows[i]["paintiff"].ToString());
                        docText = id_jajda3.Replace(docText, dsjudgement.Tables[1].Rows[i]["dated"].ToString());
                        docText = id_jajf3.Replace(docText, dsjudgement.Tables[1].Rows[i]["filed"].ToString());
                        docText = id_jajv3.Replace(docText, dsjudgement.Tables[1].Rows[i]["vol"].ToString());
                        docText = id_jajp3.Replace(docText, dsjudgement.Tables[1].Rows[i]["pg"].ToString());
                        docText = id_jajins3.Replace(docText, dsjudgement.Tables[1].Rows[i]["inst"].ToString());
                        docText = id_jajam3.Replace(docText, dsjudgement.Tables[1].Rows[i]["amount"].ToString());
                        docText = id_jajc3.Replace(docText, dsjudgement.Tables[1].Rows[i]["cost"].ToString());
                        docText = id_jajat3.Replace(docText, dsjudgement.Tables[1].Rows[i]["atty"].ToString());
                        docText = id_jajint3.Replace(docText, dsjudgement.Tables[1].Rows[i]["intt"].ToString());
                        docText = id_jajcause3.Replace(docText, dsjudgement.Tables[1].Rows[i]["cause"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_abstractofjudg4.Replace(docText, dsjudgement.Tables[1].Rows[i]["judgement_type"].ToString());
                        docText = id_jajdeffendant4.Replace(docText, dsjudgement.Tables[1].Rows[i]["Address"].ToString());
                        docText = id_jajaddress4.Replace(docText, dsjudgement.Tables[1].Rows[i]["defendant"].ToString());
                        docText = id_jajplaintiff4.Replace(docText, dsjudgement.Tables[1].Rows[i]["paintiff"].ToString());
                        docText = id_jajda4.Replace(docText, dsjudgement.Tables[1].Rows[i]["dated"].ToString());
                        docText = id_jajf4.Replace(docText, dsjudgement.Tables[1].Rows[i]["filed"].ToString());
                        docText = id_jajv4.Replace(docText, dsjudgement.Tables[1].Rows[i]["vol"].ToString());
                        docText = id_jajp4.Replace(docText, dsjudgement.Tables[1].Rows[i]["pg"].ToString());
                        docText = id_jajins4.Replace(docText, dsjudgement.Tables[1].Rows[i]["inst"].ToString());
                        docText = id_jajam4.Replace(docText, dsjudgement.Tables[1].Rows[i]["amount"].ToString());
                        docText = id_jajc4.Replace(docText, dsjudgement.Tables[1].Rows[i]["cost"].ToString());
                        docText = id_jajat4.Replace(docText, dsjudgement.Tables[1].Rows[i]["atty"].ToString());
                        docText = id_jajint4.Replace(docText, dsjudgement.Tables[1].Rows[i]["intt"].ToString());
                        docText = id_jajcause4.Replace(docText, dsjudgement.Tables[1].Rows[i]["cause"].ToString());
                    }

                    //if (i == 4)
                    //{
                    //    docText = id_abstractofjudg5.Replace(docText, dsjudgement.Tables[1].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jajdeffendant5.Replace(docText, dsjudgement.Tables[1].Rows[i]["Address"].ToString());
                    //    docText = id_jajaddress5.Replace(docText, dsjudgement.Tables[1].Rows[i]["defendant"].ToString());
                    //    docText = id_jajplaintiff5.Replace(docText, dsjudgement.Tables[1].Rows[i]["paintiff"].ToString());
                    //    docText = id_jajda5.Replace(docText, dsjudgement.Tables[1].Rows[i]["dated"].ToString());
                    //    docText = id_jajf5.Replace(docText, dsjudgement.Tables[1].Rows[i]["filed"].ToString());
                    //    docText = id_jajv5.Replace(docText, dsjudgement.Tables[1].Rows[i]["vol"].ToString());
                    //    docText = id_jajp5.Replace(docText, dsjudgement.Tables[1].Rows[i]["pg"].ToString());
                    //    docText = id_jajins5.Replace(docText, dsjudgement.Tables[1].Rows[i]["inst"].ToString());
                    //    docText = id_jajam5.Replace(docText, dsjudgement.Tables[1].Rows[i]["amount"].ToString());
                    //    docText = id_jajc5.Replace(docText, dsjudgement.Tables[1].Rows[i]["cost"].ToString());
                    //    docText = id_jajat5.Replace(docText, dsjudgement.Tables[1].Rows[i]["atty"].ToString());
                    //    docText = id_jajint5.Replace(docText, dsjudgement.Tables[1].Rows[i]["intt"].ToString());
                    //    docText = id_jajcause5.Replace(docText, dsjudgement.Tables[1].Rows[i]["cause"].ToString());
                    //}




                }

            }

            if (dsjudgement.Tables[2].Rows.Count > 0) // AFFIDAVIT OF DELINQUENT ASSESSMENT AND NOTICE OF LIEN
            {

                for (int i = 0; i < dsjudgement.Tables[2].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_affofdelinquent1.Replace(docText, dsjudgement.Tables[2].Rows[0]["judgement_type"].ToString());
                        docText = id_jadowner1.Replace(docText, dsjudgement.Tables[2].Rows[0]["owner"].ToString());
                        docText = id_jadgrantor1.Replace(docText, dsjudgement.Tables[2].Rows[0]["grantor"].ToString());
                        docText = id_jadda1.Replace(docText, dsjudgement.Tables[2].Rows[0]["dated"].ToString());
                        docText = id_jadf1.Replace(docText, dsjudgement.Tables[2].Rows[0]["filed"].ToString());
                        docText = id_jadv1.Replace(docText, dsjudgement.Tables[2].Rows[0]["vol"].ToString());
                        docText = id_jadp1.Replace(docText, dsjudgement.Tables[2].Rows[0]["pg"].ToString());
                        docText = id_jadins1.Replace(docText, dsjudgement.Tables[2].Rows[0]["inst"].ToString());
                        docText = id_jadamount1.Replace(docText, dsjudgement.Tables[2].Rows[0]["notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_affofdelinquent2.Replace(docText, dsjudgement.Tables[2].Rows[i]["judgement_type"].ToString());
                        docText = id_jadowner2.Replace(docText, dsjudgement.Tables[2].Rows[i]["owner"].ToString());
                        docText = id_jadgrantor2.Replace(docText, dsjudgement.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_jadda2.Replace(docText, dsjudgement.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_jadf2.Replace(docText, dsjudgement.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_jadv2.Replace(docText, dsjudgement.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_jadp2.Replace(docText, dsjudgement.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_jadins2.Replace(docText, dsjudgement.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_jadamount2.Replace(docText, dsjudgement.Tables[2].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_affofdelinquent3.Replace(docText, dsjudgement.Tables[2].Rows[i]["judgement_type"].ToString());
                        docText = id_jadowner3.Replace(docText, dsjudgement.Tables[2].Rows[i]["owner"].ToString());
                        docText = id_jadgrantor3.Replace(docText, dsjudgement.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_jadda3.Replace(docText, dsjudgement.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_jadf3.Replace(docText, dsjudgement.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_jadv3.Replace(docText, dsjudgement.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_jadp3.Replace(docText, dsjudgement.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_jadins3.Replace(docText, dsjudgement.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_jadamount3.Replace(docText, dsjudgement.Tables[2].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_affofdelinquent4.Replace(docText, dsjudgement.Tables[2].Rows[i]["judgement_type"].ToString());
                        docText = id_jadowner4.Replace(docText, dsjudgement.Tables[2].Rows[i]["owner"].ToString());
                        docText = id_jadgrantor4.Replace(docText, dsjudgement.Tables[2].Rows[i]["grantor"].ToString());
                        docText = id_jadda4.Replace(docText, dsjudgement.Tables[2].Rows[i]["dated"].ToString());
                        docText = id_jadf4.Replace(docText, dsjudgement.Tables[2].Rows[i]["filed"].ToString());
                        docText = id_jadv4.Replace(docText, dsjudgement.Tables[2].Rows[i]["vol"].ToString());
                        docText = id_jadp4.Replace(docText, dsjudgement.Tables[2].Rows[i]["pg"].ToString());
                        docText = id_jadins4.Replace(docText, dsjudgement.Tables[2].Rows[i]["inst"].ToString());
                        docText = id_jadamount4.Replace(docText, dsjudgement.Tables[2].Rows[i]["notes"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_affofdelinquent5.Replace(docText, dsjudgement.Tables[2].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jadowner5.Replace(docText, dsjudgement.Tables[2].Rows[i]["owner"].ToString());
                    //    docText = id_jadgrantor5.Replace(docText, dsjudgement.Tables[2].Rows[i]["grantor"].ToString());
                    //    docText = id_jadda5.Replace(docText, dsjudgement.Tables[2].Rows[i]["dated"].ToString());
                    //    docText = id_jadf5.Replace(docText, dsjudgement.Tables[2].Rows[i]["filed"].ToString());
                    //    docText = id_jadv5.Replace(docText, dsjudgement.Tables[2].Rows[i]["vol"].ToString());
                    //    docText = id_jadp5.Replace(docText, dsjudgement.Tables[2].Rows[i]["pg"].ToString());
                    //    docText = id_jadins5.Replace(docText, dsjudgement.Tables[2].Rows[i]["inst"].ToString());
                    //    docText = id_jadamount5.Replace(docText, dsjudgement.Tables[2].Rows[i]["notes"].ToString());
                    //}


                }


            }

            if (dsjudgement.Tables[3].Rows.Count > 0) // AFFIDAVIT TO FIX LIEN
            {

                for (int i = 0; i < dsjudgement.Tables[3].Rows.Count; i++)
                {
                    if (i == 0)
                    {


                        docText = id_afftofixlien1.Replace(docText, dsjudgement.Tables[3].Rows[0]["judgement_type"].ToString());
                        docText = id_jalowner1.Replace(docText, dsjudgement.Tables[3].Rows[0]["owner"].ToString());
                        docText = id_jalgrantor1.Replace(docText, dsjudgement.Tables[3].Rows[0]["grantor"].ToString());
                        docText = id_jalda1.Replace(docText, dsjudgement.Tables[3].Rows[0]["dated"].ToString());
                        docText = id_jalf1.Replace(docText, dsjudgement.Tables[3].Rows[0]["filed"].ToString());
                        docText = id_jalv1.Replace(docText, dsjudgement.Tables[3].Rows[0]["vol"].ToString());
                        docText = id_jalp1.Replace(docText, dsjudgement.Tables[3].Rows[0]["pg"].ToString());
                        docText = id_jalins1.Replace(docText, dsjudgement.Tables[3].Rows[0]["inst"].ToString());
                        docText = id_jalamount1.Replace(docText, dsjudgement.Tables[3].Rows[0]["notes"].ToString());
                    }

                    if (i == 1)
                    {


                        docText = id_afftofixlien2.Replace(docText, dsjudgement.Tables[3].Rows[i]["judgement_type"].ToString());
                        docText = id_jalowner2.Replace(docText, dsjudgement.Tables[3].Rows[i]["owner"].ToString());
                        docText = id_jalgrantor2.Replace(docText, dsjudgement.Tables[3].Rows[i]["grantor"].ToString());
                        docText = id_jalda2.Replace(docText, dsjudgement.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_jalf2.Replace(docText, dsjudgement.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_jalv2.Replace(docText, dsjudgement.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_jalp2.Replace(docText, dsjudgement.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_jalins2.Replace(docText, dsjudgement.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_jalamount2.Replace(docText, dsjudgement.Tables[3].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {


                        docText = id_afftofixlien3.Replace(docText, dsjudgement.Tables[3].Rows[i]["judgement_type"].ToString());
                        docText = id_jalowner3.Replace(docText, dsjudgement.Tables[3].Rows[i]["owner"].ToString());
                        docText = id_jalgrantor3.Replace(docText, dsjudgement.Tables[3].Rows[i]["grantor"].ToString());
                        docText = id_jalda3.Replace(docText, dsjudgement.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_jalf3.Replace(docText, dsjudgement.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_jalv3.Replace(docText, dsjudgement.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_jalp3.Replace(docText, dsjudgement.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_jalins3.Replace(docText, dsjudgement.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_jalamount3.Replace(docText, dsjudgement.Tables[3].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {


                        docText = id_afftofixlien4.Replace(docText, dsjudgement.Tables[3].Rows[i]["judgement_type"].ToString());
                        docText = id_jalowner4.Replace(docText, dsjudgement.Tables[3].Rows[i]["owner"].ToString());
                        docText = id_jalgrantor4.Replace(docText, dsjudgement.Tables[3].Rows[i]["grantor"].ToString());
                        docText = id_jalda4.Replace(docText, dsjudgement.Tables[3].Rows[i]["dated"].ToString());
                        docText = id_jalf4.Replace(docText, dsjudgement.Tables[3].Rows[i]["filed"].ToString());
                        docText = id_jalv4.Replace(docText, dsjudgement.Tables[3].Rows[i]["vol"].ToString());
                        docText = id_jalp4.Replace(docText, dsjudgement.Tables[3].Rows[i]["pg"].ToString());
                        docText = id_jalins4.Replace(docText, dsjudgement.Tables[3].Rows[i]["inst"].ToString());
                        docText = id_jalamount4.Replace(docText, dsjudgement.Tables[3].Rows[i]["notes"].ToString());
                    }

                    //if (i == 4)
                    //{


                    //    docText = id_afftofixlien5.Replace(docText, dsjudgement.Tables[3].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jalowner5.Replace(docText, dsjudgement.Tables[3].Rows[i]["owner"].ToString());
                    //    docText = id_jalgrantor5.Replace(docText, dsjudgement.Tables[3].Rows[i]["grantor"].ToString());
                    //    docText = id_jalda5.Replace(docText, dsjudgement.Tables[3].Rows[i]["dated"].ToString());
                    //    docText = id_jalf5.Replace(docText, dsjudgement.Tables[3].Rows[i]["filed"].ToString());
                    //    docText = id_jalv5.Replace(docText, dsjudgement.Tables[3].Rows[i]["vol"].ToString());
                    //    docText = id_jalp5.Replace(docText, dsjudgement.Tables[3].Rows[i]["pg"].ToString());
                    //    docText = id_jalins5.Replace(docText, dsjudgement.Tables[3].Rows[i]["inst"].ToString());
                    //    docText = id_jalamount5.Replace(docText, dsjudgement.Tables[3].Rows[i]["notes"].ToString());
                    //}
                }
            }



            if (dsjudgement.Tables[4].Rows.Count > 0) // FEDERAL TAX LIEN
            {

                for (int i = 0; i < dsjudgement.Tables[3].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_federaltaxlien1.Replace(docText, dsjudgement.Tables[4].Rows[0]["judgement_type"].ToString());
                        docText = id_jfltaxpayer1.Replace(docText, dsjudgement.Tables[4].Rows[0]["Taxpayer"].ToString());
                        docText = id_jfladdress1.Replace(docText, dsjudgement.Tables[4].Rows[0]["Address"].ToString());
                        docText = id_jfltaxpayerid1.Replace(docText, dsjudgement.Tables[4].Rows[0]["Taxpayerid"].ToString());
                        docText = id_jflda1.Replace(docText, dsjudgement.Tables[4].Rows[0]["dated"].ToString());
                        docText = id_jflf1.Replace(docText, dsjudgement.Tables[4].Rows[0]["filed"].ToString());
                        docText = id_jflv1.Replace(docText, dsjudgement.Tables[4].Rows[0]["vol"].ToString());
                        docText = id_jflp1.Replace(docText, dsjudgement.Tables[4].Rows[0]["pg"].ToString());
                        docText = id_jflins1.Replace(docText, dsjudgement.Tables[4].Rows[0]["inst"].ToString());
                        docText = id_jflamount1.Replace(docText, dsjudgement.Tables[4].Rows[0]["amount"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_federaltaxlien2.Replace(docText, dsjudgement.Tables[4].Rows[i]["judgement_type"].ToString());
                        docText = id_jfltaxpayer2.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayer"].ToString());
                        docText = id_jfladdress2.Replace(docText, dsjudgement.Tables[4].Rows[i]["Address"].ToString());
                        docText = id_jfltaxpayerid2.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jflda2.Replace(docText, dsjudgement.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_jflf2.Replace(docText, dsjudgement.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_jflv2.Replace(docText, dsjudgement.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_jflp2.Replace(docText, dsjudgement.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_jflins2.Replace(docText, dsjudgement.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_jflamount2.Replace(docText, dsjudgement.Tables[4].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_federaltaxlien3.Replace(docText, dsjudgement.Tables[4].Rows[i]["judgement_type"].ToString());
                        docText = id_jfltaxpayer3.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayer"].ToString());
                        docText = id_jfladdress3.Replace(docText, dsjudgement.Tables[4].Rows[i]["Address"].ToString());
                        docText = id_jfltaxpayerid3.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jflda3.Replace(docText, dsjudgement.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_jflf3.Replace(docText, dsjudgement.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_jflv3.Replace(docText, dsjudgement.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_jflp3.Replace(docText, dsjudgement.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_jflins3.Replace(docText, dsjudgement.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_jflamount3.Replace(docText, dsjudgement.Tables[4].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_federaltaxlien4.Replace(docText, dsjudgement.Tables[4].Rows[i]["judgement_type"].ToString());
                        docText = id_jfltaxpayer4.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayer"].ToString());
                        docText = id_jfladdress4.Replace(docText, dsjudgement.Tables[4].Rows[i]["Address"].ToString());
                        docText = id_jfltaxpayerid4.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jflda4.Replace(docText, dsjudgement.Tables[4].Rows[i]["dated"].ToString());
                        docText = id_jflf4.Replace(docText, dsjudgement.Tables[4].Rows[i]["filed"].ToString());
                        docText = id_jflv4.Replace(docText, dsjudgement.Tables[4].Rows[i]["vol"].ToString());
                        docText = id_jflp4.Replace(docText, dsjudgement.Tables[4].Rows[i]["pg"].ToString());
                        docText = id_jflins4.Replace(docText, dsjudgement.Tables[4].Rows[i]["inst"].ToString());
                        docText = id_jflamount4.Replace(docText, dsjudgement.Tables[4].Rows[i]["amount"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_federaltaxlien5.Replace(docText, dsjudgement.Tables[4].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jfltaxpayer5.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayer"].ToString());
                    //    docText = id_jfladdress5.Replace(docText, dsjudgement.Tables[4].Rows[i]["Address"].ToString());
                    //    docText = id_jfltaxpayerid5.Replace(docText, dsjudgement.Tables[4].Rows[i]["Taxpayerid"].ToString());
                    //    docText = id_jflda5.Replace(docText, dsjudgement.Tables[4].Rows[i]["dated"].ToString());
                    //    docText = id_jflf5.Replace(docText, dsjudgement.Tables[4].Rows[i]["filed"].ToString());
                    //    docText = id_jflv5.Replace(docText, dsjudgement.Tables[4].Rows[i]["vol"].ToString());
                    //    docText = id_jflp5.Replace(docText, dsjudgement.Tables[4].Rows[i]["pg"].ToString());
                    //    docText = id_jflins5.Replace(docText, dsjudgement.Tables[4].Rows[i]["inst"].ToString());
                    //    docText = id_jflamount5.Replace(docText, dsjudgement.Tables[4].Rows[i]["amount"].ToString());
                    //}


                }
            }



            if (dsjudgement.Tables[5].Rows.Count > 0) // LIEN CLAIM AFFIDAVIT
            {

                for (int i = 0; i < dsjudgement.Tables[5].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_lienclaim1.Replace(docText, dsjudgement.Tables[5].Rows[0]["judgement_type"].ToString());
                        docText = id_jlaowner1.Replace(docText, dsjudgement.Tables[5].Rows[0]["owner"].ToString());
                        docText = id_jlagrantor1.Replace(docText, dsjudgement.Tables[5].Rows[0]["grantor"].ToString());
                        docText = id_jlada1.Replace(docText, dsjudgement.Tables[5].Rows[0]["dated"].ToString());
                        docText = id_jlaf1.Replace(docText, dsjudgement.Tables[5].Rows[0]["filed"].ToString());
                        docText = id_jlav1.Replace(docText, dsjudgement.Tables[5].Rows[0]["vol"].ToString());
                        docText = id_jlap1.Replace(docText, dsjudgement.Tables[5].Rows[0]["pg"].ToString());
                        docText = id_jlains1.Replace(docText, dsjudgement.Tables[5].Rows[0]["inst"].ToString());
                        docText = id_jlaamount1.Replace(docText, dsjudgement.Tables[5].Rows[0]["amount"].ToString());
                        docText = id_jlanotes1.Replace(docText, dsjudgement.Tables[5].Rows[0]["notes"].ToString());
                    }

                    if (i == 1)
                    {
                        docText = id_lienclaim2.Replace(docText, dsjudgement.Tables[5].Rows[i]["judgement_type"].ToString());
                        docText = id_jlaowner2.Replace(docText, dsjudgement.Tables[5].Rows[i]["owner"].ToString());
                        docText = id_jlagrantor2.Replace(docText, dsjudgement.Tables[5].Rows[i]["grantor"].ToString());
                        docText = id_jlada2.Replace(docText, dsjudgement.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_jlaf2.Replace(docText, dsjudgement.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_jlav2.Replace(docText, dsjudgement.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_jlap2.Replace(docText, dsjudgement.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_jlains2.Replace(docText, dsjudgement.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_jlaamount2.Replace(docText, dsjudgement.Tables[5].Rows[i]["amount"].ToString());
                        docText = id_jlanotes2.Replace(docText, dsjudgement.Tables[5].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_lienclaim3.Replace(docText, dsjudgement.Tables[5].Rows[i]["judgement_type"].ToString());
                        docText = id_jlaowner3.Replace(docText, dsjudgement.Tables[5].Rows[i]["owner"].ToString());
                        docText = id_jlagrantor3.Replace(docText, dsjudgement.Tables[5].Rows[i]["grantor"].ToString());
                        docText = id_jlada3.Replace(docText, dsjudgement.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_jlaf3.Replace(docText, dsjudgement.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_jlav3.Replace(docText, dsjudgement.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_jlap3.Replace(docText, dsjudgement.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_jlains3.Replace(docText, dsjudgement.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_jlaamount3.Replace(docText, dsjudgement.Tables[5].Rows[i]["amount"].ToString());
                        docText = id_jlanotes3.Replace(docText, dsjudgement.Tables[5].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_lienclaim4.Replace(docText, dsjudgement.Tables[5].Rows[i]["judgement_type"].ToString());
                        docText = id_jlaowner4.Replace(docText, dsjudgement.Tables[5].Rows[i]["owner"].ToString());
                        docText = id_jlagrantor4.Replace(docText, dsjudgement.Tables[5].Rows[i]["grantor"].ToString());
                        docText = id_jlada4.Replace(docText, dsjudgement.Tables[5].Rows[i]["dated"].ToString());
                        docText = id_jlaf4.Replace(docText, dsjudgement.Tables[5].Rows[i]["filed"].ToString());
                        docText = id_jlav4.Replace(docText, dsjudgement.Tables[5].Rows[i]["vol"].ToString());
                        docText = id_jlap4.Replace(docText, dsjudgement.Tables[5].Rows[i]["pg"].ToString());
                        docText = id_jlains4.Replace(docText, dsjudgement.Tables[5].Rows[i]["inst"].ToString());
                        docText = id_jlaamount4.Replace(docText, dsjudgement.Tables[5].Rows[i]["amount"].ToString());
                        docText = id_jlanotes4.Replace(docText, dsjudgement.Tables[5].Rows[i]["notes"].ToString());
                    }

                    //if (i == 4)
                    //{
                    //    docText = id_lienclaim5.Replace(docText, dsjudgement.Tables[5].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jlaowner5.Replace(docText, dsjudgement.Tables[5].Rows[i]["owner"].ToString());
                    //    docText = id_jlagrantor5.Replace(docText, dsjudgement.Tables[5].Rows[i]["grantor"].ToString());
                    //    docText = id_jlada5.Replace(docText, dsjudgement.Tables[5].Rows[i]["dated"].ToString());
                    //    docText = id_jlaf5.Replace(docText, dsjudgement.Tables[5].Rows[i]["filed"].ToString());
                    //    docText = id_jlav5.Replace(docText, dsjudgement.Tables[5].Rows[i]["vol"].ToString());
                    //    docText = id_jlap5.Replace(docText, dsjudgement.Tables[5].Rows[i]["pg"].ToString());
                    //    docText = id_jlains5.Replace(docText, dsjudgement.Tables[5].Rows[i]["inst"].ToString());
                    //    docText = id_jlaamount5.Replace(docText, dsjudgement.Tables[5].Rows[i]["amount"].ToString());
                    //    docText = id_jlanotes5.Replace(docText, dsjudgement.Tables[5].Rows[i]["notes"].ToString());
                    //}

                }
            }





            if (dsjudgement.Tables[6].Rows.Count > 0) // NOTICE OF ASSESSMENT LIEN OR HOA
            {

                for (int i = 0; i < dsjudgement.Tables[6].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_noticeofass1.Replace(docText, dsjudgement.Tables[6].Rows[0]["judgement_type"].ToString());
                        docText = id_jnalowner1.Replace(docText, dsjudgement.Tables[6].Rows[0]["owner"].ToString());
                        docText = id_jnalgrantor1.Replace(docText, dsjudgement.Tables[6].Rows[0]["grantor"].ToString());
                        docText = id_jnalda1.Replace(docText, dsjudgement.Tables[6].Rows[0]["dated"].ToString());
                        docText = id_jnalf1.Replace(docText, dsjudgement.Tables[6].Rows[0]["filed"].ToString());
                        docText = id_jnalv1.Replace(docText, dsjudgement.Tables[6].Rows[0]["vol"].ToString());
                        docText = id_jnalp1.Replace(docText, dsjudgement.Tables[6].Rows[0]["pg"].ToString());
                        docText = id_jnalins1.Replace(docText, dsjudgement.Tables[6].Rows[0]["inst"].ToString());
                        docText = id_jnalamount1.Replace(docText, dsjudgement.Tables[6].Rows[0]["amount"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_noticeofass2.Replace(docText, dsjudgement.Tables[6].Rows[i]["judgement_type"].ToString());
                        docText = id_jnalowner2.Replace(docText, dsjudgement.Tables[6].Rows[i]["owner"].ToString());
                        docText = id_jnalgrantor2.Replace(docText, dsjudgement.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_jnalda2.Replace(docText, dsjudgement.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_jnalf2.Replace(docText, dsjudgement.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_jnalv2.Replace(docText, dsjudgement.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_jnalp2.Replace(docText, dsjudgement.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_jnalins2.Replace(docText, dsjudgement.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_jnalamount2.Replace(docText, dsjudgement.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_noticeofass3.Replace(docText, dsjudgement.Tables[6].Rows[i]["judgement_type"].ToString());
                        docText = id_jnalowner3.Replace(docText, dsjudgement.Tables[6].Rows[i]["owner"].ToString());
                        docText = id_jnalgrantor3.Replace(docText, dsjudgement.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_jnalda3.Replace(docText, dsjudgement.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_jnalf3.Replace(docText, dsjudgement.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_jnalv3.Replace(docText, dsjudgement.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_jnalp3.Replace(docText, dsjudgement.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_jnalins3.Replace(docText, dsjudgement.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_jnalamount3.Replace(docText, dsjudgement.Tables[6].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_noticeofass4.Replace(docText, dsjudgement.Tables[6].Rows[i]["judgement_type"].ToString());
                        docText = id_jnalowner4.Replace(docText, dsjudgement.Tables[6].Rows[i]["owner"].ToString());
                        docText = id_jnalgrantor4.Replace(docText, dsjudgement.Tables[6].Rows[i]["grantor"].ToString());
                        docText = id_jnalda4.Replace(docText, dsjudgement.Tables[6].Rows[i]["dated"].ToString());
                        docText = id_jnalf4.Replace(docText, dsjudgement.Tables[6].Rows[i]["filed"].ToString());
                        docText = id_jnalv4.Replace(docText, dsjudgement.Tables[6].Rows[i]["vol"].ToString());
                        docText = id_jnalp4.Replace(docText, dsjudgement.Tables[6].Rows[i]["pg"].ToString());
                        docText = id_jnalins4.Replace(docText, dsjudgement.Tables[6].Rows[i]["inst"].ToString());
                        docText = id_jnalamount4.Replace(docText, dsjudgement.Tables[6].Rows[i]["amount"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_noticeofass5.Replace(docText, dsjudgement.Tables[6].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jnalowner5.Replace(docText, dsjudgement.Tables[6].Rows[i]["owner"].ToString());
                    //    docText = id_jnalgrantor5.Replace(docText, dsjudgement.Tables[6].Rows[i]["grantor"].ToString());
                    //    docText = id_jnalda5.Replace(docText, dsjudgement.Tables[6].Rows[i]["dated"].ToString());
                    //    docText = id_jnalf5.Replace(docText, dsjudgement.Tables[6].Rows[i]["filed"].ToString());
                    //    docText = id_jnalv5.Replace(docText, dsjudgement.Tables[6].Rows[i]["vol"].ToString());
                    //    docText = id_jnalp5.Replace(docText, dsjudgement.Tables[6].Rows[i]["pg"].ToString());
                    //    docText = id_jnalins5.Replace(docText, dsjudgement.Tables[6].Rows[i]["inst"].ToString());
                    //    docText = id_jnalamount5.Replace(docText, dsjudgement.Tables[6].Rows[i]["amount"].ToString());
                    //}


                }
            }




            if (dsjudgement.Tables[7].Rows.Count > 0) // NOTICE OF CHILD SUPPORT LIEN 
            {
                for (int i = 0; i < dsjudgement.Tables[7].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_noticeofchild1.Replace(docText, dsjudgement.Tables[7].Rows[i]["judgement_type"].ToString());
                        docText = id_jnclobligor1.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligor"].ToString());
                        docText = id_jncladdress1.Replace(docText, dsjudgement.Tables[7].Rows[i]["Address"].ToString());
                        docText = id_jnclssn1.Replace(docText, dsjudgement.Tables[7].Rows[i]["ssn"].ToString());
                        docText = id_jnclobligee1.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligee"].ToString());
                        docText = id_jncltribunal1.Replace(docText, dsjudgement.Tables[7].Rows[i]["tribunal"].ToString());
                        docText = id_jnclda1.Replace(docText, dsjudgement.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_jnclf1.Replace(docText, dsjudgement.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_jnclv1.Replace(docText, dsjudgement.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_jnclp1.Replace(docText, dsjudgement.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_jnclins1.Replace(docText, dsjudgement.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_jnclamount1.Replace(docText, dsjudgement.Tables[7].Rows[i]["amount"].ToString());
                    }


                    if (i == 1)
                    {

                        docText = id_noticeofchild2.Replace(docText, dsjudgement.Tables[7].Rows[i]["judgement_type"].ToString());
                        docText = id_jnclobligor2.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligor"].ToString());
                        docText = id_jncladdress2.Replace(docText, dsjudgement.Tables[7].Rows[i]["Address"].ToString());
                        docText = id_jnclssn2.Replace(docText, dsjudgement.Tables[7].Rows[i]["ssn"].ToString());
                        docText = id_jnclobligee2.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligee"].ToString());
                        docText = id_jncltribunal2.Replace(docText, dsjudgement.Tables[7].Rows[i]["tribunal"].ToString());
                        docText = id_jnclda2.Replace(docText, dsjudgement.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_jnclf2.Replace(docText, dsjudgement.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_jnclv2.Replace(docText, dsjudgement.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_jnclp2.Replace(docText, dsjudgement.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_jnclins2.Replace(docText, dsjudgement.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_jnclamount2.Replace(docText, dsjudgement.Tables[7].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_noticeofchild3.Replace(docText, dsjudgement.Tables[7].Rows[i]["judgement_type"].ToString());
                        docText = id_jnclobligor3.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligor"].ToString());
                        docText = id_jncladdress3.Replace(docText, dsjudgement.Tables[7].Rows[i]["Address"].ToString());
                        docText = id_jnclssn3.Replace(docText, dsjudgement.Tables[7].Rows[i]["ssn"].ToString());
                        docText = id_jnclobligee3.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligee"].ToString());
                        docText = id_jncltribunal3.Replace(docText, dsjudgement.Tables[7].Rows[i]["tribunal"].ToString());
                        docText = id_jnclda3.Replace(docText, dsjudgement.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_jnclf3.Replace(docText, dsjudgement.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_jnclv3.Replace(docText, dsjudgement.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_jnclp3.Replace(docText, dsjudgement.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_jnclins3.Replace(docText, dsjudgement.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_jnclamount3.Replace(docText, dsjudgement.Tables[7].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_noticeofchild4.Replace(docText, dsjudgement.Tables[7].Rows[i]["judgement_type"].ToString());
                        docText = id_jnclobligor4.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligor"].ToString());
                        docText = id_jncladdress4.Replace(docText, dsjudgement.Tables[7].Rows[i]["Address"].ToString());
                        docText = id_jnclssn4.Replace(docText, dsjudgement.Tables[7].Rows[i]["ssn"].ToString());
                        docText = id_jnclobligee4.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligee"].ToString());
                        docText = id_jncltribunal4.Replace(docText, dsjudgement.Tables[7].Rows[i]["tribunal"].ToString());
                        docText = id_jnclda4.Replace(docText, dsjudgement.Tables[7].Rows[i]["dated"].ToString());
                        docText = id_jnclf4.Replace(docText, dsjudgement.Tables[7].Rows[i]["filed"].ToString());
                        docText = id_jnclv4.Replace(docText, dsjudgement.Tables[7].Rows[i]["vol"].ToString());
                        docText = id_jnclp4.Replace(docText, dsjudgement.Tables[7].Rows[i]["pg"].ToString());
                        docText = id_jnclins4.Replace(docText, dsjudgement.Tables[7].Rows[i]["inst"].ToString());
                        docText = id_jnclamount4.Replace(docText, dsjudgement.Tables[7].Rows[i]["amount"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_noticeofchild5.Replace(docText, dsjudgement.Tables[7].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jnclobligor5.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligor"].ToString());
                    //    docText = id_jncladdress5.Replace(docText, dsjudgement.Tables[7].Rows[i]["Address"].ToString());
                    //    docText = id_jnclssn5.Replace(docText, dsjudgement.Tables[7].Rows[i]["ssn"].ToString());
                    //    docText = id_jnclobligee5.Replace(docText, dsjudgement.Tables[7].Rows[i]["obligee"].ToString());
                    //    docText = id_jncltribunal5.Replace(docText, dsjudgement.Tables[7].Rows[i]["tribunal"].ToString());
                    //    docText = id_jnclda5.Replace(docText, dsjudgement.Tables[7].Rows[i]["dated"].ToString());
                    //    docText = id_jnclf5.Replace(docText, dsjudgement.Tables[7].Rows[i]["filed"].ToString());
                    //    docText = id_jnclv5.Replace(docText, dsjudgement.Tables[7].Rows[i]["vol"].ToString());
                    //    docText = id_jnclp5.Replace(docText, dsjudgement.Tables[7].Rows[i]["pg"].ToString());
                    //    docText = id_jnclins5.Replace(docText, dsjudgement.Tables[7].Rows[i]["inst"].ToString());
                    //    docText = id_jnclamount5.Replace(docText, dsjudgement.Tables[7].Rows[i]["amount"].ToString());
                    //}

                }


            }






            if (dsjudgement.Tables[8].Rows.Count > 0) // NOTICE OF FORECLOSURE
            {

                for (int i = 0; i < dsjudgement.Tables[8].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_noticeoffore1.Replace(docText, dsjudgement.Tables[8].Rows[i]["judgement_type"].ToString());
                        docText = id_jnfgrantee1.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantor"].ToString());
                        docText = id_jnfgrantor1.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantee"].ToString());
                        docText = id_jnfda1.Replace(docText, dsjudgement.Tables[8].Rows[i]["dated"].ToString());
                        docText = id_jnff1.Replace(docText, dsjudgement.Tables[8].Rows[i]["filed"].ToString());
                        docText = id_jnfv1.Replace(docText, dsjudgement.Tables[8].Rows[i]["vol"].ToString());
                        docText = id_jnfp1.Replace(docText, dsjudgement.Tables[8].Rows[i]["pg"].ToString());
                        docText = id_jnfins1.Replace(docText, dsjudgement.Tables[8].Rows[i]["inst"].ToString());
                        docText = id_jnfnotes1.Replace(docText, dsjudgement.Tables[8].Rows[i]["notes"].ToString());
                    }

                    if (i == 1)
                    {
                        docText = id_noticeoffore2.Replace(docText, dsjudgement.Tables[8].Rows[i]["judgement_type"].ToString());
                        docText = id_jnfgrantee2.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantor"].ToString());
                        docText = id_jnfgrantor2.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantee"].ToString());
                        docText = id_jnfda2.Replace(docText, dsjudgement.Tables[8].Rows[i]["dated"].ToString());
                        docText = id_jnff2.Replace(docText, dsjudgement.Tables[8].Rows[i]["filed"].ToString());
                        docText = id_jnfv2.Replace(docText, dsjudgement.Tables[8].Rows[i]["vol"].ToString());
                        docText = id_jnfp2.Replace(docText, dsjudgement.Tables[8].Rows[i]["pg"].ToString());
                        docText = id_jnfins2.Replace(docText, dsjudgement.Tables[8].Rows[i]["inst"].ToString());
                        docText = id_jnfnotes2.Replace(docText, dsjudgement.Tables[8].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_noticeoffore3.Replace(docText, dsjudgement.Tables[8].Rows[i]["judgement_type"].ToString());
                        docText = id_jnfgrantee3.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantor"].ToString());
                        docText = id_jnfgrantor3.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantee"].ToString());
                        docText = id_jnfda3.Replace(docText, dsjudgement.Tables[8].Rows[i]["dated"].ToString());
                        docText = id_jnff3.Replace(docText, dsjudgement.Tables[8].Rows[i]["filed"].ToString());
                        docText = id_jnfv3.Replace(docText, dsjudgement.Tables[8].Rows[i]["vol"].ToString());
                        docText = id_jnfp3.Replace(docText, dsjudgement.Tables[8].Rows[i]["pg"].ToString());
                        docText = id_jnfins3.Replace(docText, dsjudgement.Tables[8].Rows[i]["inst"].ToString());
                        docText = id_jnfnotes3.Replace(docText, dsjudgement.Tables[8].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_noticeoffore4.Replace(docText, dsjudgement.Tables[8].Rows[i]["judgement_type"].ToString());
                        docText = id_jnfgrantee4.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantor"].ToString());
                        docText = id_jnfgrantor4.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantee"].ToString());
                        docText = id_jnfda4.Replace(docText, dsjudgement.Tables[8].Rows[i]["dated"].ToString());
                        docText = id_jnff4.Replace(docText, dsjudgement.Tables[8].Rows[i]["filed"].ToString());
                        docText = id_jnfv4.Replace(docText, dsjudgement.Tables[8].Rows[i]["vol"].ToString());
                        docText = id_jnfp4.Replace(docText, dsjudgement.Tables[8].Rows[i]["pg"].ToString());
                        docText = id_jnfins4.Replace(docText, dsjudgement.Tables[8].Rows[i]["inst"].ToString());
                        docText = id_jnfnotes4.Replace(docText, dsjudgement.Tables[8].Rows[i]["notes"].ToString());
                    }

                    //if (i == 4)
                    //{
                    //    docText = id_noticeoffore5.Replace(docText, dsjudgement.Tables[8].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jnfgrantee5.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantor"].ToString());
                    //    docText = id_jnfgrantor5.Replace(docText, dsjudgement.Tables[8].Rows[i]["grantee"].ToString());
                    //    docText = id_jnfda5.Replace(docText, dsjudgement.Tables[8].Rows[i]["dated"].ToString());
                    //    docText = id_jnff5.Replace(docText, dsjudgement.Tables[8].Rows[i]["filed"].ToString());
                    //    docText = id_jnfv5.Replace(docText, dsjudgement.Tables[8].Rows[i]["vol"].ToString());
                    //    docText = id_jnfp5.Replace(docText, dsjudgement.Tables[8].Rows[i]["pg"].ToString());
                    //    docText = id_jnfins5.Replace(docText, dsjudgement.Tables[8].Rows[i]["inst"].ToString());
                    //    docText = id_jnfnotes5.Replace(docText, dsjudgement.Tables[8].Rows[i]["notes"].ToString());
                    //}
                }
            }



            if (dsjudgement.Tables[9].Rows.Count > 0) // NOTICE OF TRUSTEE SALE
            {
                for (int i = 0; i < dsjudgement.Tables[9].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_noticeoftrus1.Replace(docText, dsjudgement.Tables[9].Rows[i]["judgement_type"].ToString());
                        docText = id_jntsto1.Replace(docText, dsjudgement.Tables[9].Rows[i]["to"].ToString());
                        docText = id_jntsfrom1.Replace(docText, dsjudgement.Tables[9].Rows[i]["from"].ToString());
                        docText = id_jntsda1.Replace(docText, dsjudgement.Tables[9].Rows[i]["dated"].ToString());
                        docText = id_jntsf1.Replace(docText, dsjudgement.Tables[9].Rows[i]["filed"].ToString());
                        docText = id_jntsv1.Replace(docText, dsjudgement.Tables[9].Rows[i]["vol"].ToString());
                        docText = id_jntsp1.Replace(docText, dsjudgement.Tables[9].Rows[i]["pg"].ToString());
                        docText = id_jntsins1.Replace(docText, dsjudgement.Tables[9].Rows[i]["inst"].ToString());
                        docText = id_jntsnotes1.Replace(docText, dsjudgement.Tables[9].Rows[i]["notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_noticeoftrus2.Replace(docText, dsjudgement.Tables[9].Rows[i]["judgement_type"].ToString());
                        docText = id_jntsto2.Replace(docText, dsjudgement.Tables[9].Rows[i]["to"].ToString());
                        docText = id_jntsfrom2.Replace(docText, dsjudgement.Tables[9].Rows[i]["from"].ToString());
                        docText = id_jntsda2.Replace(docText, dsjudgement.Tables[9].Rows[i]["dated"].ToString());
                        docText = id_jntsf2.Replace(docText, dsjudgement.Tables[9].Rows[i]["filed"].ToString());
                        docText = id_jntsv2.Replace(docText, dsjudgement.Tables[9].Rows[i]["vol"].ToString());
                        docText = id_jntsp2.Replace(docText, dsjudgement.Tables[9].Rows[i]["pg"].ToString());
                        docText = id_jntsins2.Replace(docText, dsjudgement.Tables[9].Rows[i]["inst"].ToString());
                        docText = id_jntsnotes2.Replace(docText, dsjudgement.Tables[9].Rows[i]["notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_noticeoftrus3.Replace(docText, dsjudgement.Tables[9].Rows[i]["judgement_type"].ToString());
                        docText = id_jntsto3.Replace(docText, dsjudgement.Tables[9].Rows[i]["to"].ToString());
                        docText = id_jntsfrom3.Replace(docText, dsjudgement.Tables[9].Rows[i]["from"].ToString());
                        docText = id_jntsda3.Replace(docText, dsjudgement.Tables[9].Rows[i]["dated"].ToString());
                        docText = id_jntsf3.Replace(docText, dsjudgement.Tables[9].Rows[i]["filed"].ToString());
                        docText = id_jntsv3.Replace(docText, dsjudgement.Tables[9].Rows[i]["vol"].ToString());
                        docText = id_jntsp3.Replace(docText, dsjudgement.Tables[9].Rows[i]["pg"].ToString());
                        docText = id_jntsins3.Replace(docText, dsjudgement.Tables[9].Rows[i]["inst"].ToString());
                        docText = id_jntsnotes3.Replace(docText, dsjudgement.Tables[9].Rows[i]["notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_noticeoftrus4.Replace(docText, dsjudgement.Tables[9].Rows[i]["judgement_type"].ToString());
                        docText = id_jntsto4.Replace(docText, dsjudgement.Tables[9].Rows[i]["to"].ToString());
                        docText = id_jntsfrom4.Replace(docText, dsjudgement.Tables[9].Rows[i]["from"].ToString());
                        docText = id_jntsda4.Replace(docText, dsjudgement.Tables[9].Rows[i]["dated"].ToString());
                        docText = id_jntsf4.Replace(docText, dsjudgement.Tables[9].Rows[i]["filed"].ToString());
                        docText = id_jntsv4.Replace(docText, dsjudgement.Tables[9].Rows[i]["vol"].ToString());
                        docText = id_jntsp4.Replace(docText, dsjudgement.Tables[9].Rows[i]["pg"].ToString());
                        docText = id_jntsins4.Replace(docText, dsjudgement.Tables[9].Rows[i]["inst"].ToString());
                        docText = id_jntsnotes4.Replace(docText, dsjudgement.Tables[9].Rows[i]["notes"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_noticeoftrus5.Replace(docText, dsjudgement.Tables[9].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jntsto5.Replace(docText, dsjudgement.Tables[9].Rows[i]["to"].ToString());
                    //    docText = id_jntsfrom5.Replace(docText, dsjudgement.Tables[9].Rows[i]["from"].ToString());
                    //    docText = id_jntsda5.Replace(docText, dsjudgement.Tables[9].Rows[i]["dated"].ToString());
                    //    docText = id_jntsf5.Replace(docText, dsjudgement.Tables[9].Rows[i]["filed"].ToString());
                    //    docText = id_jntsv5.Replace(docText, dsjudgement.Tables[9].Rows[i]["vol"].ToString());
                    //    docText = id_jntsp5.Replace(docText, dsjudgement.Tables[9].Rows[i]["pg"].ToString());
                    //    docText = id_jntsins5.Replace(docText, dsjudgement.Tables[9].Rows[i]["inst"].ToString());
                    //    docText = id_jntsnotes5.Replace(docText, dsjudgement.Tables[9].Rows[i]["notes"].ToString());
                    //}
                }



            }





            if (dsjudgement.Tables[10].Rows.Count > 0) // ORDER TO PROCEED WITH NOTICE OF FORECLOSURE SALE
            {
                for (int i = 0; i < dsjudgement.Tables[10].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_ordertoproceed1.Replace(docText, dsjudgement.Tables[10].Rows[i]["judgement_type"].ToString());
                        docText = id_jofdeffendant1.Replace(docText, dsjudgement.Tables[10].Rows[i]["Address"].ToString());
                        docText = id_jofaddress1.Replace(docText, dsjudgement.Tables[10].Rows[i]["defendant"].ToString());
                        docText = id_jofpalintiff1.Replace(docText, dsjudgement.Tables[10].Rows[i]["paintiff"].ToString());
                        docText = id_jofda1.Replace(docText, dsjudgement.Tables[10].Rows[i]["dated"].ToString());
                        docText = id_joff1.Replace(docText, dsjudgement.Tables[10].Rows[i]["filed"].ToString());
                        docText = id_jofv1.Replace(docText, dsjudgement.Tables[10].Rows[i]["vol"].ToString());
                        docText = id_jofp1.Replace(docText, dsjudgement.Tables[10].Rows[i]["pg"].ToString());
                        docText = id_jofins1.Replace(docText, dsjudgement.Tables[10].Rows[i]["inst"].ToString());
                        docText = id_jofcause1.Replace(docText, dsjudgement.Tables[10].Rows[i]["cause"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_ordertoproceed2.Replace(docText, dsjudgement.Tables[10].Rows[i]["judgement_type"].ToString());
                        docText = id_jofdeffendant2.Replace(docText, dsjudgement.Tables[10].Rows[i]["Address"].ToString());
                        docText = id_jofaddress2.Replace(docText, dsjudgement.Tables[10].Rows[i]["defendant"].ToString());
                        docText = id_jofpalintiff2.Replace(docText, dsjudgement.Tables[10].Rows[i]["paintiff"].ToString());
                        docText = id_jofda2.Replace(docText, dsjudgement.Tables[10].Rows[i]["dated"].ToString());
                        docText = id_joff2.Replace(docText, dsjudgement.Tables[10].Rows[i]["filed"].ToString());
                        docText = id_jofv2.Replace(docText, dsjudgement.Tables[10].Rows[i]["vol"].ToString());
                        docText = id_jofp2.Replace(docText, dsjudgement.Tables[10].Rows[i]["pg"].ToString());
                        docText = id_jofins2.Replace(docText, dsjudgement.Tables[10].Rows[i]["inst"].ToString());
                        docText = id_jofcause2.Replace(docText, dsjudgement.Tables[10].Rows[i]["cause"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_ordertoproceed3.Replace(docText, dsjudgement.Tables[10].Rows[i]["judgement_type"].ToString());
                        docText = id_jofdeffendant3.Replace(docText, dsjudgement.Tables[10].Rows[i]["Address"].ToString());
                        docText = id_jofaddress3.Replace(docText, dsjudgement.Tables[10].Rows[i]["defendant"].ToString());
                        docText = id_jofpalintiff3.Replace(docText, dsjudgement.Tables[10].Rows[i]["paintiff"].ToString());
                        docText = id_jofda3.Replace(docText, dsjudgement.Tables[10].Rows[i]["dated"].ToString());
                        docText = id_joff3.Replace(docText, dsjudgement.Tables[10].Rows[i]["filed"].ToString());
                        docText = id_jofv3.Replace(docText, dsjudgement.Tables[10].Rows[i]["vol"].ToString());
                        docText = id_jofp3.Replace(docText, dsjudgement.Tables[10].Rows[i]["pg"].ToString());
                        docText = id_jofins3.Replace(docText, dsjudgement.Tables[10].Rows[i]["inst"].ToString());
                        docText = id_jofcause3.Replace(docText, dsjudgement.Tables[10].Rows[i]["cause"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_ordertoproceed4.Replace(docText, dsjudgement.Tables[10].Rows[i]["judgement_type"].ToString());
                        docText = id_jofdeffendant4.Replace(docText, dsjudgement.Tables[10].Rows[i]["Address"].ToString());
                        docText = id_jofaddress4.Replace(docText, dsjudgement.Tables[10].Rows[i]["defendant"].ToString());
                        docText = id_jofpalintiff4.Replace(docText, dsjudgement.Tables[10].Rows[i]["paintiff"].ToString());
                        docText = id_jofda4.Replace(docText, dsjudgement.Tables[10].Rows[i]["dated"].ToString());
                        docText = id_joff4.Replace(docText, dsjudgement.Tables[10].Rows[i]["filed"].ToString());
                        docText = id_jofv4.Replace(docText, dsjudgement.Tables[10].Rows[i]["vol"].ToString());
                        docText = id_jofp4.Replace(docText, dsjudgement.Tables[10].Rows[i]["pg"].ToString());
                        docText = id_jofins4.Replace(docText, dsjudgement.Tables[10].Rows[i]["inst"].ToString());
                        docText = id_jofcause4.Replace(docText, dsjudgement.Tables[10].Rows[i]["cause"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_ordertoproceed5.Replace(docText, dsjudgement.Tables[10].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jofdeffendant5.Replace(docText, dsjudgement.Tables[10].Rows[i]["Address"].ToString());
                    //    docText = id_jofaddress5.Replace(docText, dsjudgement.Tables[10].Rows[i]["defendant"].ToString());
                    //    docText = id_jofpalintiff5.Replace(docText, dsjudgement.Tables[10].Rows[i]["paintiff"].ToString());
                    //    docText = id_jofda5.Replace(docText, dsjudgement.Tables[10].Rows[i]["dated"].ToString());
                    //    docText = id_joff5.Replace(docText, dsjudgement.Tables[10].Rows[i]["filed"].ToString());
                    //    docText = id_jofv5.Replace(docText, dsjudgement.Tables[10].Rows[i]["vol"].ToString());
                    //    docText = id_jofp5.Replace(docText, dsjudgement.Tables[10].Rows[i]["pg"].ToString());
                    //    docText = id_jofins5.Replace(docText, dsjudgement.Tables[10].Rows[i]["inst"].ToString());
                    //    docText = id_jofcause5.Replace(docText, dsjudgement.Tables[10].Rows[i]["cause"].ToString());
                    //}


                }
            }




            if (dsjudgement.Tables[11].Rows.Count > 0) // STATE TAX LIEN
            {
                for (int i = 0; i < dsjudgement.Tables[11].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_statetaxlien1.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayer1.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayer"].ToString());
                        docText = id_jstladdress1.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayerid1.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jstlda1.Replace(docText, dsjudgement.Tables[11].Rows[i]["dated"].ToString());
                        docText = id_jstlf1.Replace(docText, dsjudgement.Tables[11].Rows[i]["filed"].ToString());
                        docText = id_jstlv1.Replace(docText, dsjudgement.Tables[11].Rows[i]["vol"].ToString());
                        docText = id_jstlp1.Replace(docText, dsjudgement.Tables[11].Rows[i]["pg"].ToString());
                        docText = id_jstlins1.Replace(docText, dsjudgement.Tables[11].Rows[i]["inst"].ToString());
                        docText = id_jstlamount1.Replace(docText, dsjudgement.Tables[11].Rows[i]["amount"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_statetaxlien2.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayer2.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayer"].ToString());
                        docText = id_jstladdress2.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayerid2.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jstlda2.Replace(docText, dsjudgement.Tables[11].Rows[i]["dated"].ToString());
                        docText = id_jstlf2.Replace(docText, dsjudgement.Tables[11].Rows[i]["filed"].ToString());
                        docText = id_jstlv2.Replace(docText, dsjudgement.Tables[11].Rows[i]["vol"].ToString());
                        docText = id_jstlp2.Replace(docText, dsjudgement.Tables[11].Rows[i]["pg"].ToString());
                        docText = id_jstlins2.Replace(docText, dsjudgement.Tables[11].Rows[i]["inst"].ToString());
                        docText = id_jstlamount2.Replace(docText, dsjudgement.Tables[11].Rows[i]["amount"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_statetaxlien3.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayer3.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayer"].ToString());
                        docText = id_jstladdress3.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayerid3.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jstlda3.Replace(docText, dsjudgement.Tables[11].Rows[i]["dated"].ToString());
                        docText = id_jstlf3.Replace(docText, dsjudgement.Tables[11].Rows[i]["filed"].ToString());
                        docText = id_jstlv3.Replace(docText, dsjudgement.Tables[11].Rows[i]["vol"].ToString());
                        docText = id_jstlp3.Replace(docText, dsjudgement.Tables[11].Rows[i]["pg"].ToString());
                        docText = id_jstlins3.Replace(docText, dsjudgement.Tables[11].Rows[i]["inst"].ToString());
                        docText = id_jstlamount3.Replace(docText, dsjudgement.Tables[11].Rows[i]["amount"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_statetaxlien4.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayer4.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayer"].ToString());
                        docText = id_jstladdress4.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                        docText = id_jstltaxpayerid4.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayerid"].ToString());
                        docText = id_jstlda4.Replace(docText, dsjudgement.Tables[11].Rows[i]["dated"].ToString());
                        docText = id_jstlf4.Replace(docText, dsjudgement.Tables[11].Rows[i]["filed"].ToString());
                        docText = id_jstlv4.Replace(docText, dsjudgement.Tables[11].Rows[i]["vol"].ToString());
                        docText = id_jstlp4.Replace(docText, dsjudgement.Tables[11].Rows[i]["pg"].ToString());
                        docText = id_jstlins4.Replace(docText, dsjudgement.Tables[11].Rows[i]["inst"].ToString());
                        docText = id_jstlamount4.Replace(docText, dsjudgement.Tables[11].Rows[i]["amount"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_statetaxlien5.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jstltaxpayer5.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayer"].ToString());
                    //    docText = id_jstladdress5.Replace(docText, dsjudgement.Tables[11].Rows[i]["judgement_type"].ToString());
                    //    docText = id_jstltaxpayerid5.Replace(docText, dsjudgement.Tables[11].Rows[i]["Taxpayerid"].ToString());
                    //    docText = id_jstlda5.Replace(docText, dsjudgement.Tables[11].Rows[i]["dated"].ToString());
                    //    docText = id_jstlf5.Replace(docText, dsjudgement.Tables[11].Rows[i]["filed"].ToString());
                    //    docText = id_jstlv5.Replace(docText, dsjudgement.Tables[11].Rows[i]["vol"].ToString());
                    //    docText = id_jstlp5.Replace(docText, dsjudgement.Tables[11].Rows[i]["pg"].ToString());
                    //    docText = id_jstlins5.Replace(docText, dsjudgement.Tables[11].Rows[i]["inst"].ToString());
                    //    docText = id_jstlamount5.Replace(docText, dsjudgement.Tables[11].Rows[i]["amount"].ToString());
                    //}


                }
            }





            #endregion
            #region Others

            DataSet dsothers = new DataSet();
            dsothers = gls.gettypevalue(lbl_orderno.Text, "sp_sel_others_output");
            if (dsothers.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_affandagree1.Replace(docText, dsothers.Tables[0].Rows[i]["others_type"].ToString());
                        docText = id_aagrantee1.Replace(docText, dsothers.Tables[0].Rows[i]["Grantee"].ToString());
                        docText = id_aagrantor1.Replace(docText, dsothers.Tables[0].Rows[i]["Grantor"].ToString());
                        docText = id_aada1.Replace(docText, dsothers.Tables[0].Rows[i]["Dated"].ToString());
                        docText = id_aaf1.Replace(docText, dsothers.Tables[0].Rows[i]["Filed"].ToString());
                        docText = id_aav1.Replace(docText, dsothers.Tables[0].Rows[i]["Vol"].ToString());
                        docText = id_aap1.Replace(docText, dsothers.Tables[0].Rows[i]["Pg"].ToString());
                        docText = id_aains1.Replace(docText, dsothers.Tables[0].Rows[i]["Inst"].ToString());
                        docText = id_aanotes1.Replace(docText, dsothers.Tables[0].Rows[i]["Notes"].ToString());
                    }

                    if (i == 1)
                    {
                        docText = id_affandagree2.Replace(docText, dsothers.Tables[0].Rows[i]["others_type"].ToString());
                        docText = id_aagrantee2.Replace(docText, dsothers.Tables[0].Rows[i]["Grantee"].ToString());
                        docText = id_aagrantor2.Replace(docText, dsothers.Tables[0].Rows[i]["Grantor"].ToString());
                        docText = id_aada2.Replace(docText, dsothers.Tables[0].Rows[i]["Dated"].ToString());
                        docText = id_aaf2.Replace(docText, dsothers.Tables[0].Rows[i]["Filed"].ToString());
                        docText = id_aav2.Replace(docText, dsothers.Tables[0].Rows[i]["Vol"].ToString());
                        docText = id_aap2.Replace(docText, dsothers.Tables[0].Rows[i]["Pg"].ToString());
                        docText = id_aains2.Replace(docText, dsothers.Tables[0].Rows[i]["Inst"].ToString());
                        docText = id_aanotes2.Replace(docText, dsothers.Tables[0].Rows[i]["Notes"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_affandagree3.Replace(docText, dsothers.Tables[0].Rows[i]["others_type"].ToString());
                        docText = id_aagrantee3.Replace(docText, dsothers.Tables[0].Rows[i]["Grantee"].ToString());
                        docText = id_aagrantor3.Replace(docText, dsothers.Tables[0].Rows[i]["Grantor"].ToString());
                        docText = id_aada3.Replace(docText, dsothers.Tables[0].Rows[i]["Dated"].ToString());
                        docText = id_aaf3.Replace(docText, dsothers.Tables[0].Rows[i]["Filed"].ToString());
                        docText = id_aav3.Replace(docText, dsothers.Tables[0].Rows[i]["Vol"].ToString());
                        docText = id_aap3.Replace(docText, dsothers.Tables[0].Rows[i]["Pg"].ToString());
                        docText = id_aains3.Replace(docText, dsothers.Tables[0].Rows[i]["Inst"].ToString());
                        docText = id_aanotes3.Replace(docText, dsothers.Tables[0].Rows[i]["Notes"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_affandagree4.Replace(docText, dsothers.Tables[0].Rows[i]["others_type"].ToString());
                        docText = id_aagrantee4.Replace(docText, dsothers.Tables[0].Rows[i]["Grantee"].ToString());
                        docText = id_aagrantor4.Replace(docText, dsothers.Tables[0].Rows[i]["Grantor"].ToString());
                        docText = id_aada4.Replace(docText, dsothers.Tables[0].Rows[i]["Dated"].ToString());
                        docText = id_aaf4.Replace(docText, dsothers.Tables[0].Rows[i]["Filed"].ToString());
                        docText = id_aav4.Replace(docText, dsothers.Tables[0].Rows[i]["Vol"].ToString());
                        docText = id_aap4.Replace(docText, dsothers.Tables[0].Rows[i]["Pg"].ToString());
                        docText = id_aains4.Replace(docText, dsothers.Tables[0].Rows[i]["Inst"].ToString());
                        docText = id_aanotes4.Replace(docText, dsothers.Tables[0].Rows[i]["Notes"].ToString());
                    }

                    //if (i == 4)
                    //{
                    //    docText = id_affandagree5.Replace(docText, dsothers.Tables[0].Rows[i]["others_type"].ToString());
                    //    docText = id_aagrantee5.Replace(docText, dsothers.Tables[0].Rows[i]["Grantee"].ToString());
                    //    docText = id_aagrantor5.Replace(docText, dsothers.Tables[0].Rows[i]["Grantor"].ToString());
                    //    docText = id_aada5.Replace(docText, dsothers.Tables[0].Rows[i]["Dated"].ToString());
                    //    docText = id_aaf5.Replace(docText, dsothers.Tables[0].Rows[i]["Filed"].ToString());
                    //    docText = id_aav5.Replace(docText, dsothers.Tables[0].Rows[i]["Vol"].ToString());
                    //    docText = id_aap5.Replace(docText, dsothers.Tables[0].Rows[i]["Pg"].ToString());
                    //    docText = id_aains5.Replace(docText, dsothers.Tables[0].Rows[i]["Inst"].ToString());
                    //    docText = id_aanotes5.Replace(docText, dsothers.Tables[0].Rows[i]["Notes"].ToString());
                    //}

                }


            }


            if (dsothers.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[1].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_divorce1.Replace(docText, dsothers.Tables[1].Rows[i]["others_type"].ToString());
                        docText = id_dipetitioner1.Replace(docText, dsothers.Tables[1].Rows[i]["Petitioner"].ToString());
                        docText = id_direspondent1.Replace(docText, dsothers.Tables[1].Rows[i]["Respondent"].ToString());
                        docText = id_dif1.Replace(docText, dsothers.Tables[1].Rows[i]["Filed"].ToString());
                        docText = id_dicause1.Replace(docText, dsothers.Tables[1].Rows[i]["Cause"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_divorce2.Replace(docText, dsothers.Tables[1].Rows[i]["others_type"].ToString());
                        docText = id_dipetitioner2.Replace(docText, dsothers.Tables[1].Rows[i]["Petitioner"].ToString());
                        docText = id_direspondent2.Replace(docText, dsothers.Tables[1].Rows[i]["Respondent"].ToString());
                        docText = id_dif2.Replace(docText, dsothers.Tables[1].Rows[i]["Filed"].ToString());
                        docText = id_dicause2.Replace(docText, dsothers.Tables[1].Rows[i]["Cause"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_divorce3.Replace(docText, dsothers.Tables[1].Rows[i]["others_type"].ToString());
                        docText = id_dipetitioner3.Replace(docText, dsothers.Tables[1].Rows[i]["Petitioner"].ToString());
                        docText = id_direspondent3.Replace(docText, dsothers.Tables[1].Rows[i]["Respondent"].ToString());
                        docText = id_dif3.Replace(docText, dsothers.Tables[1].Rows[i]["Filed"].ToString());
                        docText = id_dicause3.Replace(docText, dsothers.Tables[1].Rows[i]["Cause"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_divorce4.Replace(docText, dsothers.Tables[1].Rows[i]["others_type"].ToString());
                        docText = id_dipetitioner4.Replace(docText, dsothers.Tables[1].Rows[i]["Petitioner"].ToString());
                        docText = id_direspondent4.Replace(docText, dsothers.Tables[1].Rows[i]["Respondent"].ToString());
                        docText = id_dif4.Replace(docText, dsothers.Tables[1].Rows[i]["Filed"].ToString());
                        docText = id_dicause4.Replace(docText, dsothers.Tables[1].Rows[i]["Cause"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_divorce5.Replace(docText, dsothers.Tables[1].Rows[i]["others_type"].ToString());
                    //    docText = id_dipetitioner5.Replace(docText, dsothers.Tables[1].Rows[i]["Petitioner"].ToString());
                    //    docText = id_direspondent5.Replace(docText, dsothers.Tables[1].Rows[i]["Respondent"].ToString());
                    //    docText = id_dif5.Replace(docText, dsothers.Tables[1].Rows[i]["Filed"].ToString());
                    //    docText = id_dicause5.Replace(docText, dsothers.Tables[1].Rows[i]["Cause"].ToString());
                    //}
                }

            }


            if (dsothers.Tables[2].Rows.Count > 0)
            {

                for (int i = 0; i < dsothers.Tables[2].Rows.Count; i++)
                {
                    if (i == 0)
                    {


                        docText = id_genepower1.Replace(docText, dsothers.Tables[2].Rows[i]["others_type"].ToString());
                        docText = id_gpda1.Replace(docText, dsothers.Tables[2].Rows[i]["Dated"].ToString());
                        docText = id_gpf1.Replace(docText, dsothers.Tables[2].Rows[i]["Filed"].ToString());
                        docText = id_gpv1.Replace(docText, dsothers.Tables[2].Rows[i]["Vol"].ToString());
                        docText = id_gpp1.Replace(docText, dsothers.Tables[2].Rows[i]["Pg"].ToString());
                        docText = id_gpins1.Replace(docText, dsothers.Tables[2].Rows[i]["Inst"].ToString());
                        docText = id_gpnotes1.Replace(docText, dsothers.Tables[2].Rows[i]["Notes"].ToString());
                    }

                    if (i == 1)
                    {


                        docText = id_genepower2.Replace(docText, dsothers.Tables[2].Rows[i]["others_type"].ToString());
                        docText = id_gpda2.Replace(docText, dsothers.Tables[2].Rows[i]["Dated"].ToString());
                        docText = id_gpf2.Replace(docText, dsothers.Tables[2].Rows[i]["Filed"].ToString());
                        docText = id_gpv2.Replace(docText, dsothers.Tables[2].Rows[i]["Vol"].ToString());
                        docText = id_gpp2.Replace(docText, dsothers.Tables[2].Rows[i]["Pg"].ToString());
                        docText = id_gpins2.Replace(docText, dsothers.Tables[2].Rows[i]["Inst"].ToString());
                        docText = id_gpnotes2.Replace(docText, dsothers.Tables[2].Rows[i]["Notes"].ToString());
                    }

                    if (i == 2)
                    {


                        docText = id_genepower3.Replace(docText, dsothers.Tables[2].Rows[i]["others_type"].ToString());
                        docText = id_gpda3.Replace(docText, dsothers.Tables[2].Rows[i]["Dated"].ToString());
                        docText = id_gpf3.Replace(docText, dsothers.Tables[2].Rows[i]["Filed"].ToString());
                        docText = id_gpv3.Replace(docText, dsothers.Tables[2].Rows[i]["Vol"].ToString());
                        docText = id_gpp3.Replace(docText, dsothers.Tables[2].Rows[i]["Pg"].ToString());
                        docText = id_gpins3.Replace(docText, dsothers.Tables[2].Rows[i]["Inst"].ToString());
                        docText = id_gpnotes3.Replace(docText, dsothers.Tables[2].Rows[i]["Notes"].ToString());
                    }

                    if (i == 3)
                    {


                        docText = id_genepower4.Replace(docText, dsothers.Tables[2].Rows[i]["others_type"].ToString());
                        docText = id_gpda4.Replace(docText, dsothers.Tables[2].Rows[i]["Dated"].ToString());
                        docText = id_gpf4.Replace(docText, dsothers.Tables[2].Rows[i]["Filed"].ToString());
                        docText = id_gpv4.Replace(docText, dsothers.Tables[2].Rows[i]["Vol"].ToString());
                        docText = id_gpp4.Replace(docText, dsothers.Tables[2].Rows[i]["Pg"].ToString());
                        docText = id_gpins4.Replace(docText, dsothers.Tables[2].Rows[i]["Inst"].ToString());
                        docText = id_gpnotes4.Replace(docText, dsothers.Tables[2].Rows[i]["Notes"].ToString());
                    }

                    //if (i == 4)
                    //{


                    //    docText = id_genepower5.Replace(docText, dsothers.Tables[2].Rows[i]["others_type"].ToString());
                    //    docText = id_gpda5.Replace(docText, dsothers.Tables[2].Rows[i]["Dated"].ToString());
                    //    docText = id_gpf5.Replace(docText, dsothers.Tables[2].Rows[i]["Filed"].ToString());
                    //    docText = id_gpv5.Replace(docText, dsothers.Tables[2].Rows[i]["Vol"].ToString());
                    //    docText = id_gpp5.Replace(docText, dsothers.Tables[2].Rows[i]["Pg"].ToString());
                    //    docText = id_gpins5.Replace(docText, dsothers.Tables[2].Rows[i]["Inst"].ToString());
                    //    docText = id_gpnotes5.Replace(docText, dsothers.Tables[2].Rows[i]["Notes"].ToString());
                    //}

                }
            }





            if (dsothers.Tables[3].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[3].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_probate1.Replace(docText, dsothers.Tables[3].Rows[i]["others_type"].ToString());
                        docText = id_prore1.Replace(docText, dsothers.Tables[3].Rows[i]["Re"].ToString());
                        docText = id_prof1.Replace(docText, dsothers.Tables[3].Rows[i]["Filed"].ToString());
                        docText = id_procause1.Replace(docText, dsothers.Tables[3].Rows[i]["Cause"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_probate2.Replace(docText, dsothers.Tables[3].Rows[i]["others_type"].ToString());
                        docText = id_prore2.Replace(docText, dsothers.Tables[3].Rows[i]["Re"].ToString());
                        docText = id_prof2.Replace(docText, dsothers.Tables[3].Rows[i]["Filed"].ToString());
                        docText = id_procause2.Replace(docText, dsothers.Tables[3].Rows[i]["Cause"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_probate3.Replace(docText, dsothers.Tables[3].Rows[i]["others_type"].ToString());
                        docText = id_prore3.Replace(docText, dsothers.Tables[3].Rows[i]["Re"].ToString());
                        docText = id_prof3.Replace(docText, dsothers.Tables[3].Rows[i]["Filed"].ToString());
                        docText = id_procause3.Replace(docText, dsothers.Tables[3].Rows[i]["Cause"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_probate4.Replace(docText, dsothers.Tables[3].Rows[i]["others_type"].ToString());
                        docText = id_prore4.Replace(docText, dsothers.Tables[3].Rows[i]["Re"].ToString());
                        docText = id_prof4.Replace(docText, dsothers.Tables[3].Rows[i]["Filed"].ToString());
                        docText = id_procause4.Replace(docText, dsothers.Tables[3].Rows[i]["Cause"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_probate5.Replace(docText, dsothers.Tables[3].Rows[i]["others_type"].ToString());
                    //    docText = id_prore5.Replace(docText, dsothers.Tables[3].Rows[i]["Re"].ToString());
                    //    docText = id_prof5.Replace(docText, dsothers.Tables[3].Rows[i]["Filed"].ToString());
                    //    docText = id_procause5.Replace(docText, dsothers.Tables[3].Rows[i]["Cause"].ToString());
                    //}

                }

            }

            if (dsothers.Tables[4].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[4].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_reinstatement1.Replace(docText, dsothers.Tables[4].Rows[i]["others_type"].ToString());
                        docText = id_rada1.Replace(docText, dsothers.Tables[4].Rows[i]["Dated"].ToString());
                        docText = id_raf1.Replace(docText, dsothers.Tables[4].Rows[i]["Filed"].ToString());
                        docText = id_rav1.Replace(docText, dsothers.Tables[4].Rows[i]["Vol"].ToString());
                        docText = id_rap1.Replace(docText, dsothers.Tables[4].Rows[i]["Pg"].ToString());
                        docText = id_rains1.Replace(docText, dsothers.Tables[4].Rows[i]["Inst"].ToString());
                        docText = id_ranotes1.Replace(docText, dsothers.Tables[4].Rows[i]["Notes"].ToString());
                    }

                    if (i == 1)
                    {
                        docText = id_reinstatement2.Replace(docText, dsothers.Tables[4].Rows[i]["others_type"].ToString());
                        docText = id_rada2.Replace(docText, dsothers.Tables[4].Rows[i]["Dated"].ToString());
                        docText = id_raf2.Replace(docText, dsothers.Tables[4].Rows[i]["Filed"].ToString());
                        docText = id_rav2.Replace(docText, dsothers.Tables[4].Rows[i]["Vol"].ToString());
                        docText = id_rap2.Replace(docText, dsothers.Tables[4].Rows[i]["Pg"].ToString());
                        docText = id_rains2.Replace(docText, dsothers.Tables[4].Rows[i]["Inst"].ToString());
                        docText = id_ranotes2.Replace(docText, dsothers.Tables[4].Rows[i]["Notes"].ToString());
                    }

                    if (i == 2)
                    {
                        docText = id_reinstatement3.Replace(docText, dsothers.Tables[4].Rows[i]["others_type"].ToString());
                        docText = id_rada3.Replace(docText, dsothers.Tables[4].Rows[i]["Dated"].ToString());
                        docText = id_raf3.Replace(docText, dsothers.Tables[4].Rows[i]["Filed"].ToString());
                        docText = id_rav3.Replace(docText, dsothers.Tables[4].Rows[i]["Vol"].ToString());
                        docText = id_rap3.Replace(docText, dsothers.Tables[4].Rows[i]["Pg"].ToString());
                        docText = id_rains3.Replace(docText, dsothers.Tables[4].Rows[i]["Inst"].ToString());
                        docText = id_ranotes3.Replace(docText, dsothers.Tables[4].Rows[i]["Notes"].ToString());
                    }

                    if (i == 3)
                    {
                        docText = id_reinstatement4.Replace(docText, dsothers.Tables[4].Rows[i]["others_type"].ToString());
                        docText = id_rada4.Replace(docText, dsothers.Tables[4].Rows[i]["Dated"].ToString());
                        docText = id_raf4.Replace(docText, dsothers.Tables[4].Rows[i]["Filed"].ToString());
                        docText = id_rav4.Replace(docText, dsothers.Tables[4].Rows[i]["Vol"].ToString());
                        docText = id_rap4.Replace(docText, dsothers.Tables[4].Rows[i]["Pg"].ToString());
                        docText = id_rains4.Replace(docText, dsothers.Tables[4].Rows[i]["Inst"].ToString());
                        docText = id_ranotes4.Replace(docText, dsothers.Tables[4].Rows[i]["Notes"].ToString());
                    }

                    //if (i == 4)
                    //{
                    //    docText = id_reinstatement5.Replace(docText, dsothers.Tables[4].Rows[i]["others_type"].ToString());
                    //    docText = id_rada5.Replace(docText, dsothers.Tables[4].Rows[i]["Dated"].ToString());
                    //    docText = id_raf5.Replace(docText, dsothers.Tables[4].Rows[i]["Filed"].ToString());
                    //    docText = id_rav5.Replace(docText, dsothers.Tables[4].Rows[i]["Vol"].ToString());
                    //    docText = id_rap5.Replace(docText, dsothers.Tables[4].Rows[i]["Pg"].ToString());
                    //    docText = id_rains5.Replace(docText, dsothers.Tables[4].Rows[i]["Inst"].ToString());
                    //    docText = id_ranotes5.Replace(docText, dsothers.Tables[4].Rows[i]["Notes"].ToString());
                    //}


                }
            }


            if (dsothers.Tables[5].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[5].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_stateofowner1.Replace(docText, dsothers.Tables[5].Rows[i]["others_type"].ToString());
                        docText = id_soowner1.Replace(docText, dsothers.Tables[5].Rows[i]["Owner"].ToString());
                        docText = id_somanufacturer1.Replace(docText, dsothers.Tables[5].Rows[i]["Manufacturer"].ToString());
                        docText = id_soda1.Replace(docText, dsothers.Tables[5].Rows[i]["Dated"].ToString());
                        docText = id_sof1.Replace(docText, dsothers.Tables[5].Rows[i]["Filed"].ToString());
                        docText = id_sov1.Replace(docText, dsothers.Tables[5].Rows[i]["Vol"].ToString());
                        docText = id_sop1.Replace(docText, dsothers.Tables[5].Rows[i]["Pg"].ToString());
                        docText = id_soins1.Replace(docText, dsothers.Tables[5].Rows[i]["Inst"].ToString());
                        docText = id_sonotes1.Replace(docText, dsothers.Tables[5].Rows[i]["Notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_stateofowner2.Replace(docText, dsothers.Tables[5].Rows[i]["others_type"].ToString());
                        docText = id_soowner2.Replace(docText, dsothers.Tables[5].Rows[i]["Owner"].ToString());
                        docText = id_somanufacturer2.Replace(docText, dsothers.Tables[5].Rows[i]["Manufacturer"].ToString());
                        docText = id_soda2.Replace(docText, dsothers.Tables[5].Rows[i]["Dated"].ToString());
                        docText = id_sof2.Replace(docText, dsothers.Tables[5].Rows[i]["Filed"].ToString());
                        docText = id_sov2.Replace(docText, dsothers.Tables[5].Rows[i]["Vol"].ToString());
                        docText = id_sop2.Replace(docText, dsothers.Tables[5].Rows[i]["Pg"].ToString());
                        docText = id_soins2.Replace(docText, dsothers.Tables[5].Rows[i]["Inst"].ToString());
                        docText = id_sonotes2.Replace(docText, dsothers.Tables[5].Rows[i]["Notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_stateofowner3.Replace(docText, dsothers.Tables[5].Rows[i]["others_type"].ToString());
                        docText = id_soowner3.Replace(docText, dsothers.Tables[5].Rows[i]["Owner"].ToString());
                        docText = id_somanufacturer3.Replace(docText, dsothers.Tables[5].Rows[i]["Manufacturer"].ToString());
                        docText = id_soda3.Replace(docText, dsothers.Tables[5].Rows[i]["Dated"].ToString());
                        docText = id_sof3.Replace(docText, dsothers.Tables[5].Rows[i]["Filed"].ToString());
                        docText = id_sov3.Replace(docText, dsothers.Tables[5].Rows[i]["Vol"].ToString());
                        docText = id_sop3.Replace(docText, dsothers.Tables[5].Rows[i]["Pg"].ToString());
                        docText = id_soins3.Replace(docText, dsothers.Tables[5].Rows[i]["Inst"].ToString());
                        docText = id_sonotes3.Replace(docText, dsothers.Tables[5].Rows[i]["Notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_stateofowner4.Replace(docText, dsothers.Tables[5].Rows[i]["others_type"].ToString());
                        docText = id_soowner4.Replace(docText, dsothers.Tables[5].Rows[i]["Owner"].ToString());
                        docText = id_somanufacturer4.Replace(docText, dsothers.Tables[5].Rows[i]["Manufacturer"].ToString());
                        docText = id_soda4.Replace(docText, dsothers.Tables[5].Rows[i]["Dated"].ToString());
                        docText = id_sof4.Replace(docText, dsothers.Tables[5].Rows[i]["Filed"].ToString());
                        docText = id_sov4.Replace(docText, dsothers.Tables[5].Rows[i]["Vol"].ToString());
                        docText = id_sop4.Replace(docText, dsothers.Tables[5].Rows[i]["Pg"].ToString());
                        docText = id_soins4.Replace(docText, dsothers.Tables[5].Rows[i]["Inst"].ToString());
                        docText = id_sonotes4.Replace(docText, dsothers.Tables[5].Rows[i]["Notes"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //    docText = id_stateofowner5.Replace(docText, dsothers.Tables[5].Rows[i]["others_type"].ToString());
                    //    docText = id_soowner5.Replace(docText, dsothers.Tables[5].Rows[i]["Owner"].ToString());
                    //    docText = id_somanufacturer5.Replace(docText, dsothers.Tables[5].Rows[i]["Manufacturer"].ToString());
                    //    docText = id_soda5.Replace(docText, dsothers.Tables[5].Rows[i]["Dated"].ToString());
                    //    docText = id_sof5.Replace(docText, dsothers.Tables[5].Rows[i]["Filed"].ToString());
                    //    docText = id_sov5.Replace(docText, dsothers.Tables[5].Rows[i]["Vol"].ToString());
                    //    docText = id_sop5.Replace(docText, dsothers.Tables[5].Rows[i]["Pg"].ToString());
                    //    docText = id_soins5.Replace(docText, dsothers.Tables[5].Rows[i]["Inst"].ToString());
                    //    docText = id_sonotes5.Replace(docText, dsothers.Tables[5].Rows[i]["Notes"].ToString());
                    //}
                }
            }

            if (dsothers.Tables[6].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[6].Rows.Count; i++)
                {
                    if (i == 0)
                    {

                        docText = id_spoaid1.Replace(docText, dsothers.Tables[6].Rows[i]["others_type"].ToString());
                        docText = id_spoato1.Replace(docText, dsothers.Tables[6].Rows[i]["To"].ToString());
                        docText = id_spoagrantor1.Replace(docText, dsothers.Tables[6].Rows[i]["Grantor"].ToString());
                        docText = id_spoadate1.Replace(docText, dsothers.Tables[6].Rows[i]["Dated"].ToString());
                        docText = id_spoafiled1.Replace(docText, dsothers.Tables[6].Rows[i]["Filed"].ToString());
                        docText = id_spoavol1.Replace(docText, dsothers.Tables[6].Rows[i]["Vol"].ToString());
                        docText = id_spoapg1.Replace(docText, dsothers.Tables[6].Rows[i]["Pg"].ToString());
                        docText = id_spoainst1.Replace(docText, dsothers.Tables[6].Rows[i]["Inst"].ToString());
                        docText = id_spoanote1.Replace(docText, dsothers.Tables[6].Rows[i]["Notes"].ToString());
                    }

                    if (i == 1)
                    {

                        docText = id_spoaid2.Replace(docText, dsothers.Tables[6].Rows[i]["others_type"].ToString());
                        docText = id_spoato2.Replace(docText, dsothers.Tables[6].Rows[i]["To"].ToString());
                        docText = id_spoagrantor2.Replace(docText, dsothers.Tables[6].Rows[i]["Grantor"].ToString());
                        docText = id_spoadate2.Replace(docText, dsothers.Tables[6].Rows[i]["Dated"].ToString());
                        docText = id_spoafiled2.Replace(docText, dsothers.Tables[6].Rows[i]["Filed"].ToString());
                        docText = id_spoavol2.Replace(docText, dsothers.Tables[6].Rows[i]["Vol"].ToString());
                        docText = id_spoapg2.Replace(docText, dsothers.Tables[6].Rows[i]["Pg"].ToString());
                        docText = id_spoainst2.Replace(docText, dsothers.Tables[6].Rows[i]["Inst"].ToString());
                        docText = id_spoanote2.Replace(docText, dsothers.Tables[6].Rows[i]["Notes"].ToString());
                    }

                    if (i == 2)
                    {

                        docText = id_spoaid3.Replace(docText, dsothers.Tables[6].Rows[i]["others_type"].ToString());
                        docText = id_spoato3.Replace(docText, dsothers.Tables[6].Rows[i]["To"].ToString());
                        docText = id_spoagrantor3.Replace(docText, dsothers.Tables[6].Rows[i]["Grantor"].ToString());
                        docText = id_spoadate3.Replace(docText, dsothers.Tables[6].Rows[i]["Dated"].ToString());
                        docText = id_spoafiled3.Replace(docText, dsothers.Tables[6].Rows[i]["Filed"].ToString());
                        docText = id_spoavol3.Replace(docText, dsothers.Tables[6].Rows[i]["Vol"].ToString());
                        docText = id_spoapg3.Replace(docText, dsothers.Tables[6].Rows[i]["Pg"].ToString());
                        docText = id_spoainst3.Replace(docText, dsothers.Tables[6].Rows[i]["Inst"].ToString());
                        docText = id_spoanote3.Replace(docText, dsothers.Tables[6].Rows[i]["Notes"].ToString());
                    }

                    if (i == 3)
                    {

                        docText = id_spoaid4.Replace(docText, dsothers.Tables[6].Rows[i]["others_type"].ToString());
                        docText = id_spoato4.Replace(docText, dsothers.Tables[6].Rows[i]["To"].ToString());
                        docText = id_spoagrantor4.Replace(docText, dsothers.Tables[6].Rows[i]["Grantor"].ToString());
                        docText = id_spoadate4.Replace(docText, dsothers.Tables[6].Rows[i]["Dated"].ToString());
                        docText = id_spoafiled4.Replace(docText, dsothers.Tables[6].Rows[i]["Filed"].ToString());
                        docText = id_spoavol4.Replace(docText, dsothers.Tables[6].Rows[i]["Vol"].ToString());
                        docText = id_spoapg4.Replace(docText, dsothers.Tables[6].Rows[i]["Pg"].ToString());
                        docText = id_spoainst4.Replace(docText, dsothers.Tables[6].Rows[i]["Inst"].ToString());
                        docText = id_spoanote4.Replace(docText, dsothers.Tables[6].Rows[i]["Notes"].ToString());
                    }

                    //if (i == 4)
                    //{

                    //docText = id_spoaid5.Replace(docText, dsothers.Tables[6].Rows[i]["others_type"].ToString());
                    //docText = id_spoato5.Replace(docText, dsothers.Tables[6].Rows[i]["To"].ToString());
                    //docText = id_spoagrantor5.Replace(docText, dsothers.Tables[6].Rows[i]["Grantor"].ToString());
                    //docText = id_spoadate5.Replace(docText, dsothers.Tables[6].Rows[i]["Dated"].ToString());
                    //docText = id_spoafiled5.Replace(docText, dsothers.Tables[6].Rows[i]["Filed"].ToString());
                    //docText = id_spoavol5.Replace(docText, dsothers.Tables[6].Rows[i]["Vol"].ToString());
                    //docText = id_spoapg5.Replace(docText, dsothers.Tables[6].Rows[i]["Pg"].ToString());
                    //docText = id_spoainst5.Replace(docText, dsothers.Tables[6].Rows[i]["Inst"].ToString());
                    //docText = id_spoanote5.Replace(docText, dsothers.Tables[6].Rows[i]["Notes"].ToString());
                    //}
                }
            }

            #endregion
            #region declaration
            DataSet dsdeclar = new DataSet();
            dsdeclar = gls.gettypevalue(lbl_orderno.Text, "sp_sel_declaration_output");
            if (dsdeclar.Tables[0].Rows.Count > 0)
            {
                docText = id_declaration.Replace(docText, dsdeclar.Tables[0].Rows[0]["declaration"].ToString());
            }
            #endregion declaration

            using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }
        }
        return true;
    }


        #endregion wordodc


    private string ReplaceSpecialCharacters(string s)
    {
        return s.Replace("&", "&#38;");
    }

    private DateTime GetLastDayOfMonth(int iMonth)
    {
        DateTime dtTo = new DateTime(DateTime.Now.Year, iMonth, 1);
        dtTo = dtTo.AddMonths(1);
        dtTo = dtTo.AddDays(-(dtTo.Day));
        return dtTo;
    }

    private string getfullpath(string filename, string query)
    {
        string slash = @"\";
        string dec, sourcePath, pdatee, month, year, path = "";
        //string query = "select Output_SSTaxes from master_path";        
        MySqlParameter[] mParam = new MySqlParameter[1];
        MySqlDataReader mDataReader = objconnection.ExecuteSPReader(query, false, mParam);
        if (mDataReader.HasRows)
        {
            if (mDataReader.Read())
            {
                string foldername = "Output";
                sourcePath = mDataReader.GetString(0);
                //DateTime pde;
                //pde = Convert.ToDateTime(Lbldate.Text);
                DateTime pde = DateTime.Now;
                pde = pde.AddHours(-17);
                pdatee = String.Format("{0:dd MMM yy}", pde);
                month = String.Format("{0:MMMM}", pde);
                year = String.Format("{0:yyyy}", pde);
                dec = sourcePath + slash + year + slash + month + slash + pdatee + slash + foldername + slash + "1111111";
                // dir(dec);
                path = dec;
            }
        }
        mDataReader.Close();
        return path;
    }


    private string getfullpath1(string query)
    {
        string slash = @"\";
        string dec, sourcePath, pdatee, month, year, path = "";
        MySqlParameter[] mParam = new MySqlParameter[1];
        MySqlDataReader mDataReader = objconnection.ExecuteSPReader(query, false, mParam);
        if (mDataReader.HasRows)
        {
            if (mDataReader.Read())
            {
                sourcePath = mDataReader.GetString(0);
                DateTime pde;
                pde = DateTime.Now;
                pdatee = String.Format("{0:dd MMM yy}", pde);
                month = String.Format("{0:MMMM}", pde);
                year = String.Format("{0:yyyy}", pde);
                dec = sourcePath + slash + year + slash + month + slash + pdatee;
                dir(dec);

                path = dec;
            }
        }
        mDataReader.Close();
        return path;
    }


    private string Getquery()
    {
        string query = "";

        query = "select Outputpath from master_path";

        return query;
    }
    private void dir(string path)
    {


        try
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
                System.IO.DirectoryInfo dIn = new System.IO.DirectoryInfo(path);

            }
        }
        catch (System.IO.DirectoryNotFoundException)
        {

        }
        catch (Exception)
        {

        }
    }



    protected void txtgrantee_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_client_update_Click(object sender, EventArgs e)
    {
        int res = gl.updateclient(txt_orderno.Text, txt_client.Text, txt_date.Text, txt_address.Text, txt_city_zip.Text, txt_ref.Text, txt_attention.Text, txt_certdate.Text, txt_owner.Text, txt_propaddress.Text, txt_city.Text, txt_state.Text, txt_zip.Text, txt_county.Text, txt_legalinfo.Text, txt_ownerofrec.Text);
        if (res > 0)
        {
            lbl_client_show.Text = "Update Successfully....!!";

        }
    }
    protected void btn_assess_update_Click(object sender, EventArgs e)
    {
        int res = gl.updateassessmaent(txt_orderno.Text, txt_parcelid.Text, txt_taxyear.Text, txt_land.Text, txt_improv.Text, txt_total.Text, txt_taxes.Text, txt_duepaid.Text, txt_assessnotes.Text);

        if (res > 0)
        {
            lbl_assess_show.Text = "Update Successfully....!!";

        }
    }

}