<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewGame.aspx.cs" Inherits="WebApplication1.ViewGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <h1>
                <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text="{Game Name}"></asp:Label>
            </h1>
            <br />
            <strong>Release Date: <asp:Label ID="Label2" runat="server" Text="{Date}"></asp:Label>
            <br />
            </strong>
            <br />
            <strong>Age Limit:
            <asp:Label ID="Label3" runat="server" Text="{AgeLimit}"></asp:Label>
            <br />
            </strong>
            <br />
            <strong>Type:
            <asp:Label ID="Label4" runat="server" Text="{Type}"></asp:Label>
            ,
            <asp:Label ID="Label10" runat="server" Text="{attr of type}"></asp:Label>
            <br />
            <br />
            Overall Rating:
            <asp:Label ID="Label9" runat="server" Text="{Rating}"></asp:Label>
            <br />
            </strong>
            <br />
            <asp:Button ID="Button1" runat="server" Text="View Screenshots and Videos" OnClick="Button1_Click" />
&nbsp;&nbsp;&nbsp;
            <br />
    <br />
        </div>
        <strong>Game Developer(s):&nbsp;&nbsp; <br />
        </strong>
        <br />
&nbsp;<asp:Label ID="Label12" runat="server"></asp:Label>
        <br />
        <br />
        <strong>Reviews for this game:<br />
        </strong>
        <br />
        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button7" runat="server" OnClick="Button7_Click" Text="View Game Review" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="Rate this game" OnClick="Button3_Click" />
        <br />
        <br />
        <strong>Recomment this game to another user:<br />
        <br />
        </strong>
        <asp:DropDownList ID="DropDownList3" runat="server" DataSourceID="SqlDataSource1" DataTextField="email" DataValueField="email" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB1ConnectionString %>" SelectCommand="SELECT [email] FROM [Normal_Users]"></asp:SqlDataSource>
        <br />
        <br />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Recommend" />
        <br />
        <br />
        <br />
        <strong><em>For Verified Reviewers Only:</em></strong><br />
        <asp:Button ID="Button5" runat="server" Text="Write a review about this game" Width="231px" OnClick="Button5_Click" />
        <br />
        <br />
        <asp:Label ID="Label11" runat="server" style="font-weight: 700; font-style: italic" Text="You are not authorized to review a game" Visible="False"></asp:Label>
    </form>
</body>
</html>
