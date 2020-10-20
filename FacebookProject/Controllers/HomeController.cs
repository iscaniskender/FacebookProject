using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FacebookProject.Models;
using BLL.Concrete;
using FacebookProject.identity;
using Microsoft.AspNetCore.Identity;
using Cammon.Dtos;

namespace FacebookProject.Controllers
{
    public class HomeController : Controller
    {

        public readonly ApplicationContext _context;
        public readonly UserManager<UserTable> _userMenager;
        public HomeController(ApplicationContext context, UserManager<UserTable> userMenager)
        {
            _context = context;
            _userMenager = userMenager;
        }
        UserMenager UserM = new UserMenager();
        PostMenager PosM = new PostMenager();
        ConnectionMenager ConnM = new ConnectionMenager();
        public IActionResult Index()
        {
            var owneresult = UserM.GetDtoUser(User.Identity.Name);
            var count = ConnM.FriendCount(owneresult.UserId);
            ViewBag.Count = count;

            var postlist = ConnM.GetFriendsConn(owneresult.UserId);

            return View(new PostuserModel
            {
                posts = postlist,
                User = owneresult
            });
        }

        public IActionResult PushPost(DtoPost post)
        {
            int data = 0;
            if (post.Description == null && post.Photo == null)
            {
                data = 0;
            }
            else
            {
                PosM.CreatePost(post);
                data = 1;
            }
            return Json(data);
        }
        public IActionResult RefleshCreatePost()
        {
            var gelen = UserM.GetDtoUser(User.Identity.Name);
            PostuserModel pum = new PostuserModel();
            pum.User = gelen;
            return PartialView("_CreatePost", pum);
        }

        public IActionResult RefleshPostList()
        {
            var owneresult = UserM.GetDtoUser(User.Identity.Name);
            var postlist = ConnM.GetFriendsConn(owneresult.UserId);

            PostuserModel pum = new PostuserModel();
            pum.User = owneresult;
            pum.posts = postlist;
            return PartialView("_Post", pum);
        }
        public IActionResult Search(string aranan)
        {
            var result = UserM.Search(aranan);

            if (result != null)
            {
                return RedirectToAction("ProfilIndex", "Profil", new { id = result.UserId });
            }
            else
            {
                return Redirect("Error");
            }



        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
