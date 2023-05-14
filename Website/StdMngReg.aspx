<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdMngReg.aspx.cs" Inherits="M3.StdMngReg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Stadium Manager Registeration<br />
            <br />
            Name:<asp:TextBox ID="Mng_name" runat="server"></asp:TextBox>
            <br />
            Username:<asp:TextBox ID="Mng_user" runat="server"></asp:TextBox>
            <br />
            Password:<asp:TextBox ID="Mng_pass" runat="server"></asp:TextBox>
            <br />
            Stadium Name:<asp:TextBox ID="St_name" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="StdMng_Reg" runat="server" Text="Register" OnClick="StdMng_Reg_Click" />
        </div>
    </form>
</body>
</html>
