using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Constants;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Services;
using Skyline.Console.ApplicationCore.VO;
using Skyline.Console.WebMvc.Models;

namespace Skyline.Console.WebMvc.Controllers
{
    public class MenuController : BaseController
    {
        private readonly MenuService _menuService;
        public MenuController(MenuService menuService)
        {
            _menuService = menuService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Table(int page, int limit)
        {
            var menuPage = await _menuService.GetAllMenus(page, limit);
            return Json(menuPage);
        }

        [HttpGet]
        public async Task<ActionResult> Add()
        {
            ViewData["Menus"] = await GetMenusAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MenuEditVO vo)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _menuService.AddMenu(vo, new Guid(currentUserId), currentUserName);
            return Json(new BizServiceResponse(BizServiceResponseCode.Success, "新建成功"));
        }

        private async Task<IEnumerable<MenuVO>> GetMenusAsync()
        {
            IEnumerable<MenuVO> vo = new List<MenuVO>();
            var userType = User.FindFirstValue(SkylineClaimTypes.UserType);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userType == UserType.SuperAdmin.ToString())
            {
                var allMenus = await _menuService.GetSuperAdminMenus();
                vo = RecursionMenu(allMenus, Guid.Empty);
            }
            else
            {
                var userMenus = await _menuService.GetUserMenus(new Guid(currentUserId));
                var rootMenus = userMenus.FindAll(x => x.ParentGuid == Guid.Empty);
                vo = RecursionMenu(rootMenus, Guid.Empty);
            }

            ViewBag.NickName = User.FindFirstValue(ClaimTypes.Name);
            return vo;
        }

        private static IEnumerable<MenuVO> RecursionMenu(IEnumerable<Menu> list, Guid? parentId)
        {
            return list.Where(x => x.ParentGuid == parentId).Select(m => new MenuVO
            {
                Id = m.Guid,
                Name = m.Name,
                Url = m.Url,
                Icon = m.Icon,
                Children = RecursionMenu(list, m.Guid)
            });
        }
    }
}
