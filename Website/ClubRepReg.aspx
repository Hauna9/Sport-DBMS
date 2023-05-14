<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClubRepReg.aspx.cs" Inherits="M3.ClubRepReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Club Representative Registeration<br />
            <br />
            Name:<asp:TextBox ID="CR_name" runat="server"></asp:TextBox>
            <br />
            Username:<asp:TextBox ID="CR_username" runat="server"></asp:TextBox>
            <br />
            Password:<asp:TextBox ID="CR_pass" runat="server"></asp:TextBox>
            <br />
            Club name:<asp:TextBox ID="CR_club" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Club_Rep_Reg" runat="server" Text="Register" OnClick="Club_Rep_Reg_Click" />
        </div>
    </form>
</body>
</html>
