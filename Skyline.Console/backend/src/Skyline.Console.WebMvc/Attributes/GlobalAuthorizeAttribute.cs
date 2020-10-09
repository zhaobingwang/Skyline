using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class GlobalAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly MenuService menuService;
        private readonly PermissionService permissionService;
        private readonly UserService userService;

        public GlobalAuthorizeAttribute(MenuService menuService, PermissionService permissionService, UserService userService)
        {
            this.menuService = menuService;
            this.permissionService = permissionService;
            this.userService = userService;
        }

        // TODO: 优化
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.CheckNull(nameof(context));

            if (context.Filters.Count(x => x is AllowAnonymousFilter) > 0)
            {
                return;
            }

            var actionCodeAttribute = (ActionCodeAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType() == typeof(ActionCodeAttribute));
            var reqeustActionCode = actionCodeAttribute?.ActionCode;

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string controllerName = controllerActionDescriptor?.ControllerName;
            string actionName = controllerActionDescriptor?.ActionName;
            string url = $"/{controllerName}/{actionName}";

            var userData = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;
            var userBO = JsonUtil.ToObject<UserBO>(userData);
            if (userBO.IsNull())
            {
                return;
            }

            if (controllerName == "Home" && actionName == "Index")
            {
                return;
            }

            var userMenus = menuService.GetUserMenusAsync(userBO.Id).Result;
            var currentMenu = userMenus.Where(x => x.Url == $"/{controllerName}/Index");
            if (currentMenu == null || currentMenu.Count() < 1)
            {
                context.Result = new UnauthorizedResult();
            }
            var roleCodes = userService.GetRoleCodes(userBO.Id).Result;
            var userPermission = permissionService.GetPermissionByRoleCodesAsync(roleCodes).Result;

            var permissions = userPermission.Where(x => x.MenuGuid == currentMenu.FirstOrDefault()?.Guid);
            var canAccess = permissions.Any(x => string.Equals(x.ActionCode, reqeustActionCode, StringComparison.OrdinalIgnoreCase));
            if (!canAccess)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
