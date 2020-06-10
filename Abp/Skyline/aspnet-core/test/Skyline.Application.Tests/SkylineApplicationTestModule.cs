using Volo.Abp.Modularity;

namespace Skyline
{
    [DependsOn(
        typeof(SkylineApplicationModule),
        typeof(SkylineDomainTestModule)
        )]
    public class SkylineApplicationTestModule : AbpModule
    {

    }
}