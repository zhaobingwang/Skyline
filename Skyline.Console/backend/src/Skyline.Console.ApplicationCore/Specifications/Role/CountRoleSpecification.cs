using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class CountRoleSpecification : Specification<Role>
    {
        public CountRoleSpecification(string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
            {
                Query.Where(x => true);
            }
            else
            {
                Query
                    .Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
            }
        }
    }
}
