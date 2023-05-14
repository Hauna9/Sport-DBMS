<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StdMng.aspx.cs" Inherits="M3.StdMng" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:Button ID="viewStadiumInfo" runat="server" OnClick="ViewStadiumInfo_Click" Text="View Stadium Info" />
            <br />
            <asp:GridView ID="gridViewStadiumInfo" runat="server">
            </asp:GridView>
            <br />
            <asp:Button ID="viewAllRequests" runat="server" OnClick="ViewAllRequests_Click" Text="View All Requests" />
            <br />
     
            <asp:GridView ID="gridViewAllRequests" runat="server">
            </asp:GridView>
            <br />
            *Input to accept/reject request*<br />
            Input host club name<br />
            <br />
            <asp:TextBox ID="hostAccept" runat="server"></asp:TextBox>
            <br />
            Input guest club name<br />
            <asp:TextBox ID="guestAccept" runat="server"></asp:TextBox>
            <br />
            Input start time of Match<br />
            <asp:TextBox ID="startAccept" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="acceptRequest" runat="server" OnClick="acceptRequest_Click" Text="Accept Request" />
            <br />
            <br />
            <asp:Button ID="rejectRequest" runat="server" OnClick="rejectRequest_Click" Text="Reject Request" />
            <br />
            <br />

        </div>
    </form>
</body>
</html>
