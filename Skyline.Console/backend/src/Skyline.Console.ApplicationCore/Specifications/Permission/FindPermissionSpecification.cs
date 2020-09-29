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
        public FindPermissionSpecification(int page, int limit, string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
            {
                Query
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.Menu.ParentName)
                    .ThenBy(x => x.Menu.Name)
                    .ThenBy(x => x.ActionCode)
                    .ThenByDescending(x => x.LastModifyTime)
                    .ThenByDescending(x => x.CreateTime);
            }
            else
            {
                Query
                    .Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword))
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.Menu.ParentName)
                    .ThenBy(x => x.Menu.Name)
                    .ThenBy(x => x.ActionCode)
                    .ThenByDescending(x => x.LastModifyTime)
                    .ThenByDescending(x => x.CreateTime);
            }
        }
    }
}
