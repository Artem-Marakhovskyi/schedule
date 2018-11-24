using Schedule.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.BLL.DTO
{
    public class PersonDto
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Too long description")]
        [Required]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string ShortDescription { get; set; }
    }
}
