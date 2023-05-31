<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="log.aspx.vb" Inherits="Pico_Web.log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
    <asp:GridView ID="GridView1" runat="server" RowStyle-Height="100%" CellPadding="4" GridLines="None"  Width="90%">
                <AlternatingRowStyle BackColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

<HeaderStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" HorizontalAlign="Left"></HeaderStyle>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>   

        </div>
    </form>
</body>
</html>
