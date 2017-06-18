<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScreenshotsAndVideos.aspx.cs" Inherits="WebApplication1.ScreenshotsAndVideos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>
            <asp:Label ID="Label1" runat="server" Text="{Game Name}"></asp:Label>
&nbsp;Screenshots and Videos</h2>
        <p>
            <strong>Screenshots: </strong>
        </p>
        <p>
            <strong>&nbsp;</strong><asp:DropDownList ID="DropDownList3" runat="server" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
            </asp:DropDownList>
        &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="View Screenshot" />
        </p>
        <p>
            <asp:Label ID="Label2" runat="server"></asp:Label>
&nbsp;</p>
        <p style="font-weight: 700">
            Videos:</p>
        <p style="font-weight: 700">
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList>
        &nbsp;
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="View Video" />
        </p>
        <asp:Label ID="Label4" runat="server"></asp:Label>
&nbsp;</form>
</body>
</html>
