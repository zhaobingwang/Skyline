using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.Infrastructure.Data;
using Skyline.Console.WebMvc.Models;

namespace Skyline.Console.WebMvc.Controllers
{
    public class HomeController : Controller//BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SkylineDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, SkylineDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var vos = new List<MenuVO>();
            var allMenus = _dbContext.Menus.ToList();
            var rootMenus = allMenus.FindAll(x => x.ParentGuid == Guid.Empty);
            foreach (var rootMenu in rootMenus)
            {
                var subMenu = allMenus.FindAll(x => x.ParentGuid == rootMenu.Guid);
                vos.Add(new MenuVO
                {
                    Id = rootMenu.Guid,
                    Name = rootMenu.Name,
                    Url = rootMenu.Url,
                    Icon = rootMenu.Icon,
                    SubMenus = MenuEntity2VO(subMenu)
                });
            }
            return View(vos);
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
                    Icon = entity.Icon,
                    SubMenus = null
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
