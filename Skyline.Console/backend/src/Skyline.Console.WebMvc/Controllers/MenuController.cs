using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.Services;
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
            // TODO: 返回VO，不返回实体
            var menuPage = await _menuService.GetAllMenus(page, limit);
            // TODO: 优化返回数据结构
            return Json(menuPage);
        }
    }
}
