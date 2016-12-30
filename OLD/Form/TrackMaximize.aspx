<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TrackMaximize.aspx.cs" Inherits="Form_TrackMaximize" Theme="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server" style="width:150%;height:150%">
   <%-- <div>--%>
        <table style="height:1000px;width:100%;">  
        <tr> <td align="right"><asp:LinkButton ID="lnkminimize" runat="server" Text="-" OnClick="lnkminimize_Click"></asp:LinkButton></td></tr>
                 <tr>
                 
                 <td valign="top" align="center" style="width: 1092px"> 
                <asp:Panel ID="PanelGrid"  runat="server" ScrollBars="Auto" Width="100%" Height="1000px"> 
                     
                    <asp:GridView  ID="userGrid1" runat="server" SkinID="griduser" Font-Names="Verdana" Font-Size="Smaller" >                                    
                </asp:GridView>  
                </asp:Panel>
       
                 </td>
                </tr>
        </table>
    <%--</div>--%>
    </form>
</body>
</html>
