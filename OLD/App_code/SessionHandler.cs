using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for SessionHandler
/// </summary>
public class SessionHandler
{
    #region Session Variables
   
    public enum MenuVariable
    {
        HOME,
        SETTINGS,
        ASSIGNJOB,
        TRACKING,
        PRODUCTION,
        REPORTS,
        CHANGEPASSWORD,
        LOGOUT,
        ERRORPAGE,
        PRODUCTION_NEW
    }
    public enum WhichRights
    {
        Admin,
        NONADMIN
    }
    
    private static string _wMenu = "wMenu";
    public static MenuVariable wMenu
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._wMenu] == null)
            {

                return MenuVariable.HOME;
            }
            else
            {
                return (MenuVariable)HttpContext.Current.Session[SessionHandler._wMenu];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._wMenu] = value;
        }
    }
    private static string _userName = "UserName";
    public static string UserName
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._userName] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._userName];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._userName] = value;
        }
    }    
    private static string _IsAdmin = "0";
    public static bool IsAdmin
    {
        get
        {
            return Convert.ToBoolean(HttpContext.Current.Session[SessionHandler._IsAdmin]);
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._IsAdmin] = value.ToString();
        }
    }
    private static string _QC = "0";
    public static bool QC
    {
        get
        {
            return Convert.ToBoolean(HttpContext.Current.Session[SessionHandler._QC]);
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._QC] = value.ToString();
        }
    }
    private static string _Key = "0";
    public static bool Key
    {
        get
        {
            return Convert.ToBoolean(HttpContext.Current.Session[SessionHandler._Key]);
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._Key] = value.ToString();
        }
    }
    private static string _DU = "0";
    public static bool DU
    {
        get
        {
            return Convert.ToBoolean(HttpContext.Current.Session[SessionHandler._DU]);
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._DU] = value.ToString();
        }
    }
    private static string _REVIEW = "0";
    public static bool REVIEW
    {
        get
        {
            return Convert.ToBoolean(HttpContext.Current.Session[SessionHandler._REVIEW]);
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._REVIEW] = value.ToString();
        }
    }
    private static string _IsDu = false.ToString();

    public static string IsDu
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._IsDu] == null)
            {
                return false.ToString();
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._IsDu];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._IsDu] = value;
        }
    }
    private static string _ErrMsg = "ErrMsg";
    public static string ErrMsg
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._ErrMsg] == null)
            {

                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._ErrMsg];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._ErrMsg] = value;
        }
    }
    public static void RedirectPage(string url)
    {
        HttpContext.Current.Response.Redirect(url);
    }
    public static void Abandon()
    {        
        UserName = "";        
        ErrMsg = "";
        IsAdmin = false;
        IsDu = "";        
    }
    private static string _OrderId = "OrderId";
    public static string OrderId
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._OrderId] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._OrderId];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._OrderId] = value;
        }
    }
    private static string _OrderNo = "OrderNo";
    public static string OrderNo
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._OrderNo] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._OrderNo];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._OrderNo] = value;
        }
    }
    private static string _eName = "eName";
    public static string eName
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._eName] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._eName];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._eName] = value;
        }
    }
    //private static string _myDataset = "myDataset";
    //public static DataView myDataset
    //{
    //    get
    //    {
    //        if (HttpContext.Current.Session[SessionHandler._myDataset] == null)
    //        {
    //           return DataView;
    //        }
    //        else
    //        {
    //            return (DataView)HttpContext.Current.Session[SessionHandler._myDataset];
    //        }
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session[SessionHandler._myDataset] = value;
    //    }
    //}
    private static string _Rights = "Rights";
    public static string Rights
    {
        get
        {
            if (HttpContext.Current.Session[SessionHandler._Rights] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._Rights];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._Rights] = value;
        }
    }

    private static string _OtherBreakStatus = "OtherBreakStatus";
    public static string OtherBreakStatus
    {

        get
        {
            if (HttpContext.Current.Session[SessionHandler._OtherBreakStatus] == null)
            {
                return "";
            }
            else
            {
                return (string)HttpContext.Current.Session[SessionHandler._OtherBreakStatus];
            }
        }
        set
        {
            HttpContext.Current.Session[SessionHandler._OtherBreakStatus] = value;
        }
    }
    #endregion
}
