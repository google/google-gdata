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
            return this.ytEntry.Media.Thumbnails[0].Url;
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
            return this.ytEntry.Statistics.ViewCount;
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
}
