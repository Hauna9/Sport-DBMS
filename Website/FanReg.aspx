<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FanReg.aspx.cs" Inherits="M3.FanReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Fan Registration<br />
            <br />
            Name:<asp:TextBox ID="f_name" runat="server"></asp:TextBox>
            <br />
            Username:<asp:TextBox ID="f_user" runat="server"></asp:TextBox>
            <br />
            Password:<asp:TextBox ID="f_pass" runat="server"></asp:TextBox>
            <br />
            National ID:<asp:TextBox ID="f_id" runat="server"></asp:TextBox>
            <br />
            Phone number:<asp:TextBox ID="f_num" runat="server"></asp:TextBox>
            <br />
            Birthdate:<asp:TextBox ID="f_bd"  textmode="Date"  runat="server"></asp:TextBox>
            <br />
            Address:<asp:TextBox ID="f_address" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="f_reg" runat="server" Text="Register" OnClick="f_reg_Click" />
        </div>
    </form>
</body>
</html>
