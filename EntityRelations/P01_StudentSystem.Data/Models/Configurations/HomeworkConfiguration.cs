
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Models.Configurations
{
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> builder)
        {
            builder.HasKey(hs => hs.HomeworkId);

            builder.Property(hs => hs.Content)
                .IsRequired(false);

            builder.Property(hs => hs.SubmissionTime)
                .IsRequired();

            builder.HasOne(hs => hs.Student)
                .WithMany(s => s.HomeworkSubmissions)
                .HasForeignKey(hs => hs.StudentId);

            builder.HasOne(hs => hs.Course)
                .WithMany(c => c.HomeworkSubmissions)
                .HasForeignKey(hs=>hs.CourseId);                
        }
    }
}
