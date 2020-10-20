using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int Replyto { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isactive { get; set; }

        public bool isdelete { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Share> Shares { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
