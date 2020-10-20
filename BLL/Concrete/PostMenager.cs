using Cammon.Dtos;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace BLL.Concrete
{
    public class PostMenager
    {
        FacebookContext fbcontext = new FacebookContext();
        public void CreatePost(DtoPost post)
        {
            Post postentity = new Post();
            postentity.CommentCount = post.CommentCount;
            postentity.Description = post.Description;
            postentity.LikeCount = post.LikeCount;
            postentity.Photo = post.Photo;
            postentity.Replyto = post.Replyto;
            postentity.UserID = post.UserID;
            postentity.CreatedDate = DateTime.Now;
            postentity.isactive = true;
            postentity.isdelete = false; 

        fbcontext.Post.Add(postentity);
            fbcontext.SaveChanges();
        }

        public List<DtoPost> GetPostbyUserId(int id)
        {
            var result = fbcontext.Post.Where(x => x.UserID == id).Select(a => new DtoPost()
            {
                Description = a.Description,
                Photo = a.Photo,
                Id = a.Id,
                CommentCount = a.CommentCount,
                LikeCount = a.LikeCount,
                Replyto = a.Replyto,
                UserID = a.UserID,
                Fullname = a.User.FullName
            }).ToList();


            return result;

        }

    }
}
