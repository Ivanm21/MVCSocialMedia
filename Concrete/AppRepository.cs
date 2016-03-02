using Microsoft.AspNet.Identity;
using MVCSocialMedia.Abstract;
using MVCSocialMedia.Entities;
using MVCSocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Diagnostics.Contracts;

namespace MVCSocialMedia.Concrete
{
    public class AppRepository : IRepository
    {
        public AppDbContext context;
        private readonly UserManager<AppUser> userManager;

        public AppRepository()
        {
            
            context = Startup.DbContext;
           userManager = Startup.UserManagerFactory.Invoke();
           
        }

        public IEnumerable<AppUser> Users { get { return userManager.Users; } }
        public IEnumerable<Wall> Walls { get { return context.Walls; } }
        public IEnumerable<Like> Likes { get { return context.Likes; } }
        public IEnumerable<Post> Posts { get { return context.Posts; } }

        public AppUser FindUserById(string userid)
        {
            var User =  userManager.FindById(userid);
            return User;
        }

      

        public IdentityResult UpdateUser(UserModel info)
        {
            var user =  FindUserById(info.UserId);
            
            if (info.Avatar != null)
            {
                user.Avatar = info.Avatar;
                user.ImageMimeType = info.ImageMimeType;
            }

            user.Name = info.Name;
            user.Surname = info.Surname;
            user.Country = info.Country;
            user.BirthDate = info.BirthDate;
            user.Email = info.Email;
            user.PhoneNumber = info.Phone;
            
           

            var result =  userManager.Update(user);

            return result;
        }

        public void AddPost(Post post)
        {
            if (post != null)
            {
                var user =  FindUserById(post.author.Id);
                user.wall.posts.Add(post);

                var result =  userManager.Update(user);
                
            }
           
            
        }

        public string DeletePost(Guid postid)
        {
            
            var post = context.Posts.Where(x => x.PostId == postid).First();
            string result = "";
            if (post!= null)
            {
                try
                {
                    context.Posts.Remove(post);
                    context.SaveChanges();
                    result = "Success";
                    return result;
                }
                catch(Exception e)
                {
                    result = e.Message;
                    return result;
                }
                
            }
            else
            {
                result = "Fail";
                return result;
            }
        }

        public void LikePost(Guid postId, string userId)
        {

            var likes = Likes.Where(x => x.Post.PostId == postId).ToList();
            var _post = Posts.Where(x => x.PostId == postId).FirstOrDefault();
            var user =  FindUserById(userId);

            var like = likes.Where(x => x.AppUser.Id == userId).FirstOrDefault();
            if (like != null)
            {
                context.Likes.Remove(like);
            }
            else
            {
                Like newLike = new Like() { Post = _post , AppUser = user , LikeId = Guid.NewGuid()};
                context.Likes.Add(newLike);
            }


             context.SaveChanges();
        }

        public Post GetPost(Guid postId)
        {
            Contract.Ensures(postId != null, "PostId is NULL");
            Contract.Ensures(context.Posts.Find(postId) != null);

            Post post = context.Posts.Find(postId);

            return post;
        }
         

        

        public void CreateUserWall(AppUser user)
        {
            var wall = new Wall() { AppUser = user, AppUserId = user.Id, WallId = Guid.NewGuid() , posts = new List<Post>()};

            var User =  FindUserById(user.Id);
            User.wall = wall;
           
            userManager.Update(User);
            
      
        }
    

    }
}
