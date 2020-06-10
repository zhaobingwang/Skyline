using Volo.Abp.Settings;

namespace Skyline.Settings
{
    public class SkylineSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SkylineSettings.MySetting1));
        }
    }
}
