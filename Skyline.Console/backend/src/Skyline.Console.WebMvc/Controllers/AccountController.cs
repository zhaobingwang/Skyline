using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skyline.Console.ApplicationCore.Services;
using Skyline.Console.WebMvc.Models;

namespace Skyline.Console.WebMvc.Controllers
{
    public class AccountController : BaseController
    {
        private readonly AdministratorService _administratorService;
        public AccountController(AdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [AllowAnonymous, HttpGet]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl.IsNullOrWhiteSpace())
                returnUrl = "/";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //[AllowAnonymous, HttpPost]
        //public async Task<IActionResult> Login(LoginVO vo)
        //{
        //    var success = await _administratorService.LoginCheckAsync(vo.UserName, vo.Password);
        //    if (success)
        //    {

        //    }
        //}
    }
}
