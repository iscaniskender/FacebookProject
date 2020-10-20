using System;
using System.Collections.Generic;
using System.Text;

namespace Cammon.Dtos
{
    public class DtoLike
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
