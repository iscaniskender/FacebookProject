using Cammon;
using Cammon.Dtos;
using DAL;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BLL.Concrete
{
    public class ConnectionMenager
    {
        FacebookContext context = new FacebookContext();
        UserMenager userM = new UserMenager();


        public DtoConnection getconnection1(int userid, int friendid)
        {
            var result = context.connections.Where(x => x.UserIdOne == userid && x.UserIdTwo == friendid).FirstOrDefault();
            if (result == null)
            {
                return null;
            }
            else
            {
                DtoConnection conn = new DtoConnection();
                conn.CreatedDate = result.CreatedDate;
                conn.IsActive = result.IsActive;
                conn.IsDeleted = result.IsDeleted;
                conn.UserIdOne = result.UserIdOne;
                conn.UserIdTwo = result.UserIdTwo;
                return conn;
            }
        }
        public DtoConnection getconnection2(int userid, int friendid)
        {
            var result = context.connections.Where(x => x.UserIdOne == friendid && x.UserIdTwo == userid).FirstOrDefault();

            if (result == null)
            {
                return null;
            }
            else
            {
                DtoConnection conn = new DtoConnection();
                conn.CreatedDate = result.CreatedDate;
                conn.IsActive = result.IsActive;
                conn.IsDeleted = result.IsDeleted;
                conn.UserIdOne = result.UserIdOne;
                conn.UserIdTwo = result.UserIdTwo;
                return conn;
            }
        }
        public void Create(DtoConnection conn)
        {
            Connection Econn = new Connection();
            Econn.CreatedDate = conn.CreatedDate;
            Econn.IsActive = conn.IsActive;
            Econn.IsDeleted = conn.IsDeleted;
            Econn.UserIdOne = conn.UserIdOne;
            Econn.UserIdTwo = conn.UserIdTwo;
            context.connections.Add(Econn);
            context.SaveChanges();

        }
        public int FriendCount(int userid)
        {
            var friendcount = context.connections.Count(a => a.UserIdTwo == userid && a.IsActive == false);
            return friendcount;
        }
        public List<DtoUser> DisplayFriendRequest(int userid)
        {
            var result = context.connections.Where(x => x.UserIdTwo == userid && x.IsActive == false);

            List<DtoUser> users = new List<DtoUser>();

            foreach (var item in result)
            {
                var user = userM.GetDtoUserbyId(item.UserIdOne);
                users.Add(user);
            }
            return users;
        }
        public void AcceptFriend(int userid, string ownername)
        {
            var user = context.Users.Where(x => x.Username == (ownername)).FirstOrDefault();
            int userId = user.Id;

            var friend = context.connections.Where(x => x.UserIdOne == userid && x.UserIdTwo == userId).FirstOrDefault();
            friend.IsActive = true;
            context.SaveChanges();
        }
        public void DeleteFriend(int userid, string ownername)
        {
            var user = context.Users.Where(x => x.Username == (ownername)).FirstOrDefault();
            int userId = user.Id;

            var friend = context.connections.Where(x => x.UserIdOne == userid && x.UserIdTwo == userId).FirstOrDefault();
            context.Remove(friend);
            context.SaveChanges();
        }

        public List<PostTweetsUsers> GetFriendsConn(int id)
        {
            List<DtoUser> userid = new List<DtoUser>();

            var result1 = context.connections.Where(x => x.UserIdOne == id && x.IsActive == true).ToList();
            foreach (var item in result1)
            {
                DtoUser user1 = new DtoUser();
                user1.UserId = item.UserIdTwo;
                userid.Add(user1);
            }
            var result2 = context.connections.Where(x => x.UserIdTwo == id && x.IsActive == true).ToList();
            foreach (var item in result2)
            {
                DtoUser user2 = new DtoUser();
                user2.UserId = item.UserIdOne;
                userid.Add(user2);
            }
            DtoUser user = new DtoUser();
            user.UserId = id;
            userid.Add(user);


            List<DtoUser> users = new List<DtoUser>();

            foreach (var item in userid)
            {
                DtoUser user3 = new DtoUser();
                user3 = userM.GetDtoUserbyId(item.UserId);
                users.Add(user3);
            }

            List<PostTweetsUsers> ptu = new List<PostTweetsUsers>();

            foreach (var item in users)
            {
                var gelen = context.Post.Where(x => x.UserID == item.UserId)
                                                        .Include(x => x.Likes)
                                                        .Include(x => x.Shares).
                                                        Include(x => x.Comments)
                                                        .ToList();

                foreach (var posts in gelen)
                {
                    List<DtoComment> commenDtoList = new List<DtoComment>();
                    PostTweetsUsers post = new PostTweetsUsers();
                    post.Likeactive = posts.Likes.Any(x => x.IsActive == true);
                    post.Shareactive = posts.Shares.Any(x => x.IsActive == true);
                    post.UserId = item.UserId;
                    post.Name = item.FullName;
                    post.useractive = item.IsActive;
                    post.userdelete = item.IsDeleted;
                    post.Postid = posts.Id;
                    post.ProfilPhoto = item.ProfilPhoto;
                    post.PostPhoto = posts.Photo;
                    post.Text = posts.Description;
                    post.Createddate = posts.CreatedDate;
                    post.LikeCount = posts.Likes.Where(x => x.IsDeleted == false && x.IsActive == true).Count();
                    post.ShareCount = posts.Likes.Where(x => x.IsDeleted == false && x.IsActive == true).Count();
                    post.ReplyCount = posts.Comments.Where(x => x.IsDeleted == false && x.IsActive == true).Count();


                    var result = context.comments.Where(x => x.PostId == posts.Id).ToList();
                    if (result != null)
                    {
                        foreach (var item2 in result)
                        {
                            DtoComment comment = new DtoComment();
                           
                            comment.Description = item2.Description;
                            comment.UserId = item2.UserId;
                            comment.PostId = item2.PostId;
                            comment.IsActive = item2.IsActive;
                            comment.IsDeleted = item2.IsDeleted;
                            comment.Photo = userM.GetDtoUserbyId(item2.UserId).ProfilPhoto;
                            comment.Fullname = userM.GetDtoUserbyId(item2.UserId).FullName;

                            commenDtoList.Add(comment);
                        }
                    }
                    post.comments = commenDtoList;
                    ptu.Add(post);
                }
            }
            return ptu.OrderByDescending(x => x.Createddate).ToList();
        }
    }
}
