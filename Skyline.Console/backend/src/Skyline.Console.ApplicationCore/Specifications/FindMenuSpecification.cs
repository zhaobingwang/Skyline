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
        public FindMenuSpecification(Guid id)
        {
            Query.Where(x => x.Guid == id);
        }

        public FindMenuSpecification(Guid id, bool includeChildren)
        {
            if (includeChildren)
            {
                Query.Where(x => x.Guid == id || x.ParentGuid == id);
            }
            else
            {
                Query.Where(x => x.Guid == id);
            }
        }

        public FindMenuSpecification(IEnumerable<Guid> ids, IsDeleted isDeleted, Status status)
        {
            Query.Where(m => ids.Contains(m.Guid) && m.IsDeleted == isDeleted && m.Status == status);
        }

        public FindMenuSpecification(IsDeleted isDeleted, Status status)
        {
            Query.Where(m => m.IsDeleted == isDeleted && m.Status == status);
        }

        public FindMenuSpecification(int page, int limit)
        {
            Query
                .Paginate((page - 1) * limit, limit)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Sort);
        }
    }
}
