<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Form_Reports" Title="REPORTS" Theme="Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="height:565px;width:100%;">
    <tr>
    <td align="center" valign="top" style="width:18%;">
        <table>
            <tr><td>
            <div class="urbangreymenu">
                <h3 class="headerbar">Reports</h3>
            
                <ul>
                    <li><asp:LinkButton ID="LnkEod" runat="server" OnClick="LnkEod_Click">Completed Ord Report</asp:LinkButton></li>                
                </ul>
                 
                <ul>
                    <li><asp:LinkButton ID="LnkIndividual" runat="server" OnClick="LnkIndividual_Click">Individual Report</asp:LinkButton></li>                
                </ul>
                
                <ul>
                    <li><asp:LinkButton ID="Lnkotherbreaktime" runat="server" OnClick="Lnkotherbreaktime_Click">Break Time Report</asp:LinkButton></li>                
                </ul>
               
                <ul>
                    <li><asp:LinkButton ID="LnkUpload" runat="server" OnClick="LnkUpload_Click">Export</asp:LinkButton></li>                
                </ul>
           
            </div>
            </td></tr>
            <tr><td>&nbsp;</td></tr>
           
        </table>    
    </td>    
    <td style="width:92%;" valign="top" align="center">
        <table>
            <tr><td style="height:50px;" align="center"><asp:Label ID="Lblhead" runat="server" CssClass="Heading"></asp:Label></td></tr>
            <tr>            
                <td valign="middle" align="center" >
                    <asp:Panel ID="PanelDate" runat="server">
                        <table class="StatusBtn" style="width:70%;" cellspacing="8" cellpadding="4" >
                            <tr>
                               <td class="Loginlbl">
                                    From Date:
                               </td>          
                               <td>
                                    <asp:TextBox ID="txtfrmdate" runat="server" CssClass="Logintxt" Width="80px"></asp:TextBox>                                   
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" TargetControlID="txtfrmdate" PopupPosition="BottomLeft" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                               </td>                     
                               <td class="Loginlbl">
                                    To Date:
                               </td>          
                               <td>
                                    <asp:TextBox ID="txttodate" runat="server" CssClass="Logintxt" Width="80px"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" TargetControlID="txttodate" PopupPosition="BottomLeft" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                               </td> 
                               <td class="Loginlbl">
                                    Username :
                               </td>          
                               <td>
                                   <asp:DropDownList ID="ddlusername" runat="server"  CssClass="Logintxt"  Width="150px" Height="25px">
                                   </asp:DropDownList>
                               </td>
                                <td>
                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="fb5" OnClick="btnshow_Click" Visible="false"/>
                               </td>          
                            </tr>                           
                        </table>
                    </asp:Panel>
                </td>
            </tr> 
            <tr><td valign="middle" align="center">
                <asp:Label ID="lblgridname" runat="server" Font-Names="Georgia" ForeColor="Black" Font-Bold="true" Font-Size="Medium"></asp:Label>
                <asp:Panel ID="PanelGridReports" runat="server" ScrollBars="Auto" Width="1100px" Height="420px"> 
                    <asp:GridView ID="userGridreports" runat="server"  SkinID="griduser" Font-Names="Verdana" Font-Size="Smaller" Width="900px" >                    
                        <Columns>
                            <asp:TemplateField HeaderText="Sno.">                        
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>                            
                            </asp:TemplateField> 
                        </Columns>
                    </asp:GridView>  
                </asp:Panel>
            </td></tr>
        </table>
    </td>
    </tr>
</table>
</asp:Content>

