using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Skyline.EntityFrameworkCore
{
    [DependsOn(
        typeof(SkylineEntityFrameworkCoreModule)
        )]
    public class SkylineEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SkylineMigrationsDbContext>();
        }
    }
}
