using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Skyline.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class SkylineMigrationsDbContextFactory : IDesignTimeDbContextFactory<SkylineMigrationsDbContext>
    {
        public SkylineMigrationsDbContext CreateDbContext(string[] args)
        {
            SkylineEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<SkylineMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new SkylineMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
