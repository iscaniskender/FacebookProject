using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity
{
   public class Connection
    {
        [ForeignKey("User")]
        public int UserIdOne { get; set; }
        public User UserOne { get; set; }
        [ForeignKey("User")]
        public int UserIdTwo { get; set; }
        public User UserTwo { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

    }
}
