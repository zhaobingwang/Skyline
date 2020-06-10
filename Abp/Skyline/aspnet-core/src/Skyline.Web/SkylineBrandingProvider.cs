using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Components;
using Volo.Abp.DependencyInjection;

namespace Skyline.Web
{
    [Dependency(ReplaceServices = true)]
    public class SkylineBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Skyline";
    }
}
