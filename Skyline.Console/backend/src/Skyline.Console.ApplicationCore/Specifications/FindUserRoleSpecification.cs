using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindUserRoleSpecification : Specification<UserRoleMapping>
    {
        public FindUserRoleSpecification(Guid userId)
        {
            Query.Where(m => m.UserGuid == userId);
        }
    }
}
