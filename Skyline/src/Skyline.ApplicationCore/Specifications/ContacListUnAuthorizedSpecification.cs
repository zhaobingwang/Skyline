using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skyline.ApplicationCore.Specifications
{
    public class ContacListUnAuthorizedSpecification : BaseSpecification<Contact>
    {
        public ContacListUnAuthorizedSpecification(string ownerId) : base(c => c.Status == ContactStatus.Approved || c.OwnerId == ownerId)
        {
        }
    }
}
