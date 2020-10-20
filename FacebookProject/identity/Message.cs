using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookProject.identity
{
    public class Message
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string text { get; set; }
        [Required]
        public DateTime When { get; set; }
        public string UserID { get; set; }
        public virtual  UserTable User { get; set; }
    }
}
