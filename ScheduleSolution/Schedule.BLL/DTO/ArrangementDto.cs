using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.BLL.DTO
{
    public class ArrangementDto
    {
        public int Id { get; set; }

        [DisplayName("Start date for arrangement")]
        [Required]
        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;

        [DisplayName("Is handled")]
        [Required]
        public bool IsHandled { get; set; }

        [Range(0, 10)]
        [Required]
        public int Complexity { get; set; }
        
        public int SubjectId { get; set; }
        

        public int SelectedPersonId { get; set; }

        [DisplayName("Available people for arrangement")]
        public IEnumerable<PersonDto> AvailablePeople { get; set; }

        [DisplayName("Available subjects for arrangement")]
        public IEnumerable<SubjectDto> AvailableSubjects { get; set; }

        public int SelectedSubjectId { get; set; }

        [DisplayName("Available tags for arrangement")]
        public IEnumerable<TagDto> AvailableTags { get; set; }
    }
}
