using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Skyline.Domain.ContactAggregate;
using Skyline.Domain.Identity;
using Skyline.Infrastructure;
using Skyline.Infrastructure.Repositories;

namespace Skyline.WebRazor.Pages.Contacts
{
    public class IndexModel : DI_BasePageModel
    {
        IContactRepository _contactRepository;
        public IndexModel(IContactRepository contactRepository, IAuthorizationService authorizationService, UserManager<AppUser> userManager) : base(authorizationService, userManager)
        {
            _contactRepository = contactRepository;
        }
        public List<Contact> Contacts { get; set; }
        public async Task OnGet()
        {
            var currentUserId = UserManager.GetUserId(User);

            var isAuthorized = User.IsInRole(Constants.ContactAdministratorsRole)
                || User.IsInRole(Constants.ContactManagersRole);

            // 只能查看审核通过的通讯录，除非被授权或是该通讯录的创建者
            if (!isAuthorized)
            {
                Contacts = await _contactRepository.GetContacts(currentUserId);
            }
            else
            {
                Contacts = await _contactRepository.GetAllContacts();
            }
        }
    }
}