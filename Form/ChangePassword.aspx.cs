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

public partial class Form_ChangePassword : System.Web.UI.Page
{
    #region GlobalComponents
    GlobalClass gl = new GlobalClass();
    string StrOldPAss = "";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        DivError.InnerHtml = "";       
    } 
    #endregion

    #region Change Password:Ramesh dec 36 2012
    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        GetUserDetails();

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {

       Response.Redirect("~/Form/LoginPage.aspx");
        //SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
    } 
    #endregion

    public void Clear()
    {

        txtConformPassword.Text = "";
        txtNewPassword.Text = "";
        txtOldPassword.Text = "";
    }



    private void GetUserDetails()
    {
        try
        {
            if (SessionHandler.UserName != null)
            {
                string OldPass = gl.OldPassWord(SessionHandler.UserName.ToString());

                if (OldPass == txtOldPassword.Text)
                {

                    int ResultOb = gl.ChangePassword(SessionHandler.UserName.ToString(), txtNewPassword.Text);
                    if (ResultOb > 0)
                    {

                        DivError.InnerHtml = "New Passwod Hasbeen Changed Successfully!";
                    }
                    else
                    {
                        DivError.InnerHtml = "Incorrect Old password..!";
                    
                    }

                    Clear();
                }
            }
            else
            {
                SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
            }
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
