

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(r => r.CourseId);

            builder.Property(r => r.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder.Property(c => c.Url)
                .IsRequired()
                .IsUnicode(false);

            builder.Property(r => r.ResourceType)
                .IsRequired();


            builder.HasOne(r => r.Course)
                .WithMany(c => c.Resources)
                .HasForeignKey(r => r.CourseId);
        }
    }
}
