using Microsoft.AspNetCore.Identity;
using RedDog.EF.Models;
using System.Linq;

namespace RedDog.EF.DataSeed
{

    public static class DBDataInitializer
    {
        private static UserManager<AppUser> _userManager { get; set; }
        private static RoleManager<IdentityRole> _roleManager { get; set; }
        public static void SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

            SeedRoles();
            SeedUsers();
        }

        private static void SeedRoles()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }

            if (!_roleManager.RoleExistsAsync("User").GetAwaiter().GetResult())
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = _roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
        }

        private static void SeedUsers()
        {
            if (!_userManager.Users.Any())
            {
                var administrator = new AppUser
                {
                    DisplayName = "Administrator",
                    UserName = "Admin",
                    Email = "admin@test.com"
                };

                var user = new AppUser
                {
                    DisplayName = "SimpleUser",
                    UserName = "User",
                    Email = "user@test.com"
                };
                CreateUser(administrator, "Administrator");
                CreateUser(user, "User");
            }
        }

        private static void CreateUser(AppUser user, string role)
        {
            _userManager.CreateAsync(user, "P@ssw0rd").GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(user, role).GetAwaiter().GetResult();
        }
    }
}




