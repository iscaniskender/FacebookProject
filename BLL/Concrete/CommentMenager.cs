using Cammon.Dtos;
using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Concrete
{
    public  class CommentMenager
    {
        FacebookContext db = new FacebookContext();
        public void AddComment(DtoComment commentDto)
        {
            Comment comment = new Comment();
            comment.UserId = commentDto.UserId;
            comment.PostId = commentDto.PostId;
            comment.Description = commentDto.Description;
            comment.CommentCount = commentDto.CommentCount;
            comment.IsDeleted = commentDto.IsDeleted;
       
            db.comments.Add(comment);
            db.SaveChanges();
        }
    }
}
