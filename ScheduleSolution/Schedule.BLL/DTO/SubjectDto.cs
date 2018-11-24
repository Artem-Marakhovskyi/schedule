using Schedule.Data.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Schedule.BLL.DTO
{
    public class SubjectDto
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Too long label for subject")]
        [DisplayName("Subject label")]
        [Required]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        public string Label { get; set; }
    }
}
