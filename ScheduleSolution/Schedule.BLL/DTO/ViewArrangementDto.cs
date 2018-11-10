using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace Schedule.BLL.DTO
{
    public class ViewArrangementDto
    {
        public int Id { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public bool IsHandled { get; set; }

        public int Complexity { get; set; }

        public string Subject { get; set; }


        public string SelectedPerson { get; set; }
        
        public IEnumerable<string> Tags { get; set; }

        public string TagsRepresentation => Tags.Join(",");

        public string DateTimeRepresentation => DateTime.ToString("d");
    }
}
