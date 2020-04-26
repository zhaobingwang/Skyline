using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Skyline.ApplicationCore.Constants;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Authorization
{
    public class ContactIsOwnerAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Contact>
    {
        UserManager<AppUser> _userManager;
        public ContactIsOwnerAuthorizationHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Contact resource)
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
