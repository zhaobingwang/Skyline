using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class CountMenuSpecfication : Specification<Menu>
    {
        public CountMenuSpecfication(string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
                Query.Where(x => true);
            else
                Query.Where(x => x.Name.Contains(keyword) || x.ParentName.Contains(keyword));
        }
    }
}
