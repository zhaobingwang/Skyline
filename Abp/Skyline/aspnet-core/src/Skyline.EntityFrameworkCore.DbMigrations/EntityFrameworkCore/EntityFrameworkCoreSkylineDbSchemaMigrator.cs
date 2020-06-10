using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skyline.Data;
using Volo.Abp.DependencyInjection;

namespace Skyline.EntityFrameworkCore
{
    public class EntityFrameworkCoreSkylineDbSchemaMigrator
        : ISkylineDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreSkylineDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the SkylineMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<SkylineMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}