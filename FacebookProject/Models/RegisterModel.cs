using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookProject.Models
{
    public class RegisterModel
    {
        public string FullName { get; set;} 
        public string Password { get; set; }
        public string RePassword { get; set; }
        public string Username { get; set; }
        public string LiveCity { get; set; }
        public string Homeland { get; set; }
        public string ProfilPhoto { get; set; }
        public string BackgorundImage { get; set; }
        public string PhoneNumber { get; set; }
    }
}
