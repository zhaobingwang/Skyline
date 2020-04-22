using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Skyline.Infrastructure.Identity;

[assembly: HostingStartup(typeof(Skyline.WebMvc.Areas.Identity.IdentityHostingStartup))]
namespace Skyline.WebMvc.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppIdentityDbContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityDbContext>();

                services.Configure<IdentityOptions>(options =>
                {
                    // Passsword settings.
                    //options.Password.RequireDigit = false;
                    //options.Password.RequireLowercase = false;
                    //options.Password.RequireNonAlphanumeric = false;
                    //options.Password.RequireUppercase = false;
                    //options.Password.RequiredLength = 6;
                    //options.Password.RequiredUniqueChars = 1;

                    // Lokout settings.
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                services.ConfigureApplicationCookie(options =>
                {
                    // Cookie settings.
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.LoginPath = "/Identity/Account/Login";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });
            });
        }
    }
}