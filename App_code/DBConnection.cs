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
using System.Collections.Generic;

public class DBConnection
{
    public DBConnection()
    {
       
    }

    #region Variable Declration
    string Connection = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    MySqlConnection mConnection;
    MySqlDataAdapter mDa;
    MySqlCommand mCmd;
    MySqlDataReader mDr;
    DataSet ds = new DataSet();
    #endregion

    #region Property
    private string _DupOrders = null;
    public string DupOrders
    {
        get { return _DupOrders; }
        set { _DupOrders = value; }
    }

    #endregion

    #region Connection State
    public void OpenConnection()
    {
        mConnection = new MySqlConnection(Connection);
        if (mConnection.State == ConnectionState.Open)
        {
            mConnection.Close();
            mConnection.Dispose();
        }
        mConnection.Open();
    }
    #endregion

    #region Dataset
    public DataSet ExecuteSPDataset(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        OpenConnection();
        mCmd = new MySqlCommand(query, mConnection);
        ds = new DataSet();
        if (isProcedure)
        {
            mCmd.CommandType = CommandType.StoredProcedure;
            if (myParams != null)
            {
                foreach (MySqlParameter param in myParams)
                {
                    mCmd.Parameters.Add(param);
                }
            }
        }
        try
        {
            
           
            mDa = new MySqlDataAdapter(mCmd);
            mDa.Fill(ds);
            mConnection.Close();
            mConnection.Dispose();
            return ds;
        }
        catch (MySqlException ex)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw ex;
        }
    }
    public DataSet ExecuteDataset(string Query)
    {
       
        OpenConnection();
        mCmd = new MySqlCommand(Query, mConnection);
        ds = new DataSet();
        try
        {
            mDa = new MySqlDataAdapter(mCmd);
            mDa.Fill(ds);
            mConnection.Close();
            mConnection.Dispose();
            return ds;
        }
        catch (MySqlException ex)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw ex;
        }
    }


    public DataSet ExecuteDatasetnew(string Query)
    {

        OpenConnection();
        mCmd = new MySqlCommand("CALL `sp_previewall`('" + Query + "')", mConnection);
        ds = new DataSet();
        try
        {
            mDa = new MySqlDataAdapter(mCmd);
            mDa.Fill(ds);
            mConnection.Close();
            mConnection.Dispose();
            return ds;
        }
        catch (MySqlException ex)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw ex;
        }
    }


    #endregion

    #region Execute Query
    public int ExecuteSPNonQuery(string Query, bool isProcedure, MySqlParameter[] myParams)
    {
        int result = 0;
        OpenConnection();
        mCmd = new MySqlCommand(Query, mConnection);
        if (isProcedure)
        {
            mCmd.CommandType = CommandType.StoredProcedure;
            if (myParams != null)
            {
                foreach (MySqlParameter param in myParams)
                {
                    mCmd.Parameters.Add(param);
                }
            }
        }
        try
        {
            result = mCmd.ExecuteNonQuery();
            mConnection.Close();
            mConnection.Dispose();
            return result;
        }
        catch (MySqlException ex)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw ex;
        }
    }
    public int ExecuteNonQuery(string Query)
    {
        int result = 0;
       
        OpenConnection();
        mCmd = new MySqlCommand(Query, mConnection);
        try
        {
            result = mCmd.ExecuteNonQuery();
            mConnection.Close();
            mConnection.Dispose();
            return result;

         

        }
        catch (MySqlException ex)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw ex;
        }
    }
    #endregion

    #region DataReader
    public MySqlDataReader ExecuteSPReader(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        try
        {
            OpenConnection();
            mCmd = new MySqlCommand(query, mConnection);
            if (isProcedure)
            {
                mCmd.CommandType = CommandType.StoredProcedure;
                if (myParams != null)
                {
                    foreach (MySqlParameter param in myParams)
                    {
                        mCmd.Parameters.Add(param);
                    }
                }
            }

            mDr = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return mDr;
        }
        catch (MySqlException mye)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw mye;
        }
    }
    public MySqlDataReader ExecuteSPReader(string query)
    {
        try
        {
            OpenConnection();
            mCmd = new MySqlCommand(query, mConnection);
            mDr = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return mDr;
        }
        catch (MySqlException mye)
        {
            mConnection.Close();
            mConnection.Dispose();
            throw mye;
        }
    }
    #endregion
}
