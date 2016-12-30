using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;

public class allstar_global
{
    DBConnection objconnection = new DBConnection();
    MySqlParameter[] mParam;
    Connection con = new Connection();
    public allstar_global()
    {

    }

    #region production
    #region insert
    public int insert_client(string orderno, string search_date, string as_of_date, string address)
    {
        string query = "call sp_insert_client('" + orderno.Replace("'", "\\'") + "','" + search_date.Replace("'", "\\'") + "','" + as_of_date.Replace("'", "\\'") + "','" + address.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);

        return result;
    }
    public int insert_deed(string ordno, string deed_type, string grantor, string grantee, string dated, string recorded, string book, string pg, string legal, string tblno)
    {
        string query = "call sp_insert_warrantydeed('" + ordno + "','" + deed_type + "','" + grantor.Replace("'", "\\'") + "','" + grantee.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + recorded.Replace("'", "\\'") + "','" + book.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + legal.Replace("'", "\\'") + "','" + tblno.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;

    }
    public int insert_mortgage(string orderno, string mortgagor, string mortgagee, string dated, string recorded, string book, string pg, string amount, string openend_mortgage)
    {
        string query = "call sp_insert_mortgage('" + orderno.Replace("'", "\\'") + "','" + mortgagor.Replace("'", "\\'") + "','" + mortgagee.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + recorded.Replace("'", "\\'") + "','" + book.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + amount.Replace("'", "\\'") + "','" + openend_mortgage.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;
    }
    public int insert_tax(string orderno,string land, string building, string total, string id_number, string paid_amt, string paid_on, string nxt_tax_due,string pre_tax_paid,string home_exe,string water_prop)
    {
        string query = "call sp_insert_tax('" + orderno.Replace("'", "\\'") + "','" + land.Replace("'", "\\'") + "','" + building.Replace("'", "\\'") + "','" + total.Replace("'", "\\'") + "','" + id_number.Replace("'", "\\'") + "','" + paid_amt.Replace("'", "\\'") + "','" + paid_on.Replace("'", "\\'") + "','" + nxt_tax_due.Replace("'", "\\'") + "','" + pre_tax_paid.Replace("'", "\\'") + "','" + home_exe.Replace("'", "\\'") + "','" + water_prop.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;
    }
    #endregion insert
    #region update
    public int update_client(string orderno, string search_date, string as_of_date, string address)
    {
        string query = "call sp_update_client('" + orderno.Replace("'", "\\'") + "','" + search_date.Replace("'", "\\'") + "','" + as_of_date.Replace("'", "\\'") + "','" + address.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);

        return result;
    }
    public int update_deed(string ID,string ordno, string deed_type, string grantor, string grantee, string dated, string recorded, string book, string pg, string legal)
    {
        string query = "call sp_update_warrantydeed('" + ID + "','" + ordno + "','" + deed_type + "','" + grantor.Replace("'", "\\'") + "','" + grantee.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + recorded.Replace("'", "\\'") + "','" + book.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + legal.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;

    }
    public int update_mortgage(string ID,string orderno, string mortgagor, string mortgagee, string dated, string recorded, string book, string pg, string amount, string openend_mortgage)
    {
        string query = "call sp_update_mortgage('" + ID + "','" + orderno.Replace("'", "\\'") + "','" + mortgagor.Replace("'", "\\'") + "','" + mortgagee.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + recorded.Replace("'", "\\'") + "','" + book.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + amount.Replace("'", "\\'") + "','" + openend_mortgage.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;
    }

    public int update_tax(string ID,string orderno, string land, string building, string total, string id_number, string paid_amt, string paid_on, string nxt_tax_due, string pre_tax_paid, string home_exe, string water_prop)
    {
        string query = "call sp_update_tax('" + ID + "','" + orderno.Replace("'", "\\'") + "','" + land.Replace("'", "\\'") + "','" + building.Replace("'", "\\'") + "','" + total.Replace("'", "\\'") + "','" + id_number.Replace("'", "\\'") + "','" + paid_amt.Replace("'", "\\'") + "','" + paid_on.Replace("'", "\\'") + "','" + nxt_tax_due.Replace("'", "\\'") + "','" + pre_tax_paid.Replace("'", "\\'") + "','" + home_exe.Replace("'", "\\'") + "','" + water_prop.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
        return result;
    }


    #endregion update
    public int UpdateOrders(string comments)
    {
        mParam = new MySqlParameter[3];

        mParam[0] = new MySqlParameter("?$Ord_No", SessionHandler.OrderId);
        mParam[0].MySqlDbType = MySqlDbType.VarChar;
        mParam[1] = new MySqlParameter("?$pType", SessionHandler.Rights);
        mParam[1].MySqlDbType = MySqlDbType.VarChar;
        mParam[2] = new MySqlParameter("?$comments", comments);
        mParam[2].MySqlDbType = MySqlDbType.VarChar;
        

        return con.ExecuteSPScalar("sp_UpdateUserKey_new", true, mParam);
    }


    #region Show on grid
    public DataSet showclientinfo(string orderno)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_clientinfo`('" + orderno + "')");

        return ds;
    }
    public DataSet showdeed(string orderno)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_warrantydeed`('" + orderno + "')");

        return ds;
    }
    public DataSet showmortgage(string orderno)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_mortgage`('" + orderno + "')");
        return ds;
    }
    public DataSet showtax(string orderno)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_tax`('" + orderno + "')");
        return ds;
    }
    #endregion show on grid

    public DataSet client(string orderno, string searchdate, string asofdate, string address)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `Sp_client1`('" + orderno + "','" + searchdate + "','" + asofdate + "','" + address + "')");

        return ds;
    }


    public DataSet selectmortgage(string orderno, string mortgagor, string mortgagee, string dated, string recorded, string book, string pg, string amount, string openendmortgage)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_selectq_mortgage`('" + orderno + "','" + mortgagor + "','" + mortgagee + "','" + dated + "','" + recorded + "','" + book + "','" + pg + "','" + amount + "','" + openendmortgage + "')");

        return ds;
    }

    public DataSet selectdeed(string orderno, string Deed_type, string GRANTOR, string GRANTEE, string DATED, string RECORDED, string BOOK, string PG, string LEGAL)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `Sp_selectdeed`('" + orderno + "','" + Deed_type + "','" + GRANTOR + "','" + GRANTEE + "','" + DATED + "','" + RECORDED + "','" + BOOK + "','" + PG + "','" + LEGAL + "')");

        return ds;
    }


    #endregion
}