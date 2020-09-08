using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Constants;
using Skyline.Console.ApplicationCore.Services;
using Skyline.Console.WebMvc.Models;

namespace Skyline.Console.WebMvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserService _administratorService;
        public AccountController(UserService administratorService)
        {
            _administratorService = administratorService;
        }

        [AllowAnonymous, HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            if (returnUrl.IsNullOrWhiteSpace())
                returnUrl = "/";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Login(LoginVO vo)
        {
            var checkResult = await _administratorService.LoginCheckAsync(vo.UserName, vo.Password);
            if (checkResult.Success)
            {
                var userBo = checkResult.Data as UserBO;
                await SignIn(userBo);
            }
            return Json(checkResult);
        }

        private async Task SignIn(UserBO user)
        {
            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.LoginName??""),
                new Claim(SkylineClaimTypes.UserType,user.UserType.ToString()),
                new Claim(SkylineClaimTypes.UserType,user.NickName),
                new Claim(ClaimTypes.UserData,JsonUtil.ToJson(user)),
            }, "Basic"));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddDays(7),
                IsPersistent = true,
                AllowRefresh = true
            });
        }
    }
}
