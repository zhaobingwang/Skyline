using Microsoft.AspNetCore.Authorization.Infrastructure;
using Skyline.ApplicationCore.Constants;

namespace Skyline.WebMvc.Authorization
{
    public class ContactOperations
    {
        public static OperationAuthorizationRequirement Create = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.CreateOperationName
        };

        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.ReadOperationName
        };

        public static OperationAuthorizationRequirement Update = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.UpdateOperationName
        };

        public static OperationAuthorizationRequirement Delete = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.DeleteOperationName
        };

        public static OperationAuthorizationRequirement Approve = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.ApproveOperationName
        };

        public static OperationAuthorizationRequirement Reject = new OperationAuthorizationRequirement
        {
            Name = ContactConstants.RejectOperationName
        };
    }
}
