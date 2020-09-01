using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindMenuSpecification : Specification<Menu>
    {
        public FindMenuSpecification(IEnumerable<Guid> ids, IsDeleted isDeleted, Status status)
        {
            Query.Where(m => ids.Contains(m.Guid) && m.IsDeleted == isDeleted && m.Status == status);
        }
    }
}
