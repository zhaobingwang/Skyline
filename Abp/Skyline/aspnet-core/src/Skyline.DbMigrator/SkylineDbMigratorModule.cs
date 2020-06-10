using Skyline.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Skyline.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SkylineEntityFrameworkCoreDbMigrationsModule),
        typeof(SkylineApplicationContractsModule)
        )]
    public class SkylineDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
