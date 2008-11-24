<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchVideos.aspx.cs" Inherits="SearchVideos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search YouTube</title>
</head>
<body>
    <form id="form1" runat="server">
    <a href="default.aspx">Your Videos</a>
    <a href="MostPopular.aspx">Most Popular</a>
    <a href="YourPlaylists.aspx">Your Playlists</a>
    <a href="MostCommented.aspx">Most Commented</a>
    <b>Search YouTube</b>
    <div>
        <br />
        This will be searching the top rated video feed.<br />
        <br />
        Video Query: &nbsp;&nbsp; &nbsp;<asp:TextBox ID="VideoQuery" runat="server"></asp:TextBox>
        &nbsp; &nbsp; Author:
        <asp:TextBox ID="Author" runat="server"></asp:TextBox><br />
        <br />
        Category: &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
        <asp:TextBox ID="CategoryQuery" runat="server"></asp:TextBox><br />
        <br />
        Order By: &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:DropDownList ID="Relevance" runat="server">
            <asp:ListItem Value="relevance">Relevance</asp:ListItem>
            <asp:ListItem Value="published">Published</asp:ListItem>
            <asp:ListItem Value="viewCount">ViewCount</asp:ListItem>
            <asp:ListItem Value="rating">Rating</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        Include Racy? &nbsp;
        <asp:CheckBox ID="Racy" runat="server" /><br />
        <br />
        In Timeframe &nbsp;&nbsp;
        <asp:DropDownList ID="Timeframe" runat="server">
            <asp:ListItem>All Time</asp:ListItem>
            <asp:ListItem>Today</asp:ListItem>
            <asp:ListItem>This Week</asp:ListItem>
            <asp:ListItem>This Month</asp:ListItem>
        </asp:DropDownList>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:Button ID="Search" runat="server" Text="Search YouTube" OnClick="Search_Click" /><br />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SearchYouTubeSource">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Uploader" HeaderText="Uploader" SortExpression="Uploader" />
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" />
                <asp:BoundField DataField="WatchPage" HeaderText="WatchPage" SortExpression="WatchPage" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="SearchYouTubeSource" runat="server" SelectMethod="Search" TypeName="ListVideos">
            <SelectParameters>
                <asp:ControlParameter ControlID="VideoQuery" Name="videoQuery" PropertyName="Text"
                    Type="String" />
                <asp:ControlParameter ControlID="Author" Name="author" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Relevance" Name="orderby" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="Racy" Name="racy" PropertyName="Checked" Type="String" />
                <asp:ControlParameter ControlID="Timeframe" Name="time" PropertyName="SelectedValue"
                    Type="String" />
                <asp:ControlParameter ControlID="CategoryQuery" Name="category" PropertyName="Text"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
