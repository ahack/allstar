<%@ Page Title="" Language="C#" MasterPageFile="~/Form/MasterPage2.master" AutoEventWireup="true"
    CodeFile="productionology.aspx.cs" Inherits="Form_productionology" Theme="Default" %>

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
        <div id="div_title" runat="server">
            <asp:Panel ID="panel_title" runat="server">
                <table width="100%" style="border-style: double; background-color: #999966;">
                    <tr>
                        <td class="style7">
                            <asp:Label ID="lbl_processnametext" runat="server" Text="Process By" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_processname" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lbl_Ordertext" runat="server" Text="Order No :" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_orderno" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style7">
                            <asp:Label ID="lbl_keyingtext" runat="server" Text="Keying BY" Visible="False" ForeColor="#993333"></asp:Label><asp:Label
                                ID="lbl_keying" runat="server" Font-Overline="False" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style6">
                            <asp:Label ID="LblDate_text" runat="server" Text="assign date" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="LblDate" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style6">
                            <asp:Label ID="lbl_pros" runat="server" Text="Process Name" ForeColor="#993333"></asp:Label>
                            <asp:Label ID="lbl_pros_name" runat="server" Font-Size="Large" Font-Underline="True"></asp:Label>
                        </td>
                        <td class="style6">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                <asp:Panel ID="panel_menu" runat="server">
                    <asp:Menu ID="Menu1" Width="100%" runat="server" Orientation="Horizontal" OnMenuItemClick="Menu1_MenuItemClick"
                        BackColor="Black" DynamicHorizontalOffset="20" Font-Names="Verdana" Font-Size="20px"
                        ForeColor="White" StaticSubMenuIndent="20px" Height="30px" BorderStyle="None"
                        DisappearAfter="100" Font-Bold="True" Font-Underline="True" MaximumDynamicDisplayLevels="5">
                        <DynamicHoverStyle BackColor="Silver" ForeColor="White" BorderStyle="None" />
                        <DynamicMenuItemStyle HorizontalPadding="50px" VerticalPadding="10px" BackColor="#CCCCFF" />
                        <DynamicMenuStyle BackColor="#B5C7DE" BorderStyle="None" />
                        <DynamicSelectedStyle BackColor="Silver" BorderStyle="None" ForeColor="White" />
                        <Items>
                            <asp:MenuItem Text="CLIENT/OWNER" Value="0"></asp:MenuItem>
                            <asp:MenuItem Text="TAX ASSESSMENT" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="DEED" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="MORTGAGE" Value="3"></asp:MenuItem>
                            <asp:MenuItem Text="JUDGMENT" Value="4"></asp:MenuItem>
                            <asp:MenuItem Text="OTHERS" Value="5"></asp:MenuItem>
                            <asp:MenuItem Text="PREVIEW" Value="6"></asp:MenuItem>
                            <asp:MenuItem Text="COMMENTS" Value="7"></asp:MenuItem>
                        </Items>
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                        <StaticSelectedStyle BackColor="Gray" BorderStyle="None" ForeColor="White" />
                    </asp:Menu>
                </asp:Panel>
            </div>
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
                                <table width="50%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_client" runat="server" Text="Client: " ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_client" runat="server" TabIndex="1" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_date" runat="server" Text="Date:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_date" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_address" runat="server" Text="Address:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_address" runat="server" TabIndex="2" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_order" runat="server" Text="Order:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_orderno" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_ctyStZip" runat="server" Text="City/St/Zip:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_city_zip" runat="server" TabIndex="3" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_ref" runat="server" Text="Ref#:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_ref" runat="server" CssClass="textboxupper" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_attention" runat="server" Text="Attention:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_attention" runat="server" TabIndex="4" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_certdate" runat="server" Text="Certification Date:" ForeColor="#000000"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_certdate" runat="server" CssClass="textboxupper" TabIndex="6"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="vali_certi_date" runat="server" ControlToValidate="txt_certdate"
                                                ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                                ValidationGroup="gc"></asp:RegularExpressionValidator>
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
                                            <asp:Label ID="lbl_owner" runat="server" ForeColor="#000000" Text="Owner:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_owner" runat="server" TabIndex="7" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_state" runat="server" ForeColor="#000000" Text="State"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_state" runat="server" TabIndex="10" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_prpadd" runat="server" ForeColor="#000000" Text="Property Address:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_propaddress" runat="server" TabIndex="8" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_zip" runat="server" ForeColor="#000000" Text="Zip"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_zip" runat="server" TabIndex="11" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lbl_city" runat="server" ForeColor="#000000" Text="City:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_city" runat="server" TabIndex="9" CssClass="textboxupper"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_county" runat="server" ForeColor="#000000" Text="County:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_county" runat="server" TabIndex="12" CssClass="textboxupper"></asp:TextBox>
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
                                        <asp:Label ID="Label5" runat="server" Font-Bold="True" ForeColor="#000000" Text="LEGAL DESCRIPTION:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_legalinfo" runat="server" TextMode="MultiLine" Height="70px"
                                            Width="900px" CssClass="textboxupper" TabIndex="13"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                    <center>
                        <asp:Panel ID="panel1" runat="server" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px"
                            Width="95%">
                            <table width="70%">
                                <tr>
                                    <td class="style3">
                                        <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="#000000" Text="OWNER OF RECORD:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_ownerofrec" runat="server" TextMode="MultiLine" Height="20px"
                                            Width="900px" CssClass="textboxupper" TabIndex="14"></asp:TextBox>
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
                                            BorderStyle="None" ForeColor="White" Height="25px" Width="60px" OnClick="btn_client_update_Click"
                                            Visible="False" ValidationGroup="gc" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_client_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Width="60px" OnClick="btn_client_save_Click"
                                            ValidationGroup="gc" />
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
                        <asp:Panel ID="panel_info" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="100%">
                                <tr>
                                    <td align="center">
                                        <asp:Label ID="lbl_assess_show" runat="server" Font-Size="Larger" ForeColor="#006600"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <table width="70%">
                                <tr>
                                    <td align="left" colspan="6">
                                        TAX ASSESSMENT INFORMATION
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_parcelid" runat="server" Text="PARCEL ID:" Font-Bold="True" ForeColor="#000000"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_parcelid" runat="server" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_taxyear" runat="server" ForeColor="#000000" Text="TAX YEAR:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_taxyear" runat="server" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_labd" runat="server" Text="LAND: $" Font-Bold="True" ForeColor="#000000"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_land" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_tax_land" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_land" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gt" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_county1" runat="server" ForeColor="#000000" Text="IMPROVEMENTS: $"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_improv" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_tax_improv" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_improv" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gt" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_county2" runat="server" ForeColor="#000000" Text="TOTAL: $" Font-Bold="True"></asp:Label>
                                        <asp:TextBox ID="txt_total" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_tax_total" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_total" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gt" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Text="TAXES: $" Font-Bold="True" ForeColor="#000000"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_taxes" runat="server" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_tax_taxes" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_taxes" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gt" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_county3" runat="server" ForeColor="#000000" Text="DUE/PAID:" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_duepaid" runat="server" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="NOTES:" Font-Bold="True" ForeColor="#000000"></asp:Label>
                                    </td>
                                    <td colspan="5" align="left">
                                        <asp:TextBox ID="txt_assessnotes" runat="server" TextMode="MultiLine" Width="600px"
                                            Height="30px" CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                    <br />
                    <center>
                        <asp:Panel ID="panel_saveassess" runat="server" Width="95%">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btn_assess_update" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Height="25px" Width="60px" OnClick="btn_assess_update_Click"
                                            Visible="False" ValidationGroup="gt" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_assess_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" Width="60px" OnClick="btn_assess_save_Click"
                                            ValidationGroup="gt" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn_assess_cancel" runat="server" Text="Cancel" BackColor="#669999"
                                            BorderStyle="None" ForeColor="White" Height="25px" Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </center>
                </asp:View>
                <asp:View ID="Tab3" runat="server">
                    <center>
                        <asp:Panel ID="panel_warrentydeed" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_deed_type" runat="server" Text="Deed Type"></asp:Label>
                                        <asp:TextBox ID="txt_deed_type" runat="server" CssClass="textboxupper" Width="390px"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" BackColor="White"
                                            BorderStyle="Ridge" ControlToValidate="txt_deed_type" ErrorMessage="Deed Type is missing"
                                            Font-Bold="True" Font-Size="Large" ForeColor="Red" ValidationGroup="dg" BorderColor="#CC6699"
                                            Width="180px"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txt_deed_tableno" runat="server" Width="40px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="style4" align="left">
                                        <asp:Label ID="lblgrantee" runat="server" Font-Bold="True" ForeColor="#000000" Text="GRANTEE  :"></asp:Label>
                                        <asp:TextBox ID="txtgrantee" runat="server" Width="390px" OnTextChanged="txtgrantee_TextChanged"
                                            CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lblgrantor" runat="server" Font-Bold="True" ForeColor="#000000" Text="GRANTOR  :"></asp:Label>
                                        <asp:TextBox ID="txtgrantor" runat="server" ValidationGroup="deed" Width="390px"
                                            CssClass="textboxupper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lbldated" runat="server" Font-Bold="True" ForeColor="#000000" Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txtdated" runat="server" Width="118px" CssClass="textboxupper" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_deed_date" runat="server" ControlToValidate="txtdated"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="dg"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblfield" runat="server" Font-Bold="True" ForeColor="#000000" Text="FILED"></asp:Label>
                                        <asp:TextBox ID="txtfield" runat="server" CssClass="textboxupper" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_deed_filed" runat="server" ControlToValidate="txtfield"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="dg"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblvol" runat="server" Font-Bold="True" ForeColor="#000000" Text="VOL."></asp:Label>
                                        <asp:TextBox ID="txtvol" runat="server" Width="50px" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_deed_vol" runat="server" ErrorMessage="**"
                                            ControlToValidate="txtvol" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="dg"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblpg" runat="server" Font-Bold="True" ForeColor="#000000" Text="PG."></asp:Label>
                                        <asp:TextBox ID="txtpg" runat="server" Width="50px" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_deed_pg" runat="server" ErrorMessage="**"
                                            ControlToValidate="txtpg" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="dg"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblinst" runat="server" Font-Bold="True" ForeColor="#000000" Text="INST."></asp:Label>
                                        <asp:TextBox ID="txtinst" runat="server" Width="118px" CssClass="textboxupper"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_deed_inst" runat="server" ErrorMessage="**"
                                            ControlToValidate="txtinst" ValidationExpression="^[a-zA-Z0-9-]+$" ValidationGroup="dg"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblnotes" runat="server" Font-Bold="True" ForeColor="#000000" Text="NOTES  :"></asp:Label>
                                        <asp:TextBox ID="txtnotes" runat="server" Height="56px" TextMode="MultiLine" CssClass="textboxupper"
                                            Width="590px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btn_save_deedupdate" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Width="60px" Height="25px" OnClick="btn_save_deedupdate_Click"
                                            ValidationGroup="dg" Visible="False" />
                                        <asp:Button ID="btn_save_wardeed" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_save_wardeed_Click" Text="Save"
                                            ValidationGroup="dg" Width="60px" />
                                        <asp:Button ID="btn_deed_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_deed_cancel_Click" Text="Cancel"
                                            Width="60px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound"
                                            OnRowDeleting="GridView1_RowDeleting" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
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
                <asp:View ID="Tab4" runat="server">
                    <center>
                        <asp:Panel ID="Panel_Mortgage" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_mortgage" CssClass="stylecolor" runat="server">Mortgage Type:</asp:Label>
                                        <asp:TextBox ID="txt_mrg_type" runat="server" CssClass="styletextbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_mrg_type"
                                            ErrorMessage="Please Fill this field" Font-Size="Large" ValidationGroup="gm"></asp:RequiredFieldValidator>
                                    </td>
                                    <td align="right">
                                        <asp:DropDownList ID="drp_mortgage" runat="server" AutoPostBack="True" BackColor="Gray"
                                            ForeColor="White" OnSelectedIndexChanged="drp_mortgage_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_mrg_tableno" runat="server" Width="40px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_assignee" CssClass="stylecolor" runat="server" Text="ASSIGNEE:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_assignee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_assignor" CssClass="stylecolor" runat="server" Text="ASSIGNOR:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_assignor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_appointed" CssClass="stylecolor" runat="server" Text="APPOINTED:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_appointed" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_exeby" CssClass="stylecolor" runat="server" Text="EXECUTED BY:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_exeby" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_lender" CssClass="stylecolor" runat="server" Text="LENDER:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_lender" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_grantor" CssClass="stylecolor" runat="server" Text="GRANTOR:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_grantor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_payableto" CssClass="stylecolor" runat="server" Text="PATABLE TO:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_payableto" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_trustee" CssClass="stylecolor" runat="server" Text="TRUSTEE:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_trustee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_secparty" CssClass="stylecolor" runat="server" Text="SECURED PARTY:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_secparty" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_debtor" CssClass="stylecolor" runat="server" Text="DEBTOR:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_debtor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_mrg_byandbeet" CssClass="stylecolor" runat="server" Text="BY AND BETWEEN:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_byandbeet" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_mrg_dated" runat="server" CssClass="stylecolor" Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_dated" runat="server" Width="118px" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_dated" runat="server" ControlToValidate="txt_mrg_dated"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="gm"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_filed" runat="server" CssClass="stylecolor" Text="FILED"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_filed" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_filed" runat="server" ControlToValidate="txt_mrg_filed"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="gm"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_vol" runat="server" CssClass="stylecolor" Text="VOL."></asp:Label>
                                        <asp:TextBox ID="txt_mrg_vol" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_vol" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_mrg_vol" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="gm"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_pg" runat="server" CssClass="stylecolor" Text="PG."></asp:Label>
                                        <asp:TextBox ID="txt_mrg_pg" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_pg" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_mrg_pg" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="gm"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_mrg_inst" runat="server" CssClass="stylecolor" Text="INST."></asp:Label>
                                        <asp:TextBox ID="txt_mrg_inst" runat="server" Width="118px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_inst" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_mrg_inst" ValidationExpression="^[a-zA-Z0-9-]+$" ValidationGroup="gm"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" align="left">
                                        <asp:Label ID="lbl_mrg_amount" CssClass="stylecolor" runat="server" Text="AMOUNT:"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_amount" runat="server" CssClass="styletextbox"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_mrg_amount" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_mrg_amount" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gm" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_mrg_notes" runat="server" CssClass="stylecolor" Text="NOTES  :"></asp:Label>
                                        <asp:TextBox ID="txt_mrg_notes" runat="server" Height="20px" TextMode="MultiLine"
                                            Width="590px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btn_mrg_update" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_mrg_update_Click" Text="Update"
                                            Width="60px" Visible="False" ValidationGroup="gm" />
                                        <asp:Button ID="btn_mrg_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Width="60px" Height="25px" OnClick="btn_mrg_save_Click" ValidationGroup="gm" />
                                        <asp:Button ID="btn_mrg_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_mrg_cancel_Click" Text="Cancel"
                                            Width="60px" />
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
                                            OnRowDeleting="grd_mortgage_RowDeleting" OnSelectedIndexChanged="grd_mortgage_SelectedIndexChanged">
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
                <asp:View ID="Tab5" runat="server">
                    <center>
                        <asp:Panel ID="panel_judgment" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_judgement" CssClass="stylecolor" runat="server">Judgement Type:</asp:Label>
                                        <asp:TextBox ID="txt_judg_type" runat="server" CssClass="styletextbox"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_judg_type"
                                            ErrorMessage="Please fill this field" ValidationGroup="gj"></asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drp_judgement" runat="server" AutoPostBack="True" BackColor="Gray"
                                            ForeColor="White" OnSelectedIndexChanged="drp_judgement_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_judg_tableno" runat="server" Width="40px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_taxpayer" CssClass="stylecolor" runat="server" Text="TAXPAYER:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_taxpayer" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_defendant" CssClass="stylecolor" runat="server" Text="DEFENDANT:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_defendant" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_taxpayerid" CssClass="stylecolor" runat="server" Text="TAXPAYER ID :"></asp:Label>
                                        <asp:TextBox ID="txt_judg_taxpayerid" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_address" CssClass="stylecolor" runat="server" Text="DEFENDANT ADDRESS:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_address" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_plaintiff" CssClass="stylecolor" runat="server" Text="PLAINTIFF:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_plaintiff" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_owner" CssClass="stylecolor" runat="server" Text="OWNER:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_owner" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_grantor" CssClass="stylecolor" runat="server" Text="GRANTOR:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_grantor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_grantee" CssClass="stylecolor" runat="server" Text="GRANTEE:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_grantee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_obligor" CssClass="stylecolor" runat="server" Text="OBLIGOR:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_obligor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_ssn" CssClass="stylecolor" runat="server" Text="SSN : "></asp:Label>
                                        <asp:TextBox ID="txt_judg_ssn" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_obligee" CssClass="stylecolor" runat="server" Text="OBLIGEE:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_obligee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_tribunal" CssClass="stylecolor" runat="server" Text="TRIBUNAL:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_tribunal" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_to" CssClass="stylecolor" runat="server" Text="TO : "></asp:Label>
                                        <asp:TextBox ID="txt_judg_to" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_from" CssClass="stylecolor" runat="server" Text="FROM : "></asp:Label>
                                        <asp:TextBox ID="txt_judg_from" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_judg_dated" runat="server" CssClass="stylecolor" Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_dated" runat="server" Width="118px" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_dated" runat="server" ControlToValidate="txt_judg_dated"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="gj"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_judg_filed" runat="server" CssClass="stylecolor" Text="FILED"></asp:Label>
                                        <asp:TextBox ID="txt_judg_filed" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_filed" runat="server" ControlToValidate="txt_judg_filed"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="gj"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_judg_vol" runat="server" CssClass="stylecolor" Text="VOL."></asp:Label>
                                        <asp:TextBox ID="txt_judg_vol" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_vol" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_judg_vol" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="gj"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_judg_pg" runat="server" CssClass="stylecolor" Text="PG."></asp:Label>
                                        <asp:TextBox ID="txt_judg_pg" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_pg" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_judg_pg" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="gj"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_judg_inst" runat="server" CssClass="stylecolor" Text="INST."></asp:Label>
                                        <asp:TextBox ID="txt_judg_inst" runat="server" Width="118px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_inst" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_judg_inst" ValidationExpression="^[a-zA-Z0-9-]+$" ValidationGroup="gj"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_judg_cost" runat="server" CssClass="stylecolor" Text="COST : $"></asp:Label>
                                        <asp:TextBox ID="txt_judg_cost" runat="server" Width="158px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_judg_atty" runat="server" CssClass="stylecolor" Text="ATTY: $"></asp:Label>
                                        <asp:TextBox ID="txt_judg_atty" runat="server" Width="158px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_judg_int" runat="server" CssClass="stylecolor" Text="INT:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_int" runat="server" Width="118px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_amount" CssClass="stylecolor" runat="server" Text="AMOUNT $:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_amount" runat="server" CssClass="styletextbox"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_judg_amount" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_judg_amount" ValidationExpression="^\$?([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9])?$"
                                            ValidationGroup="gj" Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_judg_cause" CssClass="stylecolor" runat="server" Text="CAUSE:"></asp:Label>
                                        <asp:TextBox ID="txt_judg_cause" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_judg_notes" runat="server" CssClass="stylecolor" Text="NOTES  :"></asp:Label>
                                        <asp:TextBox ID="txt_judg_notes" runat="server" Height="20px" TextMode="MultiLine"
                                            Width="590px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btn_judg_update" runat="server" Text="Update" BackColor="#0099CC"
                                            BorderStyle="None" ForeColor="White" Width="60px" Height="25px" OnClick="btn_judg_update_Click"
                                            Visible="False" />
                                        <asp:Button ID="btn_judg_save" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_judg_save_Click" Text="Save" ValidationGroup="gj"
                                            Width="60px" />
                                        <asp:Button ID="btn_judg_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_judg_cancel_Click" Text="Cancel"
                                            Width="60px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd__judgement" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="grd__judgement_RowDataBound"
                                            OnRowDeleting="grd__judgement_RowDeleting" OnSelectedIndexChanged="grd__judgement_SelectedIndexChanged">
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

                <asp:View ID="Tab6" runat="server">
                    <center>
                        <asp:Panel ID="panel_others" runat="server" BorderColor="Gray" BorderStyle="Groove"
                            BorderWidth="1px" Width="95%">
                            <table width="70%">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_others_type" runat="server" CssClass="stylecolor" Text="Others Type"></asp:Label>
                                        <asp:TextBox ID="txt_other_type" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drp_others" runat="server" AutoPostBack="True" BackColor="Gray"
                                            ForeColor="White" OnSelectedIndexChanged="drp_others_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txt_other_tableno" runat="server" Width="40px" ReadOnly="True" Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_grantee" CssClass="stylecolor" runat="server" Text="GRANTEE:"></asp:Label>
                                        <asp:TextBox ID="txt_other_grantee" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_to" CssClass="stylecolor" runat="server" Text="TO:"></asp:Label>
                                        <asp:TextBox ID="txt_other_to" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_grantor" CssClass="stylecolor" runat="server" Text="GRANTOR:"></asp:Label>
                                        <asp:TextBox ID="txt_other_grantor" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" class="style4">
                                        <asp:Label ID="lbl_other_petitioner" CssClass="stylecolor" runat="server" Text="PETITIONER:"></asp:Label>
                                        <asp:TextBox ID="txt_other_petitioner" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_respondent" CssClass="stylecolor" runat="server" Text="RESPONDENT:"></asp:Label>
                                        <asp:TextBox ID="txt_other_respondent" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_re" CssClass="stylecolor" runat="server" Text="RE:"></asp:Label>
                                        <asp:TextBox ID="txt_other_re" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_owner" CssClass="stylecolor" runat="server" Text="OWNER:"></asp:Label>
                                        <asp:TextBox ID="txt_other_owner" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_manufacturer" CssClass="stylecolor" runat="server" Text="MANUFACTURER:"></asp:Label>
                                        <asp:TextBox ID="txt_other_manufacturer" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lbl_others_dated" runat="server" CssClass="stylecolor" Text="DATED:"></asp:Label>
                                        <asp:TextBox ID="txt_others_dated" runat="server" Width="118px" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_others_dated" runat="server" ControlToValidate="txt_others_dated"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="go"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_others_filed" runat="server" CssClass="stylecolor" Text="FILED"></asp:Label>
                                        <asp:TextBox ID="txt_others_filed" runat="server" placeholder="MM/DD/YYYY"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_others_filed" runat="server" ControlToValidate="txt_others_filed"
                                            ErrorMessage="**" Font-Size="X-Large" ValidationExpression="^(((0?[1-9]|1[012])/(0?[1-9]|1\d|2[0-8])|(0?[13456789]|1[012])/(29|30)|(0?[13578]|1[02])/31)/(19|[2-9]\d)\d{2}|0?2/29/((19|[2-9]\d)(0[48]|[2468][048]|[13579][26])|(([2468][048]|[3579][26])00)))$"
                                            ValidationGroup="go"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_others_vol" runat="server" CssClass="stylecolor" Text="VOL."></asp:Label>
                                        <asp:TextBox ID="txt_others_vol" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_others_vol" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_others_vol" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="go"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_others_pg" runat="server" CssClass="stylecolor" Text="PG."></asp:Label>
                                        <asp:TextBox ID="txt_others_pg" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_others_pg" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_others_pg" ValidationExpression="^[a-zA-Z0-9]+$" ValidationGroup="go"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_others_inst" runat="server" CssClass="stylecolor" Text="INST."></asp:Label>
                                        <asp:TextBox ID="txt_others_inst" runat="server" Width="118px"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="vali_others_inst" runat="server" ErrorMessage="**"
                                            ControlToValidate="txt_others_inst" ValidationExpression="^[a-zA-Z0-9-]+$" ValidationGroup="go"
                                            Font-Size="X-Large"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lbl_others_notes" runat="server" CssClass="stylecolor" Text="NOTES  :"></asp:Label>
                                        <asp:TextBox ID="txt_others_notes" runat="server" Height="20px" TextMode="MultiLine"
                                            Width="590px"></asp:TextBox>
                                    </td>
                                    <td align="center">
                                        <asp:Button ID="btn_others_update" runat="server" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_others_update_Click" Text="Update"
                                            Width="60px" Visible="False" ValidationGroup="go" />
                                        <asp:Button ID="btn_others_save" runat="server" Text="Save" BackColor="#0099CC" BorderStyle="None"
                                            ForeColor="White" Width="60px" Height="25px" OnClick="btn_others_save_Click"
                                            ValidationGroup="go" />
                                        <asp:Button ID="btn_others_cancel" runat="server" BackColor="#669999" BorderStyle="None"
                                            ForeColor="White" Height="25px" OnClick="btn_others_cancel_Click" Text="Cancel"
                                            Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:Label ID="lbl_other_cause" CssClass="stylecolor" runat="server" Text="CAUSE:"></asp:Label>
                                        <asp:TextBox ID="txt_other_cause" runat="server" CssClass="styletextbox"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="70%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grd_others" runat="server" BackColor="#66CCFF" BorderColor="#669999"
                                            BorderStyle="None" Font-Size="Large" ForeColor="Black" DataKeyNames="ID" OnRowDataBound="grd_others_RowDataBound"
                                            OnRowDeleting="grd_others_RowDeleting" OnSelectedIndexChanged="grd_others_SelectedIndexChanged">
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

                <asp:View ID="Tab7" runat="server">
                    <center>
                        <asp:Panel ID="panelpreview" runat="server" Width="95%">
                            <br />
                            <table width="70%" style="background-color: #999966">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gridpreview" runat="server" AutoGenerateColumns="false" OnSelectedIndexChanged="gridpreview_SelectedIndexChanged"
                                            Width="100%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                <asp:BoundField DataField="Header" HeaderText="Header" />
                                                <asp:BoundField DataField="Book" HeaderText="Book" />
                                                <asp:BoundField DataField="Page" HeaderText="Page" />
                                                <asp:BoundField DataField="InstrumentNo" HeaderText="InstrumentNo" />
                                                <asp:TemplateField HeaderText="Sequence">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtsequence" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="vali_seq" runat="server" ErrorMessage="**" ControlToValidate="txtsequence"
                                                            Font-Size="Large" ValidationGroup="gs"></asp:RequiredFieldValidator>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="7">
                                        <asp:Button ID="btnsquenceupdate" runat="server" Text="SequenceUpdate" OnClick="btnsquenceupdate_Click"
                                            ValidationGroup="gs" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                    </center>
                </asp:View>
                <asp:View ID="Tab8" runat="server">
                    <center>
                        <asp:Panel ID="panel_comments" runat="server" Width="95%">
                            <br />
                            <table width="70%" style="background-color: #999966">
                                <tr>
                                    <td colspan="2">
                                        THE FOLLOWING NAMES HAVE BEEN SEARCHED FOR ABSTRACTS OF JUDGMENT, DEPARTMENT OF
                                        JUSTICE LIENS, STATE TAX LIENS, FEDERAL TAX LIENS, CHILD SUPPORT LIENS AND LIS PENDENS:
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txt_declaration" runat="server" Width="700px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                        <asp:Button ID="btnpreview" runat="server" Text="Preview" OnClick="btnpreview_Click"
                                            Visible="False" /><asp:Button ID="btn_order_save" runat="server" Text="SAVE" OnClick="btn_order_save_Click" />
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
                </asp:View>
            </asp:MultiView>
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
