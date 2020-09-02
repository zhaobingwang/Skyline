using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.ApplicationCore.Interfaces;
using Skyline.Console.ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Services
{
    public class MenuService : ISkylineAutoDependence
    {
        private readonly IAsyncRepository<User> _userRepository;
        private readonly IAsyncRepository<UserRoleMapping> _userRoleRepository;
        private readonly IAsyncRepository<RolePermissionMapping> _rolePermissionRepository;
        private readonly IAsyncRepository<Permission> _permissionRepository;
        private readonly IAsyncRepository<Menu> _menuRepository;
        public MenuService(IAsyncRepository<User> userRepository, IAsyncRepository<UserRoleMapping> roleRepository, IAsyncRepository<Menu> menuRepository,
            IAsyncRepository<RolePermissionMapping> rolePermissionRepository, IAsyncRepository<Permission> permissionRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = roleRepository;
            _menuRepository = menuRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _permissionRepository = permissionRepository;
        }
        public async Task<List<Menu>> GetMenus(Guid userId)
        {
            // 获取当前用户信息
            var userSpec = new FindUserSpecification(userId);
            var userEntity = await _userRepository.FirstOrDefaultAsync(userSpec);
            if (userEntity == null)
                return new List<Menu>();

            // 获取当前用户对应的角色编码
            var userRoleSpec = new FindUserRoleSpecification(userId);
            var userRoleEntities = await _userRoleRepository.ListAsync(userRoleSpec);
            var roleCodes = userRoleEntities.Select(x => x.RoleCode).ToList();

            // 获取角色对应的权限编码
            var rolePermissionSpec = new FindRolePermissionSpecification(roleCodes);
            var rolePermissionEntities = await _rolePermissionRepository.ListAsync(rolePermissionSpec);
            var permissionCodes = rolePermissionEntities.Select(x => x.PermissionCode).ToList();

            // 获取权限对应的菜单编码
            var permissonSpec = new FindPermissionSpecification(permissionCodes);
            var permissionEntities = await _permissionRepository.ListAsync(permissonSpec);
            var menuIds = permissionEntities.Select(x => x.MenuGuid);
            // 获取菜单
            var menuSpec = new FindMenuSpecification(menuIds, IsDeleted.No, Status.Normal);
            var menuEntities = await _menuRepository.ListAsync(menuSpec);
            return menuEntities.OrderBy(x => x.Sort).ToList();
            //return ToMenuBO(menuEntities);
        }

        private IEnumerable<MenuBO> ToMenuBO(IEnumerable<Menu> menus)
        {
            List<MenuBO> bos = new List<MenuBO>();
            foreach (var menu in menus)
            {
                bos.Add(new MenuBO
                {
                    Id = menu.Guid,
                    Name = menu.Name,
                    Url = menu.Url,
                    Icon = menu.Icon,
                });
            }
            return bos;
        }

    }
}
