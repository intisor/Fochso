using Fochso.Entities;

namespace Fochso.Context
{
    internal class DbInitializer
    {
        internal static void Initialize(FochsoContext context)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            context.Database.EnsureCreated();

            if (context.Roles.Any())
            {
                return;
            }

            var roles = new Role[]
            {
                new Role()
                {
                    RoleName = "Admin",
                    Description = "Role for admin",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    LastModified = new DateTime() //0001-01-01 00:00:00:00
                },
                new Role()
                {
                    RoleName = "AppUser",
                    Description = "Role for nominal user",
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    LastModified = new DateTime()
                }
            };

            foreach (var r in roles)
            {
                context.Roles.Add(r);
            }

            context.SaveChanges();

            //var password = "p@ssword1";
            //var salt = HashingHelper.GenerateSalt();
            var admin = context.Roles.Where(r => r.RoleName == "Admin").SingleOrDefault();

            var users = new User[]
            {
                new User()
                {
                    UserName = "Admin",
                    //HashSalt = salt,
                    //PasswordHash = HashingHelper.HashPassword(password, salt),
                    Password = "p@ssword1",
                    Email = "admin@gmail.com",
                    RoleId = admin.Id,
                    RoleName = admin.RoleName,
                    CreatedBy = "System",
                    DateCreated = DateTime.Now,
                    LastModified = new DateTime()
                }
            };

            foreach (var u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
