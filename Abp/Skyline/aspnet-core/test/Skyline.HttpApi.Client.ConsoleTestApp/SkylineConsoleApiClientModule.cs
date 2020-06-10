using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Skyline.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(SkylineHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SkylineConsoleApiClientModule : AbpModule
    {
        
    }
}
