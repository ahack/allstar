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
using System.Text;
using System.Text.RegularExpressions;

public partial class Form_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionHandler.UserName == "")
        {
            SessionHandler.RedirectPage("~/Form/LoginPage.aspx");
        }       
        if (!IsPostBack)
        {
            string content = "";
            if (Request.QueryString["Content"] != "" && Request.QueryString["Content"] != null && Session["Content"] != "" && Session["Content"] != null)
            {                
                Searchtxt = Decryptdata(Request.QueryString["Content"]);
                content = Session["Content"].ToString();
                Type = 1;    // Search Others
                CreateContentDataTable(content);                
            }
            else if (Request.QueryString["Year"] != "" && Request.QueryString["Year"] != null && Session["Content"] != "" && Session["Content"] != null)
            {
                Type = 2; // Search Year
                content = Session["Content"].ToString();
                CreateContentDataTable(content);                
            }
        }
    }

    private string Searchtxt = "";
    private int Type = 0;

    private string Decryptdata(string encryptpwd)
    {
        string decryptpwd = string.Empty;
        UTF8Encoding encodepwd = new UTF8Encoding();
        Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        decryptpwd = new String(decoded_char);
        return decryptpwd;
    }
    private void CreateContentDataTable(string content)
    {
        DataTable dtContent = new DataTable();
        DataColumn dc1 = new DataColumn();
        dc1.DataType = typeof(string);
        dc1.ColumnName = "Content";
        dtContent.Columns.Add(dc1);
        DataRow Dr = dtContent.NewRow();
        Dr[0] = content;
        dtContent.Rows.Add(Dr);
        GridSearch.DataSource = dtContent;
        GridSearch.DataBind();
    }
    public string Highlight(string InputTxt)
    {
        string Search_Str = "";        
        if(Type == 1) Search_Str = Searchtxt;
        else if (Type == 2)
        {
            string k1 = "";
            for (int j = 1800; j < 2100; j++)
            {
                k1 += j + ",";
            }
            Search_Str = k1;
        }
        Regex RegExp = new Regex(Search_Str.Replace(",", "|").Trim(),RegexOptions.IgnoreCase);        
        return RegExp.Replace(InputTxt,new MatchEvaluator(ReplaceKeyWords));
        RegExp = null;
    }
    private string ReplaceKeyWords(Match m)
    {
        return "<span class='highlight'>" + m.Value + "</span>";
    }
}
