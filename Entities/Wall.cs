using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Entities
{
    public class Wall
    {
        public Guid WallId { get; set; }
       
        [Key, ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual ICollection<Post> posts { get; set; }

        public Wall()
        {
            posts = new List<Post>();
        }
    }
}