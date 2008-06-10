using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class UploadVideo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void PrintForm()
    {
        String url = HttpContext.Current.Session["form_upload_url"] as string;
        String redirect = HttpContext.Current.Session["form_upload_redirect"] as string;
        url += "?nexturl=" + HttpUtility.UrlEncode(redirect);
        String token = HttpContext.Current.Session["form_upload_token"] as string;
        Response.Write("<form action='"+url+"' method='post' enctype='multipart/form-data'>");
        Response.Write("<input type='file' name='file'/>");
        Response.Write("<input type='hidden' name='token' value='"+token+"'/>");
        Response.Write("<input type='submit' value='Upload'/>");
        Response.Write("</form>");
    }
}
