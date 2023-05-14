<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssocMgr.aspx.cs" Inherits="M3.AssocMgr" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            **TO ADD A Match**<br />
            <br />
            Host club name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <p>
        <asp:TextBox ID="hostClubName" runat="server"></asp:TextBox>
        </p>
        <p>
            Guest Club Name</p>
        <p>
            <asp:TextBox ID="guestClubName" runat="server"></asp:TextBox>
        </p>
        <p>
            Start time</p>
        <asp:TextBox ID="addStartTime" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        <br />
        End time<br />
        <asp:TextBox ID="addEndTime" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="addMatch" runat="server" OnClick="addMatch_Click" Text="Add Match" />
        <br />
        <br />
        ** TO DELETE A Match**<br />
        <br />
        <div>
            Host club name&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <p>
        <asp:TextBox ID="hostClubNameD" runat="server"></asp:TextBox>
        </p>
        <p>
            Guest Club Name</p>
        <p>
            <asp:TextBox ID="guestClubNameD" runat="server"></asp:TextBox>
        </p>
        <p>
            Start time</p>
        <asp:TextBox ID="addStartTimeD" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        <br />
        End time<br />
        <asp:TextBox ID="addEndTimeD" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="deleteMatch" runat="server" OnClick="deleteMatch_Click" Text="Delete Match" />
        <br />
        <br />
        * VIEW ALL UPCOMING MATCHES*<br />
        <asp:Button ID="viewUpcomingMatches" runat="server" OnClick="viewUpcomingMatches_Click" Text="View Upcoming Matches" style="margin-top: 0px" />
        <br />
        <asp:GridView ID="gridViewUpcomingMatches" runat="server">
        
        </asp:GridView>
        <br />
        *VIEW ALREADY PLAYED MATCHES*<br />
        <asp:Button ID="viewPlayedMatches" runat="server" OnClick="viewPlayedMatches_Click" Text="View Played Matches" />
        <br />
        <asp:GridView ID="gridViewPlayedMatches" runat="server">
        </asp:GridView>
        <br />
        *VIEW PAIR OF CLUBS WHO NEVER SCHEDULED TO PLAY TOG*      
        <br />
        <asp:Button ID="viewClubsNeverPlayed" runat="server" OnClick="viewClubsNeverPlayed_Click" Text="View Clubs Not Scheduled Together" />
        <br />
        <asp:GridView ID="gridViewClubsNeverPlayed" runat="server">
        </asp:GridView>
        <br />
    </form>
</body>
</html>
