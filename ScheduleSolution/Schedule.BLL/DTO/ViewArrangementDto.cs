using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace Schedule.BLL.DTO
{
    public class ViewArrangementDto
    {
        public int Id { get; set; }

        [DisplayName("Start time for arrangmenet")]
        public DateTimeOffset DateTime { get; set; }

        [DisplayName("Is handled?")]
        public bool IsHandled { get; set; }
        
        [DisplayName("Complexity")]
        public int Complexity { get; set; }

        [DisplayName("Subject name")]
        public string Subject { get; set; }

        [DisplayName("Selected person name")]
        public string SelectedPerson { get; set; }
        
        public IEnumerable<string> Tags { get; set; }

        [DisplayName("Tags list")]
        public string TagsRepresentation => Tags.Join(",");

        [DisplayName("Arrangment date")]
        public string DateTimeRepresentation => DateTime.ToString("d");
    }
}
