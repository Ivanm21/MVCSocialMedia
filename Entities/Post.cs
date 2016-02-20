using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Entities
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string Content { get; set; }
        public byte[] Img { get; set; }
        public string ImageMimeType { get; set; }
        public DateTime CreationTime { get; set; }

        public virtual Wall wall { get; set; }
        public virtual AppUser author { get; set; }
        public virtual ICollection<Like> likes {get;set;}

        public Post()
        {
            CreationTime = DateTime.Now;
            PostId = Guid.NewGuid();
            likes = new List<Like>();
        }
    }
}