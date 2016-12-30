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

public partial class Form_HomePage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }
        string status = Request.QueryString["status"];
        if (status != "") Lblinfo.Text = status;
        else Lblinfo.Text = "";
        
    }
}
