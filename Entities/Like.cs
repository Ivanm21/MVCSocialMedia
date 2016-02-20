using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Entities
{
    public class Like
    {
        public Guid LikeId { get; set; }

        

        public virtual Post Post { get; set; }
        public virtual AppUser AppUser { get;set;}
    }
}