using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
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
using  Microsoft.Office.Interop.Word;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Novacode;
using Microsoft.Office.Core;
using System.Windows;
using System.Linq;


public partial class Form_production : System.Web.UI.Page
{


    Connection cons = new Connection();
    GlobalClass gls = new GlobalClass();
    // global gl = new global();
    allstar_global al = new allstar_global();
    DBConnection objconnection = new DBConnection();
    System.Data.DataTable dt = new System.Data.DataTable();
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

            AllotProcess();
            get_clientinfo();
            grid_deed_show();
            grid_mortgage_show();
            grid_tax_show();
        }
    }




    #region Allotprocess
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
                LblDate.Text = CheckNull(mdr, 2);
                lbl_processname.Text = SessionHandler.UserName;
                lbl_pros_name.Text = CheckNull(mdr, 3);
                SessionHandler.Rights = lbl_pros_name.Text;


                lbl_state.Text = CheckNull(mdr, 4);
                lbl_county.Text = CheckNull(mdr, 5);
                lbl_product.Text = CheckNull(mdr, 6);

                if (lbl_pros_name.Text == "SEARCH")
                {
                    div_keys.Visible = false;
                    lbl_search.Visible = false;
                    lbl_searchtext.Visible = false;
                    lbl_searchqc.Visible = false;
                    lbl_searchqctext.Visible = false;
                    lbl_keying.Visible = false;
                    lbl_keyingtext.Visible = false;


                    lbl_search_comments.Visible = true;
                    txt_search_comments.Visible = true;
                    lbl_searchqc_comments.Visible = false;
                    txt_searchqc_comments.Visible = false;
                    lbl_keying_comments.Visible = false;
                    txt_keying_commend.Visible = false;
                    lbl_qc_comments.Visible = false;
                    txt_qc_comments.Visible = false;


                    btn_order_save.Visible = false;
                    btn_complete.Visible = true;

                }

                else if (lbl_pros_name.Text == "SEARCHQC")
                {
                    div_keys.Visible = false;
                    lbl_search.Text = CheckNull(mdr, 8);
                    txt_search_comments.Text = CheckNull(mdr, 7);

                    lbl_search.Visible = true;
                    lbl_searchtext.Visible = true;
                    lbl_searchqc.Visible = false;
                    lbl_searchqctext.Visible = false;

                    lbl_search_comments.Visible = true;
                    txt_search_comments.Visible = true;
                    lbl_searchqc_comments.Visible = true;
                    txt_searchqc_comments.Visible = true;
                    lbl_keying_comments.Visible = false;
                    txt_keying_commend.Visible = false;
                    lbl_qc_comments.Visible = false;
                    txt_qc_comments.Visible = false;
                    txt_search_comments.ReadOnly = true;

                    btn_order_save.Visible = false;
                    btn_complete.Visible = true;

                }


                else if (lbl_pros_name.Text == "KEYING")
                {
                    div_keys.Visible = true;
                    lbl_search.Text = CheckNull(mdr, 9);
                    lbl_searchqc.Text = CheckNull(mdr, 10);
                    txt_search_comments.Text = CheckNull(mdr, 7);
                    txt_searchqc_comments.Text = CheckNull(mdr, 8);

                    lbl_search_comments.Visible = true;
                    txt_search_comments.Visible = true;
                    lbl_searchqc_comments.Visible = true;
                    txt_searchqc_comments.Visible = true;
                    lbl_qc_comments.Visible = false;
                    txt_qc_comments.Visible = false;

                    lbl_search.Visible = true;
                    lbl_searchtext.Visible = true;
                    lbl_searchqc.Visible = true;
                    lbl_searchqctext.Visible = true;

                    txt_search_comments.ReadOnly = true;
                    txt_searchqc_comments.ReadOnly = true;
                    btn_order_save.Visible = false;
                    btn_complete.Visible = true;

                }

                else if (lbl_pros_name.Text == "QC")
                {
                    div_keys.Visible = true;
                    lbl_search.Text = CheckNull(mdr, 10);
                    lbl_searchqc.Text = CheckNull(mdr, 11);
                    lbl_keying.Text = CheckNull(mdr, 12);

                    txt_search_comments.Text = CheckNull(mdr, 7);
                    txt_searchqc_comments.Text = CheckNull(mdr, 8);
                    txt_keying_commend.Text = CheckNull(mdr, 9);

                    lbl_search.Visible = true;
                    lbl_searchtext.Visible = true;
                    lbl_searchqc.Visible = true;
                    lbl_searchqctext.Visible = true;
                    lbl_keying.Visible = true;
                    lbl_keyingtext.Visible = true;
                    lbl_qc_comments.Visible = true;
                    txt_qc_comments.Visible = true;

                    txt_keying_commend.ReadOnly = true;
                    txt_search_comments.ReadOnly = true;
                    txt_searchqc_comments.ReadOnly = true;

                    btn_order_save.Visible = true;
                    btn_complete.Visible = false;
                }

                else if (lbl_pros_name.Text == "DU")
                {
                    lbl_qc_comments.Visible = false;
                    txt_qc_comments.Visible = false;
                    btn_order_save.Visible = true;
                    btn_complete.Visible = false;

                }

                Session["Timepro"] = DateTime.Now;
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
    #endregion Allotprocess

    protected void get_clientinfo()
    {
        DataSet ds = new DataSet();
        ds = al.showclientinfo(lbl_orderno.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txt_search_date.Text = ds.Tables[0].Rows[0]["search_date"].ToString();
            txt_as_of_date.Text = ds.Tables[0].Rows[0]["as_of_date"].ToString();
            txt_address.Text = ds.Tables[0].Rows[0]["address"].ToString();
            if (txt_search_date.Text != "" || txt_as_of_date.Text != "")
            {
                btn_client_save.Visible = false;
                btn_client_update.Visible = true;
            }

        }

    }

    protected void grid_deed_show()
    {
        clear_deed();
        DataSet ds = new DataSet();
        ds = al.showdeed(lbl_orderno.Text);
        grd_deed.DataSource = ds;
        grd_deed.DataBind();
        clear_deed();


    }
    protected void grid_mortgage_show()
    {
        DataSet ds = new DataSet();
        ds = al.showmortgage(lbl_orderno.Text);
        grd_mortgage.Visible = true;
        grd_mortgage.DataSource = ds;
        grd_mortgage.DataBind();
        clear_mrg();
    }
    protected void grid_tax_show()
    {
        DataSet ds = new DataSet();
        ds = al.showtax(lbl_orderno.Text);
        grd_tax.Visible = true;
        grd_tax.DataSource = ds;
        grd_tax.DataBind();
        clear_tax();
    }






    protected void drp_deed_SelectedIndexChanged(object sender, EventArgs e)
    {
        clear_deed();
        if (drp_deed.Text == "PRIOR DEED RECORD")
        {
            txt_deed_type.Text = "PRIOR DEED RECORD";
            lbl_deed_legal.Visible = false;
            txt_deed_legal.Visible = false;

        }
        else
        {
            txt_deed_type.Text = "CURRENT DEED RECORD";
            lbl_deed_legal.Visible = true;
            txt_deed_legal.Visible = true;
        }
    }
    protected void grd_deed_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        int index = Int32.Parse(e.Item.Value);

        MultiView1.ActiveViewIndex = index;

    }

    #region save button
    protected void btn_client_save_Click(object sender, EventArgs e)
    {
        int res = al.insert_client(lbl_orderno.Text, txt_search_date.Text, txt_as_of_date.Text, txt_address.Text);
        if (res > 0)
        {
            lbl_client_show.Text = "Saved Successfully....!!";

        }
    }
    protected void btn_save_wardeed_Click(object sender, EventArgs e)
    {
        int res = al.insert_deed(lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text, txt_deed_tableno.Text);
        if (res > 0)
        {
            grid_deed_show();
        }

    }
    protected void btn_mrg_save_Click(object sender, EventArgs e)
    {
        int res = al.insert_mortgage(lbl_orderno.Text, txt_mrg_mortgager.Text, txt_mrg_mortgagee.Text, txt_mrg_dated.Text, txt_mrg_recorded.Text, txt_mrg_book.Text, txt_mrg_pg.Text, txt_mrg_amount.Text, txt_mrg_opndate.Text);
        if (res > 0)
        {
            grid_mortgage_show();
        }
    }
    protected void btn_tax_save_Click(object sender, EventArgs e)
    {
        int res = al.insert_tax(lbl_orderno.Text, txt_tax_land.Text, txt_tax_building.Text, txt_tax_total.Text, txt_tax_idno.Text, txt_tax_2015_paid.Text, txt_tax_2015_on.Text, txt_tax_next_due.Text, txt_tax_all_pre.Text, txt_tax_home.Text, txt_tax_water.Text);
        if (res > 0)
        {
            grid_tax_show();
        }
    }
    protected void btn_order_save_Click(object sender, EventArgs e)
    {
        string comments = "";
        if (ValidateComments())
        {
            if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU") comments = txt_keying_commend.Text;
            else if (lbl_pros_name.Text == "QC") comments = txt_qc_comments.Text;
            //OutputWriteUp(lbl_orderno.Text);        
            int result = al.UpdateOrders(comments);

            SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
            SessionHandler.RedirectPage("~/Form/HomePage.aspx");
        }
    }


    #endregion save button
    private bool ValidateComments()
    {
        if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU")
        {
            if (txt_keying_commend.Text == "")
            { LblError.Text = "Please Fill the Keycomments."; return false; }
        }
        else if (lbl_pros_name.Text == "QC")
        {
            if (txt_qc_comments.Text == "")
            { LblError.Text = "Please Fill the QCcomments."; return false; }
        }

        else if (lbl_pros_name.Text == "SEARCH")
        {
            if (txt_search_comments.Text == "")
            { LblError.Text = "Please Fill the Search comments."; return false; }
        }


        else if (lbl_pros_name.Text == "SEARCHQC")
        {
            if (txt_searchqc_comments.Text == "")
            { LblError.Text = "Please Fill the search-QC comments."; return false; }
        }





        return true;
    }

    #region writeup
    private string outputath;
    private bool OutputWriteUp(string order_no)
    {
        #region old
        object missing = System.Type.Missing;

        DataSet dswriteup = new DataSet();
        string query = "select roughcopy,Template from master_path_copy";
        dswriteup = gls.GetWriteUp(query);

        string sourcePath = dswriteup.Tables[0].Rows[0]["Template"].ToString();
        outputath = getfullpath1(query);
        // outputath = dswriteup.Tables[0].Rows[0]["roughcopy"].ToString();

        string docname = "";
        // outputath = getfullpath1(query);
        docname = "allstartmp" + ".docx";
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

        }


        #endregion old


        return true;
    }
    private bool BindWriteUp(DataSet dswriteup, string target)
    {

        Regex id_orderno = new Regex("@ord_no");
        Regex id_scdate = new Regex("@scrdate");
        Regex id_asdate = new Regex("@asdate");
        Regex id_adr = new Regex("@adr");


        #region Deed

        //current deed
        Regex id_cdgrantor = new Regex("@cdgrntr");
        Regex id_cdgrantee = new Regex("@cdgrnte");
        Regex id_cddate = new Regex("@cddt");
        Regex id_cdrecord = new Regex("@cdrd");
        Regex id_cdbook = new Regex("@cdbk");
        Regex id_cdpage = new Regex("@cdpg");
        Regex id_cdlegal = new Regex("@cdlgl");


        //Prior deed
        Regex id_pdgrantor = new Regex("@pdgrntr");
        Regex id_pdgrantee = new Regex("@pdgrnte");
        Regex id_pddate = new Regex("@pddt");
        Regex id_pdrecord = new Regex("@pdrd");
        Regex id_pdbook = new Regex("@pdbk");
        Regex id_pdpage = new Regex("@pdpg");

        #endregion Deed

        #region Mortgage

        // Mortgae 1
        Regex id_mrgmortgagor = new Regex("@mrgmgr");
        Regex id_mrgtgagee = new Regex("@mrgmge");
        Regex id_mrgdate = new Regex("@mrgdt");
        Regex id_mrgrecord = new Regex("@mrgrd");
        Regex id_mrgbook = new Regex("@mrgbk");
        Regex id_mrgpage = new Regex("@mrgpg");
        Regex id_mrgamount = new Regex("@MRGAMT");
        Regex id_mrgopn = new Regex("@mro");

        #endregion


        #region tax
        //tax 1
        Regex id_taxland = new Regex("@taxlnd");
        Regex id_taxbuilding = new Regex("@taxbld");
        Regex id_taxtotal = new Regex("@taxtot");
        Regex id_taxid = new Regex("@TAXID");
        Regex id_taxamtof = new Regex("@taxamtof");
        Regex id_taxon = new Regex("@taxon");
        Regex id_taxnext = new Regex("@taxnxt");
        Regex id_taxal = new Regex("@taxal");
        Regex id_taxhome = new Regex("@taxhm");
        Regex id_taxwater = new Regex("@taxwtr");

        #endregion tax


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
                docText = id_orderno.Replace(docText, dsgeneral.Tables[0].Rows[0]["orderno"].ToString());
                docText = id_scdate.Replace(docText, dsgeneral.Tables[0].Rows[0]["search_date"].ToString());
                docText = id_asdate.Replace(docText, dsgeneral.Tables[0].Rows[0]["as_of_date"].ToString());
                docText = id_adr.Replace(docText, dsgeneral.Tables[0].Rows[0]["address"].ToString());

            }
            else
            {
                docText = id_orderno.Replace(docText, string.Empty);
                docText = id_scdate.Replace(docText, string.Empty);
                docText = id_asdate.Replace(docText, string.Empty);
                docText = id_adr.Replace(docText, string.Empty);

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
                        docText = id_cdgrantor.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_cdgrantee.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_cddate.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_cdrecord.Replace(docText, dsdeed.Tables[0].Rows[i]["RECORDED"].ToString());
                        docText = id_cdbook.Replace(docText, dsdeed.Tables[0].Rows[i]["BOOK"].ToString());
                        docText = id_cdpage.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());
                        docText = id_cdlegal.Replace(docText, dsdeed.Tables[0].Rows[i]["LEGAL"].ToString());

                    }
                    if (i == 1)
                    {
                        docText = id_pdgrantor.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTOR"].ToString());
                        docText = id_pdgrantee.Replace(docText, dsdeed.Tables[0].Rows[i]["GRANTEE"].ToString());
                        docText = id_pddate.Replace(docText, dsdeed.Tables[0].Rows[i]["DATED"].ToString());
                        docText = id_pdrecord.Replace(docText, dsdeed.Tables[0].Rows[i]["RECORDED"].ToString());
                        docText = id_pdbook.Replace(docText, dsdeed.Tables[0].Rows[i]["BOOK"].ToString());
                        docText = id_pdpage.Replace(docText, dsdeed.Tables[0].Rows[i]["PG"].ToString());

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
                        docText = id_mrgmortgagor.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgagor"].ToString());
                        docText = id_mrgtgagee.Replace(docText, dsmortgage.Tables[0].Rows[i]["mortgagee"].ToString());
                        docText = id_mrgdate.Replace(docText, dsmortgage.Tables[0].Rows[i]["dated"].ToString());
                        docText = id_mrgrecord.Replace(docText, dsmortgage.Tables[0].Rows[i]["recorded"].ToString());
                        docText = id_mrgbook.Replace(docText, dsmortgage.Tables[0].Rows[i]["book"].ToString());
                        docText = id_mrgpage.Replace(docText, dsmortgage.Tables[0].Rows[i]["pg"].ToString());
                        docText = id_mrgamount.Replace(docText, dsmortgage.Tables[0].Rows[i]["amount"].ToString());
                        docText = id_mrgopn.Replace(docText, dsmortgage.Tables[0].Rows[i]["openend_mortgage"].ToString());

                    }

                }
            }



            #endregion


            #region Tax

            DataSet dsothers = new DataSet();
            dsothers = gls.gettypevalue(lbl_orderno.Text, "sp_sel_Tax_output");
            if (dsothers.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsothers.Tables[0].Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        docText = id_taxland.Replace(docText, dsothers.Tables[0].Rows[i]["land"].ToString());
                        docText = id_taxbuilding.Replace(docText, dsothers.Tables[0].Rows[i]["building"].ToString());
                        docText = id_taxtotal.Replace(docText, dsothers.Tables[0].Rows[i]["total"].ToString());
                        docText = id_taxid.Replace(docText, dsothers.Tables[0].Rows[i]["id_number"].ToString());
                        docText = id_taxamtof.Replace(docText, dsothers.Tables[0].Rows[i]["2015__paid_amt"].ToString());
                        docText = id_taxon.Replace(docText, dsothers.Tables[0].Rows[i]["2015_on"].ToString());
                        docText = id_taxnext.Replace(docText, dsothers.Tables[0].Rows[i]["nxt_tax_due"].ToString());
                        docText = id_taxal.Replace(docText, dsothers.Tables[0].Rows[i]["pre_tax_paid"].ToString());
                        docText = id_taxhome.Replace(docText, dsothers.Tables[0].Rows[i]["home_exe"].ToString());
                        docText = id_taxwater.Replace(docText, dsothers.Tables[0].Rows[i]["water_prop"].ToString());
                    }


                }


            }



            #endregion


            using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
            {
                sw.Write(docText);
            }
        }
        #endregion wordodc

        return true;
    }






    private string getfullpath1(string query)
    {
        string slash = @"\";
        string dec, sourcePath, pdatee, month, year, path = "";
        MySqlParameter[] mParam = new MySqlParameter[1];

        MySqlDataReader mDataReader = cons.ExecuteSPReader(query, false, mParam);
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

    #endregion writeup


    #region Cancrl & clear
    protected void clear_deed()
    {
        btn_save_wardeed.Visible = true;
        btn_save_deedupdate.Visible = false;

        txt_deed_type.Text = "";
        txt_deed_grantor.Text = "";
        txt_deed_grantee.Text = "";
        txt_deed_dated.Text = "";
        txt_deed_recorded.Text = "";
        txt_deed_book.Text = "";
        txt_deed_pg.Text = "";
        txt_deed_legal.Text = "";
        txt_deed_type.Text = drp_deed.Text;
    }
    protected void clear_mrg()
    {
        btn_mrg_save.Visible = true;
        btn_mrg_update.Visible = false;
        txt_mrg_mortgager.Text = "";
        txt_mrg_mortgagee.Text = "";
        txt_mrg_dated.Text = "";
        txt_mrg_recorded.Text = "";
        txt_mrg_book.Text = "";
        txt_mrg_pg.Text = "";
        txt_mrg_amount.Text = "";
        txt_mrg_opndate.Text = "";

    }
    protected void clear_tax()
    {
        btn_tax_save.Visible = true;
        btn_tax_update.Visible = false;

        txt_tax_land.Text = "";
        txt_tax_building.Text = "";
        txt_tax_total.Text = "";
        txt_tax_idno.Text = "";
        txt_tax_2015_paid.Text = "";
        txt_tax_2015_on.Text = "";
        txt_tax_next_due.Text = "";
        txt_tax_all_pre.Text = "";
        txt_tax_home.Text = "";
        txt_tax_water.Text = "";
    }
    protected void btn_deed_cancel_Click(object sender, EventArgs e)
    {
        clear_deed();
    }
    protected void btn_mrg_cancel_Click(object sender, EventArgs e)
    {
        clear_mrg();
    }
    protected void btn_tax_cancel_Click(object sender, EventArgs e)
    {
        clear_tax();
    }
    #endregion Cancel & clear

    #region grid works
    protected void grd_deed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void grd_deed_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_save_wardeed.Visible = false;
        btn_save_deedupdate.Visible = true;
        ID = grd_deed.SelectedRow.Cells[2].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        ordno = grd_deed.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_type.Text = grd_deed.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_grantor.Text = grd_deed.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_grantee.Text = grd_deed.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_dated.Text = grd_deed.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_recorded.Text = grd_deed.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_book.Text = grd_deed.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_pg.Text = grd_deed.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_deed_legal.Text = grd_deed.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_deed.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;


    }
    protected void grd_mortgage_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_mrg_save.Visible = false;
        btn_mrg_update.Visible = true;
        ID = grd_mortgage.SelectedRow.Cells[2].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        ordno = grd_mortgage.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_mortgager.Text = grd_mortgage.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_mortgagee.Text = grd_mortgage.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_dated.Text = grd_mortgage.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_recorded.Text = grd_mortgage.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_book.Text = grd_mortgage.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_pg.Text = grd_mortgage.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_amount.Text = grd_mortgage.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_mrg_opndate.Text = grd_mortgage.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_mortgage.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

    }
    protected void grd_mortgage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    protected void grd_tax_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_tax_save.Visible = false;
        btn_tax_update.Visible = true;
        ID = grd_tax.SelectedRow.Cells[2].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[2].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        ordno = grd_tax.SelectedRow.Cells[3].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[3].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_land.Text = grd_tax.SelectedRow.Cells[4].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[4].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_building.Text = grd_tax.SelectedRow.Cells[5].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[5].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_total.Text = grd_tax.SelectedRow.Cells[6].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[6].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_idno.Text = grd_tax.SelectedRow.Cells[7].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[7].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_2015_paid.Text = grd_tax.SelectedRow.Cells[8].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[8].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_2015_on.Text = grd_tax.SelectedRow.Cells[9].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[9].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_next_due.Text = grd_tax.SelectedRow.Cells[10].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[10].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_all_pre.Text = grd_tax.SelectedRow.Cells[11].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[11].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_home.Text = grd_tax.SelectedRow.Cells[12].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[12].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;
        txt_tax_water.Text = grd_tax.SelectedRow.Cells[13].Text != "&nbsp;" ? grd_tax.SelectedRow.Cells[13].Text.Replace("&#39;", "'").Replace("&amp;", "&").Replace("&quot;", "\"") : string.Empty;

    }
    protected void grd_tax_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        e.Row.Cells[2].Visible = false;
    }
    #endregion


    protected void btn_client_update_Click(object sender, EventArgs e)
    {
        int res = al.update_client(lbl_orderno.Text, txt_search_date.Text, txt_as_of_date.Text, txt_address.Text);
    }


    protected void btn_save_deedupdate_Click(object sender, EventArgs e)
    {
        int res = al.update_deed(ID, lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text);
        if (res > 0)
        {
            grid_deed_show();
        }
    }
    protected void btn_mrg_update_Click(object sender, EventArgs e)
    {
        int res = al.update_mortgage(ID, lbl_orderno.Text, txt_mrg_mortgager.Text, txt_mrg_mortgagee.Text, txt_mrg_dated.Text, txt_mrg_recorded.Text, txt_mrg_book.Text, txt_mrg_pg.Text, txt_mrg_amount.Text, txt_mrg_opndate.Text);
        if (res > 0)
        {
            grid_mortgage_show();
        }
    }
    protected void btn_tax_update_Click(object sender, EventArgs e)
    {
        int res = al.update_tax(ID, lbl_orderno.Text, txt_tax_land.Text, txt_tax_building.Text, txt_tax_total.Text, txt_tax_idno.Text, txt_tax_2015_paid.Text, txt_tax_2015_on.Text, txt_tax_next_due.Text, txt_tax_all_pre.Text, txt_tax_home.Text, txt_tax_water.Text);
        if (res > 0)
        {
            grid_tax_show();
        }
    }
    protected void btn_complete_Click(object sender, EventArgs e)
    {
        string comments = "";
        if (ValidateComments())
        {
            if (lbl_pros_name.Text == "KEYING" || lbl_pros_name.Text == "DU") comments = txt_keying_commend.Text;
            else if (lbl_pros_name.Text == "QC") comments = txt_qc_comments.Text;
            else if (lbl_pros_name.Text == "SEARCH") comments = txt_search_comments.Text;
            else if (lbl_pros_name.Text == "SEARCHQC") comments = txt_searchqc_comments.Text;

            int result = al.UpdateOrders(comments);

            SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
            SessionHandler.RedirectPage("~/Form/HomePage.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sampletable();
    }



    private void sampletable()
    {

        using (DocX document = DocX.Create(@"D:\\Test.docx"))
        {
            DataSet ds = new DataSet();
            ds = al.client(lbl_orderno.Text, txt_search_date.Text, txt_as_of_date.Text, txt_address.Text);


            Novacode.Table signOfftbl = document.AddTable(39, 9);
            signOfftbl.Alignment = Alignment.left;


            string headlineText = "";


            var headLineFormat = new Formatting();
            headLineFormat.FontFamily = new Novacode.Font("Arial Black");
            headLineFormat.Size = 800D;
            headLineFormat.Position = 1500;

            //Row 0
            string m1 = ds.Tables[0].Rows[0]["orderno"].ToString();
            signOfftbl.Rows[0].Cells[0].Paragraphs.First().Append("ORDER #AST-").FontSize(12).Bold().Font(new Novacode.Font("Times new roman")).Append(m1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[0].Width = 200;


            signOfftbl.Rows[0].Cells[1].Paragraphs.First().Alignment = Alignment.center;
            signOfftbl.Rows[0].Cells[1].Paragraphs.First().Append("2 OWNER").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[1].Width = 300;

            signOfftbl.Rows[0].Cells[2].Paragraphs.First().Append("$").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[2].Width = 100;

            signOfftbl.Rows[0].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[3].Width = 2;

            signOfftbl.Rows[0].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[4].Width = 2;

            signOfftbl.Rows[0].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[5].Width = 2;

            signOfftbl.Rows[0].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[6].Width = 2;

            signOfftbl.Rows[0].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[7].Width = 2;

            signOfftbl.Rows[0].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[8].Width = 2;



            signOfftbl.Rows[0].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 1           
            signOfftbl.Rows[1].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[0].Width = 600;

            signOfftbl.Rows[1].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[1].Width = 1.5;

            signOfftbl.Rows[1].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[2].Width = 1.5;

            signOfftbl.Rows[1].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[3].Width = 1.5;

            signOfftbl.Rows[1].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[4].Width = 1.5;

            signOfftbl.Rows[1].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[5].Width = 1.5;

            signOfftbl.Rows[1].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[6].Width = 1.5;

            signOfftbl.Rows[1].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[7].Width = 1.5;

            signOfftbl.Rows[1].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[8].Width = 1.5;


            signOfftbl.Rows[1].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 2
            string m2 = ds.Tables[0].Rows[0]["search_date"].ToString();
            string n1 = ds.Tables[0].Rows[0]["as_of_date"].ToString();
            signOfftbl.Rows[2].Cells[0].Paragraphs.First().Append("SEARCH DATE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[0].Width = 200;

            signOfftbl.Rows[2].Cells[1].Paragraphs.First().Append(m2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[1].Width = 110;

            signOfftbl.Rows[2].Cells[2].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[2].Cells[2].Paragraphs.First().Append("AS OF DATE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[2].Width = 170;

            signOfftbl.Rows[2].Cells[3].Paragraphs.First().Append(n1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[3].Width = 120;

            signOfftbl.Rows[2].Cells[4].Paragraphs.First().Append("").Bold();
            signOfftbl.Rows[2].Cells[4].Width = 4;

            signOfftbl.Rows[2].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[5].Width = 2;

            signOfftbl.Rows[2].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[6].Width = 2;

            signOfftbl.Rows[2].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[7].Width = 2;

            signOfftbl.Rows[2].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[8].Width = 2;



            signOfftbl.Rows[2].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 3  
            string m3 = ds.Tables[0].Rows[0]["address"].ToString();
            signOfftbl.Rows[3].Height = 50;
            signOfftbl.Rows[3].Cells[0].Paragraphs.First().Append("ADDRESS:").Font(new Novacode.Font("Times new roman")).Append(m3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[3].Cells[0].Width = 600;

            signOfftbl.Rows[3].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[1].Width = 1.5;

            signOfftbl.Rows[3].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[2].Width = 1.5;

            signOfftbl.Rows[3].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[3].Width = 1.5;

            signOfftbl.Rows[3].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[4].Width = 1.5;

            signOfftbl.Rows[3].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[5].Width = 1.5;

            signOfftbl.Rows[3].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[6].Width = 1.5;

            signOfftbl.Rows[3].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[7].Width = 1.5;

            signOfftbl.Rows[3].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[8].Width = 1.5;


            signOfftbl.Rows[3].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 4      

            DataSet ds3 = new DataSet();

            ds3 = al.selectdeed(lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text);


            signOfftbl.Rows[4].Cells[0].Paragraphs.First().Append("CURRENT DEED RECORD").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[4].Cells[0].Width = 600;

            signOfftbl.Rows[4].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[1].Width = 1.5;

            signOfftbl.Rows[4].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[2].Width = 1.5;

            signOfftbl.Rows[4].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[3].Width = 1.5;

            signOfftbl.Rows[4].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[4].Width = 1.5;

            signOfftbl.Rows[4].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[5].Width = 1.5;

            signOfftbl.Rows[4].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[6].Width = 1.5;

            signOfftbl.Rows[4].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[7].Width = 1.5;

            signOfftbl.Rows[4].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[8].Width = 1.5;


            signOfftbl.Rows[4].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 5           
            signOfftbl.Rows[5].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[0].Width = 600;

            signOfftbl.Rows[5].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[1].Width = 1.5;

            signOfftbl.Rows[5].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[2].Width = 1.5;

            signOfftbl.Rows[5].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[3].Width = 1.5;

            signOfftbl.Rows[5].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[4].Width = 1.5;

            signOfftbl.Rows[5].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[5].Width = 1.5;

            signOfftbl.Rows[5].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[6].Width = 1.5;

            signOfftbl.Rows[5].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[7].Width = 1.5;

            signOfftbl.Rows[5].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[8].Width = 1.5;


            signOfftbl.Rows[5].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 6        

            string k1 = ds3.Tables[0].Rows[0]["GRANTOR"].ToString();

            signOfftbl.Rows[6].Cells[0].Paragraphs.First().Append("GRANTOR:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[6].Cells[0].Width = 95;

            signOfftbl.Rows[6].Cells[1].Paragraphs.First().Append(k1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[6].Cells[1].Width = 505;

            signOfftbl.Rows[6].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[2].Width = 3;

            signOfftbl.Rows[6].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[3].Width = 1.5;

            signOfftbl.Rows[6].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[4].Width = 1.5;

            signOfftbl.Rows[6].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[5].Width = 1.5;

            signOfftbl.Rows[6].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[6].Width = 1.5;

            signOfftbl.Rows[6].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[7].Width = 1.5;

            signOfftbl.Rows[6].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[8].Width = 1.5;



            signOfftbl.Rows[6].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 7   

            string k2 = ds3.Tables[0].Rows[0]["GRANTEE"].ToString();

            signOfftbl.Rows[7].Cells[0].Paragraphs.First().Append("GRANTEE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[7].Cells[0].Width = 95;

            signOfftbl.Rows[7].Cells[1].Paragraphs.First().Append(k2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[7].Cells[1].Width = 505;

            signOfftbl.Rows[7].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[2].Width = 3;

            signOfftbl.Rows[7].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[3].Width = 1.5;

            signOfftbl.Rows[7].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[4].Width = 1.5;

            signOfftbl.Rows[7].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[5].Width = 1.5;

            signOfftbl.Rows[7].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[6].Width = 1.5;

            signOfftbl.Rows[7].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[7].Width = 1.5;

            signOfftbl.Rows[7].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[8].Width = 1.5;


            signOfftbl.Rows[7].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 8                
            string k3 = ds3.Tables[0].Rows[0]["DATED"].ToString();
            string k4 = ds3.Tables[0].Rows[0]["RECORDED"].ToString();
            string k5 = ds3.Tables[0].Rows[0]["BOOK"].ToString();
            string k6 = ds3.Tables[0].Rows[0]["PG"].ToString();
            signOfftbl.Rows[8].Cells[0].Paragraphs.First().Append("DATED:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[0].Width = 75;

            signOfftbl.Rows[8].Cells[1].Paragraphs.First().Append(k3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[1].Width = 85;

            signOfftbl.Rows[8].Cells[2].Paragraphs.First().Append("RECORDED");
            signOfftbl.Rows[8].Cells[2].Width = 85;

            signOfftbl.Rows[8].Cells[3].Paragraphs.First().Append(k4).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[3].Width = 85;

            signOfftbl.Rows[8].Cells[4].Paragraphs.First().Append("BOOK");
            signOfftbl.Rows[8].Cells[4].Width = 65;

            signOfftbl.Rows[8].Cells[5].Paragraphs.First().Append(k5).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[5].Width = 65;

            signOfftbl.Rows[8].Cells[6].Paragraphs.First().Append("PAGE");
            signOfftbl.Rows[8].Cells[6].Width = 65;

            signOfftbl.Rows[8].Cells[7].Paragraphs.First().Append(k6).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[7].Width = 75;

            signOfftbl.Rows[8].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[8].Cells[8].Width = 12;



            //Row 9                   
            signOfftbl.Rows[9].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[0].Width = 600;

            signOfftbl.Rows[9].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[1].Width = 1.5;

            signOfftbl.Rows[9].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[2].Width = 1.5;

            signOfftbl.Rows[9].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[3].Width = 1.5;

            signOfftbl.Rows[9].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[4].Width = 1.5;

            signOfftbl.Rows[9].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[5].Width = 1.5;

            signOfftbl.Rows[9].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[6].Width = 1.5;

            signOfftbl.Rows[9].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[7].Width = 1.5;

            signOfftbl.Rows[9].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[8].Width = 1.5;


            signOfftbl.Rows[9].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 10        
            string k7 = ds3.Tables[0].Rows[0]["LEGAL"].ToString();
            signOfftbl.Rows[10].Height = 50;
            signOfftbl.Rows[10].Cells[0].Paragraphs.First().Append("LEGAL:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[10].Cells[0].Width = 100;

            signOfftbl.Rows[10].Cells[1].Paragraphs.First().Append(k7).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[10].Cells[1].Width = 500;

            signOfftbl.Rows[10].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[2].Width = 3;

            signOfftbl.Rows[10].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[3].Width = 1.5;

            signOfftbl.Rows[10].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[4].Width = 1.5;

            signOfftbl.Rows[10].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[5].Width = 1.5;

            signOfftbl.Rows[10].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[6].Width = 1.5;

            signOfftbl.Rows[10].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[7].Width = 1.5;

            signOfftbl.Rows[10].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[8].Width = 1.5;



            signOfftbl.Rows[10].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 11
            signOfftbl.Rows[11].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[0].Width = 600;

            signOfftbl.Rows[11].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[1].Width = 1.5;

            signOfftbl.Rows[11].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[2].Width = 1.5;

            signOfftbl.Rows[11].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[3].Width = 1.5;

            signOfftbl.Rows[11].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[4].Width = 1.5;

            signOfftbl.Rows[11].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[5].Width = 1.5;

            signOfftbl.Rows[11].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[6].Width = 1.5;

            signOfftbl.Rows[11].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[7].Width = 1.5;

            signOfftbl.Rows[11].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[8].Width = 1.5;


            signOfftbl.Rows[11].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));







            ds3 = al.selectdeed(lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text);

            //ROW 12
            signOfftbl.Rows[12].Cells[0].Paragraphs.First().Append("PRIOR DEED RECORD:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[12].Cells[0].Width = 600;

            signOfftbl.Rows[12].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[1].Width = 1.5;

            signOfftbl.Rows[12].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[2].Width = 1.5;

            signOfftbl.Rows[12].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[3].Width = 1.5;

            signOfftbl.Rows[12].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[4].Width = 1.5;

            signOfftbl.Rows[12].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[5].Width = 1.5;

            signOfftbl.Rows[12].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[6].Width = 1.5;

            signOfftbl.Rows[12].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[7].Width = 1.5;

            signOfftbl.Rows[12].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[8].Width = 1.5;


            signOfftbl.Rows[12].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 13
            signOfftbl.Rows[13].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[0].Width = 600;

            signOfftbl.Rows[13].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[1].Width = 1.5;

            signOfftbl.Rows[13].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[2].Width = 1.5;

            signOfftbl.Rows[13].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[3].Width = 1.5;

            signOfftbl.Rows[13].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[4].Width = 1.5;

            signOfftbl.Rows[13].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[5].Width = 1.5;

            signOfftbl.Rows[13].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[6].Width = 1.5;

            signOfftbl.Rows[13].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[7].Width = 1.5;

            signOfftbl.Rows[13].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[13].Cells[8].Width = 1.5;


            signOfftbl.Rows[13].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[13].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //ROW 14
            string l1 = ds3.Tables[1].Rows[0]["GRANTOR"].ToString();

            signOfftbl.Rows[14].Cells[0].Paragraphs.First().Append("GRANTOR:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[14].Cells[0].Width = 110;

            signOfftbl.Rows[14].Cells[1].Paragraphs.First().Append(l1);
            signOfftbl.Rows[14].Cells[1].Width = 490;

            signOfftbl.Rows[14].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[2].Width = 1.5;

            signOfftbl.Rows[14].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[3].Width = 1.5;

            signOfftbl.Rows[14].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[4].Width = 1.5;

            signOfftbl.Rows[14].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[5].Width = 1.5;

            signOfftbl.Rows[14].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[6].Width = 1.5;

            signOfftbl.Rows[14].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[7].Width = 1.5;

            signOfftbl.Rows[14].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[14].Cells[8].Width = 3;


            signOfftbl.Rows[14].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[14].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[14].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[14].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[14].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[14].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 15

            string l2 = ds3.Tables[1].Rows[0]["GRANTEE"].ToString();

            signOfftbl.Rows[15].Height = 50;
            signOfftbl.Rows[15].Cells[0].Paragraphs.First().Append("GRANTEE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[15].Cells[0].Width = 110;

            signOfftbl.Rows[15].Cells[1].Paragraphs.First().Append(l2);
            signOfftbl.Rows[15].Cells[1].Width = 490;

            signOfftbl.Rows[15].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[2].Width = 1.5;

            signOfftbl.Rows[15].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[3].Width = 1.5;

            signOfftbl.Rows[15].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[4].Width = 1.5;

            signOfftbl.Rows[15].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[5].Width = 1.5;

            signOfftbl.Rows[15].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[6].Width = 1.5;

            signOfftbl.Rows[15].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[7].Width = 1.5;

            signOfftbl.Rows[15].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[15].Cells[8].Width = 3;


            signOfftbl.Rows[15].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[15].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[15].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[15].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[15].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[15].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 16     
            string l3 = ds3.Tables[1].Rows[0]["DATED"].ToString();
            string l4 = ds3.Tables[1].Rows[0]["RECORDED"].ToString();
            string l5 = ds3.Tables[1].Rows[0]["BOOK"].ToString();
            string l6 = ds3.Tables[1].Rows[0]["PG"].ToString();


            signOfftbl.Rows[16].Cells[0].Paragraphs.First().Append("DATED:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[16].Cells[0].Width = 75;

            signOfftbl.Rows[16].Cells[1].Paragraphs.First().Append(l3);
            signOfftbl.Rows[16].Cells[1].Width = 95;

            signOfftbl.Rows[16].Cells[2].Paragraphs.First().Append("RECORDED");
            signOfftbl.Rows[16].Cells[2].Width = 85;

            signOfftbl.Rows[16].Cells[3].Paragraphs.First().Append(l4);
            signOfftbl.Rows[16].Cells[3].Width = 95;

            signOfftbl.Rows[16].Cells[4].Paragraphs.First().Append("BOOK");
            signOfftbl.Rows[16].Cells[4].Width = 65;

            signOfftbl.Rows[16].Cells[5].Paragraphs.First().Append(l5);
            signOfftbl.Rows[16].Cells[5].Width = 65;

            signOfftbl.Rows[16].Cells[6].Paragraphs.First().Append("PAGE");
            signOfftbl.Rows[16].Cells[6].Width = 65;

            signOfftbl.Rows[16].Cells[7].Paragraphs.First().Append(l6);
            signOfftbl.Rows[16].Cells[7].Width = 55;

            signOfftbl.Rows[16].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[16].Cells[8].Width = 12;



            //Row 17          
            signOfftbl.Rows[17].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[0].Width = 600;

            signOfftbl.Rows[17].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[1].Width = 1.5;

            signOfftbl.Rows[17].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[2].Width = 1.5;

            signOfftbl.Rows[17].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[3].Width = 1.5;

            signOfftbl.Rows[17].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[4].Width = 1.5;

            signOfftbl.Rows[17].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[5].Width = 1.5;

            signOfftbl.Rows[17].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[6].Width = 1.5;

            signOfftbl.Rows[17].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[7].Width = 1.5;

            signOfftbl.Rows[17].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[17].Cells[8].Width = 1.5;


            signOfftbl.Rows[17].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[17].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));







            //Row 18                        
            signOfftbl.Rows[18].Cells[0].Paragraphs.First().Append("MORTGAGE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[18].Cells[0].Width = 600;

            signOfftbl.Rows[18].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[1].Width = 1.5;

            signOfftbl.Rows[18].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[2].Width = 1.5;

            signOfftbl.Rows[18].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[3].Width = 1.5;

            signOfftbl.Rows[18].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[4].Width = 1.5;

            signOfftbl.Rows[18].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[5].Width = 1.5;

            signOfftbl.Rows[18].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[6].Width = 1.5;

            signOfftbl.Rows[18].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[7].Width = 1.5;

            signOfftbl.Rows[18].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[8].Width = 1.5;


            signOfftbl.Rows[18].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 19          
            signOfftbl.Rows[19].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[0].Width = 600;

            signOfftbl.Rows[19].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[1].Width = 1.5;

            signOfftbl.Rows[19].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[2].Width = 1.5;

            signOfftbl.Rows[19].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[3].Width = 1.5;

            signOfftbl.Rows[19].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[4].Width = 1.5;

            signOfftbl.Rows[19].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[5].Width = 1.5;

            signOfftbl.Rows[19].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[6].Width = 1.5;

            signOfftbl.Rows[19].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[7].Width = 1.5;

            signOfftbl.Rows[19].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[8].Width = 1.5;


            signOfftbl.Rows[19].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 20   

            DataSet ds1 = new DataSet();
            ds1 = al.selectmortgage(lbl_orderno.Text, txt_mrg_mortgager.Text, txt_mrg_mortgagee.Text, txt_mrg_dated.Text, txt_mrg_recorded.Text, txt_mrg_book.Text, txt_mrg_pg.Text, txt_mrg_amount.Text, txt_mrg_opndate.Text);

            string a1 = ds1.Tables[0].Rows[0]["mortgagor"].ToString();


            signOfftbl.Rows[20].Cells[0].Paragraphs.First().Append("1.").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[0].Width = 40;

            signOfftbl.Rows[20].Cells[1].Paragraphs.First().Append("MORTGAGOR:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[1].Width = 130;

            signOfftbl.Rows[20].Cells[2].Paragraphs.First().Append("").Append(a1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[2].Width = 430;

            signOfftbl.Rows[20].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[3].Width = 3;

            signOfftbl.Rows[20].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[4].Width = 1.5;

            signOfftbl.Rows[20].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[5].Width = 1.5;

            signOfftbl.Rows[20].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[6].Width = 1.5;

            signOfftbl.Rows[20].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[7].Width = 1.5;

            signOfftbl.Rows[20].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[8].Width = 3;



            signOfftbl.Rows[20].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 21 

            string a2 = ds1.Tables[0].Rows[0]["mortgagee"].ToString();

            signOfftbl.Rows[21].Height = 50;
            signOfftbl.Rows[21].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[0].Width = 40;

            signOfftbl.Rows[21].Cells[1].Paragraphs.First().Append("MORTGAGEE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[21].Cells[1].Width = 130;

            signOfftbl.Rows[21].Cells[2].Paragraphs.First().Append(a2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[21].Cells[2].Width = 430;

            signOfftbl.Rows[21].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[3].Width = 3;

            signOfftbl.Rows[21].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[4].Width = 1.5;

            signOfftbl.Rows[21].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[5].Width = 1.5;

            signOfftbl.Rows[21].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[6].Width = 1.5;

            signOfftbl.Rows[21].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[7].Width = 1.5;

            signOfftbl.Rows[21].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[8].Width = 3;



            signOfftbl.Rows[21].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 22  
            string a3 = ds1.Tables[0].Rows[0]["dated"].ToString();
            string a4 = ds1.Tables[0].Rows[0]["recorded"].ToString();
            string a5 = ds1.Tables[0].Rows[0]["book"].ToString();
            string a6 = ds1.Tables[0].Rows[0]["pg"].ToString();
            string a7 = ds1.Tables[0].Rows[0]["amount"].ToString();

            signOfftbl.Rows[22].Cells[0].Paragraphs.First().Append("DATED:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[0].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[0].Width = 75;

            signOfftbl.Rows[22].Cells[1].Paragraphs.First().Append("").Append(a3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[1].Width = 85;


            signOfftbl.Rows[22].Cells[2].Paragraphs.First().Append("RECORDED:");
            signOfftbl.Rows[22].Cells[2].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[2].Width = 85;

            signOfftbl.Rows[22].Cells[3].Paragraphs.First().Append("").Append(a4).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[3].Width = 85;


            signOfftbl.Rows[22].Cells[4].Paragraphs.First().Append("BOOK:");
            signOfftbl.Rows[22].Cells[4].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[4].Width = 75;

            signOfftbl.Rows[22].Cells[5].Paragraphs.First().Append("").Append(a5).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[5].Width = 55;


            signOfftbl.Rows[22].Cells[6].Paragraphs.First().Append("PAGE:");
            signOfftbl.Rows[22].Cells[6].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[6].Width = 75;

            signOfftbl.Rows[22].Cells[7].Paragraphs.First().Append("").Append(a6).Bold().Font(new Novacode.Font("Times new roman")); ;
            signOfftbl.Rows[22].Cells[7].Width = 65;

            signOfftbl.Rows[22].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[22].Cells[8].Width = 12;



            //ROW 23            
            signOfftbl.Rows[23].Cells[0].Paragraphs.First().Append("AMOUNT:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[0].Width = 110;

            signOfftbl.Rows[23].Cells[1].Paragraphs.First().Append("$").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[1].Width = 25;

            signOfftbl.Rows[23].Cells[2].Paragraphs.First().Append(a7).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[2].Width = 465;

            signOfftbl.Rows[23].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[3].Width = 3;

            signOfftbl.Rows[23].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[4].Width = 1.5;

            signOfftbl.Rows[23].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[5].Width = 1.5;

            signOfftbl.Rows[23].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[6].Width = 1.5;

            signOfftbl.Rows[23].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[7].Width = 1.5;

            signOfftbl.Rows[23].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[8].Width = 3;


            signOfftbl.Rows[23].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //ROW 24           
            signOfftbl.Rows[24].Cells[0].Paragraphs.First().Append("OPEN END MORTGAGE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[24].Cells[0].Width = 220;

            signOfftbl.Rows[24].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[1].Width = 100;

            signOfftbl.Rows[24].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[2].Width = 90;

            signOfftbl.Rows[24].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[3].Width = 190;

            signOfftbl.Rows[24].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[4].Width = 4.5;

            signOfftbl.Rows[24].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[5].Width = 1.5;

            signOfftbl.Rows[24].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[6].Width = 1.5;

            signOfftbl.Rows[24].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[7].Width = 1.5;

            signOfftbl.Rows[24].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[8].Width = 3;


            signOfftbl.Rows[24].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 25          
            signOfftbl.Rows[25].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[0].Width = 600;

            signOfftbl.Rows[25].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[1].Width = 1.5;

            signOfftbl.Rows[25].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[2].Width = 1.5;

            signOfftbl.Rows[25].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[3].Width = 1.5;

            signOfftbl.Rows[25].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[4].Width = 1.5;

            signOfftbl.Rows[25].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[5].Width = 1.5;

            signOfftbl.Rows[25].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[6].Width = 1.5;

            signOfftbl.Rows[25].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[7].Width = 1.5;

            signOfftbl.Rows[25].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[8].Width = 1.5;


            signOfftbl.Rows[25].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 26          
            signOfftbl.Rows[26].Cells[0].Paragraphs.First().Append("JUDGEMENTS OR LIENS:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[26].Cells[0].Width = 600;

            signOfftbl.Rows[26].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[1].Width = 1.5;

            signOfftbl.Rows[26].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[2].Width = 1.5;

            signOfftbl.Rows[26].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[3].Width = 1.5;

            signOfftbl.Rows[26].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[4].Width = 1.5;

            signOfftbl.Rows[26].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[5].Width = 1.5;

            signOfftbl.Rows[26].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[6].Width = 1.5;

            signOfftbl.Rows[26].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[7].Width = 1.5;

            signOfftbl.Rows[26].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[8].Width = 1.5;


            signOfftbl.Rows[26].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 27          
            signOfftbl.Rows[27].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[0].Width = 600;

            signOfftbl.Rows[27].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[1].Width = 1.5;

            signOfftbl.Rows[27].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[2].Width = 1.5;

            signOfftbl.Rows[27].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[3].Width = 1.5;

            signOfftbl.Rows[27].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[4].Width = 1.5;

            signOfftbl.Rows[27].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[5].Width = 1.5;

            signOfftbl.Rows[27].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[6].Width = 1.5;

            signOfftbl.Rows[27].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[7].Width = 1.5;

            signOfftbl.Rows[27].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[8].Width = 1.5;


            signOfftbl.Rows[27].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 28          
            signOfftbl.Rows[28].Cells[0].Paragraphs.First().Append("TAX INFORMATION:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[28].Cells[0].Width = 600;

            signOfftbl.Rows[28].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[1].Width = 1.5;

            signOfftbl.Rows[28].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[2].Width = 1.5;

            signOfftbl.Rows[28].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[3].Width = 1.5;

            signOfftbl.Rows[28].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[4].Width = 1.5;

            signOfftbl.Rows[28].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[5].Width = 1.5;

            signOfftbl.Rows[28].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[6].Width = 1.5;

            signOfftbl.Rows[28].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[7].Width = 1.5;

            signOfftbl.Rows[28].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[8].Width = 1.5;


            signOfftbl.Rows[28].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 29          
            signOfftbl.Rows[29].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[0].Width = 600;

            signOfftbl.Rows[29].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[1].Width = 1.5;

            signOfftbl.Rows[29].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[2].Width = 1.5;

            signOfftbl.Rows[29].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[3].Width = 1.5;

            signOfftbl.Rows[29].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[4].Width = 1.5;

            signOfftbl.Rows[29].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[5].Width = 1.5;

            signOfftbl.Rows[29].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[6].Width = 1.5;

            signOfftbl.Rows[29].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[7].Width = 1.5;

            signOfftbl.Rows[29].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[8].Width = 1.5;


            signOfftbl.Rows[29].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 30           
            signOfftbl.Rows[30].Cells[0].Paragraphs.First().Append("TAX ASSESSMENT:");
            signOfftbl.Rows[30].Cells[0].Width = 220;

            signOfftbl.Rows[30].Cells[1].Paragraphs.First().Append("LAND");
            signOfftbl.Rows[30].Cells[1].Width = 100;

            signOfftbl.Rows[30].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[30].Cells[2].Width = 40;

            signOfftbl.Rows[30].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[3].Width = 240;

            signOfftbl.Rows[30].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[4].Width = 4.5;

            signOfftbl.Rows[30].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[5].Width = 1.5;

            signOfftbl.Rows[30].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[6].Width = 1.5;

            signOfftbl.Rows[30].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[7].Width = 1.5;

            signOfftbl.Rows[30].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[8].Width = 3;


            signOfftbl.Rows[30].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 31           
            signOfftbl.Rows[31].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[0].Width = 220;

            signOfftbl.Rows[31].Cells[1].Paragraphs.First().Append("BUILDING");
            signOfftbl.Rows[31].Cells[1].Width = 100;

            signOfftbl.Rows[31].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[31].Cells[2].Width = 40;

            signOfftbl.Rows[31].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[3].Width = 240;

            signOfftbl.Rows[31].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[4].Width = 4.5;

            signOfftbl.Rows[31].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[5].Width = 1.5;

            signOfftbl.Rows[31].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[6].Width = 1.5;

            signOfftbl.Rows[31].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[7].Width = 1.5;

            signOfftbl.Rows[31].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[8].Width = 3;


            signOfftbl.Rows[31].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 32          
            signOfftbl.Rows[32].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[0].Width = 220;

            signOfftbl.Rows[32].Cells[1].Paragraphs.First().Append("OTHERS");
            signOfftbl.Rows[32].Cells[1].Width = 100;

            signOfftbl.Rows[32].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[32].Cells[2].Width = 40;

            signOfftbl.Rows[32].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[3].Width = 240;

            signOfftbl.Rows[32].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[4].Width = 4.5;

            signOfftbl.Rows[32].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[5].Width = 1.5;

            signOfftbl.Rows[32].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[6].Width = 1.5;

            signOfftbl.Rows[32].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[7].Width = 1.5;

            signOfftbl.Rows[32].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[8].Width = 3;


            signOfftbl.Rows[32].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 33           
            signOfftbl.Rows[33].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[0].Width = 220;

            signOfftbl.Rows[33].Cells[1].Paragraphs.First().Append("TOTAL");
            signOfftbl.Rows[33].Cells[1].Width = 100;

            signOfftbl.Rows[33].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[33].Cells[2].Width = 40;

            signOfftbl.Rows[33].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[3].Width = 240;

            signOfftbl.Rows[33].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[4].Width = 4.5;

            signOfftbl.Rows[33].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[5].Width = 1.5;

            signOfftbl.Rows[33].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[6].Width = 1.5;

            signOfftbl.Rows[33].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[7].Width = 1.5;

            signOfftbl.Rows[33].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[8].Width = 3;


            signOfftbl.Rows[33].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 34         
            signOfftbl.Rows[34].Cells[0].Paragraphs.First().Append("TAX ID NUMBER:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[34].Cells[0].Width = 220;

            signOfftbl.Rows[34].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[1].Width = 240;

            signOfftbl.Rows[34].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[2].Width = 140;

            signOfftbl.Rows[34].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[3].Width = 4.5;

            signOfftbl.Rows[34].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[4].Width = 1.5;

            signOfftbl.Rows[34].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[5].Width = 1.5;

            signOfftbl.Rows[34].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[6].Width = 1.5;

            signOfftbl.Rows[34].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[7].Width = 1.5;

            signOfftbl.Rows[34].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[8].Width = 1.5;



            signOfftbl.Rows[34].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 35          
            signOfftbl.Rows[35].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[0].Width = 600;

            signOfftbl.Rows[35].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[1].Width = 1.5;

            signOfftbl.Rows[35].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[2].Width = 1.5;

            signOfftbl.Rows[35].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[3].Width = 1.5;

            signOfftbl.Rows[35].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[4].Width = 1.5;

            signOfftbl.Rows[35].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[5].Width = 1.5;

            signOfftbl.Rows[35].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[6].Width = 1.5;

            signOfftbl.Rows[35].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[7].Width = 1.5;

            signOfftbl.Rows[35].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[8].Width = 1.5;


            signOfftbl.Rows[35].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 36          
            signOfftbl.Rows[36].Cells[0].Paragraphs.First().Append("2015 TAXES PAID IN THE AMOUNT OF:");
            signOfftbl.Rows[36].Cells[0].Width = 320;

            signOfftbl.Rows[36].Cells[1].Paragraphs.First().Append("$");
            signOfftbl.Rows[36].Cells[1].Width = 40;

            signOfftbl.Rows[36].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[2].Width = 60;

            signOfftbl.Rows[36].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[3].Width = 70;

            signOfftbl.Rows[36].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[4].Width = 110;

            signOfftbl.Rows[36].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[5].Width = 8;

            signOfftbl.Rows[36].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[6].Width = 1.5;

            signOfftbl.Rows[36].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[7].Width = 1.5;

            signOfftbl.Rows[36].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[8].Width = 1.5;


            signOfftbl.Rows[36].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[36].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[36].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 37          
            signOfftbl.Rows[37].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[0].Width = 600;

            signOfftbl.Rows[37].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[1].Width = 1.5;

            signOfftbl.Rows[37].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[2].Width = 1.5;

            signOfftbl.Rows[37].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[3].Width = 1.5;

            signOfftbl.Rows[37].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[4].Width = 1.5;

            signOfftbl.Rows[37].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[5].Width = 1.5;

            signOfftbl.Rows[37].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[6].Width = 1.5;

            signOfftbl.Rows[37].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[7].Width = 1.5;

            signOfftbl.Rows[37].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[8].Width = 1.5;


            signOfftbl.Rows[37].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            document.InsertParagraph(headlineText, false, headLineFormat);
            document.AddFooters();

            Footer footer_first = document.Footers.first;

            document.InsertTable(signOfftbl);
            document.Save();
        }
    }





    protected void btnmultiple_Click(object sender, EventArgs e)
    {

        using (DocX document = DocX.Create(@"D:\\Test.docx"))
        {
            DataSet ds = new DataSet();
            ds = al.client(lbl_orderno.Text, txt_search_date.Text, txt_as_of_date.Text, txt_address.Text);

            DataSet ds3 = new DataSet();
            ds3 = al.selectdeed(lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text);



            Novacode.Table signOfftbl = document.AddTable(39, 9);
            signOfftbl.Alignment = Alignment.left;


            string headlineText = "";


            var headLineFormat = new Formatting();
            headLineFormat.FontFamily = new Novacode.Font("Arial Black");
            headLineFormat.Size = 800D;
            headLineFormat.Position = 1500;

            //Row 0
            string m1 = ds.Tables[0].Rows[0]["orderno"].ToString();
            signOfftbl.Rows[0].Cells[0].Paragraphs.First().Append("ORDER #AST-").FontSize(12).Bold().Font(new Novacode.Font("Times new roman")).Append(m1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[0].Width = 200;


            signOfftbl.Rows[0].Cells[1].Paragraphs.First().Alignment = Alignment.center;
            signOfftbl.Rows[0].Cells[1].Paragraphs.First().Append("2 OWNER").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[1].Width = 300;

            signOfftbl.Rows[0].Cells[2].Paragraphs.First().Append("$").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[0].Cells[2].Width = 100;

            signOfftbl.Rows[0].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[3].Width = 2;

            signOfftbl.Rows[0].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[4].Width = 2;

            signOfftbl.Rows[0].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[5].Width = 2;

            signOfftbl.Rows[0].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[6].Width = 2;

            signOfftbl.Rows[0].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[7].Width = 2;

            signOfftbl.Rows[0].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[0].Cells[8].Width = 2;



            signOfftbl.Rows[0].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[0].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 1           
            signOfftbl.Rows[1].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[0].Width = 600;

            signOfftbl.Rows[1].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[1].Width = 1.5;

            signOfftbl.Rows[1].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[2].Width = 1.5;

            signOfftbl.Rows[1].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[3].Width = 1.5;

            signOfftbl.Rows[1].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[4].Width = 1.5;

            signOfftbl.Rows[1].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[5].Width = 1.5;

            signOfftbl.Rows[1].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[6].Width = 1.5;

            signOfftbl.Rows[1].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[7].Width = 1.5;

            signOfftbl.Rows[1].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[1].Cells[8].Width = 1.5;


            signOfftbl.Rows[1].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[1].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 2
            string m2 = ds.Tables[0].Rows[0]["search_date"].ToString();
            string n1 = ds.Tables[0].Rows[0]["as_of_date"].ToString();
            signOfftbl.Rows[2].Cells[0].Paragraphs.First().Append("SEARCH DATE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[0].Width = 200;

            signOfftbl.Rows[2].Cells[1].Paragraphs.First().Append(m2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[1].Width = 110;

            signOfftbl.Rows[2].Cells[2].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[2].Cells[2].Paragraphs.First().Append("AS OF DATE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[2].Width = 170;

            signOfftbl.Rows[2].Cells[3].Paragraphs.First().Append(n1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[2].Cells[3].Width = 120;

            signOfftbl.Rows[2].Cells[4].Paragraphs.First().Append("").Bold();
            signOfftbl.Rows[2].Cells[4].Width = 4;

            signOfftbl.Rows[2].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[5].Width = 2;

            signOfftbl.Rows[2].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[6].Width = 2;

            signOfftbl.Rows[2].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[7].Width = 2;

            signOfftbl.Rows[2].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[2].Cells[8].Width = 2;



            signOfftbl.Rows[2].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[2].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 3  
            string m3 = ds.Tables[0].Rows[0]["address"].ToString();
            signOfftbl.Rows[3].Height = 50;
            signOfftbl.Rows[3].Cells[0].Paragraphs.First().Append("ADDRESS:").Font(new Novacode.Font("Times new roman")).Append(m3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[3].Cells[0].Width = 600;

            signOfftbl.Rows[3].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[1].Width = 1.5;

            signOfftbl.Rows[3].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[2].Width = 1.5;

            signOfftbl.Rows[3].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[3].Width = 1.5;

            signOfftbl.Rows[3].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[4].Width = 1.5;

            signOfftbl.Rows[3].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[5].Width = 1.5;

            signOfftbl.Rows[3].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[6].Width = 1.5;

            signOfftbl.Rows[3].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[7].Width = 1.5;

            signOfftbl.Rows[3].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[3].Cells[8].Width = 1.5;


            signOfftbl.Rows[3].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[3].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 4                       
            signOfftbl.Rows[4].Cells[0].Paragraphs.First().Append("CURRENT DEED RECORD").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[4].Cells[0].Width = 600;

            signOfftbl.Rows[4].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[1].Width = 1.5;

            signOfftbl.Rows[4].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[2].Width = 1.5;

            signOfftbl.Rows[4].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[3].Width = 1.5;

            signOfftbl.Rows[4].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[4].Width = 1.5;

            signOfftbl.Rows[4].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[5].Width = 1.5;

            signOfftbl.Rows[4].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[6].Width = 1.5;

            signOfftbl.Rows[4].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[7].Width = 1.5;

            signOfftbl.Rows[4].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[4].Cells[8].Width = 1.5;


            signOfftbl.Rows[4].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[4].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 5           
            signOfftbl.Rows[5].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[0].Width = 600;

            signOfftbl.Rows[5].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[1].Width = 1.5;

            signOfftbl.Rows[5].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[2].Width = 1.5;

            signOfftbl.Rows[5].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[3].Width = 1.5;

            signOfftbl.Rows[5].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[4].Width = 1.5;

            signOfftbl.Rows[5].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[5].Width = 1.5;

            signOfftbl.Rows[5].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[6].Width = 1.5;

            signOfftbl.Rows[5].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[7].Width = 1.5;

            signOfftbl.Rows[5].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[5].Cells[8].Width = 1.5;


            signOfftbl.Rows[5].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[5].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 6        

            string k1 = ds3.Tables[0].Rows[0]["GRANTOR"].ToString();

            signOfftbl.Rows[6].Cells[0].Paragraphs.First().Append("GRANTOR:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[6].Cells[0].Width = 95;

            signOfftbl.Rows[6].Cells[1].Paragraphs.First().Append(k1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[6].Cells[1].Width = 505;

            signOfftbl.Rows[6].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[2].Width = 3;

            signOfftbl.Rows[6].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[3].Width = 1.5;

            signOfftbl.Rows[6].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[4].Width = 1.5;

            signOfftbl.Rows[6].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[5].Width = 1.5;

            signOfftbl.Rows[6].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[6].Width = 1.5;

            signOfftbl.Rows[6].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[7].Width = 1.5;

            signOfftbl.Rows[6].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[6].Cells[8].Width = 1.5;



            signOfftbl.Rows[6].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[6].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 7   

            string k2 = ds3.Tables[0].Rows[0]["GRANTEE"].ToString();

            signOfftbl.Rows[7].Cells[0].Paragraphs.First().Append("GRANTEE:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[7].Cells[0].Width = 95;

            signOfftbl.Rows[7].Cells[1].Paragraphs.First().Append(k2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[7].Cells[1].Width = 505;

            signOfftbl.Rows[7].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[2].Width = 3;

            signOfftbl.Rows[7].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[3].Width = 1.5;

            signOfftbl.Rows[7].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[4].Width = 1.5;

            signOfftbl.Rows[7].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[5].Width = 1.5;

            signOfftbl.Rows[7].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[6].Width = 1.5;

            signOfftbl.Rows[7].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[7].Width = 1.5;

            signOfftbl.Rows[7].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[7].Cells[8].Width = 1.5;


            signOfftbl.Rows[7].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[7].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 8                
            string k3 = ds3.Tables[0].Rows[0]["DATED"].ToString();
            string k4 = ds3.Tables[0].Rows[0]["RECORDED"].ToString();
            string k5 = ds3.Tables[0].Rows[0]["BOOK"].ToString();
            string k6 = ds3.Tables[0].Rows[0]["PG"].ToString();
            signOfftbl.Rows[8].Cells[0].Paragraphs.First().Append("DATED:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[0].Width = 75;

            signOfftbl.Rows[8].Cells[1].Paragraphs.First().Append(k3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[1].Width = 85;

            signOfftbl.Rows[8].Cells[2].Paragraphs.First().Append("RECORDED");
            signOfftbl.Rows[8].Cells[2].Width = 85;

            signOfftbl.Rows[8].Cells[3].Paragraphs.First().Append(k4).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[3].Width = 85;

            signOfftbl.Rows[8].Cells[4].Paragraphs.First().Append("BOOK");
            signOfftbl.Rows[8].Cells[4].Width = 65;

            signOfftbl.Rows[8].Cells[5].Paragraphs.First().Append(k5).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[5].Width = 65;

            signOfftbl.Rows[8].Cells[6].Paragraphs.First().Append("PAGE");
            signOfftbl.Rows[8].Cells[6].Width = 65;

            signOfftbl.Rows[8].Cells[7].Paragraphs.First().Append(k6).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[8].Cells[7].Width = 75;

            signOfftbl.Rows[8].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[8].Cells[8].Width = 12;



            //Row 9                   
            signOfftbl.Rows[9].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[0].Width = 600;

            signOfftbl.Rows[9].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[1].Width = 1.5;

            signOfftbl.Rows[9].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[2].Width = 1.5;

            signOfftbl.Rows[9].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[3].Width = 1.5;

            signOfftbl.Rows[9].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[4].Width = 1.5;

            signOfftbl.Rows[9].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[5].Width = 1.5;

            signOfftbl.Rows[9].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[6].Width = 1.5;

            signOfftbl.Rows[9].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[7].Width = 1.5;

            signOfftbl.Rows[9].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[9].Cells[8].Width = 1.5;


            signOfftbl.Rows[9].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[9].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 10        
            string k7 = ds3.Tables[0].Rows[0]["LEGAL"].ToString();
            signOfftbl.Rows[10].Height = 50;
            signOfftbl.Rows[10].Cells[0].Paragraphs.First().Append("LEGAL:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[10].Cells[0].Width = 100;

            signOfftbl.Rows[10].Cells[1].Paragraphs.First().Append(k7).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[10].Cells[1].Width = 500;

            signOfftbl.Rows[10].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[2].Width = 3;

            signOfftbl.Rows[10].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[3].Width = 1.5;

            signOfftbl.Rows[10].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[4].Width = 1.5;

            signOfftbl.Rows[10].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[5].Width = 1.5;

            signOfftbl.Rows[10].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[6].Width = 1.5;

            signOfftbl.Rows[10].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[7].Width = 1.5;

            signOfftbl.Rows[10].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[10].Cells[8].Width = 1.5;



            signOfftbl.Rows[10].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[10].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 11
            signOfftbl.Rows[11].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[0].Width = 600;

            signOfftbl.Rows[11].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[1].Width = 1.5;

            signOfftbl.Rows[11].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[2].Width = 1.5;

            signOfftbl.Rows[11].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[3].Width = 1.5;

            signOfftbl.Rows[11].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[4].Width = 1.5;

            signOfftbl.Rows[11].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[5].Width = 1.5;

            signOfftbl.Rows[11].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[6].Width = 1.5;

            signOfftbl.Rows[11].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[7].Width = 1.5;

            signOfftbl.Rows[11].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[11].Cells[8].Width = 1.5;


            signOfftbl.Rows[11].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[11].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));









            //ROW 12          
            signOfftbl.Rows[12].Cells[0].Paragraphs.First().Append("PRIOR DEED RECORD:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[12].Cells[0].Width = 600;

            signOfftbl.Rows[12].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[1].Width = 1.5;

            signOfftbl.Rows[12].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[2].Width = 1.5;

            signOfftbl.Rows[12].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[3].Width = 1.5;

            signOfftbl.Rows[12].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[4].Width = 1.5;

            signOfftbl.Rows[12].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[5].Width = 1.5;

            signOfftbl.Rows[12].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[6].Width = 1.5;

            signOfftbl.Rows[12].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[7].Width = 1.5;

            signOfftbl.Rows[12].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[12].Cells[8].Width = 1.5;


            signOfftbl.Rows[12].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[12].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 13
            for (int i = 0; i < ds3.Tables[1].Rows.Count; i++)
            {
                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[0].Width = 600;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[1].Width = 1.5;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[2].Width = 1.5;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[3].Width = 1.5;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[4].Width = 1.5;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[5].Width = 1.5;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[6].Width = 1.5;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[7].Width = 1.5;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 1.5;


                signOfftbl.Rows[i].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





                //ROW 14                     

                string l1 = ds3.Tables[1].Rows[i]["GRANTOR"].ToString();

                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append("GRANTOR:").Bold().Font(new Novacode.Font("Times new roman"));
                signOfftbl.Rows[i].Cells[0].Width = 110;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append(l1);
                signOfftbl.Rows[i].Cells[1].Width = 490;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[2].Width = 1.5;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[3].Width = 1.5;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[4].Width = 1.5;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[5].Width = 1.5;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[6].Width = 1.5;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[7].Width = 1.5;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 3;


                signOfftbl.Rows[i].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



                //ROW 15

                string l2 = ds3.Tables[1].Rows[i]["GRANTEE"].ToString();

                signOfftbl.Rows[i].Height = 50;
                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append("GRANTEE:").Bold().Font(new Novacode.Font("Times new roman"));
                signOfftbl.Rows[i].Cells[0].Width = 110;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append(l2);
                signOfftbl.Rows[i].Cells[1].Width = 490;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[2].Width = 1.5;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[3].Width = 1.5;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[4].Width = 1.5;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[5].Width = 1.5;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[6].Width = 1.5;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[7].Width = 1.5;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 3;


                signOfftbl.Rows[i].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




                //Row 16     
                string l3 = ds3.Tables[1].Rows[i]["DATED"].ToString();
                string l4 = ds3.Tables[1].Rows[i]["RECORDED"].ToString();
                string l5 = ds3.Tables[1].Rows[i]["BOOK"].ToString();
                string l6 = ds3.Tables[1].Rows[i]["PG"].ToString();


                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append("DATED:").Bold().Font(new Novacode.Font("Times new roman"));
                signOfftbl.Rows[i].Cells[0].Width = 75;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append(l3);
                signOfftbl.Rows[i].Cells[1].Width = 95;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("RECORDED");
                signOfftbl.Rows[i].Cells[2].Width = 85;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append(l4);
                signOfftbl.Rows[i].Cells[3].Width = 95;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("BOOK");
                signOfftbl.Rows[i].Cells[4].Width = 65;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append(l5);
                signOfftbl.Rows[i].Cells[5].Width = 65;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("PAGE");
                signOfftbl.Rows[i].Cells[6].Width = 65;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append(l6);
                signOfftbl.Rows[i].Cells[7].Width = 55;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 12;



                //Row 17          
                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[0].Width = 600;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[1].Width = 1.5;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[2].Width = 1.5;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[3].Width = 1.5;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[4].Width = 1.5;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[5].Width = 1.5;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[6].Width = 1.5;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[7].Width = 1.5;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 1.5;


                signOfftbl.Rows[i].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            }








            //Row 18                        
            signOfftbl.Rows[18].Cells[0].Paragraphs.First().Append("MORTGAGE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[18].Cells[0].Width = 600;

            signOfftbl.Rows[18].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[1].Width = 1.5;

            signOfftbl.Rows[18].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[2].Width = 1.5;

            signOfftbl.Rows[18].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[3].Width = 1.5;

            signOfftbl.Rows[18].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[4].Width = 1.5;

            signOfftbl.Rows[18].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[5].Width = 1.5;

            signOfftbl.Rows[18].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[6].Width = 1.5;

            signOfftbl.Rows[18].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[7].Width = 1.5;

            signOfftbl.Rows[18].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[18].Cells[8].Width = 1.5;


            signOfftbl.Rows[18].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[18].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 19          
            signOfftbl.Rows[19].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[0].Width = 600;

            signOfftbl.Rows[19].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[1].Width = 1.5;

            signOfftbl.Rows[19].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[2].Width = 1.5;

            signOfftbl.Rows[19].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[3].Width = 1.5;

            signOfftbl.Rows[19].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[4].Width = 1.5;

            signOfftbl.Rows[19].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[5].Width = 1.5;

            signOfftbl.Rows[19].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[6].Width = 1.5;

            signOfftbl.Rows[19].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[7].Width = 1.5;

            signOfftbl.Rows[19].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[19].Cells[8].Width = 1.5;


            signOfftbl.Rows[19].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[19].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 20   

            DataSet ds1 = new DataSet();
            ds1 = al.selectmortgage(lbl_orderno.Text, txt_mrg_mortgager.Text, txt_mrg_mortgagee.Text, txt_mrg_dated.Text, txt_mrg_recorded.Text, txt_mrg_book.Text, txt_mrg_pg.Text, txt_mrg_amount.Text, txt_mrg_opndate.Text);

            string a1 = ds1.Tables[0].Rows[0]["mortgagor"].ToString();


            signOfftbl.Rows[20].Cells[0].Paragraphs.First().Append("1.").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[0].Width = 40;

            signOfftbl.Rows[20].Cells[1].Paragraphs.First().Append("MORTGAGOR:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[1].Width = 130;

            signOfftbl.Rows[20].Cells[2].Paragraphs.First().Append("").Append(a1).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[20].Cells[2].Width = 430;

            signOfftbl.Rows[20].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[3].Width = 3;

            signOfftbl.Rows[20].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[4].Width = 1.5;

            signOfftbl.Rows[20].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[5].Width = 1.5;

            signOfftbl.Rows[20].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[6].Width = 1.5;

            signOfftbl.Rows[20].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[7].Width = 1.5;

            signOfftbl.Rows[20].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[20].Cells[8].Width = 3;



            signOfftbl.Rows[20].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[20].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 21 

            string a2 = ds1.Tables[0].Rows[0]["mortgagee"].ToString();

            signOfftbl.Rows[21].Height = 50;
            signOfftbl.Rows[21].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[0].Width = 40;

            signOfftbl.Rows[21].Cells[1].Paragraphs.First().Append("MORTGAGEE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[21].Cells[1].Width = 130;

            signOfftbl.Rows[21].Cells[2].Paragraphs.First().Append(a2).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[21].Cells[2].Width = 430;

            signOfftbl.Rows[21].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[3].Width = 3;

            signOfftbl.Rows[21].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[4].Width = 1.5;

            signOfftbl.Rows[21].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[5].Width = 1.5;

            signOfftbl.Rows[21].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[6].Width = 1.5;

            signOfftbl.Rows[21].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[7].Width = 1.5;

            signOfftbl.Rows[21].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[21].Cells[8].Width = 3;



            signOfftbl.Rows[21].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[21].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 22  
            string a3 = ds1.Tables[0].Rows[0]["dated"].ToString();
            string a4 = ds1.Tables[0].Rows[0]["recorded"].ToString();
            string a5 = ds1.Tables[0].Rows[0]["book"].ToString();
            string a6 = ds1.Tables[0].Rows[0]["pg"].ToString();
            string a7 = ds1.Tables[0].Rows[0]["amount"].ToString();

            signOfftbl.Rows[22].Cells[0].Paragraphs.First().Append("DATED:").Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[0].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[0].Width = 75;

            signOfftbl.Rows[22].Cells[1].Paragraphs.First().Append("").Append(a3).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[1].Width = 85;


            signOfftbl.Rows[22].Cells[2].Paragraphs.First().Append("RECORDED:");
            signOfftbl.Rows[22].Cells[2].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[2].Width = 85;

            signOfftbl.Rows[22].Cells[3].Paragraphs.First().Append("").Append(a4).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[3].Width = 85;


            signOfftbl.Rows[22].Cells[4].Paragraphs.First().Append("BOOK:");
            signOfftbl.Rows[22].Cells[4].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[4].Width = 75;

            signOfftbl.Rows[22].Cells[5].Paragraphs.First().Append("").Append(a5).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[22].Cells[5].Width = 55;


            signOfftbl.Rows[22].Cells[6].Paragraphs.First().Append("PAGE:");
            signOfftbl.Rows[22].Cells[6].Paragraphs.First().Alignment = Alignment.right;
            signOfftbl.Rows[22].Cells[6].Width = 75;

            signOfftbl.Rows[22].Cells[7].Paragraphs.First().Append("").Append(a6).Bold().Font(new Novacode.Font("Times new roman")); ;
            signOfftbl.Rows[22].Cells[7].Width = 65;

            signOfftbl.Rows[22].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[22].Cells[8].Width = 12;



            //ROW 23            
            signOfftbl.Rows[23].Cells[0].Paragraphs.First().Append("AMOUNT:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[0].Width = 110;

            signOfftbl.Rows[23].Cells[1].Paragraphs.First().Append("$").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[1].Width = 25;

            signOfftbl.Rows[23].Cells[2].Paragraphs.First().Append(a7).Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[23].Cells[2].Width = 465;

            signOfftbl.Rows[23].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[3].Width = 3;

            signOfftbl.Rows[23].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[4].Width = 1.5;

            signOfftbl.Rows[23].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[5].Width = 1.5;

            signOfftbl.Rows[23].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[6].Width = 1.5;

            signOfftbl.Rows[23].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[7].Width = 1.5;

            signOfftbl.Rows[23].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[23].Cells[8].Width = 3;


            signOfftbl.Rows[23].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[23].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //ROW 24           
            signOfftbl.Rows[24].Cells[0].Paragraphs.First().Append("OPEN END MORTGAGE:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[24].Cells[0].Width = 220;

            signOfftbl.Rows[24].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[1].Width = 100;

            signOfftbl.Rows[24].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[2].Width = 90;

            signOfftbl.Rows[24].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[3].Width = 190;

            signOfftbl.Rows[24].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[4].Width = 4.5;

            signOfftbl.Rows[24].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[5].Width = 1.5;

            signOfftbl.Rows[24].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[6].Width = 1.5;

            signOfftbl.Rows[24].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[7].Width = 1.5;

            signOfftbl.Rows[24].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[24].Cells[8].Width = 3;


            signOfftbl.Rows[24].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[24].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 25          
            signOfftbl.Rows[25].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[0].Width = 600;

            signOfftbl.Rows[25].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[1].Width = 1.5;

            signOfftbl.Rows[25].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[2].Width = 1.5;

            signOfftbl.Rows[25].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[3].Width = 1.5;

            signOfftbl.Rows[25].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[4].Width = 1.5;

            signOfftbl.Rows[25].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[5].Width = 1.5;

            signOfftbl.Rows[25].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[6].Width = 1.5;

            signOfftbl.Rows[25].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[7].Width = 1.5;

            signOfftbl.Rows[25].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[25].Cells[8].Width = 1.5;


            signOfftbl.Rows[25].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[25].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 26          
            signOfftbl.Rows[26].Cells[0].Paragraphs.First().Append("JUDGEMENTS OR LIENS:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[26].Cells[0].Width = 600;

            signOfftbl.Rows[26].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[1].Width = 1.5;

            signOfftbl.Rows[26].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[2].Width = 1.5;

            signOfftbl.Rows[26].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[3].Width = 1.5;

            signOfftbl.Rows[26].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[4].Width = 1.5;

            signOfftbl.Rows[26].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[5].Width = 1.5;

            signOfftbl.Rows[26].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[6].Width = 1.5;

            signOfftbl.Rows[26].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[7].Width = 1.5;

            signOfftbl.Rows[26].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[26].Cells[8].Width = 1.5;


            signOfftbl.Rows[26].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[26].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //Row 27          
            signOfftbl.Rows[27].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[0].Width = 600;

            signOfftbl.Rows[27].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[1].Width = 1.5;

            signOfftbl.Rows[27].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[2].Width = 1.5;

            signOfftbl.Rows[27].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[3].Width = 1.5;

            signOfftbl.Rows[27].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[4].Width = 1.5;

            signOfftbl.Rows[27].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[5].Width = 1.5;

            signOfftbl.Rows[27].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[6].Width = 1.5;

            signOfftbl.Rows[27].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[7].Width = 1.5;

            signOfftbl.Rows[27].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[27].Cells[8].Width = 1.5;


            signOfftbl.Rows[27].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[27].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 28          
            signOfftbl.Rows[28].Cells[0].Paragraphs.First().Append("TAX INFORMATION:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[28].Cells[0].Width = 600;

            signOfftbl.Rows[28].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[1].Width = 1.5;

            signOfftbl.Rows[28].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[2].Width = 1.5;

            signOfftbl.Rows[28].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[3].Width = 1.5;

            signOfftbl.Rows[28].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[4].Width = 1.5;

            signOfftbl.Rows[28].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[5].Width = 1.5;

            signOfftbl.Rows[28].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[6].Width = 1.5;

            signOfftbl.Rows[28].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[7].Width = 1.5;

            signOfftbl.Rows[28].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[28].Cells[8].Width = 1.5;


            signOfftbl.Rows[28].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[28].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




            //Row 29          
            signOfftbl.Rows[29].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[0].Width = 600;

            signOfftbl.Rows[29].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[1].Width = 1.5;

            signOfftbl.Rows[29].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[2].Width = 1.5;

            signOfftbl.Rows[29].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[3].Width = 1.5;

            signOfftbl.Rows[29].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[4].Width = 1.5;

            signOfftbl.Rows[29].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[5].Width = 1.5;

            signOfftbl.Rows[29].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[6].Width = 1.5;

            signOfftbl.Rows[29].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[7].Width = 1.5;

            signOfftbl.Rows[29].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[29].Cells[8].Width = 1.5;


            signOfftbl.Rows[29].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[29].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 30           
            signOfftbl.Rows[30].Cells[0].Paragraphs.First().Append("TAX ASSESSMENT:");
            signOfftbl.Rows[30].Cells[0].Width = 220;

            signOfftbl.Rows[30].Cells[1].Paragraphs.First().Append("LAND");
            signOfftbl.Rows[30].Cells[1].Width = 100;

            signOfftbl.Rows[30].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[30].Cells[2].Width = 40;

            signOfftbl.Rows[30].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[3].Width = 240;

            signOfftbl.Rows[30].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[4].Width = 4.5;

            signOfftbl.Rows[30].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[5].Width = 1.5;

            signOfftbl.Rows[30].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[6].Width = 1.5;

            signOfftbl.Rows[30].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[7].Width = 1.5;

            signOfftbl.Rows[30].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[30].Cells[8].Width = 3;


            signOfftbl.Rows[30].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[30].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //ROW 31           
            signOfftbl.Rows[31].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[0].Width = 220;

            signOfftbl.Rows[31].Cells[1].Paragraphs.First().Append("BUILDING");
            signOfftbl.Rows[31].Cells[1].Width = 100;

            signOfftbl.Rows[31].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[31].Cells[2].Width = 40;

            signOfftbl.Rows[31].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[3].Width = 240;

            signOfftbl.Rows[31].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[4].Width = 4.5;

            signOfftbl.Rows[31].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[5].Width = 1.5;

            signOfftbl.Rows[31].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[6].Width = 1.5;

            signOfftbl.Rows[31].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[7].Width = 1.5;

            signOfftbl.Rows[31].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[31].Cells[8].Width = 3;


            signOfftbl.Rows[31].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[31].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 32          
            signOfftbl.Rows[32].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[0].Width = 220;

            signOfftbl.Rows[32].Cells[1].Paragraphs.First().Append("OTHERS");
            signOfftbl.Rows[32].Cells[1].Width = 100;

            signOfftbl.Rows[32].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[32].Cells[2].Width = 40;

            signOfftbl.Rows[32].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[3].Width = 240;

            signOfftbl.Rows[32].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[4].Width = 4.5;

            signOfftbl.Rows[32].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[5].Width = 1.5;

            signOfftbl.Rows[32].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[6].Width = 1.5;

            signOfftbl.Rows[32].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[7].Width = 1.5;

            signOfftbl.Rows[32].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[32].Cells[8].Width = 3;


            signOfftbl.Rows[32].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[32].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            //ROW 33           
            signOfftbl.Rows[33].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[0].Width = 220;

            signOfftbl.Rows[33].Cells[1].Paragraphs.First().Append("TOTAL");
            signOfftbl.Rows[33].Cells[1].Width = 100;

            signOfftbl.Rows[33].Cells[2].Paragraphs.First().Append("$");
            signOfftbl.Rows[33].Cells[2].Width = 40;

            signOfftbl.Rows[33].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[3].Width = 240;

            signOfftbl.Rows[33].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[4].Width = 4.5;

            signOfftbl.Rows[33].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[5].Width = 1.5;

            signOfftbl.Rows[33].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[6].Width = 1.5;

            signOfftbl.Rows[33].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[7].Width = 1.5;

            signOfftbl.Rows[33].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[33].Cells[8].Width = 3;


            signOfftbl.Rows[33].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[33].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 34         
            signOfftbl.Rows[34].Cells[0].Paragraphs.First().Append("TAX ID NUMBER:").Bold().Font(new Novacode.Font("Times new roman"));
            signOfftbl.Rows[34].Cells[0].Width = 220;

            signOfftbl.Rows[34].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[1].Width = 240;

            signOfftbl.Rows[34].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[2].Width = 140;

            signOfftbl.Rows[34].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[3].Width = 4.5;

            signOfftbl.Rows[34].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[4].Width = 1.5;

            signOfftbl.Rows[34].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[5].Width = 1.5;

            signOfftbl.Rows[34].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[6].Width = 1.5;

            signOfftbl.Rows[34].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[7].Width = 1.5;

            signOfftbl.Rows[34].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[34].Cells[8].Width = 1.5;



            signOfftbl.Rows[34].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[34].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 35          
            signOfftbl.Rows[35].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[0].Width = 600;

            signOfftbl.Rows[35].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[1].Width = 1.5;

            signOfftbl.Rows[35].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[2].Width = 1.5;

            signOfftbl.Rows[35].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[3].Width = 1.5;

            signOfftbl.Rows[35].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[4].Width = 1.5;

            signOfftbl.Rows[35].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[5].Width = 1.5;

            signOfftbl.Rows[35].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[6].Width = 1.5;

            signOfftbl.Rows[35].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[7].Width = 1.5;

            signOfftbl.Rows[35].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[35].Cells[8].Width = 1.5;


            signOfftbl.Rows[35].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[35].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));



            //Row 36          
            signOfftbl.Rows[36].Cells[0].Paragraphs.First().Append("2015 TAXES PAID IN THE AMOUNT OF:");
            signOfftbl.Rows[36].Cells[0].Width = 320;

            signOfftbl.Rows[36].Cells[1].Paragraphs.First().Append("$");
            signOfftbl.Rows[36].Cells[1].Width = 40;

            signOfftbl.Rows[36].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[2].Width = 60;

            signOfftbl.Rows[36].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[3].Width = 70;

            signOfftbl.Rows[36].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[4].Width = 110;

            signOfftbl.Rows[36].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[5].Width = 8;

            signOfftbl.Rows[36].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[6].Width = 1.5;

            signOfftbl.Rows[36].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[7].Width = 1.5;

            signOfftbl.Rows[36].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[36].Cells[8].Width = 1.5;


            signOfftbl.Rows[36].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[36].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[36].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));





            //Row 37          
            signOfftbl.Rows[37].Cells[0].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[0].Width = 600;

            signOfftbl.Rows[37].Cells[1].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[1].Width = 1.5;

            signOfftbl.Rows[37].Cells[2].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[2].Width = 1.5;

            signOfftbl.Rows[37].Cells[3].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[3].Width = 1.5;

            signOfftbl.Rows[37].Cells[4].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[4].Width = 1.5;

            signOfftbl.Rows[37].Cells[5].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[5].Width = 1.5;

            signOfftbl.Rows[37].Cells[6].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[6].Width = 1.5;

            signOfftbl.Rows[37].Cells[7].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[7].Width = 1.5;

            signOfftbl.Rows[37].Cells[8].Paragraphs.First().Append("");
            signOfftbl.Rows[37].Cells[8].Width = 1.5;


            signOfftbl.Rows[37].Cells[2].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
            signOfftbl.Rows[37].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


            document.InsertTable(signOfftbl);
            document.Save();
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        using (DocX document = DocX.Create(@"D:\\Test.docx"))
        {

            DataSet ds3 = new DataSet();
            ds3 = al.selectdeed(lbl_orderno.Text, txt_deed_type.Text, txt_deed_grantor.Text, txt_deed_grantee.Text, txt_deed_dated.Text, txt_deed_recorded.Text, txt_deed_book.Text, txt_deed_pg.Text, txt_deed_legal.Text);
            //ds3.Tables[1].Rows.Add();



            int count123 = Convert.ToInt32(ds3.Tables[1].Rows.Count);
            //int count1 = (count * 3) - 1;
            ds3.Tables[1].ImportRow(ds3.Tables[1].Rows[count123 - 1]);

            int count = Convert.ToInt32(ds3.Tables[1].Rows.Count);

            int count1 = (count * 2);



            Novacode.Table signOfftbl = document.AddTable(count1, 9);
            signOfftbl.Alignment = Alignment.left;



            //ROW 14                     


            string TEXT = "GRANTOR:";
            string TEXT1 = "GRANTEE";

            for (int i = 0; i < count; i++)
            {
                string l1 = "asdads";
                string l2 = "ads";



                if (i != 0)
                {

                    l1 = ds3.Tables[1].Rows[i]["GRANTOR"].ToString();
                    l2 = ds3.Tables[1].Rows[i]["GRANTEE"].ToString();
                    i++;
                }
                else
                {
                    l1 = ds3.Tables[1].Rows[i]["GRANTOR"].ToString();
                    l2 = ds3.Tables[1].Rows[i]["GRANTEE"].ToString();
                }


                signOfftbl.Rows[i].Cells[0].Paragraphs.First().Append(TEXT).Bold().Font(new Novacode.Font("Times new roman"));
                signOfftbl.Rows[i].Cells[0].Width = 110;

                signOfftbl.Rows[i].Cells[1].Paragraphs.First().Append(l1);
                signOfftbl.Rows[i].Cells[1].Width = 490;

                signOfftbl.Rows[i].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[2].Width = 1.5;

                signOfftbl.Rows[i].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[3].Width = 1.5;

                signOfftbl.Rows[i].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[4].Width = 1.5;

                signOfftbl.Rows[i].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[5].Width = 1.5;

                signOfftbl.Rows[i].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[6].Width = 1.5;

                signOfftbl.Rows[i].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[7].Width = 1.5;

                signOfftbl.Rows[i].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[i].Cells[8].Width = 3;


                signOfftbl.Rows[i].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[i].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));




                int j = i + 1;

                signOfftbl.Rows[j].Height = 50;
                signOfftbl.Rows[j].Cells[0].Paragraphs.First().Append(TEXT1).Bold().Font(new Novacode.Font("Times new roman"));
                signOfftbl.Rows[j].Cells[0].Width = 110;

                signOfftbl.Rows[j].Cells[1].Paragraphs.First().Append(l2);
                signOfftbl.Rows[j].Cells[1].Width = 490;

                signOfftbl.Rows[j].Cells[2].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[2].Width = 1.5;

                signOfftbl.Rows[j].Cells[3].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[3].Width = 1.5;

                signOfftbl.Rows[j].Cells[4].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[4].Width = 1.5;

                signOfftbl.Rows[j].Cells[5].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[5].Width = 1.5;

                signOfftbl.Rows[j].Cells[6].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[6].Width = 1.5;

                signOfftbl.Rows[j].Cells[7].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[7].Width = 1.5;

                signOfftbl.Rows[j].Cells[8].Paragraphs.First().Append("");
                signOfftbl.Rows[j].Cells[8].Width = 3;


                signOfftbl.Rows[j].Cells[3].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[j].Cells[4].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[j].Cells[5].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[j].Cells[6].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[j].Cells[7].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));
                signOfftbl.Rows[j].Cells[8].SetBorder(TableCellBorderType.Left, new Novacode.Border(Novacode.BorderStyle.Tcbs_double, BorderSize.one, 1, Color.Transparent));


                //if (i != 0)
                //{
                //    i--;
                //}

            }

            document.InsertTable(signOfftbl);
            document.Save();
        }
    }   
}





    

    

       


          
        
    

   

    

    

    
   
       
    


     

    


   

                


