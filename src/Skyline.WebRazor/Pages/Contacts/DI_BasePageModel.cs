using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Skyline.Domain.Identity;
using Skyline.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebRazor.Pages.Contacts
{
    public class DI_BasePageModel : PageModel
    {
        public ApplicationDbContext Context { get; }
        protected IAuthorizationService AuthorizationService { get; }
        protected UserManager<AppUser> UserManager { get; }
        public DI_BasePageModel(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<AppUser> userManager) : base()
        {
            Context = context;
            UserManager = userManager;
            AuthorizationService = authorizationService;
        }
    }
}
