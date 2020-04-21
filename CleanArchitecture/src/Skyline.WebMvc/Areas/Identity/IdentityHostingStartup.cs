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
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppIdentityDbContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AppIdentityDbContext>();
            });
        }
    }
}