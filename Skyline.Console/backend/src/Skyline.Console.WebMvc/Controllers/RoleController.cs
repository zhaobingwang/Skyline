using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Services;
using Skyline.Console.ApplicationCore.VO;
using Skyline.Utils;

namespace Skyline.Console.WebMvc.Controllers
{
    public class RoleController : BaseController
    {
        private readonly RoleService _roleService;
        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }
        [ActionCode(ActionCodeConst.VIEW)]
        public IActionResult Index()
        {
            return View();
        }

        [ActionCode(ActionCodeConst.VIEW)]
        [HttpPost]
        public async Task<IActionResult> Table(int page, int limit, string keyword)
        {
            var rolePage = await _roleService.GetRolesAsync(page, limit, keyword);
            return Json(rolePage);
        }

        [ActionCode(ActionCodeConst.CREATE)]
        [HttpGet]
        public IActionResult Add()
        {
            var status = EnumUtil.GetDictionary<Status>();
            ViewData["Status"] = status;
            return View();
        }

        [ActionCode(ActionCodeConst.CREATE)]
        [HttpPost]
        public async Task<IActionResult> Add(AddOrEditRoleVO vo)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _roleService.AddAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }

        [ActionCode(ActionCodeConst.EDIT)]
        [HttpGet]
        public async Task<IActionResult> Edit(string code)
        {
            var status = EnumUtil.GetDictionary<Status>();
            ViewData["Status"] = status;
            var role = await _roleService.GetAddOrEditRoleVOAsync(code);
            return View(role);
        }

        [ActionCode(ActionCodeConst.EDIT)]
        [HttpPost]
        public async Task<IActionResult> Edit(AddOrEditRoleVO vo)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await _roleService.EditAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }
    }
}
