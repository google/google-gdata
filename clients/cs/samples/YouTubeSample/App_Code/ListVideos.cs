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



    private static List<YouTubeWrapper> GetVideos(string videofeed)
    {
        List<YouTubeWrapper> list = new List<YouTubeWrapper>();

        GAuthSubRequestFactory authFactory = new GAuthSubRequestFactory(YouTubeService.YTService, "TesterApp");

        YouTubeService service = new YouTubeService(authFactory.ApplicationName,
            "ytapi-FrankMantek-TestaccountforGD-sjgv537n-0",
            "AI39si4v3E6oIYiI60ndCNDqnPP5lCqO28DSvvDPnQt-Mqia5uPz2e4E-gMSBVwHXwyn_LF1tWox4LyM-0YQd2o4i_3GcXxa2Q"
            );
        authFactory.Token = HttpContext.Current.Session["token"] as string;
        service.RequestFactory = authFactory;

        YouTubeQuery query = new YouTubeQuery(videofeed);

        try
        {
            YouTubeFeed f = service.Query(query);

            foreach (YouTubeEntry entry in f.Entries)
            {
                list.Add(new YouTubeWrapper(entry));
            }
        }
        catch (GDataRequestException gdre)
        {
            HttpWebResponse response = (HttpWebResponse)gdre.Response;
        }
        return list;

    }

}
