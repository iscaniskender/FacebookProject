using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BLL.Concrete;
using Cammon;
using Cammon.Dtos;
using FacebookProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FacebookProject.Controllers
{
    public class ProfilController : Controller
    {
        UserMenager UserM = new UserMenager();
        ConnectionMenager ConnM = new ConnectionMenager();
        PostMenager PostM = new PostMenager();
        public IActionResult ProfilIndex(int id)
        {
            var result = UserM.GetDtoUserbyId(id);
            var owner = UserM.GetDtoUser(User.Identity.Name);
            if(result==null)
            {
                return RedirectToAction("Error","Home");
            }
            if (result.UserId == owner.UserId)
            {
                ViewBag.durum = "Profili Düzenle";
            }

            DtoConnection f1 = ConnM.getconnection1(result.UserId, owner.UserId);
            DtoConnection f2 = ConnM.getconnection2(result.UserId, owner.UserId);


            if (result.UserId != owner.UserId)
            {
                ViewBag.UserType = "Guest";
            }

            if (f1 == null && f2 == null)
            {
                ViewBag.NotFriends = "True";
            }
            if (f1 != null)
            {
                if (!f1.IsActive)
                {
                    ViewBag.NotFriends = "Pending";
                }
            }
            if (f2 != null)
            {
                if (!f2.IsActive)
                {
                    ViewBag.NotFriends = "Pending";
                }
            }

            PostuserModel model = new PostuserModel();
            model.User = result;
            model.posts = UserM.sprofiltweet(id);
            return View(model);
        }
        public IActionResult AddFriend(string friend)
        {
            var friendresult = UserM.GetDtoUser(friend);

            var owneresult = UserM.GetDtoUser(User.Identity.Name);

            DtoConnection conn = new DtoConnection();

            conn.CreatedDate = DateTime.Now;
            conn.IsActive = false;
            conn.IsDeleted = false;
            conn.UserIdOne = owneresult.UserId;
            conn.UserIdTwo = friendresult.UserId;

            ConnM.Create(conn);
            return Json(true);
        }
        public IActionResult DisplayRequestFriend()
        {
            var owneresult = UserM.GetDtoUser(User.Identity.Name);
            var list = ConnM.DisplayFriendRequest(owneresult.UserId);
            return View(list);
        }
        public IActionResult AcceptFriends(int userid)
        {
            ConnM.AcceptFriend(userid, User.Identity.Name);
            return Json(true);
        }
        public IActionResult deleteFriends(int userid)
        {
            ConnM.DeleteFriend(userid, User.Identity.Name);
            return Json(true);
        }

        //public IActionResult partialpost(int id)
        //{

        //    var list = PostM.GetPostbyUserId(id);
        //    return PartialView("_Post",list);
        //}
    }

}
