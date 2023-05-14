<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssocMngReg.aspx.cs" Inherits="M3.AssocMngReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Enter your name:<br />
            <asp:TextBox ID="name" runat="server"></asp:TextBox>
            <br />
            Choose a username:<br />
            <asp:TextBox ID="username" runat="server"></asp:TextBox>
            <br />
            Choose a password: (min 8 characters, alphanumeric)<br />
            <asp:TextBox ID="password" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="register" runat="server" Text="Register" OnClick="register_Click" style="margin-bottom: 0px" />
            <br />
        </div>
    </form>
</body>
</html>