using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon.Dtos
{
    public  class DtoComment
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentCount { get; set; }
        public int Commentlike { get; set; }
        public string Description { get; set; }
        public int CommentTo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public string Fullname { get; set; }
        public string Photo { get; set; }
       
    }
}
