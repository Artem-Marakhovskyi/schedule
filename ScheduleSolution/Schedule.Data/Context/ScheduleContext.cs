using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace Schedule.Data.Context
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext() : base("ScheduleConnection")
        {
                
        }
        
        public DbSet<Person> People { get; set; }

        public DbSet<Arrangement> Arrangements { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Subject> Subjects { get; set; }
    }
}
