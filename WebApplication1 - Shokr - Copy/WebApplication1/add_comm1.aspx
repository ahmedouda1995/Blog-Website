﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="add_comm1.aspx.cs" Inherits="WebApplication1.add_comm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div >
    <asp:TextBox ID="txt_search" runat="server" style="margin-left:500px"></asp:TextBox>
        <asp:DropDownList runat="server" ID="searchitem"  >
  <asp:ListItem Value="0">Verified Reviewer</asp:ListItem>
  <asp:ListItem Value="1">Development Team</asp:ListItem>
  <asp:ListItem Value="2">Games</asp:ListItem>
  <asp:ListItem Value="3">Conferences</asp:ListItem>
            <asp:ListItem Value="4">Community</asp:ListItem>
</asp:DropDownList> 
        <asp:Button ID="btn_search" Text="Search" runat="server" OnClick="btn_search_Click" />
        <div>
            <asp:LinkButton ID="btn_myprofile" runat="server" Text="My Profile" OnClick="btn_myprofile_Click" style="margin-left:500px"></asp:LinkButton>
           <asp:LinkButton ID="btn_logout" runat="server" Text="Log out" OnClick="btn_logout_Click" style="margin-left:50px"></asp:LinkButton>
            <asp:LinkButton ID="btn_community" runat="server" Text="Communities" OnClick="btn_community_Click" style="margin-left:50px"></asp:LinkButton>
           <asp:LinkButton ID="btn_conference" runat="server" Text="Conferences" OnClick="btn_conference_Click" style="margin-left:50px"></asp:LinkButton>
        </div>

    </div>
        <br />
        <br />

    <div>
     <asp:Label ID="label1" runat="server" Text="fill in the below to add a comment on this topic."></asp:Label>
    <br />
        <br />
        <br />
        
         <asp:Label ID="label3" runat="server" Text="comment content : "></asp:Label>
        <asp:TextBox ID="textbox3" runat="server" Width="828px"></asp:TextBox>
        <br />
        <br />
        
        <asp:Button ID="button1" runat="server" Text="add" onclick="add_comm_method1" />
    </div>
    </form>
</body>
</html>
