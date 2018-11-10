using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Schedule.Data.Context.Configuration
{
    class TagsArrangementConfiguration : IEntityTypeConfiguration<TagArrangement>
    {
        public void Configure(EntityTypeBuilder<TagArrangement> modelBuilder)
        {
            modelBuilder
                .HasKey(bc => new { bc.ArrangementId, bc.TagId });

            modelBuilder
                .HasOne(bc => bc.Tag)
                .WithMany(b => b.TagArrangements)
                .HasForeignKey(bc => bc.TagId);

            modelBuilder
                .HasOne(bc => bc.Arrangement)
                .WithMany(c => c.TagArrangements)
                .HasForeignKey(bc => bc.ArrangementId);
        }
    }
}
