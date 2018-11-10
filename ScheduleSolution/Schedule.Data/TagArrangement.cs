using System;
using System.Collections.Generic;
using System.Text;

namespace Schedule.Data
{
    public class TagArrangement
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ArrangementId { get; set; }
        public Arrangement Arrangement { get; set; }
    }
}
