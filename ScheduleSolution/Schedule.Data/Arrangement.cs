using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data
{
    public class Arrangement
    {
        public int Id { get; set; }

        public DateTimeOffset DateTime { get; set; }

        public bool IsHandled { get; set; }

        public int Complexity { get; set; }
        
        public int PersonId { get; set; }

        public int SubjectId { get; set; }

        public virtual Person Person { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual List<Tag> Tags { get; set; }
    }
}
