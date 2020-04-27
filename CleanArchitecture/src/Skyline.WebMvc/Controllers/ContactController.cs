using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Skyline.ApplicationCore.Constants;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.Infrastructure.Identity;
using Skyline.WebMvc.Authorization;
using Skyline.WebMvc.Commands;
using Skyline.WebMvc.Queries;
using Skyline.WebMvc.ViewModels;

namespace Skyline.WebMvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<AppUser> _userManager;
        IAuthorizationService AuthorizationService;
        public ContactController(IMediator mediator, UserManager<AppUser> userManager, IAuthorizationService authorizationService)
        {
            _mediator = mediator;
            _userManager = userManager;
            AuthorizationService = authorizationService;
        }
        public async Task<IActionResult> Index()
        {
            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = User.IsInRole(AppIdentityConstants.Roles.ADMINISTRATORS)
                || User.IsInRole(AppIdentityConstants.Roles.MANAGERS);
            var vm = await _mediator.Send(new ContactListQuery(currentUserId, isAuthorized));
            return View(vm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ContactCreateViewModel
            {
                Name = "测试",
                Province = "浙江省",
                City = "杭州市",
                Address = "江陵路",
                Email = "test@contoso.com",
                MobileNumber = "1",
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, vm, ContactOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            var success = await _mediator.Send(new ContactCreate(currentUserId, vm));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var contact = await _mediator.Send(new ContactEditQuery(id));
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, vm, ContactOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            await _mediator.Send(new ContactEdit(vm));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var vm = await _mediator.Send(new ContactDetailsQuery(id));
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditStatus(EditStatusViewModel vm)
        {
            await _mediator.Send(new EditStatusCommand(vm));
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, new ContactDetailsViewModel(), ContactOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            await _mediator.Send(new ContactDeleteCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}