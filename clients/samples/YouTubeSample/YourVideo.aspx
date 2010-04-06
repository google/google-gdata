<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourVideo.aspx.cs" Inherits="YourVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Your Videos</title>
</head>
<body>

    <b>Your Videos</b>
    <a href="MostPopular.aspx">Most Popular</a>
    <a href="MostCommented.aspx">Most Commented</a>
    <a href="YourPlaylists.aspx">Your Playlists</a>
    <a href="SearchVideos.aspx">Search YouTube</a>
        <br />
        <br />
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
        <% CheckForUpload(); %>
        <fieldset id="UploadForm1">
        <legend>Upload New Video</legend>
        <table>
        <tr><td>Title:</td>
        <td><asp:TextBox ID="Title" runat="server"></asp:TextBox></td></tr>
        <tr><td>Description:</td>
        <td><asp:TextBox ID="Description" runat="server" Columns="50"></asp:TextBox></td></tr>
        <tr><td>Keywords:</td>
        <td><asp:TextBox ID="Keyword" runat="server"></asp:TextBox></td></tr>
        <tr><td>Category:</td>
        <td>
        <asp:DropDownList ID="Category" runat="server">
            <asp:ListItem Value="Autos">Autos &amp; Vehicles</asp:ListItem>
            <asp:ListItem Value="Music">Music</asp:ListItem>
            <asp:ListItem Value="Animals">Pets &amp; Animals</asp:ListItem>
            <asp:ListItem Value="Sports">Sports</asp:ListItem>
            <asp:ListItem Value="Travel">Travel &amp; Events</asp:ListItem>
            <asp:ListItem Value="Games">Gadgets &amp; Games</asp:ListItem>
            <asp:ListItem Value="Comedy">Comedy</asp:ListItem>
            <asp:ListItem Value="People">People &amp; Blogs</asp:ListItem>
            <asp:ListItem Value="News">News &amp; Politics</asp:ListItem>
            <asp:ListItem Value="Entertainment">Entertainment</asp:ListItem>
            <asp:ListItem Value="Education">Education</asp:ListItem>
            <asp:ListItem Value="Howto">Howto &amp; Style</asp:ListItem>
            <asp:ListItem Value="Nonprofit">Nonprofit &amp; Activism</asp:ListItem>
            <asp:ListItem Value="Tech">Science &amp; Technology</asp:ListItem>
        </asp:DropDownList></td></tr>
        <tr><td colspan="2"><input id="SubmitVideo" type="submit" value="Upload" runat="server" onserverclick="SubmitVideo_ServerClick"/></td></tr>
        </table>
        </fieldset>
        
    
    <div>
        <br /> <br />
        <asp:Button ID="Refresh" runat="server" OnClick="Refresh_Click" Text="Refresh" /><br />
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataMostPopular" AutoGenerateColumns="False" OnInit="Page_Load" DataKeyNames="Description,Summary,Uploader,Title">
            <Columns>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Uploader" HeaderText="Uploader" SortExpression="Uploader" />
                <asp:BoundField DataField="ViewCount" HeaderText="ViewCount" ReadOnly="True" SortExpression="ViewCount" />
                <asp:BoundField DataField="CommmentCount" HeaderText="CommmentCount" ReadOnly="True"
                    SortExpression="CommmentCount" />
                <asp:BoundField DataField="Rating" HeaderText="Rating" ReadOnly="True" SortExpression="Rating" />
                <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                <asp:BoundField DataField="Content" HeaderText="Content" SortExpression="Content" />
                <asp:BoundField DataField="Summary" HeaderText="Summary" SortExpression="Summary" />
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataMostPopular" runat="server" OnLoad="Page_Load" SelectMethod="YourVideos" TypeName="ListVideos" DataObjectTypeName="Google.YouTube.Video" UpdateMethod="Update"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
