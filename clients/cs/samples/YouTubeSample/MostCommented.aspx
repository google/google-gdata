<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MostCommented.aspx.cs" Inherits="MostPopular" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Most Commented YouTube Videos</title>
</head>
<body>
    <form id="form1" runat="server">
    <a href="default.aspx">Your Videos</a>
    <a href="MostPopular.aspx">Most Popular</a>
    <b>Most Commented</b>
    <a href="YourPlaylists.aspx">Your Playlists</a>
    <a href="SearchVideos.aspx">Search YouTube</a>
    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataMostPopular" AutoGenerateColumns="False" OnInit="Page_Load">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Uploader" HeaderText="Uploader" SortExpression="Uploader" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" />
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataMostPopular" runat="server" OnLoad="Page_Load" SelectMethod="MostCommented" TypeName="ListVideos"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
