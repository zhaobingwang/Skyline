using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Skyline.Tools.SeedData.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Skyline.Tools.SeedData
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using ServiceProvider serviceProvider = RegisterServices(args);

            IConfiguration configuration = serviceProvider.GetService<IConfiguration>();
            ILogger logger = serviceProvider.GetService<ILogger<Program>>();

            logger.LogInformation($"{configuration["AppName"]} Start...");

            // Seed data
            logger.LogInformation(Path.Combine(Directory.GetCurrentDirectory()));
            logger.LogInformation(configuration.GetConnectionString("sqlite"));
            var worker = serviceProvider.GetRequiredService<DataWorker>();
            await worker.WorkWithDapper();
        }

        private static ServiceProvider RegisterServices(string[] args)
        {
            IConfiguration configuration = SetupConfiguration(args);
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(cfg => cfg.AddConsole()
                .AddFilter("Default", LogLevel.Information)
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("Microsoft.Hosting.Lifetime", LogLevel.Information));
            serviceCollection.AddSingleton(configuration);

            serviceCollection.AddDbContext<EFContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("sqlite"), providerOptions =>
                providerOptions.CommandTimeout(60))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            serviceCollection.AddScoped<DataWorker>();

            return serviceCollection.BuildServiceProvider();
        }

        private static IConfiguration SetupConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
