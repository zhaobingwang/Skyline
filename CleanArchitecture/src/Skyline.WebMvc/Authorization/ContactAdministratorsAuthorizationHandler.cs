using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Skyline.ApplicationCore.Constants;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Authorization
{
    public class ContactAdministratorsAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Contact resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            // Administrators can no anything.
            if (context.User.IsInRole(AppIdentityConstants.Roles.ADMINISTRATORS))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
