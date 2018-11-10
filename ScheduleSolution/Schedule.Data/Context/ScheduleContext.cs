using Microsoft.EntityFrameworkCore;
using Schedule.Data.Context.Configuration;

namespace Schedule.Data.Context
{
    public class ScheduleContext : DbContext
    {
        public ScheduleContext(DbContextOptions<ScheduleContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TagsArrangementConfiguration());
        }

        public DbSet<Person> People { get; set; }

        public DbSet<Arrangement> Arrangements { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TagArrangement> TagArrangement { get; set; }
    }
}
