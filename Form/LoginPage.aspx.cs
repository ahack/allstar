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
using MySql.Data;
using MySql.Data.MySqlClient;

public partial class Form_LoginPage : System.Web.UI.Page
{
    GlobalClass gl = new GlobalClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        SessionHandler.UserName = "";
        SessionHandler.IsAdmin = false;
    }    
    protected void btnsubmin_Click(object sender, EventArgs e)
    {
        MySqlDataReader mdra;
        Error.Text = "";
        mdra = gl.checkLogin(Username.Text,Password.Text);
        try
        {
            if (mdra.HasRows)
            {
                if (mdra.Read())
                {
                    gl.Admin = mdra.GetString(0);
                    gl.Key = mdra.GetString(1);
                    gl.QC = mdra.GetString(2);
                    gl.DU = mdra.GetString(3);
                    gl.Review = mdra.GetString(4);
                    SessionHandler.UserName =Username.Text;
                    if (Int16.Parse(gl.Admin) == 1)
                    {
                        SessionHandler.IsAdmin = true;
                        SessionHandler.RedirectPage("~/Form/Homepage.aspx");
                    }
                    else if ((Int16.Parse(gl.Admin) == 0))
                    {
                        SessionHandler.IsAdmin = false;
                        SessionHandler.RedirectPage("~/Form/Homepage.aspx");
                    }
                }
                else
                {
                    Error.Text = "Please Check Username And Password";
                }
            }
            else
            {
                Error.Text = "Please Check Username And Password";
            }
        }
        catch (NullReferenceException)
        {
            Error.Text = "Connection not established. Please try again...";
        }
    }
}
