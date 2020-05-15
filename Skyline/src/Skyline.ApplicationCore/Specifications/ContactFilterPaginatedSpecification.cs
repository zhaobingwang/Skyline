using Skyline.ApplicationCore.Entities.ContactAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skyline.ApplicationCore.Specifications
{
    public class ContactFilterPaginatedSpecification : BaseSpecification<Contact>
    {
        public ContactFilterPaginatedSpecification(int skip, int take) : base(c => true)
        {
            ApplyPaging(skip, take);
        }
    }
}
