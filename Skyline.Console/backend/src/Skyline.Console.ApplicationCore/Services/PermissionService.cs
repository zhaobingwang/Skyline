using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Console.ApplicationCore.Specifications;
using Skyline.Console.ApplicationCore.VO;
using Skyline.Console.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Services
{

    public class PermissionService : ISkylineAutoDependence
    {
        private readonly IAsyncRepository<Permission> permissionRepository;
        private readonly IAsyncRepository<Menu> menuRepository;

        public PermissionService(IAsyncRepository<Permission> permissionRepository, IAsyncRepository<Menu> menuRepository)
        {
            this.permissionRepository = permissionRepository;
            this.menuRepository = menuRepository;
        }

        public async Task<LayuiTablePageVO> GetPagesAsync(int page, int limit, string keyword)
        {
            var permSpec = new FindPermissionSpecification(page, limit, keyword);
            var permCountSpec = new CountPermissionSpecification(keyword);
            var menuSpec = new FindMenuSpecification(Enums.IsDeleted.No, Enums.Status.Normal);

            var permissions = await permissionRepository.ListAsync(permSpec);
            var totalCount = await permissionRepository.CountAsync(permCountSpec);

            var menus = await menuRepository.ListAsync(menuSpec);

            var vo = ToPermissionVO(permissions, menus);
            return new LayuiTablePageVO(vo, totalCount, 1);
        }

        private IEnumerable<PermissionListVO> ToPermissionVO(IEnumerable<Permission> permissions, IEnumerable<Menu> menus)
        {
            var vo = new List<PermissionListVO>();
            foreach (var permission in permissions)
            {
                vo.Add(new PermissionListVO
                {
                    Code = permission.Code,
                    ActionCode = permission.ActionCode,
                    Description = permission.Description,
                    IsDeleted = permission.IsDeleted,
                    LastModifyTime = permission.LastModifyTime,
                    LastModifyUserGuid = permission.LastModifyUserGuid,
                    LastModifyUserLoginName = permission.LastModifyUserLoginName,
                    MenuGuid = permission.MenuGuid,
                    MenuName = menus.FirstOrDefault(x => x.Guid == permission.MenuGuid)?.Name,
                    Name = permission.Name,
                    Status = permission.Status,
                    Type = permission.Type
                });
            }
            return vo;
        }
    }
}
