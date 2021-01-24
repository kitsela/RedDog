using Microsoft.AspNetCore.Identity;
using RedDog.EF.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedDog.EF.DataSeed
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                            {
                                new AppUser
                                    {
                                        DisplayName = "TestUserFirst",
                                        UserName = "TestUserFirst",
                                        Email = "testuserfirst@test.com"
                                    },

                                new AppUser
                                    {
                                        DisplayName = "TestUserSecond",
                                        UserName = "TestUserSecond",
                                        Email = "testusersecond@test.com"
                                    }
                              };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "P@ssw0rd");
                }
            }
        }
    }
}
