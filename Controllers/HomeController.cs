using Microsoft.AspNet.Identity;
using MVCSocialMedia.Abstract;
using MVCSocialMedia.Concrete;
using MVCSocialMedia.Entities;
using MVCSocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCSocialMedia.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;
        

        public HomeController (IRepository repo)
        {
            repository = repo;
            
        }

        public ActionResult Index(string userId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                userId = User.Identity.GetUserId();
                Session["userId"] = userId;
            }

            var currentUser =  repository.FindUserById(userId);
            
            UserModel user;
            if (currentUser!= null)
            {
                if (currentUser.wall == null)
                {
                     repository.CreateUserWall(currentUser);
                    currentUser.wall =  new Wall();
                    
                }

                user = new UserModel(currentUser);
                
            }
            else
            {
                user = new UserModel() { Wall = new Wall()  };
            }
            var posts = from post in user.Wall.posts
                        orderby post.CreationTime descending
                        select post;
            user.Wall.posts = posts.ToList();


            return View(user);
        }

        public ViewResult Edit()
        {
            var currentUser =  repository.FindUserById(User.Identity.GetUserId());
            UserModel user = new UserModel(currentUser);
            return View(user);

        }

        [HttpPost]
        public ActionResult Edit(UserModel user, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                
                if (image != null)
                {
                    user.ImageMimeType = image.ContentType;
                    user.Avatar = new byte[image.ContentLength];
                    image.InputStream.Read(user.Avatar, 0, image.ContentLength);
                }
                
                var result =  repository.UpdateUser(user);

                if (result.Succeeded)
                {
                   
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }

                return View();

                
            }
            else
            {

                return View(user);
            }
        }

        [HttpPost]
        public RedirectResult AddPost(UserModel user, string content, HttpPostedFileBase image = null )
        {
            var User = repository.FindUserById(user.UserId);
            var Post = new Post() { author = User, Content = content, wall = User.wall };

            if (image != null)
            {
                Post.ImageMimeType = image.ContentType;
                Post.Img = new byte[image.ContentLength];
                image.InputStream.Read(Post.Img, 0, image.ContentLength);
            }

             repository.AddPost(Post);

            if (!Url.IsLocalUrl(user.ReturnUrl))
            {
                return Redirect("/");
            }
            else
            {
                return Redirect(user.ReturnUrl);
            }
         
        }

        [HttpPost]
        public RedirectResult DeletePost(Guid postId, string ownerId, string returnUrl)
        {
            if (postId != null)
            {
                 repository.DeletePost(postId, ownerId);
            }

            if (!Url.IsLocalUrl(returnUrl))
            {
                return Redirect("/");
            }
            else
            {
                return Redirect(returnUrl);
            }

        }

       
        [ValidateInput(false)]
        public JsonResult LikePost(string postId)
         {
            if(postId != null)
            {
                var userId = User.Identity.GetUserId();
                repository.LikePost(Guid.Parse(postId), userId);
            }
            Post post = repository.GetPost(Guid.Parse(postId));
            int likesCount = post.likes.Count;
            return Json(likesCount);
            
        }

        public FileContentResult GetImage(string userid)
        {
            if (userid == null)
            {
                var currentuser =  repository.FindUserById(User.Identity.GetUserId());
                return File(currentuser.Avatar, currentuser.ImageMimeType);
            }
            else
            {
                AppUser User = repository.Users.FirstOrDefault(p => p.Id == userid);
                if (User != null)
                {
                    return File(User.Avatar, User.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
            
        }

        public FileContentResult GetPostImage(Guid postid)
        {
            if (postid == null)
            {
                return null;
            }
            else
            {
                var Post = repository.Posts.Where(p => p.PostId == postid).FirstOrDefault();
                if (Post != null)
                {
                    return File(Post.Img, Post.ImageMimeType);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}