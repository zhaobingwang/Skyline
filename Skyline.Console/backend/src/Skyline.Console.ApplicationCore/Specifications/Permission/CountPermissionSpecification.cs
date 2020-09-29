using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class CountPermissionSpecification : Specification<Permission>
    {
        public CountPermissionSpecification(string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
            {
                Query
                    .Where(x => true);
            }
            else
            {
                Query
                    .Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
        }
    }
}
