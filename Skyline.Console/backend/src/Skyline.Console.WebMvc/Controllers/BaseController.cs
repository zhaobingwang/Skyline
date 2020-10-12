using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Skyline.Console.WebMvc.Controllers
{
    [GlobalAuthorize]
    public class BaseController : Controller
    {
    }
}
