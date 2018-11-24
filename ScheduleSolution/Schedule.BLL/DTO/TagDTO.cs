using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Schedule.Data.Validation;

namespace Schedule.BLL.DTO
{
    public class TagDto
    {
        public int TagId { get; set; }

        [Required]
        [DisplayName("Content")]
        [OnlyText(ErrorMessage = "Only text is allowed")]
        [MaxLength(20, ErrorMessage = "Too long value, please cut up to 20 symbols")]
        public string Content { get; set; }
            
        public bool Selected { get; set; }
    }
}
