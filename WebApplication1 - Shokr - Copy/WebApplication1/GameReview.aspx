<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameReview.aspx.cs" Inherits="WebApplication1.GameReview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
   <form id="form1" runat="server">
       
        <h2>
            <asp:Label ID="Label9" runat="server" Text="{Game Name}"></asp:Label>
        </h2>

    <div>
    
        <h3>Review @:
            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
&nbsp;By:
            <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View Reviewer Profile" />
        </h3>
        <p><em>Review Content: </em>
        </p>
    
    </div>
        <p>
            <asp:Label ID="Label11" runat="server" Text="Label" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:Label>
        </p>
        <p>
            Add comment:
        </p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Height="79px" Width="262px"></asp:TextBox>
        &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Add Comment" />
        </p>
        <p>
            Comments:</p>
        <p>
            &nbsp;</p>
   </form>
</body>
</html>
