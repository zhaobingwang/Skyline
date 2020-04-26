using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Skyline.ApplicationCore.Constants;
using Skyline.Infrastructure.Identity;
using Skyline.WebMvc.ViewModels;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Authorization
{
    public class ContactIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ContactBaseViewModel>
    {
        UserManager<AppUser> _userManager;
        public ContactIsOwnerAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ContactBaseViewModel resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for CRUD permission, return.
            if (requirement.Name != ContactConstants.CreateOperationName &&
                requirement.Name != ContactConstants.DeleteOperationName &&
                requirement.Name != ContactConstants.UpdateOperationName &&
                requirement.Name != ContactConstants.ReadOperationName)
            {
                return Task.CompletedTask;
            }

            if (resource.OwnerId == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
