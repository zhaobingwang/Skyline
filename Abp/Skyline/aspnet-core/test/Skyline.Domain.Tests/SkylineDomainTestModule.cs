using Skyline.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Skyline
{
    [DependsOn(
        typeof(SkylineEntityFrameworkCoreTestModule)
        )]
    public class SkylineDomainTestModule : AbpModule
    {

    }
}