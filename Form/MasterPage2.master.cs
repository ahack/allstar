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

public partial class Form_MasterPage2 : System.Web.UI.MasterPage
{
    GlobalClass gl = new GlobalClass();
    Connection db = new Connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        Lblusername.Text = "Welcome " + SessionHandler.UserName;

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
        Other_breakMsgbx.Visible = false;
    }
    protected void LnkLogout_Click(object sender, EventArgs e)
    {
        pagedimmer.Visible = true;
        Other_logoutMsgbx.Visible = true;
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

        string rights = string.Empty;
        if (SessionHandler.Rights == "KEYING" || SessionHandler.Rights == "DU") rights = "KEYING";
        else rights = SessionHandler.Rights.Trim();

        if (lnkOthers.Text == "Break")
        {
            query = "CALL sp_insert_break('" + SessionHandler.UserName + "','" + pdate + "','" + rights + "','" + SessionHandler.OrderId + "','" + SessionHandler.OrderNo + "')";
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
                string query = "update other_breakdetails set Comments='" + txtcomments.Text + "',Outtime='" + ptime + "',upstatus='1',tottime=TIMEDIFF('" + ptime + "',Intime) where name='" + SessionHandler.UserName + "' and pdate='" + pdate + "' and upstatus='0' and Order_id='"+SessionHandler.OrderId+"'";
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
    protected void btnlogoutresaon_Click(object sender, EventArgs e)
    {
        string query = "";
        int result = 0;
        int res = 0;
        DateTime dt = new DateTime();
        dt = DateTime.Now;
        string pdate = dt.ToString("dd-MM-yyyy");
        string ptime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        query = "insert into tbl_logout_reason(Reason,OrderNo,Process,Name,Pdate,Logout_time) values('" + txtlogoutreason.Text + "','" + SessionHandler.OrderNo + "' ,'" + SessionHandler.Rights + "' ,'" + SessionHandler.UserName + "' , '" + pdate + "' , '" + ptime + "' )";
        result = db.ExecuteSPNonQuery(query);
        if (result > 0)
        {
            res = gl.UserLogOut();
            if (res > 0)
            {
                pagedimmer.Visible = false;
                Other_logoutMsgbx.Visible = false;
                SessionHandler.OrderId = "";
                SessionHandler.Rights = "";
                SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
                SessionHandler.RedirectPage("~/Form/HomePage.aspx");

            }
        }
    }
}
