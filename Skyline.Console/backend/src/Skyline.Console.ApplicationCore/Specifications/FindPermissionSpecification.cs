using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindPermissionSpecification : Specification<Permission>
    {
        public FindPermissionSpecification(List<string> codes)
        {
            Query.Where(x => codes.Contains(x.Code));
        }
    }
}
