using MVCSocialMedia.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCSocialMedia.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }

        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public byte[] Avatar { get; set; }
        public string ImageMimeType { get; set; }
        public Wall Wall { get; set; }

        public UserModel() { }

        public UserModel(AppUser user)
        {
            this.UserId = user.Id;
            this.Name = user.Name;
            this.Surname = user.Surname;
            this.Country = user.Country;
            this.BirthDate = user.BirthDate;
            this.Email = user.Email;
            this.Phone = user.PhoneNumber;
            this.Avatar = user.Avatar;
            this.ImageMimeType = user.ImageMimeType;
            this.Wall = user.wall;

        }

        
    }
}