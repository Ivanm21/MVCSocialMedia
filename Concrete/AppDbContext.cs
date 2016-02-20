using Microsoft.AspNet.Identity.EntityFramework;
using MVCSocialMedia.Abstract;
using MVCSocialMedia.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Concrete
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext()
            : base("MVCSocialMedia")
        {
        }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Wall> Walls { get; set; }
    }
}