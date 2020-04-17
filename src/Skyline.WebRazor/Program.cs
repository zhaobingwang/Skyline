using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skyline.Infrastructure;

namespace Skyline.WebRazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate();
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool.
                    // dotnet user-secrets set SeedUserPW <pw>
                    var testUserPw = config["SeedUserPw"];
                    SeedApplicationDbContextData.Initialize(service, testUserPw).Wait();
                }
                catch (Exception ex)
                {
                    var logger = service.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }

            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
