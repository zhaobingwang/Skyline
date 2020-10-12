using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.Services;

namespace Skyline.Console.WebMvc.Controllers
{
    public class PermissionController : BaseController
    {
        private readonly PermissionService permissionService;

        public PermissionController(PermissionService permissionService)
        {
            this.permissionService = permissionService;
        }

        [ActionCode(ActionCodeConst.VIEW)]
        public IActionResult Index()
        {
            return View();
        }

        [ActionCode(ActionCodeConst.VIEW)]
        public async Task<IActionResult> Table(int page, int limit, string keyword)
        {
            var result = await permissionService.GetPagesAsync(page, limit, keyword);
            return Json(result);
        }
    }
}
