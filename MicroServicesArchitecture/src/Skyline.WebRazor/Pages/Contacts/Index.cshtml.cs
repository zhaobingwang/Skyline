using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skyline.Domain.ContactAggregate;
using Skyline.Domain.Identity;
using Skyline.Infrastructure;
using Skyline.Infrastructure.Repositories;
using Skyline.WebRazor.Application.Queries;

namespace Skyline.WebRazor.Pages.Contacts
{
    public class IndexModel : DI_BasePageModel
    {
        IMediator _mediator;
        public IndexModel(IMediator mediator, IAuthorizationService authorizationService, UserManager<AppUser> userManager) : base(authorizationService, userManager)
        {
            _mediator = mediator;
        }
        public List<Contact> Contacts { get; set; }
        public async Task OnGet()
        {
            var currentUserId = UserManager.GetUserId(User);

            var isAuthorized = User.IsInRole(Constants.ContactAdministratorsRole)
                || User.IsInRole(Constants.ContactManagersRole);

            var query = new ContactListQuery(currentUserId, isAuthorized);
            Contacts = await _mediator.Send(query);
        }
    }
}