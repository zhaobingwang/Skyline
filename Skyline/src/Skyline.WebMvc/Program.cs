using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Skyline.Infrastructure.Data;
using System;

namespace Skyline.WebMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILogger<Program>>();
                try
                {
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    // Set password with the Secret Manager tool
                    // or dotnet user-secrets set Identity:DefaultPassword <pw>
                    var pw = config["Identity:DefaultPassword"];

                    // Seed Identity Data
                    SeedData.SeedAsync(service, pw).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred seeding the db");
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
