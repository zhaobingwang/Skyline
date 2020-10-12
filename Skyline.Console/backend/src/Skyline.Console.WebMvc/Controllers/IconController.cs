﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Skyline.Console.WebMvc.Controllers
{
    public class IconController : BaseController
    {
        [ActionCode(ActionCodeConst.VIEW)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
