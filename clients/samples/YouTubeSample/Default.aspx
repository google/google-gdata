<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Import Namespace="Google.GData.Client" %>
<%@ Import Namespace="Google.GData.Extensions" %>
<%@ Import Namespace="Google.GData.YouTube" %>
<%@ Import Namespace="System.Net" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Test Site</title>
</head>
<body>

    <form id="form1" runat="server">
    <h1>AuthSub Sample Page</h1>
    <div>
    <%
        Uri targetUri = new Uri(Request.Url, "YourVideo.aspx");
        
        GotoAuthSubLink.Visible = false;
        
        if (Session["token"] != null)
        {
            Response.Redirect(targetUri.ToString());
        }
        else if (Request.QueryString["token"] != null)
        {
            String token = Request.QueryString["token"];
            Session["token"] = AuthSubUtil.exchangeForSessionToken(token, null).ToString();
            Response.Redirect(targetUri.ToString(), true);
        }
        else //no auth data, print link
        {
            GotoAuthSubLink.Text = "Login to your Google Account";
            GotoAuthSubLink.Visible = true;
            GotoAuthSubLink.NavigateUrl = AuthSubUtil.getRequestUrl(Request.Url.ToString(),
                "http://gdata.youtube.com",false,true);
        }
        
     %>
    <asp:HyperLink ID="GotoAuthSubLink" runat="server"/>

    </div>
    </form>
</body>
</html>