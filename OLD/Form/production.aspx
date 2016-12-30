<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPage2.master" AutoEventWireup="true"
    CodeFile="production.aspx.cs" Inherits="Form_production" Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <script type="text/javascript">
            var specialKeys = new Array();
            specialKeys.push(8); //Backspace
            specialKeys.push(9); //Tab
            specialKeys.push(46); //Delete
            specialKeys.push(36); //Home
            specialKeys.push(35); //End
            specialKeys.push(37); //Left
            specialKeys.push(39); //Right
            function IsAlphaNumeric(e) {
                var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
                var ret = ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
                document.getElementById("error").style.display = ret ? "none" : "inline";
                return ret;
            }
            function phonenumber() {
                var inputtxt = document.getElementById["text1"].toString();
                var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
                if (inputtxt.value.match(phoneno)) {
                    return true;
                }
                else {
                    alert("Not a valid Phone Number");
                    return false;
                }
            }  
        </script>

        <script type = "text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
    </script>
        <script type="text/javascript">
            var seconds = 01;
            function secondPassed() {

                var minutes = Math.round((seconds - 30) / 60);
                var remainingSeconds = seconds % 60;
                if (remainingSeconds < 10) {
                    remainingSeconds = "0" + remainingSeconds;
                }

                document.getElementById('countdown').innerHTML = minutes + ":" + remainingSeconds;

                if (seconds == 0) {
                    clearInterval(countdownTimer);
                    document.getElementById('countdown').innerHTML = "Buzz Buzz";
                } else {
                    seconds++;
                }
            }

            var countdownTimer = setInterval('secondPassed()', 1000);
    </script>
        <div id="div_title" runat="server">
            <asp:Panel ID="panel_title" runat="server">
                <table width="100%" style="border-style: double; background-color: #999966;">
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lbl_Ordertext" runat="server" Text="Order No :" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_orderno" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:Label ID="LblDate_text" runat="server" Text="assign date" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="LblDate" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lbl_processnametext" runat="server" Text="Process By" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_processname" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:Label ID="lbl_pros" runat="server" Text="Process Name" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_pros_name" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="lbl_searchtext" runat="server" Text="Search BY" Visible="False" ForeColor="#993333"></asp:Label><asp:Label
                                ID="lbl_search" runat="server" Font-Overline="False" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:Label ID="lbl_searchqctext" runat="server" Text="Search QC BY" Visible="False"
                                ForeColor="#993333"></asp:Label><asp:Label ID="lbl_searchqc" runat="server" Font-Overline="False"
                                    Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lbl_keyingtext" runat="server" Text="Keying BY" Visible="False" ForeColor="#993333"></asp:Label><asp:Label
                                ID="lbl_keying" runat="server" Font-Overline="False" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                         <td class="style7">
                          
                         </td> 
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div id="div_keys" runat="server">
            <asp:Panel ID="panel_menu" runat="server">
                <asp:Menu ID="Menu1" Width="100%" runat="server" Orientation="Horizontal" BackColor="#B5C7DE"
                    DynamicHorizontalOffset="20" Font-Names="Verdana" Font-Size="20px" ForeColor="White"
                    StaticSubMenuIndent="20px" Height="30px" BorderStyle="None" DisappearAfter="100"
                    Font-Bold="True" Font-Underline="True" MaximumDynamicDisplayLevels="5" OnMenuItemClick="Menu1_MenuItemClick">
                    <DynamicHoverStyle BackColor="#B5C7DE" ForeColor="White" BorderStyle="None" />
                    <DynamicMenuItemStyle HorizontalPadding="50px" VerticalPadding="10px" BackColor="#CCCCFF" />
                    <DynamicMenuStyle BackColor="#B5C7DE" BorderStyle="None" />
                    <DynamicSelectedStyle BackColor="Silver" BorderStyle="None" ForeColor="White" />
                    <Items>
                        <asp:MenuItem Text="ORDER/OWNER" Value="0"></asp:MenuItem>
                        <asp:MenuItem Text="DEED" Value="1"></asp:MenuItem>
                        <asp:MenuItem Text="MORTGAGE" Value="2"></asp:MenuItem>
                        <asp:MenuItem Text="TAX INFORMATION" Value="3"></asp:MenuItem>
                    </Items>
                    <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                    <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                    <StaticSelectedStyle BackColor="Gray" BorderStyle="None" ForeColor="White" />
                </asp:Menu>
            </asp:Panel>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="Tab1" runat="server">
                    <center>
                        <asp:Panel ID="panel_client" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <center>
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <asp:Label ID="lbl_client_show" runat="server" Font-Size="Larger" ForeColor="#006600"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:Panel>
                    </center>
                    <center>
                        <asp:Panel ID="panel_owner" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <center>
                                <table width="70%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_search_date" runat="server" ForeColor="#000000" Text="SEARCH DATE:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_search_date" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_as_of_date" runat="server" ForeColor="#000000" Text="AS OF DATE:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_as_of_date" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:Panel>
                    </center>
                    <center>
                        <asp:Panel ID="panel_legalinfo" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_address" runat="server" Font-Bold="True" ForeColor="#000000" Text="ADDRESS:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_address" runat="server" TextMode="MultiLine" Height="70px" Width="900px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                    <center>
                        <asp:Panel ID="panel_client_save" runat="server" Width="95%">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_client_update" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Height="25px" Width="60px" Visible="False"
                                            ValidationGroup="gc" OnClick="btn_client_update_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_client_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Width="60px" ValidationGroup="gc" OnClick="btn_client_save_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_client_cancel" runat="server" Text="Cancel" BackColor="#669999"
                                            BorderStyle="None" ForeColor="White" Height="25px" Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                </asp:View>
                <asp:View ID="Tab2" runat="server">
                    <center>
                        <asp:Panel ID="panel_deed" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_deed_type" CssClass="stylecolor" runat="server">DEED TYPE:</asp:Label>
                                        <asp:TextBox ID="txt_deed_type" runat="server" CssClass="styletextbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_deed_type"
                                            ErrorMessage="Please Fill this field" Font-Size="Large" ValidationGroup="gd"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                        <asp:DropDownList ID="drp_deed" runat="server" AutoPostBack="True" BackColor="Gray"
                                            ForeColor="White" OnSelectedIndexChanged="drp_deed_SelectedIndexChanged">
                                            <asp:ListItem>CURRENT DEED RECORD</asp:ListItem>
                                            <asp:ListItem>PRIOR DEED RECORD</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_deed_tableno" runat="server" Width="40px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="style4" align="left">
                                        <asp:Label ID="lbl_deed_grantor" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="GRANTEE  :"></asp:Label>
                                        <asp:TextBox ID="txt_deed_grantor" runat="server" Width="390px" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_deed_grantee" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="GRANTOR  :"></asp:Label>
                                        <asp:TextBox ID="txt_deed_grantee" runat="server" ValidationGroup="deed" Width="390px"
                                            CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbl_deed_dated" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txt_deed_dated" runat="server" Width="118px" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_deed_recorded" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="RECORDED"></asp:Label>
                                        <asp:TextBox ID="txt_deed_recorded" runat="server" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_deed_book" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="BOOK"></asp:Label>
                                        <asp:TextBox ID="txt_deed_book" runat="server" Width="50px" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_deed_pg" runat="server" Font-Bold="True" ForeColor="#000000" Text="PAGE"></asp:Label>
                                        <asp:TextBox ID="txt_deed_pg" runat="server" Width="50px" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_deed_legal" runat="server" Font-Bold="True" ForeColor="#000000"
                                            Text="LEGAL :"></asp:Label>
                                        <asp:TextBox ID="txt_deed_legal" runat="server" Height="56px" TextMode="MultiLine"
                                            CssClass="textboxupper" Width="590px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btn_save_deedupdate" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Width="60px" Height="25px" ValidationGroup="dg"
                                            Visible="False" OnClick="btn_save_deedupdate_Click" />
                                        <asp:Button ID="btn_save_wardeed" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Save" ValidationGroup="dg" Width="60px"
                                            OnClick="btn_save_wardeed_Click" />
                                        <asp:Button ID="btn_deed_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Cancel" Width="60px" OnClick="btn_deed_cancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd_deed" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="grd_deed_RowDataBound"
                                            OnRowDeleting="grd_deed_RowDeleting" OnSelectedIndexChanged="grd_deed_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="#FFCC99" BorderStyle="None" ForeColor="Black" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Link" HeaderText="select" SelectText="Edit" ShowHeader="True"
                                                    ShowSelectButton="True" />
                                                <asp:CommandField ButtonType="Link" HeaderText="DELETE" ShowDeleteButton="True" />
                                            </Columns>
                                            <HeaderStyle BackColor="#006666" BorderStyle="None" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                    <br />
                </asp:View>
                <asp:View ID="Tab3" runat="server">
                    <center>
                        <asp:Panel ID="Panel_Mortgage" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_mortgager" CssClass="stylecolor" runat="server" Text="MRTGAGOR:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_mortgager" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_mortgagee" CssClass="stylecolor" runat="server" Text="MORTGAGEE:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_mortgagee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_mrg_dated" runat="server" CssClass="stylecolor" Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_dated" runat="server" Width="118px" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_recorded" runat="server" CssClass="stylecolor" Text="RECORDED:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_recorded" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_book" runat="server" CssClass="stylecolor" Text="BOOK"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_book" runat="server"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_pg" runat="server" CssClass="stylecolor" Text="PG."></asp:Label>
                                        <asp:TextBox ID="txt_mrg_pg" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_mrg_amount" CssClass="stylecolor" runat="server" Text="AMOUNT:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_amount" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_mrg_opndate" runat="server" CssClass="stylecolor" Text="OPEN END MORTGAGE"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_opndate" runat="server" Height="20px" TextMode="MultiLine"
                                            Width="590px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btn_mrg_update" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Update" Width="60px" Visible="False" ValidationGroup="gm"
                                            OnClick="btn_mrg_update_Click" />
                                        <asp:Button ID="btn_mrg_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Width="60px" Height="25px" ValidationGroup="gm" OnClick="btn_mrg_save_Click" />
                                        <asp:Button ID="btn_mrg_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Cancel" Width="60px" OnClick="btn_mrg_cancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd_mortgage" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="grd_mortgage_RowDataBound"
                                            OnSelectedIndexChanged="grd_mortgage_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="#FFCC99" BorderStyle="None" ForeColor="Black" />
                                            <Columns>
                                                <asp:CommandField ButtonType="Link" SelectText="EDIT" ShowSelectButton="True" />
                                                <asp:CommandField ButtonType="Link" ShowDeleteButton="True" />
                                            </Columns>
                                            <HeaderStyle BackColor="#006666" BorderStyle="None" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                </asp:View>
                <asp:View ID="Tab4" runat="server">
                    <center>
                        <asp:Panel ID="panel_tax" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td>
                                        TAX ASSESSMENT:
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_tax_land" CssClass="stylecolor" runat="server" Text="LAND: $"></asp:Label>
                                        <asp:TextBox ID="txt_tax_land" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_tax_building" CssClass="stylecolor" runat="server" Text="BUILDING: $"></asp:Label>
                                        <asp:TextBox ID="txt_tax_building" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_tax_total" CssClass="stylecolor" runat="server" Text="TOTAL: $"></asp:Label>
                                        <asp:TextBox ID="txt_tax_total" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_tax_idno" CssClass="stylecolor" runat="server" Text="TAX ID NUMBER:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_idno" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="left">
                                        <asp:Label ID="lbl_tax_2015_paid" CssClass="stylecolor" runat="server" Text="2015 TAXES PAID IN THE AMOUNT OF:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_2015_paid" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="lbl_tax_2015_on" CssClass="stylecolor" runat="server" Text="ON:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_2015_on" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_tax_next_due" CssClass="stylecolor" runat="server" Text="NEXT TAXES DUE ON:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_next_due" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_tax_all_pre" CssClass="stylecolor" runat="server" Text="ALL PREVIOUS TAXES PAID:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_all_pre" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_tax_home" CssClass="stylecolor" runat="server" Text="HOMESTEAD EXEMPTION:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_home" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_tax_water" CssClass="stylecolor" runat="server" Text="WATERFRONT PROPERTY:"></asp:Label>
                                        <asp:TextBox ID="txt_tax_water" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="5">
                                        <asp:Button ID="btn_tax_update" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Width="60px" Height="25px" Visible="False"
                                            OnClick="btn_tax_update_Click" />
                                        <asp:Button ID="btn_tax_save" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Save" ValidationGroup="gj" Width="60px"
                                            OnClick="btn_tax_save_Click" />
                                        <asp:Button ID="btn_tax_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" Text="Cancel" Width="60px" OnClick="btn_tax_cancel_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd_tax" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="grd_tax_RowDataBound"
                                            OnSelectedIndexChanged="grd_tax_SelectedIndexChanged">
                                            <AlternatingRowStyle BackColor="#FFCC99" BorderStyle="None" ForeColor="Black" />
                                            <Columns>
                                                <asp:CommandField SelectText="EDIT" ShowSelectButton="True" ButtonType="Link" />
                                                <asp:CommandField ShowDeleteButton="True" ButtonType="Link" />
                                            </Columns>
                                            <HeaderStyle BackColor="#006666" BorderStyle="None" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                </asp:View>
            </asp:MultiView>
        </div>
        <div id="div_cmd" runat="server">
            <center>
                <asp:Panel ID="panel_comments" runat="server" Width="95%">
                    <br />
                    <table width="70%" style="background-color: #999966">
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_search_comments" runat="server" Text="Search comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_search_comments" runat="server" Width="680px" CssClass="textboxupper"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_searchqc_comments" runat="server" Text="Search-QC comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_searchqc_comments" runat="server" Width="680px" CssClass="textboxupper"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_keying_comments" runat="server" Text="Keying comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_keying_commend" runat="server" Width="680px" CssClass="textboxupper"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbl_qc_comments" runat="server" Text="QC comments"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txt_qc_comments" runat="server" Width="680px" CssClass="textboxupper"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btn_order_save" runat="server" Text="SAVE" OnClick="btn_order_save_Click" />
                                <asp:Button ID="btn_complete" runat="server" Text="Complete" OnClick="btn_complete_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="LblError" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="Yellow"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </asp:Panel>
            </center>
        </div>
    </div>
    <style type="text/css">
        .page
        {
            background-color: #fff;
            margin: 20px auto 0px auto;
            border: 3px solid #496077;
        }
        .style3
        {
            width: 107px;
        }
        .stylecolor
        {
            font-weight: bold;
            color: #000000;
            width: 75px;
        }
        .styletextbox
        {
            width: 400px;
            text-transform: uppercase;
        }
        
        .style4
        {
            height: 30px;
        }
        
        .style6
        {
            height: 23px;
        }
        
        .style7
        {
            height: 24px;
        }
    </style>

   
</asp:Content>
