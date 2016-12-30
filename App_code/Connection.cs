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

/// <summary>
/// Summary description for Connection
/// </summary>
public class Connection
{
    string myConnection = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    MySqlConnection mConnection;
    MySqlDataAdapter mDa;
    MySqlCommand mCmd;
    MySqlDataReader mDr;
    public Connection()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private void openConnection()
    {
        mConnection = new MySqlConnection(myConnection);
        if (mConnection.State == ConnectionState.Open)
        {
            mConnection.Close();
        }
        mConnection.Open();
    }

    public DataSet ExecuteQuery(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        openConnection();
        mCmd = new MySqlCommand(query, mConnection);
        DataSet ds = new DataSet();
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
            return ds;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return ds;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }

    public MySqlDataAdapter ExecuteSPAdapter(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        openConnection();
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
        try
        {
            mDa = new MySqlDataAdapter(mCmd);
            return mDa;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return mDa;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }

    


    public MySqlDataAdapter ExecuteSPAdapter(string query)
    {
        try
        {
            mDa = new MySqlDataAdapter(query, myConnection);
            return mDa;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return mDa;
        }
    }
    public DataSet ExecuteQuery1(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        openConnection();
        mCmd = new MySqlCommand(query, mConnection);
        DataSet ds = new DataSet();
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
            return ds;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return ds;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }
    public MySqlDataReader ExecuteSPReader(string query, bool isProcedure, MySqlParameter[] myParams)
    {
        openConnection();
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
        try
        {
            mDr = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
            //mDr = mCmd.ExecuteReader();
            return mDr;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return mDr;
        }
        finally
        {
            //mConnection.Close();
            //mConnection.Dispose();
        }
    }
    public MySqlDataReader ExecuteStoredProcedure(string Query, bool isProcedure, MySqlParameter[] myParams)
    {
        openConnection();

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
        mDr = mCmd.ExecuteReader();
        return mDr;
    }
    public MySqlDataReader ExecuteSPReader(string query)
    {

        openConnection();
        mCmd = new MySqlCommand(query, mConnection);
        try
        {
            mDr = mCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return mDr;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return mDr;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }
    public int ExecuteSPNonQuery(string Query, bool isProcedure, MySqlParameter[] myParams)
    {
        int result;
        openConnection();
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
        }
        catch (MySqlException mye)
        {
            if (mye.Number == 1062)
            {
                SessionHandler.ErrMsg = "Duplicate Entry: Name already found.";
            }
            else
            {
                SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            }
            return -1;

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
        return result;

    }
    public int ExecuteSPNonQuery(string Query)
    {
        int result;
        openConnection();
        mCmd = new MySqlCommand(Query, mConnection);

        try
        {
            result = mCmd.ExecuteNonQuery();
        }
        catch (MySqlException mye)
        {
            if (mye.Number == 1062)
            {
                SessionHandler.ErrMsg = "Duplicate Entry: Name already found.";
            }
            else
            {
                SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            }
            return -1;

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
        return result;

    }
    public DataSet ExecuteQuery(string Query)
    {
        DataSet ds;
        openConnection();
        mCmd = new MySqlCommand(Query, mConnection);
        ds = new DataSet();
        mDa = new MySqlDataAdapter(mCmd);
        mDa.Fill(ds);
        mConnection.Close();
        mConnection.Dispose();
        return ds;

    }
    public int ExecuteSPScalar(string Query, bool isProcedure, MySqlParameter[] myParams)
    {
        int result;
        openConnection();
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
        }
        catch (MySqlException mye)
        {
            if (mye.Number == 1062)
            {
                SessionHandler.ErrMsg = "Duplicate Entry: Name already found.";
            }
            else
            {
                SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            }
            return -1;

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
        return result;

    }
    public int ExecuteSPScalar(string Query)
    {
        openConnection();
        mCmd = new MySqlCommand(Query, mConnection);

        try
        {
            return Convert.ToInt16(mCmd.ExecuteScalar());
        }
        catch (MySqlException mye)
        {

            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return -1;

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }
    public string ExecuteScalar(string Query)
    {
        openConnection();
        mCmd = new MySqlCommand(Query, mConnection);

        try
        {
            return Convert.ToString(mCmd.ExecuteScalar());
        }
        catch (MySqlException mye)
        {

            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return "";

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
    }
    public string ExecuteScalar(string Query, bool isProcedure, MySqlParameter[] myParams)
    {
        string result;
        openConnection();
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
            result = Convert.ToString(mCmd.ExecuteScalar());
        }
        catch (MySqlException mye)
        {
            if (mye.Number == 1062)
            {
                SessionHandler.ErrMsg = "Duplicate Entry: Name already found.";
            }
            else
            {
                SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            }
            return "";

        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }
        return result;

    }
    public DataSet ExecuteTables(string[] Querys)
    {
        DataSet ds = new DataSet();
        openConnection();
        mCmd = new MySqlCommand();
        mCmd.Connection = mConnection;
        mDa = new MySqlDataAdapter();
        mDa.SelectCommand = mCmd;
        try
        {
            int count = 0;
            foreach (string query in Querys)
            {
                mDa.SelectCommand.CommandText = query;
                mDa.Fill(ds, "Table" + count);
                count++;
            }
            return ds;
        }
        catch (MySqlException mye)
        {
            SessionHandler.ErrMsg = mye.Number + " " + mye.Message;
            return ds;
        }
        finally
        {
            mConnection.Close();
            mConnection.Dispose();
        }

    }
}
