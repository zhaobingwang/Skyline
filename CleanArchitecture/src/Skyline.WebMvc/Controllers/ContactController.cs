using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skyline.ApplicationCore.Constants;
using Skyline.Infrastructure.Identity;
using Skyline.WebMvc.Commands;

namespace Skyline.WebMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = User.IsInRole(AppIdentityConstants.Roles.ADMINISTRATORS)
                || User.IsInRole(AppIdentityConstants.Roles.MANAGERS);
            var vm = await _mediator.Send(new ContactList(currentUserId, isAuthorized));
            return View(vm);
        }
    }
}