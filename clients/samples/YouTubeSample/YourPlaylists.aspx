<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourPlaylists.aspx.cs" Inherits="YourPlayLists" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Your Playlists</title>
</head>
<body>
    <form id="form1" runat="server">
    <b>Your Playlists</b>
    <a href="default.aspx">Your Videos</a>
    <a href="MostPopular.aspx">Most Popular</a>
    <a href="MostCommented.aspx">Most Commented</a>
    <a href="SearchVideos.aspx">Search YouTube</a>
        <br />
        <br />
    
    <div>
        <br /> <br />
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataMostPopular" AutoGenerateColumns="False" OnInit="Page_Load">
            <Columns>
                <asp:BoundField DataField="CountHint" HeaderText="CountHint" ReadOnly="True" SortExpression="CountHint" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" />
                <asp:BoundField DataField="Summary" HeaderText="Summary" SortExpression="Summary" />
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataMostPopular" runat="server" OnLoad="Page_Load" SelectMethod="PlayLists" TypeName="ListVideos"></asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
