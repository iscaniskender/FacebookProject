using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon
{
    public class DtoUser
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LiveCity { get; set; }
        public string Homeland { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProfilPhoto { get; set; }
        public string BackgroundImage { get; set; }
        public String PhoneNumber { get; set; }
        public bool Private { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
  
    }
}
