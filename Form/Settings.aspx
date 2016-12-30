<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true"
    CodeFile="Settings.aspx.cs" Inherits="Form_Settings" Title="SETTINGS" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="height: 565px; width: 100%;">
        <tr>
            <td align="center" valign="top" style="width: 18%;">
                <table>
                    <tr>
                        <td>
                            <div class="urbangreymenu">
                                <h3 class="headerbar">
                                    SETTINGS</h3>
                                <ul>
                                    <li>
                                        <asp:LinkButton ID="LnkUser" runat="server" OnClick="LnkUser_Click">User Details</asp:LinkButton></li>
                                    <li>
                                        <asp:LinkButton ID="LnkNewuser" runat="server" OnClick="LnkNewuser_Click">New User</asp:LinkButton></li>
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
                            <asp:Panel ID="PanelNew" runat="server">
                                <table class="LoginTable" style="width: 400px;" cellspacing="5" cellpadding="2">
                                    <tr>
                                        <td colspan="2" align="center" class="tdBackcolor">
                                            Create New User
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Loginlbl">
                                            FullName
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtfullname" runat="server" CssClass="Logintxt"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Fullname"
                                                ControlToValidate="txtfullname" Text="*" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Loginlbl">
                                            Username
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtusername" runat="server" CssClass="Logintxt"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Required Username" ControlToValidate="txtusername" Text="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <table style="width: 300px;" cellspacing="5" cellpadding="2">
                                                <tr>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="chkadmin" runat="server" Text="Admin" OnCheckedChanged="chkadmin_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </td>
                                                    <td class="Loginlbl">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="ChkSearch" runat="server" Text="Search" AutoPostBack="true" 
                                                            oncheckedchanged="ChkSearch_CheckedChanged" />
                                                    </td>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="ChkSqc" runat="server" Text="Search-QC" AutoPostBack="true" 
                                                            oncheckedchanged="ChkSqc_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="ChkProduction" runat="server" Text="Keying" AutoPostBack="true"
                                                            OnCheckedChanged="ChkProduction_CheckedChanged" />
                                                    </td>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="Chkqc" runat="server" Text="Key-QC" OnCheckedChanged="Chkqc_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="Loginlbl">
                                                        <asp:CheckBox ID="ChkDu" runat="server" Text="DU" OnCheckedChanged="ChkDu_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="Chkreview" runat="server" Text="Review" AutoPostBack="true" OnCheckedChanged="Chkreview_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="fb5" ValidationGroup="Save"
                                                OnClick="btnsave_Click" />
                                            <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="fb5" OnClick="btnupdate_Click" />
                                            <asp:Button ID="btnclear" runat="server" Text="Cancel" CssClass="fb5" OnClick="btnclear_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="Lbluser" runat="server" CssClass="LiteralErr"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelGrid" runat="server" ScrollBars="Vertical" Width="700px" Height="400px">
                                <asp:GridView ID="userGrid" runat="server" AutoGenerateColumns="False" SkinID="GridUser1"
                                    Width="650px" OnRowDataBound="userGrid_RowDataBound" OnRowCommand="userGrid_RowCommand"
                                    OnRowEditing="userGrid_RowEditing" OnRowDeleting="userGrid_RowDeleting">
                                    <Columns>
                                        <asp:BoundField DataField="Username" HeaderText="User Name">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Admin" HeaderText="Admin">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                         <asp:BoundField DataField="S1" HeaderText="Search">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>

                                         <asp:BoundField DataField="SQC" HeaderText="Search-QC">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>


                                        <asp:BoundField DataField="Key" HeaderText="Keying">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="QC" HeaderText="Key-QC">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DU" HeaderText="DU">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Review" HeaderText="REVIEW">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:BoundField>
                                        <asp:ButtonField CommandName="Edit" HeaderText="Edit" Text="Edit">
                                            <ItemStyle ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:ButtonField>
                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True">
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle ForeColor="Black" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:CommandField>
                                    </Columns>
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function Uncheck(Chk1, Ck2, Ck3, Ck4) {
            if (document.getElementById(Chk1).checked == true) {
                document.getElementById(Ck2).checked = false;
                document.getElementById(Ck3).checked = false;
                document.getElementById(Ck4).checked = false;
            }
            else {
                document.getElementById(Ck2).checked = false;
                document.getElementById(Ck3).checked = false;
                document.getElementById(Ck4).checked = false;
            }
        } 
    </script>
</asp:Content>
