using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon.Dtos
{
    public class PostTweetsUsers
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string ProfilPhoto { get; set; }
        public int Postid { get; set; }
        public string Text { get; set; }
        public DateTime Createddate { get; set; }
        public int Replyto { get; set; }
        public string PostPhoto { get; set; }
        public bool useractive { get; set; }
        public bool userdelete { get; set; }
        public int LikeCount { get; set; }

        public bool Likeactive { get; set; }

        public int ShareCount { get; set; }

        public bool Shareactive { get; set; }
        public int ReplyCount { get; set; }
    }
}
