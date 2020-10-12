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

namespace Skyline.Console.WebMvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class GlobalAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        //private readonly MenuService menuService;
        //private readonly PermissionService permissionService;
        //private readonly UserService userService;

        //public GlobalAuthorizeAttribute(MenuService menuService, PermissionService permissionService, UserService userService)
        //{
        //    this.menuService = menuService;
        //    this.permissionService = permissionService;
        //    this.userService = userService;
        //}

        // TODO: 优化
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            context.CheckNull(nameof(context));

            // 允许匿名访问
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.Count(x => x.GetType() == typeof(AllowAnonymousAttribute)) > 0;
            if (allowAnonymous)
            {
                return;
            }

            // 获取依赖的服务对象
            var menuService = (MenuService)context.HttpContext.RequestServices.GetService(typeof(MenuService));
            var permissionService = (PermissionService)context.HttpContext.RequestServices.GetService(typeof(PermissionService));
            var userService = (UserService)context.HttpContext.RequestServices.GetService(typeof(UserService));


            // 获取权限码
            var actionCodeAttribute = (ActionCodeAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x.GetType() == typeof(ActionCodeAttribute));
            var reqeustActionCode = actionCodeAttribute?.ActionCode;

            // 获取当前请求的地址
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            string controllerName = controllerActionDescriptor?.ControllerName;
            string actionName = controllerActionDescriptor?.ActionName;
            string url = $"/{controllerName}/{actionName}";

            // 获取用户数据
            var userData = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;
            var userBO = JsonUtil.ToObject<UserBO>(userData);
            if (userBO.IsNull())
            {
                return;
            }

            // 判断用户是否有此菜单
            var userMenus = menuService.GetUserMenusAsync(userBO.Id, true).Result;
            var currentMenu = userMenus.Where(x => x.Url != null && x.Url.StartsWith($"/{controllerName}/"));
            if (currentMenu == null || currentMenu.Count() < 1)
            {
                context.Result = new UnauthorizedResult();
            }

            // 判断用户是否有当前操作权限
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
