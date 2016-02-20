using MVCSocialMedia.Abstract;
using MVCSocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSocialMedia.Controllers
{
    public class UserListController : Controller
    {
        private IRepository repository;

        public UserListController(IRepository repo)
        {
            repository = repo;
        }

        // GET: UserList
        public ActionResult Index()
        {
            UserListModel users = new UserListModel();
            users.Users = repository.Users.ToList();
            return View(users);
        }
    }
}