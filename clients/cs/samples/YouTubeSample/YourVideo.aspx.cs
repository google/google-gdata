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
using System.Net;
using Google.GData.Client;
using Google.GData.Extensions.MediaRss;
using Google.GData.YouTube;


public partial class MostPopular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SubmitVideo_ServerClick(object sender, EventArgs e)
    {
        if (this.VideoUpload.HasFile &&
            String.IsNullOrEmpty(this.Title.Text) == false &&
            String.IsNullOrEmpty(this.Description.Text) == false &&
            String.IsNullOrEmpty(this.Category.Text) == false &&
            String.IsNullOrEmpty(this.Keyword.Text) == false)
        {

            GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory(YouTubeService.YTService, "TesterApp");

            YouTubeService service = new YouTubeService(authFactory.ApplicationName,
                "ytapi-FrankMantek-TestaccountforGD-sjgv537n-0",
                "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q"
                );

            authFactory.Token = HttpContext.Current.Session["token"] as string;
            service.RequestFactory = authFactory;

            try
            {
                YouTubeEntry entry = new YouTubeEntry();

                entry.MediaSource = new MediaFileSource(this.VideoUpload.FileContent, this.VideoUpload.FileName, "video/quicktime");
                entry.Media = new MediaGroup();
                entry.Media.Description = new MediaDescription(this.Description.Text);
                entry.Media.Title = new MediaTitle(this.Title.Text);
                entry.Media.Keywords = new MediaKeywords(this.Keyword.Text);

                // entry.Media.Categories
                MediaCategory category = new MediaCategory(this.Category.Text);
                category.Attributes["scheme"] = YouTubeService.DefaultCategory;

                entry.Media.Categories.Add(category);
                service.Upload(entry);
                this.ObjectDataMostPopular.Select();
            }
            catch (GDataRequestException gdre)
            {
                HttpWebResponse response = (HttpWebResponse)gdre.Response;
            }
        }
    }
    protected void Refresh_Click(object sender, EventArgs e)
    {
        this.ObjectDataMostPopular.Select();
    }
}
