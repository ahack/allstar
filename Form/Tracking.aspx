<%@ Page Language="C#" MasterPageFile="~/Form/MasterPage.master" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="Form_Tracking" Title="TRACKING" Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table style="height:565px;width:100%;">
    <tr>
    <td align="center" valign="top" style="width:18%;">
        <table>
            <tr><td>
            <div class="urbangreymenu">
            <h3 class="headerbar">TRACKING</h3>
            <ul>
                <li><asp:LinkButton ID="LnkUpload" runat="server" OnClick="LnkUpload_Click">Export</asp:LinkButton></li></ul>
             <ul>             
               <li> <asp:HyperLink ID="lnkdemo" Text="Maximize" runat="server" style="cursor:pointer; text-decoration:underline;"></asp:HyperLink></li></ul>
            </div>
            </td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <asp:DetailsView ID="DetailsView1" runat="server" Height="93px" SkinID="DetailsViewc"
                        Width="139px" Font-Names="Verdana" Font-Size="Smaller" AutoGenerateRows="False" OnItemCommand="DetailsView1_ItemCommand" OnPageIndexChanging="DetailsView1_PageIndexChanging">
                        <Fields>
                            <asp:ButtonField CommandName="Total" DataTextField="Total" HeaderText="Total" ShowHeader="True"
                                Text="Total">
                                <HeaderStyle ForeColor="Black" />
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="Completed" DataTextField="Completed" HeaderText="Completed"
                                ShowHeader="True" Text="Completed" />
                            <asp:ButtonField CommandName="YTS" DataTextField="YTS" HeaderText="YTS" ShowHeader="True"
                                Text="YTS" />
                                 <asp:ButtonField CommandName="Searchcompleted" DataTextField="Searchcompleted" HeaderText="Search Completed"
                                ShowHeader="True" Text="Search Completed" />

                                 <asp:ButtonField CommandName="SearchQCcompleted" DataTextField="SearchQCcompleted" HeaderText="Search QC completed"
                                ShowHeader="True" Text="Search QC completed" />


                            <asp:ButtonField CommandName="Working" DataTextField="Working" HeaderText="Working"
                                ShowHeader="True" Text="Working" />
                            <asp:ButtonField CommandName="KeyingCompleted" DataTextField="KeyingCompleted" HeaderText="KeyingCompleted"
                                ShowHeader="True" Text="KeyingCompleted" />
                            <asp:ButtonField CommandName="Rejected" DataTextField="Rejected" HeaderText="Rejected"
                                ShowHeader="True" Text="Rejected" />
                            <asp:ButtonField CommandName="Locked" DataTextField="Locked" HeaderText="Locked"
                                ShowHeader="True" Text="Locked" />
                             <asp:ButtonField CommandName="Hold" DataTextField="Hold" HeaderText="Hold"
                                ShowHeader="True" Text="Hold" />
                        </Fields>                        
                    </asp:DetailsView>
                </td>
            </tr>
        </table>    
    </td>    
    <td style="width:92%;" valign="top" align="center">
        <table>
            <tr><td style="height:50px;" align="center"><asp:Label ID="Lblhead" runat="server" CssClass="Heading"></asp:Label></td></tr>
            <tr>            
                <td valign="middle" align="center" >
                    <asp:Panel ID="PanelDate" runat="server">
                        <table class="StatusBtn" style="width:490px;" cellspacing="8" cellpadding="4" >
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
                               <td>
                                <asp:Button ID="btnshow" runat="server" Text="Show" CssClass="fb5" OnClick="btnshow_Click"/>
                               </td>                    
                            </tr>  
                                                
                        </table>
                    </asp:Panel>
                </td>
            </tr> 
            <tr><td>&nbsp;</td></tr>           
            <tr><td valign="middle" align="center">
                <asp:Panel ID="PanelGrid" runat="server" ScrollBars="Auto" Width="1000px" Height="420px">         
                    <asp:GridView ID="userGrid" runat="server"  SkinID="griduser" Font-Names="Verdana" Font-Size="Smaller" OnRowDataBound="userGrid_RowDataBound" OnRowCreated="userGrid_RowCreated" >                    
                    </asp:GridView>  
                </asp:Panel>
            </td></tr>
        </table>
    </td>
    </tr>
</table>
</asp:Content>

