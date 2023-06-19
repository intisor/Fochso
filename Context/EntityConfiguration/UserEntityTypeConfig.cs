using Fochso.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fochso.Context.EntityConfiguration
{
    public class UserEntityTypeConfig : IEntityTypeConfiguration<User>
	{
        public void Configure(EntityTypeBuilder<User> builder)
        {
			builder.ToTable("Users");

			builder.HasKey(u => u.Id);

			builder.Property(u => u.UserName)
				.IsRequired()
				.HasMaxLength(10);

			//builder.Property(u => u.Password)
			//	.IsRequired();


			builder.Property(u => u.Email)
				.IsRequired();

			builder.Property(u => u.RoleId)
				.IsRequired();

			builder.Property(u => u.RoleName);

			builder.HasOne(u => u.Role)
				   .WithMany(sre => sre.Users)
				   .HasForeignKey(sre => sre.RoleId);
		}
    }
}



	

