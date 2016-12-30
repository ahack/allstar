<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MISUpdate.aspx.cs" Inherits="Form_MISUpdate"
    MasterPageFile="~/Form/MasterPage.master" Theme="Default" Title="MISUPDATE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="height: 100%; width: 100%; vertical-align: top;">
        <tr>
            <td style="text-align: center;">
                <asp:Label ID="Lblhead" runat="server" CssClass="Heading">Daily Update To MIS</asp:Label></td>
        </tr>
        <tr>
            <td style="padding-left:550px;">
                <table>
                    <tr>
                        <td class="Loginlbl">
                            Date</td>
                        <td>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="Logintxt" Width="80px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM-dd-yyyy" TargetControlID="txtDate"
                                PopupPosition="BottomLeft" CssClass="cal_Theme1">
                            </cc1:CalendarExtender><asp:Button ID="btnshow" runat="server" CssClass="fb5" Text="Show" OnClick="cmdShow_Click" /></td>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 50px;" align="center">
                <asp:Label ID="LblError" runat="server" Style="color: Red; font-size: 12px; font-weight: bolder;"></asp:Label>
        </tr>
        <tr>
            <td valign="middle" align="center">
                <asp:GridView ID="GridView1" runat="Server" AllowSorting="True" RowStyle-HorizontalAlign="Center"
                    HeaderStyle-CssClass="gridviewhead" AutoGenerateColumns="false" HeaderStyle-BackColor="#212121"
                    HeaderStyle-ForeColor="white" EmptyDataText="No Records Found" Width="700px">
                    <Columns>
                        <%--  <asp:TemplateField HeaderText="project" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="project" Text='<%#Eval("project") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <%--  
                            <asp:TemplateField HeaderText="05:00 - 06:00">
                                <ItemTemplate>
                                    <asp:Label ID="hour0" Text='<%#Eval("05:00 - 06:00") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Hour">
                            <ItemTemplate>
                                <asp:Label ID="Tittle" Text='<%#Eval("tittle") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18:00 - 19:00">
                            <ItemTemplate>
                                <asp:Label ID="hour0" Text='<%#Eval("18:00 - 19:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19:00 - 20:00">
                            <ItemTemplate>
                                <asp:Label ID="hour1" Text='<%#Eval("19:00 - 20:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="20:00 - 21:00">
                            <ItemTemplate>
                                <asp:Label ID="hour2" Text='<%#Eval("20:00 - 21:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="21:00 - 22:00">
                            <ItemTemplate>
                                <asp:Label ID="hour3" Text='<%#Eval("21:00 - 22:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="22:00 - 23:00">
                            <ItemTemplate>
                                <asp:Label ID="hour4" Text='<%#Eval("22:00 - 23:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="23:00 - 24:00">
                            <ItemTemplate>
                                <asp:Label ID="hour5" Text='<%#Eval("23:00 - 24:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="00:00 - 01:00">
                            <ItemTemplate>
                                <asp:Label ID="hour6" Text='<%#Eval("00:00 - 01:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="01:00 - 02:00">
                            <ItemTemplate>
                                <asp:Label ID="hour7" Text='<%#Eval("01:00 - 02:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="02:00 - 03:00">
                            <ItemTemplate>
                                <asp:Label ID="hour8" Text='<%#Eval("02:00 - 03:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="03:00 - 04:00">
                            <ItemTemplate>
                                <asp:Label ID="hour9" Text='<%#Eval("03:00 - 04:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="04:00 - 05:00">
                            <ItemTemplate>
                                <asp:Label ID="hour10" Text='<%#Eval("04:00 - 05:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="05:00 - 06:00">
                            <ItemTemplate>
                                <asp:Label ID="hour11" Text='<%#Eval("05:00 - 06:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="06:00 - 07:00">
                            <ItemTemplate>
                                <asp:Label ID="hour12" Text='<%#Eval("06:00 - 07:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="07:00 - 08:00">
                            <ItemTemplate>
                                <asp:Label ID="hour13" Text='<%#Eval("07:00 - 08:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="08:00 - 09:00">
                            <ItemTemplate>
                                <asp:Label ID="hour14" Text='<%#Eval("08:00 - 09:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="09:00 - 10:00">
                            <ItemTemplate>
                                <asp:Label ID="hour15" Text='<%#Eval("09:00 - 10:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10:00 - 11:00">
                            <ItemTemplate>
                                <asp:Label ID="hour16" Text='<%#Eval("10:00 - 11:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="11:00 - 12:00">
                            <ItemTemplate>
                                <asp:Label ID="hour17" Text='<%#Eval("11:00 - 12:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12:00 - 13:00">
                            <ItemTemplate>
                                <asp:Label ID="hour18" Text='<%#Eval("12:00 - 13:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="13:00 - 14:00">
                            <ItemTemplate>
                                <asp:Label ID="hour19" Text='<%#Eval("13:00 - 14:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="14:00 - 15:00">
                            <ItemTemplate>
                                <asp:Label ID="hour20" Text='<%#Eval("14:00 - 15:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="15:00 - 16:00">
                            <ItemTemplate>
                                <asp:Label ID="hour21" Text='<%#Eval("15:00 - 16:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="16:00 - 17:00">
                            <ItemTemplate>
                                <asp:Label ID="hour22" Text='<%#Eval("16:00 - 17:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="17:00 - 18:00">
                            <ItemTemplate>
                                <asp:Label ID="hour23" Text='<%#Eval("17:00 - 18:00") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td valign="middle" align="center" style="padding-left: 380px;">
                <table style="width: 100%; height: 100%;" cellpadding="3">
                    <tr>
                        <td style="width: 70%; vertical-align: top;">
                            <asp:GridView ID="GridViewResult" runat="Server" AllowSorting="True" Width="560px"
                                RowStyle-HorizontalAlign="Center" HeaderStyle-CssClass="gridviewhead" AutoGenerateColumns="false"
                                HeaderStyle-BackColor="#212121" HeaderStyle-ForeColor="white" EmptyDataText="No Records Found">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name">
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KeyCount" HeaderText="KeyCount" HtmlEncode="False">
                                        <ItemStyle Width="30px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AvgkeyTime" HeaderText="Average key Time">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QcCount" HeaderText="Qc Count">
                                        <ItemStyle Width="80px" />
                                        <HeaderStyle Wrap="True" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AvgqcTime" HeaderText="Average Qc Time">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ReviewCount" HeaderText="Review Count">
                                        <ItemStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AvgReviewTime" HeaderText="Average Review Time">
                                        <ItemStyle Width="100px" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                        <td style="width: 30%; display: none;">
                            <table width="100%">
                                <tr>
                                    <td valign="top">
                                        <asp:GridView ID="GridViewVolumeUpdate" runat="Server" AllowSorting="True" RowStyle-HorizontalAlign="Center"
                                            AutoGenerateColumns="false" HeaderStyle-BackColor="#c2c2c2" HeaderStyle-ForeColor="black"
                                            Width="600px">
                                            <Columns>
                                                <asp:BoundField DataField="Received" HeaderText="Received" HtmlEncode="False">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Rejected" HeaderText="Rejected" HtmlEncode="False">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Delivered" HeaderText="Delivered" HtmlEncode="False">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:TextBox TextMode="MultiLine" runat="server" Height="140px" ID="txtComments"
                                            Width="600px">
                    
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </td> </tr>
    <tr>
        <br />
        <br />
        <td align="center" style="padding-left: 40px;">
            <asp:Button ID="btnUpdate" runat="server" Text="Submit" CssClass="fb5" OnClick="MoveToMIS_Click" />
            <br />
            <br />
        </td>
    </tr>
    </table>
</asp:Content>
