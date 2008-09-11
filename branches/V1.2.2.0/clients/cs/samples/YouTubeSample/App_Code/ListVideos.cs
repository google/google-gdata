using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using System.Net;



/// <summary>
/// Summary description for VideoList
/// </summary>
public static class ListVideos
{
    public static List<YouTubeWrapper> MostPopular()
    {
        return GetVideos(YouTubeQuery.MostViewedVideo);
    }

    public static List<YouTubeWrapper> YourVideos()
    {
        return GetVideos(YouTubeQuery.DefaultUploads);
    }

    public static List<YouTubeWrapper> MostCommented()
    {
        return GetVideos(YouTubeQuery.MostDiscussedVideo);
    }

    public static List<YouTubeWrapper> Search(string videoQuery, string author, string orderby, bool racy, string time, string category )
    {
        YouTubeQuery query = new YouTubeQuery(YouTubeQuery.TopRatedVideo);
        if (String.IsNullOrEmpty(videoQuery) != true)
        {
            query.VQ = videoQuery;
        }
        if (String.IsNullOrEmpty(author) != true)
        {
            query.Author = author;
        }
        if (String.IsNullOrEmpty(orderby) != true)
        {
            query.OrderBy = orderby;
        }
        if (racy == true)
        {
            query.Racy = "include";
        }
        if (String.IsNullOrEmpty(time) != true)
        {
            if (time == "All Time")
                query.Time = YouTubeQuery.UploadTime.AllTime;
            else if (time == "Today")
                query.Time = YouTubeQuery.UploadTime.Today;
            else if (time == "This Week")
                query.Time = YouTubeQuery.UploadTime.ThisWeek;
            else if (time == "This Month")
                query.Time = YouTubeQuery.UploadTime.ThisMonth;
        }
        if (String.IsNullOrEmpty(category) != true)
        {
            QueryCategory q  = new QueryCategory(new AtomCategory(category));
            query.Categories.Add(q);
        }
        return ListVideos.GetVideos(query);
    }



    private static List<YouTubeWrapper> GetVideos(string videofeed)
    {
        YouTubeQuery query = new YouTubeQuery(videofeed);
        return ListVideos.GetVideos(query);
    }

    private static List<YouTubeWrapper> GetVideos(YouTubeQuery q)
    {
        List<YouTubeWrapper> list = new List<YouTubeWrapper>();

        GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory(YouTubeService.YTService, "TesterApp");

        YouTubeService service = new YouTubeService(authFactory.ApplicationName,
            "ytapi-FrankMantek-TestaccountforGD-sjgv537n-0",
            "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q"
            );
        authFactory.Token = HttpContext.Current.Session["token"] as string;
        service.RequestFactory = authFactory;

        try
        {
            YouTubeFeed f = service.Query(q);

            foreach (YouTubeEntry entry in f.Entries)
            {
                list.Add(new YouTubeWrapper(entry));
            }
            if (list.Count == 0) 
            {
                list.Add(new YouTubeErrorWrapper("Nothing to see here, no results found"));
            }
        }
        catch (GDataRequestException gdre)
        {
            HttpWebResponse response = (HttpWebResponse)gdre.Response;
            list.Add(new YouTubeErrorWrapper("An Error happened during the request"));
        }
        
        return list;
    }


}
