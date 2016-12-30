<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true"
    CodeFile="AssignJob.aspx.cs" Inherits="Form_AssignJob" Title="ASSIGNJOB" Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;<table style="height: 565px; width: 100%;">
        <tr>
            <td align="center" valign="top" style="width: 18%;">
                <table>
                    <tr>
                        <td>
                            <div class="urbangreymenu">
                                <h3 class="headerbar">
                                    ASSINGJOB</h3>
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="LnkUpload" runat="server" OnClick="LnkUpload_Click">Upload Orders</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LnkReset" runat="server" OnClick="LnkReset_Click">Reset Orders</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LnkClearDatabase" runat="server" OnClick="LnkClearDatabase_Click">Claer Database</asp:LinkButton></li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 92%;" valign="top" align="center">
                <table>
                    <tr>
                        <td style="height: 50px;" align="center">
                            <asp:Label ID="Lblhead" runat="server" CssClass="Heading"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelAssign" runat="server">
                                <table style="width: 80%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="Lblerr" runat="server" CssClass="LiteralErr"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtorders" runat="server" TextMode="MultiLine" CssClass="Logintxt"
                                                Width="650px" Height="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td class="Loginlbl">
                                                        Date
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdate" runat="server" CssClass="Logintxt" Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtdate"
                                                            PopupPosition="BottomLeft" CssClass="cal_Theme1">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:Button ID="btntransmint" runat="server" CssClass="fb5" Text="Transmit" OnClick="btntransmint_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Loginlbl">
                                            <asp:Label ID="Lblinfo" runat="server" Text="Pasting Format Like As: Order # | State |County |Product type"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button ID="btnassign" runat="server" Text="Assign" CssClass="fb5" OnClick="btnassign_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="middle" align="center">
                                            <asp:Panel ID="PanelAssignGrid" runat="server" ScrollBars="Auto" Width="830px" Height="220px">
                                                <table>
                                                    <tr>
                                                        <td valign="bottom">
                                                            <asp:Panel ID="Panelstatus" runat="server">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Image ID="Imgright" runat="server" ImageUrl="~/App_Themes/Default/Images/right12.png"
                                                                                Width="50px" Height="50px" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="Lblstatus" runat="server" CssClass="status"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:GridView ID="AssignGrid" runat="server" SkinID="GridTrackingNew" Font-Names="Georgia"
                                                                Width="800px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No.">
                                                                        <ItemTemplate>
                                                                            <%# Container.DataItemIndex + 1 %>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelReset" runat="server">
                                <table class="ResetTable" cellpadding="3px" cellspacing="3px">
                                    <tr>
                                        <td>
                                            <asp:ListBox ID="Lslorders" runat="server" CssClass="Logintxt" AutoPostBack="true"
                                                Width="300px" Height="420px" OnSelectedIndexChanged="Lslorders_SelectedIndexChanged"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td valign="top" align="left">
                                            <table style="width: 200px; height: 420px;" class="StatusBtn">
                                                <tr>
                                                    <td>
                                                        <table style="width: 200px;" class="StatusBtn1">
                                                            <tr>
                                                                <td class="Loginlbl" valign="Middle">
                                                                    Date
                                                                </td>
                                                                <td style="width: 80px;">
                                                                    <asp:TextBox ID="txtrdate" runat="server" CssClass="Logintxt" Width="80px"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txtrdate"
                                                                        PopupPosition="BottomLeft" CssClass="cal_Theme1">
                                                                    </cc1:CalendarExtender>
                                                                </td>
                                                                <td valign="top">
                                                                    <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="fb5" OnClick="btnGo_Click" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Loginlbl" style="width: 100px;" colspan="3">
                                                        Status: &nbsp;
                                                        <asp:Label ID="lblorderstatus" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chks1" runat="server" Text="Search" AutoPostBack="true" 
                                                            oncheckedchanged="chks1_CheckedChanged" />
                                                        <asp:CheckBox ID="chksqc" runat="server" Text="Search-Qc" AutoPostBack="true" 
                                                            oncheckedchanged="chksqc_CheckedChanged"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkkey" runat="server" Text="keying" AutoPostBack="true" OnCheckedChanged="chkkey_CheckedChanged" />
                                                        <asp:CheckBox ID="chkqc" runat="server" Text="Key-Qc" AutoPostBack="true" OnCheckedChanged="chkqc_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkreview" runat="server" Text="Review" AutoPostBack="true" OnCheckedChanged="chkreview_CheckedChanged"
                                                            Visible="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" valign="middle">
                                                        <table style="width: 200px;" class="StatusBtn1">
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="btnreset" runat="server" Text="Reset" CssClass="fb5" OnClick="btnreset_Click" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Button ID="btnLock" runat="server" Text="Lock" CssClass="fb5" OnClick="btnLock_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="center">
                                                                    <asp:Button ID="Btndelete" runat="server" Text="Delete" CssClass="fb5" OnClick="Btndelete_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center">
                                                                    <asp:Button ID="BtnPriority" runat="server" Text="Priority" CssClass="fb5" OnClick="BtnPriority_Click" />
                                                                </td>
                                                                <td align="center">
                                                                    <asp:Button ID="BtnReject" runat="server" Text="Reject" CssClass="fb5" OnClick="BtnReject_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 90px;">
                                                        Search
                                                        <br />
                                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="Logintxt" Width="200px"></asp:TextBox>
                                                        <br />
                                                        <asp:Button ID="btsearch" runat="server" Text="Search" CssClass="fb5" OnClick="btsearch_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" class="LiteralErr">
                                            <asp:Literal ID="ErrLiteral" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Panel ID="PanelClearData" runat="server" Visible="False" Width="500px" BorderColor="AliceBlue"
                                BorderStyle="Ridge" BorderWidth="2px">
                                <table width="500px" class="Table2">
                                    <tr align="center">
                                        <td align="right">
                                            <asp:Label ID="Username" runat="server" Text="Enter The Password:" Font-Names="Verdana"
                                                CssClass="sublink"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPassword" runat="server" Font-Names="Verdana" TextMode="Password"
                                                CssClass="TextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="2">
                                            <asp:Button ID="cmdOK" runat="server" Text="OK" CssClass="fb5" OnClick="cmdOK_Click" />
                                            <span>&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                            <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="fb5" OnClick="cmdCancel_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="Label5" runat="server" Text="" Font-Names="Verdana" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
