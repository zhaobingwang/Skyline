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

namespace Skyline.WebRazor.Pages.Contacts
{
    public class IndexModel : DI_BasePageModel
    {
        public IndexModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<AppUser> userManager) : base(context, authorizationService, userManager)
        {
        }
        public IList<Contact> Contacts { get; set; }
        public async Task OnGet()
        {
            var contact = from c in Context.Contacts
                          select c;
            var currentUserId = UserManager.GetUserId(User);
            Contacts = await contact.ToListAsync();
        }
    }
}