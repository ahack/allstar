<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Usercontrols_Menu" %>
<table style="width: 100%;">
    <tr>
        <td class="cssmenu">
            <ul>
                <li>
                    <asp:LinkButton ID="LnkHome" runat="server" Text="HOME" OnClick="LnkHome_Click"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LnkSettings" runat="server" Text="SETTINGS" OnClick="LnkSettings_Click"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LnkAssignjob" runat="server" Text="ASSIGNJOB" OnClick="LnkAssignjob_Click"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LnkTracking" runat="server" Text="TRACKING" OnClick="LnkTracking_Click"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="lnkprod" runat="server" Text="production" OnClick="lnkprod_Click"></asp:LinkButton>
                </li>
                <%-- <li>
                    <asp:LinkButton ID="LnkProduction" runat="server" Text="PRODUCTION" OnClick="LnkProduction_Click"></asp:LinkButton></li>--%>
                <li>
                    <asp:LinkButton ID="LnkReports" runat="server" Text="REPORTS" OnClick="LnkReports_Click"></asp:LinkButton></li>
                <li>
                    <asp:LinkButton ID="LnkChangePass" runat="server" Text="ChangePassWord" OnClick="LnkChangePass_Click"></asp:LinkButton>
                </li>
            </ul>
        </td>
    </tr>
</table>
