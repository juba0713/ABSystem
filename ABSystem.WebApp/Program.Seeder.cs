using ABSystem.Data.Models;
using ABSystem.Data;
using Microsoft.AspNetCore.Identity;
using ABSystem.Resources.Constants;

namespace ABSystem.WebApp
{
    public class Seeder
    {
        public static void Seed(ABSystemDbContext context)
        {

            // Ensure database exists (use only for development/testing environments)
            context.Database.EnsureCreated();

            CreateRequiredFolders();

            // Seed roles
            SeedRoles(context);

            // Seed users
            SeedUsers(context);
        }

        private static void CreateRequiredFolders()
        {
            Console.WriteLine("FOLDERRRRRRRRRRRRRRRRRRRR");
            string rootPath = CommonConstant.RootPath;

  
            if (!Directory.Exists(rootPath))
            {
                Directory.CreateDirectory(rootPath);
            }

            string roomFolderPath = Path.Combine(rootPath, CommonConstant.RoomFileName);

            if (!Directory.Exists(roomFolderPath))
            {
                Directory.CreateDirectory(roomFolderPath);
            }

        }

        private static void SeedRoles(ABSystemDbContext context)
        {
            // Check and seed 'Admin' role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                context.Roles.Add(adminRole);
                Console.WriteLine("Admin role seeded successfully.");
            }

            // Check and seed 'SuperAdmin' role
            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var superAdminRole = new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN"
                };
                context.Roles.Add(superAdminRole);
                Console.WriteLine("SuperAdmin role seeded successfully.");
            }

            // Check and seed 'User' role
            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var userRole = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                };
                context.Roles.Add(userRole);
                Console.WriteLine("User role seeded successfully.");
            }

            context.SaveChanges();
        }

        private static void SeedUsers(ABSystemDbContext context)
        {
            var passwordHasher = new PasswordHasher<User>();

            // Seed Admin User
            if (!context.Users.Any(u => u.Email == "demo@admin.com"))
            {
                var admin = CreateUser("Admin", "Admin", "demo@admin.com", passwordHasher);
                context.Users.Add(admin);
                context.SaveChanges();
                AssignRole(context, admin, "Admin");
            }

            // Seed SuperAdmin User
            if (!context.Users.Any(u => u.Email == "demo@superadmin.com"))
            {
                var superAdmin = CreateUser("SuperAdmin", "SuperAdmin", "demo@superadmin.com", passwordHasher);
                context.Users.Add(superAdmin);
                context.SaveChanges();
                AssignRole(context, superAdmin, "SuperAdmin");
            }

            // Seed User
            if (!context.Users.Any(u => u.Email == "demo@user.com"))
            {
                var user = CreateUser("User", "User", "demo@user.com", passwordHasher);
                context.Users.Add(user);
                context.SaveChanges();
                AssignRole(context, user, "User");
            }
        }

        private static User CreateUser(string firstName, string lastName, string email, PasswordHasher<User> passwordHasher)
        {
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = 0
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "12345678");
            Console.WriteLine($"{firstName} {lastName} user seeded successfully.");
            return user;
        }

        private static void AssignRole(ABSystemDbContext context, User user, string roleName)
        {
            var roleId = context.Roles.First(r => r.Name == roleName).Id;
            context.UserRoles.Add(new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = roleId
            });
            context.SaveChanges();
            Console.WriteLine($"{user.Email} assigned to {roleName} role successfully.");
        }
    }
}
