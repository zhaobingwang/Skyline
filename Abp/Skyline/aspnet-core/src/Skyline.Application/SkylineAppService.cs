using System;
using System.Collections.Generic;
using System.Text;
using Skyline.Localization;
using Volo.Abp.Application.Services;

namespace Skyline
{
    /* Inherit your application services from this class.
     */
    public abstract class SkylineAppService : ApplicationService
    {
        protected SkylineAppService()
        {
            LocalizationResource = typeof(SkylineResource);
        }
    }
}
