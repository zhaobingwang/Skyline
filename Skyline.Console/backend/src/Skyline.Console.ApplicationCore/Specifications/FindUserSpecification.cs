using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindUserSpecification : Specification<User>
    {
        public FindUserSpecification(Guid userId)
        {
            Query.Where(u => u.Guid == userId);
        }
    }
}
