using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionCodeAttribute : Attribute
    {
        public string ActionCode { get; set; }
        public ActionCodeAttribute(string actionCode)
        {
            ActionCode = actionCode;
        }
    }
}
