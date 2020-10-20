using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cammon.Dtos;
using DAL;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FacebookProject.Controllers
{
    public class PostController : Controller
    {
        FacebookContext context = new FacebookContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetIndex()
        {
            return View();
        }
        public IActionResult Likeoperation(Like entity)
        {
                var sorgu = context.Likes.Where(x => x.PostId == entity.PostId && x.UserId == entity.UserId).FirstOrDefault();
               
            if(sorgu==null)
            {
                entity.IsActive = true;

                context.Likes.Add(entity);
                context.SaveChanges();
            }
            else
            {
                sorgu.IsActive =  true;
                context.Likes.Update(sorgu);
                context.SaveChanges();
            } 
            return Json(true);
        }
        public IActionResult Disslike(Like entity)
        {
            var sorgu = context.Likes.Where(x => x.PostId == entity.PostId && x.UserId == entity.UserId).FirstOrDefault();
            sorgu.IsActive = false;
            context.Likes.Update(sorgu);
            context.SaveChanges();
            return Json(true);
        }
    }
}
