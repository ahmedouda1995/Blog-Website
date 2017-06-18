<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="request to create a comm.aspx.cs" Inherits="WebApplication1.request_to_create_a_comm" %>

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
    <asp:Label ID="label1" runat="server" Text="provide your request for a new community by filling in the name and description of it below."></asp:Label>
    <br />
        <br />
        <br />
        
         <asp:Label ID="label2" runat="server" Text="Community name : "></asp:Label>
        <asp:TextBox ID="textbox1" runat="server"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="label3" runat="server" Text="It's description :  "></asp:Label>
        <asp:TextBox ID="textbox2" runat="server" Height="95px" Width="829px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="go1" runat="server" Text="enter" onclick="create_request_for_a_comm" />

    </div>
    </form>
</body>
</html>
