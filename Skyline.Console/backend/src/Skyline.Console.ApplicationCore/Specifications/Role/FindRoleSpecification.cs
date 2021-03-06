﻿using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class FindRoleSpecification : Specification<Role>
    {
        public FindRoleSpecification(Status status, IsDeleted isDeleted)
        {
            Query.Where(x => x.Status == status && x.IsDeleted == isDeleted);
        }

        public FindRoleSpecification(string code)
        {
            Query.Where(x => x.Code == code);
        }

        public FindRoleSpecification(List<string> codes)
        {
            Query.Where(x => codes.Contains(x.Code));
        }


        public FindRoleSpecification(int page, int limit, string keyword)
        {
            if (keyword.IsNullOrWhiteSpace())
            {
                Query
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code)
                    .ThenByDescending(x => x.ModifyTime);
            }
            else
            {
                Query
                    .Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword))
                    .Paginate((page - 1) * limit, limit)
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.Code)
                    .ThenByDescending(x => x.ModifyTime);
            }
        }
    }
}
