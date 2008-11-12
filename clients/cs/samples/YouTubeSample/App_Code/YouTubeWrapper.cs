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
    private PlaylistsEntry plEntry;
    private YouTubeBaseEntry baseEntry; 

    public YouTubeWrapper()
    {
    }

    public YouTubeWrapper(YouTubeBaseEntry entry)
    {
        this.ytEntry = entry as YouTubeEntry;
        this.plEntry = entry as PlaylistsEntry;
        this.baseEntry = entry as YouTubeBaseEntry;
    }

     public string VideoTitle
    {
        get
        {
            return this.baseEntry.Title.Text;
        }
    }

    public string VideoThumbnailUrl
    {
        get
        {
            if (this.ytEntry != null && this.ytEntry.Media != null && 
                this.ytEntry.Media.Thumbnails.Count > 0)
                return this.ytEntry.Media.Thumbnails[0].Url;
            return "";
        }

    }

    public string Description
    {
        get
        {
            return this.baseEntry.Summary.Text;
        }
    }

    public string Published
    {
        get
        {
            return this.baseEntry.Published.ToShortDateString();
        }
    }

    public string Author
    {
        get
        {
            return this.baseEntry.Authors[0].Name;
        }
    }

    public string NumberOfViews
    {
        get
        {
            if (this.ytEntry != null && this.ytEntry.Statistics != null)
                return this.ytEntry.Statistics.ViewCount;
            return "";
        }
    }

    public int NumberOfComments
    {
        get
        {
            if (this.ytEntry != null && 
                this.ytEntry.Comments != null &&
                this.ytEntry.Comments.FeedLink != null)
            {
                    return this.ytEntry.Comments.FeedLink.CountHint;
            }
            return 0;
        }
    }

    public string Rating
    {
        get
        {
            if (this.ytEntry != null &&
                this.ytEntry.Rating != null)
            {
                return this.ytEntry.Rating.Average.ToString();
            }
            return "not rated";
        }
    }

    public string Restricted
    {
        get
        {
            if (this.ytEntry != null && 
                this.ytEntry.Media != null &&
                this.ytEntry.Media.Rating != null)
            {
                return this.ytEntry.Media.Rating.Value;
            }
            return "not restricted";
        }
    }

    public string Country
    {
        get
        {
            if (this.ytEntry != null && 
                this.ytEntry.Media != null &&
                this.ytEntry.Media.Rating != null)
            {
                return this.ytEntry.Media.Rating.Country;
            }
            return "";
        }
    }

    public int CountHint
    {
        get
        {
            if (this.plEntry != null)
                return this.plEntry.CountHint;
            return -1;
        }
    }


    public Uri YouTubePage
    {
        get
        {
            if (this.ytEntry != null && this.ytEntry.AlternateUri != null)
                return new Uri(this.ytEntry.AlternateUri.ToString());
            return null;
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
}

