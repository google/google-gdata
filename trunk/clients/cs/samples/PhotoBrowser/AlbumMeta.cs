using System;
using System.ComponentModel;
using Google.GData.Client;
using Google.GData.Photos;

namespace PhotoBrowser
{
	/// <summary>
	/// This class presents the meta data of an album entry that we want 
	/// to expose in the property browser
	/// </summary>
	public class AlbumMeta
	{
        PicasaEntry myEntry;

		public AlbumMeta(PicasaEntry entry)
		{
            this.myEntry = entry;
		}

        [Category("Base Album Data"),
        Description("Specifies the name of the album.")]
        public string AlbumTitle
        {
            get 
            {
                return this.myEntry.Title.Text;
            }
            set 
            {
                this.myEntry.Title.Text = value;
            }
        }

        [Category("Base Album Data"),
        Description("Specifies the summary of the album.")]
        public string AlbumSummary
        {
            get 
            {
                return this.myEntry.Summary.Text;
            }
            set 
            {
                this.myEntry.Summary.Text = value;
            }
        }

        [Category("Base Album Data"),
        Description("Specifies the author of the album.")]
        public string AlbumAuthor
        {
            get 
            {
                AtomPersonCollection authors = this.myEntry.Authors;
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
                    return person.Name;
                }
                return "No Author given";

            }
            set 
            {
                AtomPersonCollection authors = this.myEntry.Authors;
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
                    person.Name = value;
                }
            }
        }

        [Category("Base Album Data"),
        Description("Specifies the author's URI.")]
        public string AlbumAuthorUri
        {
            get 
            {
                AtomPersonCollection authors = this.myEntry.Authors;
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
                    return person.Uri.ToString();
                }
                return "No Author given";

            }
            set 
            {
                AtomPersonCollection authors = this.myEntry.Authors;
                if (authors != null && authors.Count >0) 
                {
                    AtomPerson person = authors[0];
                    person.Uri = new AtomUri(value);
                }
            }
        }

        [Category("Base Album Data"),
        Description("Specifies the author's nickname")]
        public string AlbumAuthorNickname
        {
            get 
            {
                return this.myEntry.getPhotoExtensionValue(GPhotoNameTable.Nickname);
            }
            set 
            {
               this.myEntry.setPhotoExtension(GPhotoNameTable.Nickname, value);
            }
        }

        [Category("Meta Album Data"),
        Description("Number of pictures in the album")]
        public string AlbumPictureCount
        {
            get 
            {
                return this.myEntry.getPhotoExtensionValue(GPhotoNameTable.NumPhotos);
            }
       }
        [Category("Meta Album Data"),
        Description("Number of pictures remaining")]
        public string AlbumPictureCountRemaining
        {
            get 
            {
                return this.myEntry.getPhotoExtensionValue(GPhotoNameTable.NumPhotosRemaining);
            }
        }

        [Category("Meta Album Data"),
        Description("Bytes used")]
        public string AlbumBytesUsed
        {
            get 
            {
                return this.myEntry.getPhotoExtensionValue(GPhotoNameTable.BytesUsed);
            }
        }

        [Category("Commenting"),
        Description("Comments enabled?")]
        public bool AlbumCommentsEnabled
        {
            get 
            {
                return bool.Parse(this.myEntry.getPhotoExtensionValue(GPhotoNameTable.CommentingEnabled));
            }
            set 
            {
                this.myEntry.setPhotoExtension(GPhotoNameTable.CommentingEnabled, value.ToString().ToLower());
            }
        }
        
        [Category("Commenting"),
        Description("Number of comments")]
        public string AlbumCommentCount
        {
            get 
            {
                return this.myEntry.getPhotoExtensionValue(GPhotoNameTable.CommentCount);
            }
        }
  	}
}
