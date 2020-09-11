using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class CountUserSpecification : Specification<User>
    {
        public CountUserSpecification(string keyword)
        {
            Query
                .Where(x => x.LoginName.Contains(keyword) || x.NickName.Contains(keyword));
        }
    }
}
