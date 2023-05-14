<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemAdminActions.aspx.cs" Inherits="M3.SystemAdminActions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            SYSTEM ADMIN
            <br />
&nbsp;<br />
            <br />
            Add Club&nbsp;&nbsp;
            <br />
&nbsp;Club Name:<asp:TextBox ID="C_name_add" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Club Location:<asp:TextBox ID="C_loc_add" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Add_C" runat="server" Text="Confirm Adding Club" OnClick="Add_C_Click" />
            <br />
            <br />
            <br />
            <br />
            Delete Club<br />
            Club Name:<asp:TextBox ID="C_name_del" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Del_C" runat="server" Text="Confirm Deleting Club" OnClick="Del_C_Click" />
            <br />
            <br />
            <br />
            <br />
            Add Stadium<br />
            Stadium Name:&nbsp;&nbsp;&nbsp; <asp:TextBox ID="St_name_add" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp; Stadium Location:<asp:TextBox ID="St_loc_add" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            Stadium Capacity:<asp:TextBox ID="St_cap_add" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Add_St" runat="server" Text="Confirm Adding Stadium" OnClick="Add_St_Click" />
            <br />
            <br />
            <br />
            <br />
            Delete Stadium<br />
            Stadium Name:<asp:TextBox ID="St_name_del" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Del_St" runat="server" Text="Confirm Deleting Stadium" OnClick="Del_St_Click" />
            <br />
            <br />
            <br />
            <br />
            Block Fan<br />
            Fan National ID:<asp:TextBox ID="fan_id_blk" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Block_fan" runat="server" Text="Confirm Blocking Fan" OnClick="Block_fan_Click" />
            <br />
            <br />
            <br />
            <br />
            Unblock Fan<br />
            Fan National ID:<asp:TextBox ID="fan_id_unblk" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Unblock_fan" runat="server" Text="Confirm Unblocking Fan" OnClick="Unblock_fan_Click" />
            <br />
        </div>
    </form>
</body>
</html>
