using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BLL.Concrete;
using Cammon;
using FacebookProject.identity;
using FacebookProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace FacebookProject.Controllers
{
    public class AccountController : Controller
    {
        UserMenager UserM = new UserMenager();

        //parola sıfırlama giriş herşeyi yöneten sınıf

        private UserManager<UserTable> _userMenager;

        //cookieyi yöneten kısım

        private SignInManager<UserTable> _signInMenager;
        private IHostingEnvironment _Environment;

        public AccountController(UserManager<UserTable> userMenager, SignInManager<UserTable> signInMenager, IHostingEnvironment environment)
        {
            _userMenager = userMenager;
            _signInMenager = signInMenager;
            _Environment = environment;
        }
        public IActionResult LoginIndex()
        {

            return View();

        }

        ////csrf token securirity
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        public async Task<IActionResult> Register(DtoUser register)
        {

            var User = new UserTable()
            {
                Email = register.Username,
                FullName = register.FullName,
                UserName = register.Username,
                Password = register.Password,
                LiveCity = register.LiveCity,
                Homeland = register.Homeland,
                ProfilPhoto = register.ProfilPhoto,
                BackgorundImage = register.BackgroundImage,
                PhoneNumber = register.PhoneNumber

            };
            int data = 0;

            //UserM.Create(register);

            var result = await _userMenager.CreateAsync(User, register.Password);
            if (result.Succeeded)
            {

                //generate Token
                //Email
                data = 1;
                await Task.Run(() => UserM.Create(register));

            }
            else
            {
                data = 2;
                //hata fırlat ajaxla    
            }
            return Json(data);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var User = new UserTable()
            {
                UserName = model.Username,
                Password = model.Password
            };
            var userresult = await _userMenager.FindByNameAsync(User.UserName);

            int data = 0;
            if (userresult == null)
            {
                data = 0;
                //ajax hata mesajı gönder
            }
            var result = await _signInMenager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result.Succeeded)
            {
                data = 1;

            }
            //ajaxla hata fırlat
            return Json(data);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInMenager.SignOutAsync();
            return Redirect("LoginIndex");
        }

        //public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        //{
        //    long size = files.Sum(f => f.Length);

        //    // full path to file in temp location
        //    var filePath = Path.GetTempFileName();

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await formFile.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    // process uploaded files
        //    // Don't rely on or trust the FileName property without validation.

        //    return Ok(new { count = files.Count, size, filePath });
        //}
        public IActionResult UploadPostImage()
        {
            string save_path = this._Environment.WebRootPath + "\\image\\";
            var uploaded_files = Request.Form.Files;

            foreach (var file in uploaded_files)
            {
                string file_name = file.FileName;
                string filename_server = save_path + "\\" + file_name;

                try
                {

                }
                catch
                {
                    using (FileStream stream = new FileStream(filename_server, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

            }

            return null;
        }

    }
}
