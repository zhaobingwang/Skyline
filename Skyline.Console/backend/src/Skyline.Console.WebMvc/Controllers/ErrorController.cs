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
                return View("/views/error/404.html");
            }
            else if (Response.StatusCode == 500)
            {
                return View("/views/error/500.html");
            }
            return View();
        }

        public IActionResult NotFoundPage()
        {
            return View();
        }
    }
}
