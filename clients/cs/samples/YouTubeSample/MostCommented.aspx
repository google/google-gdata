<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MostCommented.aspx.cs" Inherits="MostPopular" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <a href="YourVideo.aspx">Your Videos</a>
    <a href="MostPopular.aspx">Most Popular</a>

    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataMostPopular" AutoGenerateColumns="False" OnInit="Page_Load">
            <Columns>
                <asp:ImageField DataImageUrlField="VideoThumbnailUrl">
                </asp:ImageField>
                <asp:BoundField DataField="NumberOfComments" HeaderText="NumberOfComments" ReadOnly="True"
                    SortExpression="NumberOfComments" />
                <asp:BoundField DataField="VideoTitle" HeaderText="VideoTitle" ReadOnly="True" SortExpression="VideoTitle" />
                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                    SortExpression="Description" />
                <asp:BoundField DataField="Published" HeaderText="Published" ReadOnly="True" SortExpression="Published" />
                <asp:BoundField DataField="Author" HeaderText="Author" ReadOnly="True" SortExpression="Author" />
                <asp:BoundField DataField="NumberOfViews" HeaderText="NumberOfViews" ReadOnly="True"
                    SortExpression="NumberOfViews" />
            </Columns>
        </asp:GridView>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ObjectDataSource ID="ObjectDataMostPopular" runat="server" OnLoad="Page_Load" SelectMethod="MostCommented" TypeName="ListVideos"></asp:ObjectDataSource>
    
    </div>
    </form>
</body>
</html>
