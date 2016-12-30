<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="Form_LoginPage" Title="LOGIN" Theme="Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="height:565px;width:98%;">
    <tr><td style="height:100px;" >&nbsp;</td></tr>
    <tr><td align="center" valign="Top">
    <%--<asp:Login ID="Login1" runat="server" OnLoginError="Login1_LoginError" OnLoggedIn="Login1_LoggedIn" OnLoggingIn="Login1_LoggingIn">
    <LayoutTemplate>--%>
        <table class="LoginTable" style="width:360px;" cellspacing="8px" cellpadding="4px" >
            <tr>
                <td colspan="2" align="center" class="tdBackcolor">
                    SignIn
                </td>
            </tr>
            <tr>
                <td class="Loginlbl">Username</td> 
                <td><asp:TextBox ID="Username" runat="server" placeholder="User name" CssClass="Logintxt"></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Required Username" ControlToValidate="Username" Display="Dynamic" Text="*" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                </td>                
            </tr>
            <tr>
                <td class="Loginlbl">Password</td>
                <td><asp:TextBox ID="Password" runat="server" placeholder="Password" CssClass="Logintxt" TextMode="Password" ></asp:TextBox>
                <asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Required Password" ControlToValidate="Password" Display="Dynamic" Text="*" meta:resourcekey="RequiredFieldValidator1Resource2"></asp:RequiredFieldValidator></td>                
            </tr>
            <tr>
                <td colspan="2" align="center" ><asp:Button ID="btnsubmin" CommandName="Login" runat="server" Text="Submit" CssClass="fb5" OnClick="btnsubmin_Click" /></td>
            </tr>
        </table>
    <%--</LayoutTemplate>
    </asp:Login>   --%>     
    </td></tr>
    <tr><td class="LiteralErr"><asp:Literal ID="Error" runat="server"></asp:Literal></td></tr>
</table>
</asp:Content>

