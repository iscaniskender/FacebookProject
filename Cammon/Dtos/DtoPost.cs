using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon.Dtos
{
    public class DtoPost
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public int Replyto { get; set; }
        public int UserID { get; set; }
        public string Fullname { get; set; }
        public bool isactive { get; set; }
        public bool isdeleted { get; set; }

    }
}
