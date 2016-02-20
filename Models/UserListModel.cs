using MVCSocialMedia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Models
{
    public class UserListModel
    {
        public List<AppUser> Users { get; set; }

        public UserListModel()
        {
            Users = new List<AppUser>();
        }
    }
}