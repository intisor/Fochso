using Fochso.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdealDiscuss.Context.EntityConfiguration
{
    public class TeacherEntityTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher");
            builder.HasKey(t => t.Id);

            //builder.HasOne(q => q.User)
            //    .WithMany(u => u.Questions)
            //    .HasForeignKey(q => q.UserId)
            //    .IsRequired();

            builder.Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(150);

			builder.Property(q => q.TeachingSubject)
			   .IsRequired()
			   .HasMaxLength(100000);
        }
    }
}
