using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon.Dtos
{
    public class DtoConnection
    {
        public int Id { get; set; }
        public int UserIdOne { get; set; }
        public int UserIdTwo { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
