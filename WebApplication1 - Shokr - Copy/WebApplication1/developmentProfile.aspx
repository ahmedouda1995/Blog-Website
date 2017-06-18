<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="developmentProfile.aspx.cs" Inherits="WebApplication1.developmentProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Development Team Profile</title>
</head>
<body>
   <form id="form1" runat="server" >
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

        <div >
            <asp:Label Text="Email: " runat="server" style="margin-left:500px"> 
            </asp:Label>

            <asp:Label ID="lbl_email" Text=" " runat="server"></asp:Label>
            <br/>
            <asp:Label Text="Name: " runat="server" style="margin-left:500px"> 
            </asp:Label>

            <asp:Label ID="lbl_name" Text=" " runat="server"></asp:Label>
            <br/>
           
            <asp:Label Text="Date Of Foundation: " runat="server" style="margin-left:500px"> 
            </asp:Label>

            <asp:Label ID="lbl_dof" Text=" " runat="server"></asp:Label>
            <br/>
            <asp:Label Text="Company: "  runat="server" style="margin-left:500px"> 
            </asp:Label>

            <asp:Label ID="lbl_company" Text=" " runat="server"></asp:Label>
            <br/>
            <asp:Label Text="Preferred Genre: " runat="server" style="margin-left:500px"> 
            </asp:Label>

            <asp:Label ID="lbl_genre" Text=" " runat="server"></asp:Label>

        </div>
        <div >
            <asp:Button ID="btn_edit" Text="Edit Profile" runat="server" OnClick="edit_profile" style="margin-left:500px"> </asp:Button>
           
        </div>
    </form>
</body>
</html>
