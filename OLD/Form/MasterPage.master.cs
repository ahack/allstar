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

public partial class Form_MasterPage : System.Web.UI.MasterPage
{
    Connection db = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        DateLabel.Text = DateTime.Now.ToLongDateString();

        if (SessionHandler.OtherBreakStatus == "Other UnBreak")
        {
            lnkOthers.Text = "UnBreak";
            lnkOthers.ForeColor = System.Drawing.Color.Green;
            lnkOthers.Attributes.Add("style", "text-decoration:blink");
            pagedimmer.Visible = true;
            Other_breakMsgbx.Visible = true;            
        }

        pagedimmer.Visible = false;
        Other_breakMsgbx.Visible = false;
        ToggleButtons();

        if (SessionHandler.UserName != "") Lbusername.Text = "Welcome " + SessionHandler.UserName;
        else Lbusername.Text = "Welcome...";
        
    }
    private void ToggleButtons()
    {
        if (SessionHandler.UserName == "")
        { Lnklogout.Visible = false; lnkOthers.Visible = false; }
        else { Lnklogout.Visible = true; lnkOthers.Visible = true; }
    }    
    protected void Lnklogout_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.LOGOUT;
        SessionHandler.Abandon();
        SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
    }
    protected void lnkOthers_Click(object sender, EventArgs e)
    {
        string query = "";
        txtcomments.Text = "";
        lblothererror.Text = "";
        int result = 0;
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        string pdate = dt.ToString("dd-MM-yyyy");
        string ptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (lnkOthers.Text == "Break")
        {
            query = "insert into other_breakdetails(Name,PDate,Intime) values('" + SessionHandler.UserName + "' , '" + pdate + "' , '" + ptime + "' )";
            result = db.ExecuteSPNonQuery(query);
            if (result > 0)
            {
                lnkOthers.Text = "UnBreak";
                lnkOthers.ForeColor = System.Drawing.Color.Red;
                SessionHandler.OtherBreakStatus = "UnBreak";
                lnkOthers.Attributes.Add("style", "text-decoration:blink");
                pagedimmer.Visible = true;
                Other_breakMsgbx.Visible = true;
            }
        }
    }
    protected void Btnok_Click(object sender, EventArgs e)
    {
        int result = 0;
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        string pdate = dt.ToString("dd-MM-yyyy");
        string ptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        if (txtcomments.Text != "")
        {
            if (lnkOthers.Text == "UnBreak")
            {
                string query = "update other_breakdetails set Comments='" + txtcomments.Text + "',Outtime='" + ptime + "',upstatus='1',tottime=TIMEDIFF('" + ptime + "',Intime) where name='" + SessionHandler.UserName + "' and pdate='" + pdate + "' and upstatus='0'";
                result = db.ExecuteSPNonQuery(query);
                if (result > 0)
                {
                    lnkOthers.Text = "Break";
                    lnkOthers.ForeColor = System.Drawing.Color.White;
                    SessionHandler.OtherBreakStatus = "Break";
                    lnkOthers.Attributes.Add("style", "text-decoration:none");
                    pagedimmer.Visible = false;
                    Other_breakMsgbx.Visible = false;
                }
            }
        }
        else
        {
            lblothererror.Text = "Please Enter the Comments";
            pagedimmer.Visible = true;
            Other_breakMsgbx.Visible = true;
        }
    }
}
