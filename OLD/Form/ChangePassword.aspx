<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Form_ChangePassword"
    MasterPageFile="~/Form/MasterPage.master" Theme="Default" Title="CHANGEPWD" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="height: 565px; width: 100%;">
        <tr>
            <td align="center" valign="top" style="width: 18%;">
                <%--<table>
            <tr><td>
            <div class="urbangreymenu">
            <h3 class="headerbar">TRACKING</h3>
            <ul>
                
            </ul>
             <ul>
             <%--<li><asp:LinkButton ID="LnkMaximize" runat="server" OnClick="LnkMaximize_Click" >Maximize</asp:LinkButton></li>--%>
                <%--   <li> <asp:HyperLink ID="lnkdemo" Text="Maximize" runat="server" style="cursor:pointer; text-decoration:underline;"></asp:HyperLink></li>               
            </ul>
            </div>
            </td></tr>
            <tr><td>&nbsp;</td></tr>
            
        </table>--%>
            </td>
            <td style="width: 92%;" valign="top" align="center">
                <table>
                    <tr>
                        <td style="height: 50px;" align="center">
                            <asp:Label ID="Lblhead" runat="server" CssClass="Heading">Change Password</asp:Label></td>
                    </tr>
                    <tr>
                        <td valign="middle" align="center">
                            <asp:Panel ID="PanelDate" runat="server">
                                <table class="StatusBtn" style="width: 490px;" cellspacing="8" cellpadding="4">
                                    <tr>
                                        <td class="Loginlbl">
                                            Old Password
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="Logintxt" Width="170px" onkeypress="javascript:Clear()" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Loginlbl">
                                            New Password
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="Logintxt" Width="170px" onkeypress="javascript:Clear()"  TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Loginlbl">
                                            Confirm Password
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConformPassword" runat="server" CssClass="Logintxt" Width="170px" onkeypress="javascript:Clear() "  TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="fb5" OnClientClick="return ValidatePass();" OnClick="btnUpdate_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="fb5" OnClick="btnCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table align="left">
                                    <tr>
                                        <td colspan="2">
                                            <b style="color: Red">
                                                <div id="DivError" runat="server">
                                                </div>
                                            </b>
                                        </td>
                                    </tr>
                                </table>

                                <script type="text/javascript" language="javascript">
    
    function Clear()
    {       
            document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='';
    }
    
      function ValidatePass()
      {
          var OldPass=document.getElementById("ctl00_ContentPlaceHolder1_txtOldPassword").value;      
          var NewPassWord=document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassword").value;
          var ConfirmPassWord=document.getElementById("ctl00_ContentPlaceHolder1_txtConformPassword").value;  
          
  
            if(OldPass == "") {                  
              document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error: Old Password Can Not Be Null!';              
              document.getElementById("ctl00_ContentPlaceHolder1_txtOldPassword").focus();
              return false;
            }
            if(NewPassWord == "") {      
              document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error: New Password Can Not Be Null!';              
              document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassword").focus()
              return false;
            }
            if(ConfirmPassWord == "") {      
              
              document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error: Confirm Password Can Not Be Null!';              
              
              document.getElementById("ctl00_ContentPlaceHolder1_txtConformPassword").focus();  
              return false;
            }
            
         
           
             if(NewPassWord!= "")
             {
             
                    if(NewPassWord.length < 8)
                       {
                        
                        
                        document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error:New Password must contain more than eight characters!';              
                        document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassword").focus();
                        return false;
                      }
                  
                     re = /[0-9]/;
                      if(!re.test(NewPassWord))
                       {
                        
                        document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error:New password must contain at least one number (0-9)!';              
                        document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassword").focus();
                        return false;
                       }
             
              }
      
                  if(NewPassWord!= "")
                  {
                  
                            var iChars = "!`@#$%^&*()+=-[]\\\';,./{}|\":<>?~_";                               
                            var flag=false;
                            for (var i = 0; i < NewPassWord.length; i++)
                            {      
                                if (iChars.indexOf(NewPassWord.charAt(i)) != -1)
                                {                                   
                                flag=true;
                                } 
                                
                            }
                            
                            
                            if(flag==false)
                            {
                                 
                                 document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='New Password Must Contains atleast one Special Charector!';              
                                 document.getElementById("ctl00_ContentPlaceHolder1_txtNewPassword").focus();
                                 return false;
                            }
                            
                            
                  }
                  
                  if(NewPassWord!="" && OldPass!="" )
                  {                     
                     if(NewPassWord!=ConfirmPassWord )
                     {
                      document.getElementById("ctl00_ContentPlaceHolder1_DivError").innerHTML='Error:New Password and Confirm Password did not match.,Please enter both as same!';              
                      document.getElementById("ctl00_ContentPlaceHolder1_txtConformPassword").focus();
                      return false;
                     }
                     else
                     {
                      return true;   
                     }
                  }
                  return true;
      }
       
                                </script>

                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
