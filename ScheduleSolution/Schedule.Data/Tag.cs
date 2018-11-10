using Schedule.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data
{
    public class Tag
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public virtual ICollection<TagArrangement> TagArrangements { get; set; }
    }
}
