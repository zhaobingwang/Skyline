using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Console.Infrastructure.Data;

namespace Skyline.Console.WebMvc
{
    public class Startup
    {
        private const string COOKIE_NAME = "skyline.console.token";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                //options.Filters.Add<GlobalAuthorizeAttribute>();
            }).AddRazorRuntimeCompilation();


            //cookies身份认证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = COOKIE_NAME;
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(7);
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.SlidingExpiration = true;
                    options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(Directory.GetCurrentDirectory()));
                });

            services.AddDbContext<SkylineDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection"))
            );
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EFRepository<>));
            //services.AddScoped<GlobalAuthorizeAttribute>();
            services.AddSkylineService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseStatusCodePagesWithRedirects("/Error/CodePage/{0}");
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Index");
                app.UseStatusCodePagesWithRedirects("/Error/CodePage/{0}");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
