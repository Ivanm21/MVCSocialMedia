using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Entities
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string BirthDate { get; set; }

        public byte[] Avatar { get; set; }
        public string ImageMimeType { get; set; }

        public virtual Wall wall { get; set; }
        public virtual ICollection<Post> posts { get; set; }
        public virtual ICollection<Like> likes { get; set; }
    }
}