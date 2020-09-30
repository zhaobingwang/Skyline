using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skyline.Console.ApplicationCore.Constants;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Services;
using Skyline.Console.Infrastructure.Data;
using Skyline.Console.WebMvc.Models;

namespace Skyline.Console.WebMvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MenuService _menuService;

        public HomeController(ILogger<HomeController> logger, MenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
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
                var userMenus = await _menuService.GetUserMenusAsync(new Guid(currentUserId));
                vo = RecursionMenu(userMenus, Guid.Empty);
            }

            ViewBag.NickName = User.FindFirstValue(ClaimTypes.Name);
            return View(vo);
        }

        public ActionResult Default()
        {
            return View();
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

        private List<MenuVO> MenuEntity2VO(List<Menu> entities)
        {
            List<MenuVO> vos = new List<MenuVO>();
            foreach (var entity in entities)
            {
                vos.Add(new MenuVO
                {
                    Id = entity.Guid,
                    Name = entity.Name,
                    Url = entity.Url,
                    Icon = entity.Icon
                });
            }
            return vos;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
