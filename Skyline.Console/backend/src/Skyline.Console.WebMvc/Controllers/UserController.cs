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
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Table(int page, int limit, string keyword)
        {
            var userPage = await _userService.GetAllUsersAsync(page, limit, keyword);
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
            var result = await _userService.AddUserAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var guid = new Guid(id);
            var user = await _userService.GetUserVOAsync(guid);

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
            var result = await _userService.EditAsync(vo, new Guid(currentUserId), currentUserName);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var result = await _userService.DeleteAsync(new Guid(id));
            return Json(result);
        }
    }
}
