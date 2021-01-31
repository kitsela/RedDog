
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RedDog.EF;
using RedDog.EF.Models;
using Microsoft.AspNetCore.Identity;
using MediatR;
using RedDog.API.Handlers.User.LoginHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedDog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(LoginHandler).Assembly);

            //configureEF
            services.AddDbContext<ApplicationDbContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //register identity
            var identityBuilder = services.AddIdentity<AppUser, IdentityRole>();
            
            //todo check difference
            //var builder = services.AddIdentityCore<AppUser>();
            //var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            //identity use this context to save data
            identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
            identityBuilder.AddSignInManager<SignInManager<AppUser>>();
            //.AddDefaultTokenProviders();


            services.AddMvc(option =>
            {
                // switch from EnableEndpointRouting to IRouter. 
                option.EnableEndpointRouting = false;
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .RequireAuthenticatedUser()
                                    .Build();
                option.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
