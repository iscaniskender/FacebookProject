using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; }
        public Post Post{ get; set; }
        public int CommentCount { get; set; }
        public int Commentlike { get; set; }
        public string Description { get; set; }
        public int CommentTo{ get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
