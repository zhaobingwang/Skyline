using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Skyline.Console.WebMvc.Controllers
{
    public class MenuController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
