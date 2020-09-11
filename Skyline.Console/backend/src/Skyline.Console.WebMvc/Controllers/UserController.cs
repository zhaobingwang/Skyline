using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.Services;

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
            return View();
        }
    }
}
