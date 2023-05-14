<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClubRepActions.aspx.cs" Inherits="M3.ClubRepActions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Club Representative<br />
            <br />
            <asp:Button ID="Club_info" runat="server" Text="View Club Info" OnClick="Club_info_Click" />
            <br />
            Club Name:<asp:Label ID="c_info_name" runat="server" Text=""></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            Club Location:<asp:Label ID="c_info_loc" runat="server" Text=""></asp:Label>
            <br />
            <br />
            <br />
            <asp:Button ID="View_matches" runat="server" Text="View Upcoming Matches" OnClick="View_matches_Click" />
            <br />


            <asp:GridView ID="GridView1" runat="server" Height="209px" Width="703px"></asp:GridView>
            <br />
            <br />
            <br />
            <asp:Button ID="View_Stadium" runat="server" Text="View Available Stadium" OnClick="View_Stadium_Click" />
            <br />
            Date:<asp:TextBox ID="Date_st_v" TextMode="DateTimeLocal"
runat="server"></asp:TextBox>
            <br />
            <asp:GridView ID="GridView2" runat="server" style="margin-top: 0px" Width="651px">
            </asp:GridView>
            <br />
            <br />
            <br />
            Send Request:<br />
            Stadium Name:<asp:TextBox ID="std_name" runat="server"></asp:TextBox>
            &nbsp;&nbsp;
            Start Time:<asp:TextBox ID="st_time" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
            <br />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Send_Request" runat="server" Text="Confirm Sending Request" OnClick="Send_Request_Click" />
            <br />
            <br />
            <br />
            <br />
            <br />
            </div>


    </form>
</body>
</html>
