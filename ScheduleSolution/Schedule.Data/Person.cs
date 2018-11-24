using Schedule.Data.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Too long value. Please cut to 100 symbols")]
        [DisplayName("Name")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Too long value. Please cut to 100 symbols")]
        [DisplayName("Surname")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Too long value. Please cut to 100 symbols")]
        [DisplayName("Alternative ego")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string AlternativeEgo { get; set; }

        public virtual List<Arrangement> Arrangements { get; set; }
    }
}
