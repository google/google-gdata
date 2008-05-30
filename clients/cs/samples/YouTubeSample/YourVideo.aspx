<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YourVideo.aspx.cs" Inherits="MostPopular" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Your Videos</title>
</head>
<body>
    <a href="MostPopular.aspx">Most Popular</a>
    <a href="MostCommented.aspx">Most Commented</a>
    <a href="SearchVideos.aspx">Search YouTube</a>
        <br />
        <br />
    <form id="form1" method="post" enctype="multipart/form-data" runat="server">
        New Video: &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:FileUpload ID="VideoUpload" runat="server" />
        (note, that .NET by default restricts this to 4MB)<br />
        Content type: &nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>video/mp4</asp:ListItem>
            <asp:ListItem>video/quicktime</asp:ListItem>
        </asp:DropDownList><br />
        Title: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:TextBox ID="Title" runat="server"></asp:TextBox><br />
        Description: &nbsp;&nbsp;
        <asp:TextBox ID="Description" runat="server"></asp:TextBox><br />
        Keywords: &nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="Keyword" runat="server"></asp:TextBox><br />
        Category: &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:TextBox ID="Category" runat="server">Nonprofit</asp:TextBox>&nbsp;
        <input id="SubmitVideo" type="submit" value="Upload" runat="server" onserverclick="SubmitVideo_ServerClick"/>
        (note, most likely there is a delay between upload and availability)<br />
        <br />
        <asp:Button ID="Refresh" runat="server" OnClick="Refresh_Click" Text="Refresh" /><br />
    
    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataMostPopular" AutoGenerateColumns="False" OnInit="Page_Load">
            <Columns>
                <asp:ImageField DataImageUrlField="VideoThumbnailUrl">
                </asp:ImageField>
                <asp:BoundField DataField="VideoTitle" HeaderText="VideoTitle" ReadOnly="True" SortExpression="VideoTitle" />
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                    SortExpression="Description" />
                <asp:BoundField DataField="Published" HeaderText="Published" ReadOnly="True" SortExpression="Published" />
                <asp:BoundField DataField="Author" HeaderText="Author" ReadOnly="True" SortExpression="Author" />
                <asp:BoundField DataField="NumberOfViews" HeaderText="NumberOfViews" ReadOnly="True"
                    SortExpression="NumberOfViews" />
                <asp:BoundField DataField="NumberOfComments" HeaderText="NumberOfComments" ReadOnly="True"
                    SortExpression="NumberOfComments" />
                <asp:HyperLinkField DataNavigateUrlFields="YouTubePage" DataTextField="VideoTitle" DataTextFormatString="&quot;Watch the video&quot;"/>
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataMostPopular" runat="server" OnLoad="Page_Load" SelectMethod="YourVideos" TypeName="ListVideos"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
