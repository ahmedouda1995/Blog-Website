<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="WebApplication1.HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>First PAGE</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>Login</p>
        
        <asp:Label ID="lbl_username" runat="server" Text="Username:   "></asp:Label>
        <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>

        <asp:Label ID="lbl_password" runat="server" Text="Password:   "></asp:Label>
        <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>

        <asp:Button ID="btn_login" runat="server" Text="Login" onclick="login" />
    
   
    

   <br />
        <p>Sign UP</p>
        <br />
            
    <p>
        <asp:Label ID="Label3" runat="server" Text="Username:   "></asp:Label>
        <asp:TextBox ID="email" runat="server"></asp:TextBox>

    </p>
   <p>
        <asp:Label ID="Label4" runat="server" Text="Password:   "></asp:Label>
        <asp:TextBox ID="sign_password" runat="server" TextMode="Password"></asp:TextBox>
     </p>  
    <p>
        <asp:Label ID="Label5" runat="server" Text="Preferred Genre: "></asp:Label>
        <asp:DropDownList runat="server" ID="preferredGenre" >
  <asp:ListItem value="Sports">Sports</asp:ListItem>
  <asp:ListItem value="Action">Action</asp:ListItem>
  <asp:ListItem value="Strategy">Strategy</asp:ListItem>
  <asp:ListItem value="RPG">RPG</asp:ListItem>
</asp:DropDownList> 

        </p>

       <p>
        <asp:Label ID="Label6" runat="server" Text="User Type: "></asp:Label>
        <asp:DropDownList runat="server" ID="usertype" >
  <asp:ListItem value="Normal User">Normal User</asp:ListItem>
  <asp:ListItem value="Verified Reviewer">Verified Reviewer</asp:ListItem>
  <asp:ListItem value="Development Team">Development Team</asp:ListItem>
 
</asp:DropDownList> 

        </p>
    <p>

        <asp:Button ID="signup" runat="server" Text="Sign Up" onclick="sign_up" />
    
    </p>
    </div>
    </form>
</body>
</html>

