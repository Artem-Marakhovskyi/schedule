using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.BLL.DTO
{
    public class TagDto
    {
        public int TagId { get; set; }

        public string Content { get; set; }

        public bool Selected { get; set; }
    }
}
