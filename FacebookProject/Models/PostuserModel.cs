using Cammon;
using Cammon.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacebookProject.Models
{
    public class PostuserModel
    {

        public DtoUser User { get; set; }

        public List<PostTweetsUsers> posts { get; set; }

    }
}
