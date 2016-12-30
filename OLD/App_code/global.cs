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


public class global
{
    DBConnection objconnection = new DBConnection();
    MySqlParameter[] mParam;

	public global()
	{
		
	}

    #region Check NUll
    private string checkNullDB(MySqlDataReader mdr, string field)
    {
        if (mdr[field] == DBNull.Value) return "";
        else return mdr.GetString(field);
    }
    #endregion
    #region production
    public void insertdeed(string ordno,string deed_type,string grantee,string grantor,string dated,string field,string vol,string pg,string inst,string notes,string tableno)
    {
        string query = "call sp_insert_warrantydeed('" + ordno+ "','" + deed_type+ "','" + grantee.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + field.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + notes + "','"+tableno +"')";
        int result = objconnection.ExecuteNonQuery(query);

    }

    public void updatedeed(string ID,string ordno, string deed_type, string grantee, string grantor, string dated, string field, string vol, string pg, string inst, string notes)
    {
        string query = "call sp_update_warrantydeed('" + ID + "','" + ordno.Replace("'", "\\'") + "','" + deed_type.Replace("'", "\\'") + "','" + grantee.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + field.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + notes.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
    }

   
        public void insertmortgage(string orderno,string mortgage_type,string mortgage_type_2,string assigne,string assignor,string appointed,string executed_by,string lender,string grantor,string payable_to,string trustee,string secured_party,string deptor,string by_and_between,string dated,string filed,string vol,string pg,string inst,string   amount,string notes,string tableno)
    {
        string query = "call sp_insert_mortgage('" + orderno.Replace("'", "\\'") + "','" + mortgage_type.Replace("'", "\\'") + "','" + mortgage_type_2.Replace("'", "\\'") + "','" + assigne.Replace("'", "\\'") + "','" + assignor.Replace("'", "\\'") + "','" + appointed.Replace("'", "\\'") + "','" + executed_by.Replace("'", "\\'") + "','" + lender.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + payable_to.Replace("'", "\\'") + "','" + trustee.Replace("'", "\\'") + "','" + secured_party.Replace("'", "\\'") + "','" + deptor.Replace("'", "\\'") + "','" + by_and_between.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + filed.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + amount.Replace("$", "") + "','" + notes.Replace("'", "\\'") + "','" + tableno.Replace("'", "\\'") + "')";
        int result = objconnection.ExecuteNonQuery(query);
    }

        public int insertclient(string orderno, string client, string date, string address, string city_zip, string refs, string attention, string conformdate, string owner, string propaddress, string city, string state, string zip, string county, string legalinfo, string ownerofrec)
        {
            string query = "call sp_insert_client('" + orderno.Replace("'", "\\'") + "','" + client.Replace("'", "\\'") + "','" + date.Replace("'", "\\'") + "','" + address.Replace("'", "\\'") + "','" + city_zip.Replace("'", "\\'") + "','" + refs.Replace("'", "\\'") + "','" + attention.Replace("'", "\\'") + "','" + conformdate.Replace("'", "\\'") + "','" + owner.Replace("'", "\\'") + "','" + propaddress.Replace("'", "\\'") + "','" + city.Replace("'", "\\'") + "','" + state.Replace("'", "\\'") + "','" + zip.Replace("'", "\\'") + "','" + county.Replace("'", "\\'") + "','" + legalinfo.Replace("'", "\\'") + "','" + ownerofrec.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

            return result;
        }

