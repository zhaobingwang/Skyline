using Skyline.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Skyline.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class SkylineController : AbpController
    {
        protected SkylineController()
        {
            LocalizationResource = typeof(SkylineResource);
        }
    }
}