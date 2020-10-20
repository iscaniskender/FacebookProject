using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LiveCity { get; set; }
        public string Homeland { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProfilPhoto { get; set; }
        public string BackgorundImage { get; set; }
        public String PhoneNumber { get; set; }
        public bool Private { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
