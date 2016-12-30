<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Theme="Default" Inherits="Form_HomePage" Title="HOMEPAGE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="height:565px;width:100%">    
    <tr>
    <td valign="Top" align="center">   
        <table style="width:70%;height:565px;">
        <tr><td>&nbsp;</td></tr>
        <tr><td valign="Top" align="center">
        <marquee> <h1>Titleology </h1></marquee>
        </td></tr>
        <tr><td><asp:Label ID="Lblinfo" runat="server" CssClass="LiteralErr"></asp:Label></td></tr>
        </table>              
    </td>
    </tr>  
</table>
</asp:Content>

