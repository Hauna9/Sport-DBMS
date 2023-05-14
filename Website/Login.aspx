<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="M3.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 189px">
    <form id="form1" runat="server">
        <div>
            Please Login<br />
            <br />
            username:<br />
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
            <br />
            password:<br />
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="log" runat="server" OnClick="login" Text="Login" />
        &nbsp;&nbsp;Register As&nbsp;
            <asp:DropDownList ID="Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="registerType">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem>Sports Association Manager</asp:ListItem>
                <asp:ListItem>Club Representative</asp:ListItem>
                <asp:ListItem>Stadium Manager</asp:ListItem>
                <asp:ListItem>Fan</asp:ListItem>
            </asp:DropDownList>
         </div>
    </form>
</body>
</html>





