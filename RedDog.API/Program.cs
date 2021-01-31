using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedDog.EF.DataSeed;
using RedDog.EF.Models;
using System;
using System.Diagnostics;

namespace RedDog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            InitializeDB(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitializeDB(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    DBDataInitializer.SeedData(userManager, roleManager);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("************** DB Initializing error *********************", ex);
                }
            }
        }
    }
}
