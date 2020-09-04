using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class CountSpecfication : Specification<Menu>
    {
        public CountSpecfication()
        {
            Query.Where(x => true);
        }
    }
}
