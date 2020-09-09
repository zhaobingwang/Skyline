using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Skyline.Console.WebMvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            if (Response.StatusCode == 404)
            {
                return View("404");
            }
            else if (Response.StatusCode == 500)
            {
                return View("500");
            }
            return View();
        }

        [Route("/Error/CodePage/{httpCode}")]
        public IActionResult CodePage(int httpCode)
        {
            ViewData["HttpCodeSVG"] = httpCode + ".svg";
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
