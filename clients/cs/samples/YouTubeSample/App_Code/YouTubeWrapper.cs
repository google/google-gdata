using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;


/// <summary>
/// Summary description for YouTubeWrapper
/// </summary>
public class YouTubeWrapper
{
    private YouTubeEntry ytEntry;

    public YouTubeWrapper()
    {
    }

    public YouTubeWrapper(YouTubeEntry entry)
    {
        this.ytEntry = entry;
    }

     public string VideoTitle
    {
        get
        {
            return this.ytEntry.Title.Text;
        }
    }

    public string VideoThumbnailUrl
    {
        get
        {
            if (this.ytEntry.Media.Thumbnails.Count > 0)
                return this.ytEntry.Media.Thumbnails[0].Url;
            return "";
        }

    }

    public string Description
    {
        get
        {
            return this.ytEntry.Media.Description.Value;
        }
    }

    public string Published
    {
        get
        {
            return this.ytEntry.Published.ToShortDateString();
        }
    }

    public string Author
    {
        get
        {
            return this.ytEntry.Authors[0].Name;
        }
    }

    public string NumberOfViews
    {
        get
        {
            if (this.ytEntry.Statistics != null)
                return this.ytEntry.Statistics.ViewCount;
            return "";
        }
    }

    public int NumberOfComments
    {
        get
        {
            if (this.ytEntry.Comments != null)
            {
                if (this.ytEntry.Comments.FeedLink != null)
                {
                    return this.ytEntry.Comments.FeedLink.CountHint;
                }
            }
            return 0;
        }
    }

    public Uri YouTubePage
    {
        get
        {
            return new Uri(this.ytEntry.AlternateUri.ToString());
        }
    }
}


public class YouTubeErrorWrapper : YouTubeWrapper
{
    private string message;

    public YouTubeErrorWrapper(string message)
    {
        this.message = message;
    }

    public new string VideoTitle
    {
        get
        {
            return this.message;
        }
    }

    public new string VideoThumbnailUrl
    {
        get
        {
            return "";
        }
    }

    public new string Description
    {
        get
        {
            return "";
        }
    }

    public new string Published
    {
        get
        {
            return "";
        }
    }

    public new string Author
    {
        get
        {
            return "";
        }
    }

    public new string NumberOfViews
    {
        get
        {
            return "";
        }
    }

    public new int NumberOfComments
    {
        get
        {
            return 0;
        }
    }

}

