using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineClassRegister.Areas.Identity.Data;

namespace OnlineClassRegister.Models
{
    // Class responsible for creating roles and admin user 
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context =
                new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            string[] roles = { "Administrator", "Teacher", "Parent", "Student" }; // available roles


            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }

            // admin user credentials
            var user = new OnlineClassRegisterUser
            {
                FirstName = "admin",
                LastName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                UserName = "admin@gmail.com",
                DateOfBirth = DateTime.Now,
                NormalizedUserName = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<OnlineClassRegisterUser>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<OnlineClassRegisterUser>(context);
                var result = userStore.CreateAsync(user);
            }

            AssignRoles(serviceProvider, user.Email, roles);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<OnlineClassRegisterUser> _userManager =
                services.GetService<UserManager<OnlineClassRegisterUser>>();
            OnlineClassRegisterUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}