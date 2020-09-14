using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindUserSpecification : Specification<User>
    {
        public FindUserSpecification(Guid userId)
        {
            Query.Where(u => u.Guid == userId);
        }

        public FindUserSpecification(string loginName)
        {
            Query.Where(u => u.LoginName == loginName);
        }

        public FindUserSpecification(int page, int limit, string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
            {
                Query
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.LoginName)
                    .ThenBy(x => x.NickName)
                    .ThenBy(x => x.LastModifyTime);
            }
            else
            {
                Query
                    .Where(x => x.LoginName.Contains(keyword) || x.NickName.Contains(keyword))
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.LoginName)
                    .ThenBy(x => x.NickName)
                    .ThenBy(x => x.LastModifyTime);
            }
        }
    }
}
