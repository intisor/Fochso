using Fochso.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Fochsso.Context
{
    public class ClassEntityTypeConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Class");

            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.Name)
                   .HasMaxLength(200);

            builder.Property(qr => qr.Description)
                .HasMaxLength(2000);

            builder.HasMany(qr => qr.Students)
                   .WithOne(qr => qr.ClassClass)
                   .HasForeignKey(qr => qr.ClassId);
        }
    }
}
