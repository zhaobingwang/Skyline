using AngleSharp.Text;
using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindRolePermissionSpecification : Specification<RolePermissionMapping>
    {
        public FindRolePermissionSpecification(List<string> roleCodes)
        {
            Query.Where(x => roleCodes.Contains(x.RoleCode));
        }
    }
}
