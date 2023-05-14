<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fan.aspx.cs" Inherits="M3.Fan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Insert date to find matches on the date:<br />
            <asp:TextBox ID="viewMatches" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="viewAllMatches" runat="server"  Text="View All Matches To Attend" OnClick="viewAllMatches_Click" />
            <br />
        </div>
        <asp:GridView ID="gridViewAllMatches" runat="server">
            <Columns>
              
            </Columns>
        </asp:GridView>
        <p>
            *TO PURCHASE A TICKET*</p>
        <p>
            Enter your ID number:</p>
        <p>
            <asp:TextBox ID="ID" runat="server"></asp:TextBox>
        </p>
        <p>
            Enter the name of the host club of the match:</p>
        <p>
            <asp:TextBox ID="hostClubName" runat="server"></asp:TextBox>
        </p>
        <p>
            Enter the name of the guest club of the match:</p>
        <p>
            <asp:TextBox ID="guestClubName" runat="server"></asp:TextBox>
        </p>
        <p>
            Enter the start date and time of the match in format ___:</p>
        <p>
            <asp:TextBox ID="startTime" TextMode="DateTimeLocal" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="purchaseTicket" runat="server" OnClick="purchaseTicket_Click" Text="Purchase Ticket" />
    </form>
</body>
</html>
