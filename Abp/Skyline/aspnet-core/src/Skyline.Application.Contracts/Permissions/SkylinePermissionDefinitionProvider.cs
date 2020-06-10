using Skyline.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Skyline.Permissions
{
    public class SkylinePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SkylinePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(SkylinePermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SkylineResource>(name);
        }
    }
}
