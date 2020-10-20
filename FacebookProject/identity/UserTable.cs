using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookProject.identity
{
    public class UserTable:IdentityUser
    {
        //public UserTable()
        //{
        //    Messages = new HashSet<Message>();
        //}

        public string FullName { get; set; }
        public string Password { get; set; }
        public string LiveCity { get; set; }
        public string Homeland { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProfilPhoto { get; set; }
        public string BackgorundImage { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
