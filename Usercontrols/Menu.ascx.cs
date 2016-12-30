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

public partial class Usercontrols_Menu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GetUserRights();
        ToggleClass();        
    }    
    private void GetUserRights()
    {
        LnkHome.Visible = true;
        if (SessionHandler.UserName == "") LnkHome.Attributes["class"] = "currentmenu";
        LnkSettings.Visible = false;
        LnkAssignjob.Visible = false;
        LnkTracking.Visible = false;
        //LnkProduction.Visible = false;
        LnkReports.Visible = false;
        LnkChangePass.Visible = false;
        lnkprod.Visible = false;

        
        if (SessionHandler.IsAdmin == true && SessionHandler.UserName !="")
        {
            LnkSettings.Visible = true ;
            LnkAssignjob.Visible = true;
            LnkTracking.Visible = true;
           // LnkProduction.Visible = true;
            LnkReports.Visible = true;
            LnkChangePass.Visible =true;
            lnkprod.Visible = true;          
        }
        else if (SessionHandler.IsAdmin == false && SessionHandler.UserName !="")
        {
            lnkprod.Visible = true;            
        }
    }
    private void ToggleClass()
    {
        switch (SessionHandler.wMenu)
        {      
         
            case SessionHandler.MenuVariable.HOME:
                LnkHome.Attributes["class"] = "currentmenu";
                break;
            case SessionHandler.MenuVariable.SETTINGS:
                LnkSettings.Attributes["class"] = "currentmenu";
                break;
            case SessionHandler.MenuVariable.ASSIGNJOB:
                LnkAssignjob.Attributes["class"] = "currentmenu";
                break;
            case SessionHandler.MenuVariable.TRACKING:
                LnkTracking.Attributes["class"] = "currentmenu";
                break;
            case SessionHandler.MenuVariable.PRODUCTION:
               // Session["TimePro"] = DateTime.Now;
                lnkprod.Attributes["class"] = "currentmenu";
                break; 
            case SessionHandler.MenuVariable.REPORTS:
                LnkReports.Attributes["class"] = "currentmenu";
                break;
            case SessionHandler.MenuVariable.CHANGEPASSWORD:
                LnkChangePass.Attributes["class"] = "currentmenu";
                break;

            case SessionHandler.MenuVariable.PRODUCTION_NEW:
                LnkChangePass.Attributes["class"] = "currentmenu";
                break;    
        }
    }
    protected void LnkHome_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.HOME;
        SessionHandler.RedirectPage("~/Form/HomePage.aspx");
    }
    protected void LnkSettings_Click(object sender, EventArgs e)
    {        
            SessionHandler.wMenu = SessionHandler.MenuVariable.SETTINGS;
            SessionHandler.RedirectPage("~/Form/Settings.aspx");     
    }
    protected void LnkAssignjob_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.ASSIGNJOB;
        SessionHandler.RedirectPage("~/Form/AssignJob.aspx");
    }
    protected void LnkTracking_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.TRACKING ;
        SessionHandler.RedirectPage("~/Form/Tracking.aspx");
    }
    protected void LnkProduction_Click(object sender, EventArgs e)
    {
        Session["TimePro"] = DateTime.Now.ToString("HH:mm:ss");
        SessionHandler.wMenu = SessionHandler.MenuVariable.PRODUCTION ;
        SessionHandler.RedirectPage("~/Form/production.aspx");
    }
    protected void LnkReports_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.REPORTS;
        SessionHandler.RedirectPage("~/Form/Reports.aspx");
    }
    protected void LnkChangePass_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.CHANGEPASSWORD;
        SessionHandler.RedirectPage("~/Form/ChangePassword.aspx");
    }
    protected void lnkprod_Click(object sender, EventArgs e)
    {
        SessionHandler.wMenu = SessionHandler.MenuVariable.PRODUCTION_NEW;
        SessionHandler.RedirectPage("~/Form/production.aspx");

    }
}
