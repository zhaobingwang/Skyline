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
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly RoleService roleService;

        public UserController(UserService userService, RoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Table(int page, int limit, string keyword)
        {
            var userPage = await userService.GetAllUsersAsync(page, limit, keyword);
            return Json(userPage);
        }

        public IActionResult Add()
        {
            var userTypeDict = EnumUtil.GetDictionary<UserType>();
            // 去除超级管理员
            userTypeDict = userTypeDict.Where(x => x.Key != 99).ToDictionary(x => x.Key, x => x.Value);
            ViewData["UserType"] = userTypeDict;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserVO vo)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await userService.AddUserAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var guid = new Guid(id);
            var user = await userService.GetUserVOAsync(guid);

            var userTypeDict = EnumUtil.GetDictionary<UserType>();
            // 去除超级管理员
            if (user.Type != UserType.SuperAdmin)
                userTypeDict = userTypeDict.Where(x => x.Key != 99).ToDictionary(x => x.Key, x => x.Value);
            ViewData["UserType"] = userTypeDict;
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(EditUserVO vo)
        {
            // TODO:放到basecontroller
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUserName = User.FindFirstValue(ClaimTypes.Name);
            var result = await userService.EditAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await userService.DeleteAsync(new Guid(id));
            return Json(result);
        }

        [HttpGet]
        public IActionResult AssignRole(string id)
        {
            ViewData["UID"] = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleTransferAsync(string id)
        {
            Guid uid = new Guid(id);
            var roles = await roleService.GetRolesAsync();
            var urm = await userService.GetAssignedRolesAsync(uid);
            var assignRoleVO = roles.Select(x => new AssignRoleVO { RoleCode = x.Code, RoleName = x.Name, IsAssign = urm.Count(y => y.RoleCode == x.Code) > 0 });
            LayuiTransferVO vo = new LayuiTransferVO();
            var data = new List<LayuiTransferDataVO>();
            foreach (var item in assignRoleVO)
            {
                data.Add(new LayuiTransferDataVO
                {
                    Value = item.RoleCode,
                    Title = item.RoleName,
                    Selected = item.IsAssign
                });
            }
            vo.Data = data;
            vo.Selected = data.Where(x => x.Checked).Select(x => x.Value).ToArray();
            return Json(vo);
        }

        [HttpPost]
        public async Task<IActionResult> IncreaseRole(string uid, IEnumerable<LayuiTransferDataVO> data)
        {
            var result = await userService.AddUserRoleAsync(new Guid(uid), data);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseRole(string uid, IEnumerable<LayuiTransferDataVO> data)
        {
            var result = await userService.DeleteUserRoleAsync(new Guid(uid), data);
            return Json(result);
        }
    }
}
