using Fochso.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fopchso.Context.EntityConfiguration
{
    public class StudenEntityTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(c => new { c.Name, c.Id })
             .IsUnique();

            builder.Property(c => c.Class)
                .HasMaxLength(200);

            builder.HasOne(qr => qr.ClassClass)
                   .WithMany(qr => qr.Students)
                   .HasForeignKey(qr => qr.ClassId)
                   .IsRequired();
        }
    }
}