        public int updateclient(string orderno, string client, string date, string address, string city_zip, string refs, string attention, string conformdate, string owner, string propaddress, string city, string state, string zip, string county, string legalinfo, string ownerofrec)
        {
            string query = "call sp_update_client('" + orderno.Replace("'", "\\'") + "','" + client.Replace("'", "\\'") + "','" + date.Replace("'", "\\'") + "','" + address.Replace("'", "\\'") + "','" + city_zip.Replace("'", "\\'") + "','" + refs.Replace("'", "\\'") + "','" + attention.Replace("'", "\\'") + "','" + conformdate.Replace("'", "\\'") + "','" + owner.Replace("'", "\\'") + "','" + propaddress.Replace("'", "\\'") + "','" + city.Replace("'", "\\'") + "','" + state.Replace("'", "\\'") + "','" + zip.Replace("'", "\\'") + "','" + county.Replace("'", "\\'") + "','" + legalinfo.Replace("'", "\\'") + "','" + ownerofrec.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

            return result;
        }
        public int insertassessmaent(string orderno, string parcel_id, string tax_year, string land, string improvements, string total, string taxes, string due_paid, string notes)
        {
            string query = "call sp_insert_assessment('" + orderno.Replace("'", "\\'") + "','" + parcel_id.Replace("'", "\\'") + "','" + tax_year.Replace("'", "\\'") + "','" + land.Replace("'", "\\'") + "','" + improvements.Replace("'", "\\'") + "','" + total.Replace("'", "\\'") + "','" + taxes.Replace("'", "\\'") + "','" + due_paid.Replace("'", "\\'") + "','" + notes.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);
            return result;
        }
        public int updateassessmaent(string orderno, string parcel_id, string tax_year, string land, string improvements, string total, string taxes, string due_paid, string notes)
        {
            string query = "call sp_update_assessment('" + orderno.Replace("'", "\\'") + "','" + parcel_id.Replace("'", "\\'") + "','" + tax_year.Replace("'", "\\'") + "','" + land.Replace("'", "\\'") + "','" + improvements.Replace("'", "\\'") + "','" + total.Replace("'", "\\'") + "','" + taxes.Replace("'", "\\'") + "','" + due_paid.Replace("'", "\\'") + "','" + notes.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);
            return result;
        }
    
    
       public void updatemortgage(string ID, string orderno, string mortgage_type, string mortgage_type_2, string assigne, string assignor, string appointed, string executed_by, string lender, string grantor, string payable_to, string trustee, string secured_party, string deptor, string by_and_between, string dated, string filed, string vol, string pg, string inst, string amount, string notes)
        {
            string query = "call sp_update_mortgage('" + ID + "','" + orderno.Replace("'", "\\'") + "','" + mortgage_type.Replace("'", "\\'") + "','" + mortgage_type_2.Replace("'", "\\'") + "','" + assigne.Replace("'", "\\'") + "','" + assignor.Replace("'", "\\'") + "','" + appointed.Replace("'", "\\'") + "','" + executed_by.Replace("'", "\\'") + "','" + lender.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + payable_to.Replace("'", "\\'") + "','" + trustee.Replace("'", "\\'") + "','" + secured_party.Replace("'", "\\'") + "','" + deptor.Replace("'", "\\'") + "','" + by_and_between.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + filed.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + amount.Replace("$", "") + "','" + notes.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);
        }


        public void insertothers(string orderno, string others_type, string other_type_2, string Grantee, string Grantor, string Petitioner, string To, string Respondent, string Owner, string Re, string Manufacturer, string Dated, string Filed, string Vol, string Pg, string Inst, string Cause, string Notes, string tableno)
        {
            string query = "call sp_insert_others('" + orderno.Replace("'", "\\'") + "','" + others_type.Replace("'", "\\'") + "','" + other_type_2.Replace("'", "\\'") + "','" + Grantee.Replace("'", "\\'") + "','" + Grantor.Replace("'", "\\'") + "','" + Petitioner.Replace("'", "\\'") + "','" + To.Replace("'", "\\'") + "','" + Respondent.Replace("'", "\\'") + "','" + Owner.Replace("'", "\\'") + "','" + Re.Replace("'", "\\'") + "','" + Manufacturer.Replace("'", "\\'") + "','" + Dated.Replace("'", "\\'") + "','" + Filed.Replace("'", "\\'") + "','" + Vol.Replace("'", "\\'") + "','" + Pg.Replace("'", "\\'") + "','" + Inst.Replace("'", "\\'") + "','" + Cause.Replace("'", "\\'") + "','" + Notes.Replace("'", "\\'") + "','" + tableno.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

        }

        public void updateothers(string ID, string orderno, string others_type,string others_type_2, string Grantee, string Grantor, string Petitioner, string To, string Respondent, string Owner, string Re, string Manufacturer, string Dated, string Filed, string Vol, string Pg, string Inst, string Cause, string Notes)
        {
            string query = "call sp_update_others('" + ID + "','" + orderno.Replace("'", "\\'") + "','" + others_type.Replace("'", "\\'") + "','" + others_type_2.Replace("'", "\\'") + "','" + Grantee.Replace("'", "\\'") + "','" + Grantor.Replace("'", "\\'") + "','" + Petitioner.Replace("'", "\\'") + "','" + To.Replace("'", "\\'") + "','" + Respondent.Replace("'", "\\'") + "','" + Owner.Replace("'", "\\'") + "','" + Re.Replace("'", "\\'") + "','" + Manufacturer.Replace("'", "\\'") + "','" + Dated.Replace("'", "\\'") + "','" + Filed.Replace("'", "\\'") + "','" + Vol.Replace("'", "\\'") + "','" + Pg.Replace("'", "\\'") + "','" + Inst.Replace("'", "\\'") + "','" + Cause.Replace("'", "\\'") + "','" + Notes.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

        }


        public void insertjudgement(string orderno, string judgement_type, string judgement_type_2, string Taxpayer, string Address, string Taxpayerid, string defendant, string paintiff, string owner, string grantor, string grantee, string obligor, string ssn, string obligee, string tribunal, string to, string from, string dated, string filed, string vol, string pg, string inst, string cost, string atty, string intt, string amount, string cause, string notes, string tableno)
        {
            string query = "call sp_insert_judgement('" + orderno.Replace("'", "\\'") + "','" + judgement_type.Replace("'", "\\'") + "','" + judgement_type_2.Replace("'", "\\'") + "','" + Taxpayer.Replace("'", "\\'") + "','" + Address.Replace("'", "\\'") + "','" + Taxpayerid.Replace("'", "\\'") + "','" + defendant.Replace("'", "\\'") + "','" + paintiff.Replace("'", "\\'") + "','" + owner.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + grantee.Replace("'", "\\'") + "','" + obligor.Replace("'", "\\'") + "','" + ssn.Replace("'", "\\'") + "','" + obligee.Replace("'", "\\'") + "','" + tribunal.Replace("'", "\\'") + "','" + to.Replace("'", "\\'") + "','" + from.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + filed.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + cost.Replace("'", "\\'") + "','" + atty.Replace("'", "\\'") + "','" + intt.Replace("'", "\\'") + "','" + amount.Replace("$", "") + "','" + cause.Replace("'", "\\'") + "','" + notes.Replace("'", "\\'") + "','" + tableno.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

        }

        public void updatejudgement(string ID,string orderno, string judgement_type,string judgement_type_2, string Taxpayer, string Address, string Taxpayerid, string defendant, string paintiff, string owner, string grantor, string grantee, string obligor, string ssn, string obligee, string tribunal, string to, string from, string dated, string filed, string vol, string pg, string inst, string cost, string atty, string intt, string amount, string cause, string notes)
        {
            string query = "call sp_update_judgement('" + ID + "','" + orderno.Replace("'", "\\'") + "','" + judgement_type.Replace("'", "\\'") + "','" + judgement_type_2.Replace("'","\\'")+"','" + Taxpayer.Replace("'", "\\'") + "','" + Address.Replace("'", "\\'") + "','" + Taxpayerid.Replace("'", "\\'") + "','" + defendant.Replace("'", "\\'") + "','" + paintiff.Replace("'", "\\'") + "','" + owner.Replace("'", "\\'") + "','" + grantor.Replace("'", "\\'") + "','" + grantee.Replace("'", "\\'") + "','" + obligor.Replace("'", "\\'") + "','" + ssn.Replace("'", "\\'") + "','" + obligee.Replace("'", "\\'") + "','" + tribunal.Replace("'", "\\'") + "','" + to.Replace("'", "\\'") + "','" + from.Replace("'", "\\'") + "','" + dated.Replace("'", "\\'") + "','" + filed.Replace("'", "\\'") + "','" + vol.Replace("'", "\\'") + "','" + pg.Replace("'", "\\'") + "','" + inst.Replace("'", "\\'") + "','" + cost.Replace("'", "\\'") + "','" + atty.Replace("'", "\\'") + "','" + intt.Replace("'", "\\'") + "','" + amount.Replace("$", "") + "','" + cause.Replace("'", "\\'") + "','" + notes.Replace("'", "\\'") + "')";
            int result = objconnection.ExecuteNonQuery(query);

        }


        public DataSet showclientinfo(string orderno)
        {
            DataSet ds = new DataSet();
            ds = objconnection.ExecuteDataset("CALL `sp_select_clientinfo`('" + orderno + "')");

            return ds;
        }

        public DataSet showtaxass(string orderno)
        {
            DataSet ds = new DataSet();
            ds = objconnection.ExecuteDataset("CALL `sp_select_taxass`('" + orderno + "')");

            return ds;
        }


    public DataSet showdeed(string orderno)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_warrantydeed`('" + orderno + "')");

        return ds;
    }

    public DataSet showmortgage(string orderno,string mortgage_type)
    {
        DataSet ds=new DataSet ();
        ds = objconnection.ExecuteDataset("CALL `sp_select_mortgage`('"+orderno +"','"+mortgage_type +"')");
        return ds;
    }

    public DataSet showothers(string orderno, string others_type)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_others`('" + orderno + "','" + others_type + "')");
        return ds;
    }
    public DataSet showjudgement(string orderno, string judgement_type)
    {
        DataSet ds = new DataSet();
        ds = objconnection.ExecuteDataset("CALL `sp_select_judgement`('" + orderno + "','" + judgement_type + "')");
        return ds;
    }
    public DataSet getshowpreviewall(string orderno)
    {
        //DataSet ds = new DataSet();
        //ds = objconnection.ExecuteDataset("CALL `sp_select_previewall`('" + orderno + "')");
        //return ds;
        DataSet ds = new DataSet();

        ds = objconnection.ExecuteDataset("CALL `sp_select_previewall`('" + orderno + "')");
        return ds;

    }



   

   




    #endregion production

    #region Login Page
   

    public bool checkLogin(string User, string Password)
    {
        try
        {
            bool result = false;
            MySqlDataReader mReader;
            string query = "select User_Name,Admin from user_status where User_Name ='" + User.ToLower().Trim() + "' and Password ='" + Password + "' limit 1";
            mReader = objconnection.ExecuteSPReader(query);
            if (mReader.HasRows)
            {
                if (mReader.Read())
                {
                    SessionHandler.UserName = checkNullDB(mReader, "User_Name");
                    SessionHandler.IsAdmin = checkNullDB(mReader, "Admin") == "1" ? true : false;
                    result = true;
                }
            }
            mReader.Close();
            mReader.Dispose();
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion

}