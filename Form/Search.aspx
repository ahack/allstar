<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Form_Search" Theme="Default" Title="Search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .highlight 
        {
            text-decoration:none; font-weight:bold;
            color:black; background:yellow;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridSearch" runat="server" ShowHeader="false" SkinID="GridUser1" Font-Size="14px"> 
            <Columns>
                <asp:TemplateField HeaderText="FirstName">
                    <ItemTemplate><%# Highlight(Eval("Content").ToString())%>'</ItemTemplate>
                </asp:TemplateField>      
            </Columns> 
        </asp:GridView>
    </div>
    </form>
</body>
</html>
