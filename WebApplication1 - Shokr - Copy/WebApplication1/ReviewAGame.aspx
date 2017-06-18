<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReviewAGame.aspx.cs" Inherits="WebApplication1.ReviewAGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Add a review to
            <asp:Label ID="Label1" runat="server" Text="{Game Name}"></asp:Label>
        </h2>
    <div>
    
    </div>
        <asp:TextBox ID="TextBox1" runat="server" Height="258px" Width="488px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Post Review" OnClick="Button1_Click" />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" style="font-weight: 700; font-style: italic" Text="Review posted succesfully" Visible="False"></asp:Label>
    </form>
</body>
</html>
