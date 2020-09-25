using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindUserRoleMappingSpecfication : Specification<UserRoleMapping>
    {
        public FindUserRoleMappingSpecfication(Guid uid)
        {
            Query
                .Where(x => x.UserGuid == uid);
        }
    }
}
