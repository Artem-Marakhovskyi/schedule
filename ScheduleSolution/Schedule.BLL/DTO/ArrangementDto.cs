using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.BLL.DTO
{
    public class ArrangementDto
    {
        public int Id { get; set; }

        public DateTimeOffset DateTime { get; set; } = DateTimeOffset.Now;

        public bool IsHandled { get; set; }

        public int Complexity { get; set; }
        
        public int SubjectId { get; set; }
        

        public int SelectedPersonId { get; set; }

        public IEnumerable<PersonDto> AvailablePeople { get; set; }


        public IEnumerable<SubjectDto> AvailableSubjects { get; set; }

        public int SelectedSubjectId { get; set; }


        public IEnumerable<TagDto> AvailableTags { get; set; }
    }
}
