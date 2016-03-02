using Microsoft.AspNet.Identity;
using MVCSocialMedia.Entities;
using MVCSocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCSocialMedia.Abstract
{
    public interface IRepository
    {
        IEnumerable<AppUser> Users { get; }
        IEnumerable<Like> Likes { get; }
        IEnumerable<Post> Posts { get; }
        IEnumerable<Wall> Walls { get; }

        AppUser FindUserById(string user);

        IdentityResult UpdateUser(UserModel info);
        void CreateUserWall(AppUser user);
        void AddPost(Post post);
        string DeletePost(Guid postid);
        void LikePost(Guid postId, string userId);
        Post GetPost(Guid postId);
    }
}
