using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.Data
{
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string AlternativeEgo { get; set; }

        public virtual List<Arrangement> Arrangements { get; set; }
    }
}
