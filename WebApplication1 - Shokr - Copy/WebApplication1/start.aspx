<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="start.aspx.cs" Inherits="WebApplication1.start" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>

<body style="background-color:bisque">
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
    <asp:HyperLink ID="link1" runat="server" NavigateUrl="~/request to create a comm.aspx">1-request to create a new community</asp:HyperLink>
        <br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="link2" runat="server" NavigateUrl="~/point_2.aspx">2-view all communities page</asp:HyperLink>
        <br />
        <br />
        <br />
        <br />
        <asp:HyperLink ID="link3" runat="server" NavigateUrl="~/third_point.aspx">3-join a community or add a topic on existing community</asp:HyperLink>
        <br />
    </div>
    </form>
</body>
</html>
