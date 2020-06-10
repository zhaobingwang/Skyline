using Skyline.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Skyline.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class SkylinePageModel : AbpPageModel
    {
        protected SkylinePageModel()
        {
            LocalizationResourceType = typeof(SkylineResource);
        }
    }
}