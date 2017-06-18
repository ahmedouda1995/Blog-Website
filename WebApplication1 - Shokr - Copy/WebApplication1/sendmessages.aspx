﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sendmessages.aspx.cs" Inherits="WebApplication1.sendmessages" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Message Sender</title>
</head>
<body>
    <form id="form2" runat="server" style="margin-left:500px">
        <div >
    <asp:TextBox ID="txt_search" runat="server" ></asp:TextBox>
        <asp:DropDownList runat="server" ID="searchitem"  >
  <asp:ListItem Value="0">Verified Reviewer</asp:ListItem>
  <asp:ListItem Value="1">Development Team</asp:ListItem>
  <asp:ListItem Value="2">Games</asp:ListItem>
  <asp:ListItem Value="3">Conferences</asp:ListItem>
            <asp:ListItem Value="4">Community</asp:ListItem>
</asp:DropDownList> 
        <asp:Button ID="btn_search" Text="Search" runat="server" OnClick="btn_search_Click" />
        <div>
            <asp:LinkButton ID="btn_myprofile" runat="server" Text="My Profile" OnClick="btn_myprofile_Click" ></asp:LinkButton>
           <asp:LinkButton ID="btn_logout" runat="server" Text="Log out" OnClick="btn_logout_Click" style="margin-left:50px"></asp:LinkButton>
            <asp:LinkButton ID="btn_community" runat="server" Text="Communities" OnClick="btn_community_Click" style="margin-left:50px"></asp:LinkButton>
           <asp:LinkButton ID="btn_conference" runat="server" Text="Conferences" OnClick="btn_conference_Click" style="margin-left:50px"></asp:LinkButton>
        </div>

    </div>
        <br />
        <br />
<asp:Button ID="back" Text="Back" runat="server" OnClick="back_Click"/>
        <br />
<asp:TextBox ID="txt_msg" runat="server" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
       <asp:Button ID="btn_msg" runat="server" Text="send" OnClick="send_Click" />
        <br />
    </form>
   

</body>
</html>