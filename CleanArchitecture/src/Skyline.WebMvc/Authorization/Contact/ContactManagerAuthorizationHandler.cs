using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Skyline.ApplicationCore.Constants;
using Skyline.WebMvc.ViewModels;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Authorization
{
    public class ContactManagerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, ContactBaseViewModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, ContactBaseViewModel resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            // If not asking for approval/reject, return.
            if (requirement.Name != ContactConstants.ApproveOperationName &&
                requirement.Name != ContactConstants.RejectOperationName)
            {
                return Task.CompletedTask;
            }

            // Managers can approve or reject.
            if (context.User.IsInRole(AppIdentityConstants.Roles.MANAGERS))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
