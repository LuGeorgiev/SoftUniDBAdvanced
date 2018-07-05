using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace P01_StudentSystem.Data.Models.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.StudentId);

            builder.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode();

            builder.Property(s => s.PhoneNumber)
                .HasColumnType("NCHAR(10)")                
                .IsRequired(false)
                .IsUnicode(false);

            builder.Property(s => s.RegisterdOn)
                .IsRequired();

            builder.Property(s => s.BirthDate)
                .IsRequired(false);
        }
    }
}
