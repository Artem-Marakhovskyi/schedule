using Schedule.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Schedule.Data
{
    public class Subject
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Too long value. Please cut to 100 symbols")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Too long value. Please cut to 100 symbols")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string Description { get; set; }
    }
}
