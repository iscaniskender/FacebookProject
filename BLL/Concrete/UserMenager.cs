
using Cammon;
using Cammon.Dtos;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Concrete
{
    public class UserMenager
    {
        User _user = new User();

        FacebookContext context = new FacebookContext();
        public void Create(DtoUser dtouser)
        {
            _user.FullName = dtouser.FullName;
            _user.Username = dtouser.Username;
            _user.Password = dtouser.Password;
            _user.LiveCity = dtouser.LiveCity;
            _user.Homeland = dtouser.Homeland;
            _user.ProfilPhoto = dtouser.ProfilPhoto;
            _user.BackgorundImage = dtouser.BackgroundImage;
            _user.PhoneNumber = dtouser.PhoneNumber;
            _user.CreatedDate = DateTime.Now;
            _user.IsActive = true;
            _user.IsDeleted = false;
            _user.Private = true;

            context.Users.Add(_user);
            context.SaveChanges();
        }
        public DtoUser GetDtoUser(string username)
        {
            var result = context.Users.Where(x => x.Username == username).FirstOrDefault();

            DtoUser dtouser = new DtoUser();
            dtouser.FullName = result.FullName;
            dtouser.CreatedDate = result.CreatedDate;
            dtouser.ProfilPhoto = result.ProfilPhoto;
            dtouser.BackgroundImage = result.BackgorundImage;
            dtouser.LiveCity = result.LiveCity;
            dtouser.Homeland = result.Homeland;
            dtouser.PhoneNumber = result.PhoneNumber;
            dtouser.IsActive = result.IsActive;
            dtouser.IsDeleted = result.IsDeleted;
            dtouser.Private = result.Private;
            dtouser.Username = result.Username;
            dtouser.UserId = result.Id;

            return dtouser;
        }
        public DtoUser Search(string aranan)
        {
            if (aranan == null)
            {
                return null;
            }
            var result = context.Users.Where(x => x.FullName.ToLower() == aranan.ToLower()).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                DtoUser dtouser = new DtoUser();
                dtouser.FullName = result.FullName;
                dtouser.CreatedDate = result.CreatedDate;
                dtouser.ProfilPhoto = result.ProfilPhoto;
                dtouser.BackgroundImage = result.BackgorundImage;
                dtouser.LiveCity = result.LiveCity;
                dtouser.Homeland = result.Homeland;
                dtouser.PhoneNumber = result.PhoneNumber;
                dtouser.IsActive = result.IsActive;
                dtouser.IsDeleted = result.IsDeleted;
                dtouser.Private = result.Private;
                dtouser.Username = result.Username;
                dtouser.UserId = result.Id;
                return dtouser;
            }

        }



        public DtoUser GetDtoUserbyId(int id)
        {
            var result = context.Users.Where(x => x.Id == id).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                DtoUser dtouser = new DtoUser();
                dtouser.FullName = result.FullName;
                dtouser.CreatedDate = result.CreatedDate;
                dtouser.ProfilPhoto = result.ProfilPhoto;
                dtouser.BackgroundImage = result.BackgorundImage;
                dtouser.LiveCity = result.LiveCity;
                dtouser.Homeland = result.Homeland;
                dtouser.PhoneNumber = result.PhoneNumber;
                dtouser.IsActive = result.IsActive;
                dtouser.IsDeleted = result.IsDeleted;
                dtouser.Private = result.Private;
                dtouser.Username = result.Username;
                dtouser.UserId = result.Id;
                return dtouser;
            }

        }
        public List<PostTweetsUsers> sprofiltweet(int id)
        {
            var user = GetDtoUserbyId(id);

            List<PostTweetsUsers> ptu = new List<PostTweetsUsers>();

                var gelen = context.Post.Where(x => x.UserID == id)
                                                        .Include(x => x.Likes)
                                                        .Include(x => x.Shares).
                                                        Include(x => x.Comments)
                                                        .ToList();

                foreach (var posts in gelen)
                {
                    PostTweetsUsers post = new PostTweetsUsers();
                    post.Likeactive = posts.Likes.Any(x => x.UserId == id && x.PostId == posts.Id && x.IsDeleted == false);
                    post.Shareactive = posts.Likes.Any(x => x.UserId == id && x.PostId == posts.Id && x.IsDeleted == false);
                    post.UserId = user.UserId;
                    post.Name = user.FullName;
                    post.useractive = user.IsActive;
                    post.userdelete = user.IsDeleted;
                    post.Postid = posts.Id;
                    post.ProfilPhoto = user.ProfilPhoto;
                    post.PostPhoto = posts.Photo;
                    post.Text = posts.Description;
                    post.Createddate = posts.CreatedDate;
                    post.LikeCount = posts.Likes.Where(x => x.IsDeleted == false && x.IsActive == true).Count();
                    post.ShareCount = posts.Likes.Where(x => x.IsDeleted == false && x.IsActive == true).Count();
                    post.ReplyCount = posts.Comments.Where(x => x.IsDeleted == false && x.IsActive == true).Count();
                    ptu.Add(post);
                }
            
            return ptu.OrderByDescending(x=>x.Createddate).ToList();
        }
    }
}
