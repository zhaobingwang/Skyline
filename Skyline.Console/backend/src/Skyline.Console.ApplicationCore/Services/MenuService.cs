using Skyline.Console.ApplicationCore.BO;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;
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
        public async Task<List<Menu>> GetUserMenusAsync(Guid userId, bool includeHideMenu = false)
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
            if (includeHideMenu)
                return menuEntities.OrderBy(x => x.Sort).ToList();
            else
                return menuEntities.Where(x => x.HideMenu == YesOrNo.No).OrderBy(x => x.Sort).ToList();
            //return ToMenuBO(menuEntities);
        }

        public async Task<List<Menu>> GetSuperAdminMenus()
        {
            // 获取菜单
            var menuEntities = await _menuRepository.ListAllAsync();
            return menuEntities.Where(x => x.Status == Status.Normal && x.IsDeleted == IsDeleted.No && x.HideMenu == YesOrNo.No)
                .OrderBy(x => x.Sort)
                .ThenBy(x => x.CreateTime)
                .ToList();
        }

        public async Task<IEnumerable<MenuTreeVO>> GetMenuTreeAsync(string currentUserId, string userType)
        {
            var spec = new FindMenuSpecification(IsDeleted.No, Status.Normal);
            var menuEntities = await _menuRepository.ListAsync(spec);
            IEnumerable<MenuTreeVO> vo = new List<MenuTreeVO>();

            if (userType == UserType.SuperAdmin.ToString())
            {
                var allMenus = await GetSuperAdminMenus();
                vo = RecursionMenu(allMenus, Guid.Empty);
            }
            else
            {
                var userMenus = await GetUserMenusAsync(new Guid(currentUserId));
                var rootMenus = userMenus.FindAll(x => x.ParentGuid == Guid.Empty);
                vo = RecursionMenu(rootMenus, Guid.Empty);
            }
            return vo;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var spec = new FindMenuSpecification(id, true);
            var menus = await _menuRepository.ListAsync(spec);
            var result = await _menuRepository.DeleteAsync(menus);
            return result == menus.Count;
        }

        private static IEnumerable<MenuTreeVO> RecursionMenu(List<Menu> list, Guid? parentId)
        {
            return list.Where(x => x.ParentGuid == parentId).Select(m => new MenuTreeVO
            {
                Id = m.Guid,
                Title = m.Name,
                Children = RecursionMenu(list, m.Guid)
            });
        }

        public async Task<LayuiTablePageVO> GetAllMenus(int page, int limit, string keyword)
        {
            // 获取菜单
            var pageSpec = new FindMenuSpecification(page, limit, keyword);
            var menuEntities = await _menuRepository.ListAsync(pageSpec);
            var totalCount = await _menuRepository.CountAsync(new CountMenuSpecfication(keyword));
            var menuBo = ToMenuTableBO(menuEntities);
            return new LayuiTablePageVO(menuBo, totalCount, 1);
        }

        public async Task<bool> AddMenu(MenuEditVO vo, Guid createdUserId, string createdUserName)
        {
            var now = DateTime.UtcNow;
            var entity = new Menu
            {
                Guid = Guid.NewGuid(),
                Name = vo.Name,
                Url = vo.Url,
                Icon = vo.Icon,
                ParentGuid = vo.ParentId,
                ParentName = vo.ParentName,
                Sort = vo.Sort,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                CreateTime = now,
                CreateUserGuid = createdUserId,
                CreateUserLoginName = createdUserName,
                LastModifyTime = now,
                LastModifyUserGuid = createdUserId,
                LastModifyUserLoginName = createdUserName,
                HideMenu = YesOrNo.No,
            };
            var result = await _menuRepository.AddAsync(entity);
            return result != null;
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

        // TODO: AUTOMAPPER
        private IEnumerable<MenuTableBO> ToMenuTableBO(IEnumerable<Menu> menus)
        {
            List<MenuTableBO> bos = new List<MenuTableBO>();
            foreach (var menu in menus)
            {
                bos.Add(new MenuTableBO
                {
                    Id = menu.Guid,
                    Name = menu.Name,
                    Url = menu.Url,
                    Icon = menu.Icon,
                    ParentId = menu.ParentGuid,
                    ParentName = menu.ParentName,
                    Status = menu.Status,
                    IsDeleted = menu.IsDeleted,
                    CreateTime = menu.CreateTime,
                    CreateUserLoginName = menu.CreateUserLoginName,
                    LastModifyTime = menu.LastModifyTime,
                    LastModifyUserLoginName = menu.LastModifyUserLoginName
                });
            }
            return bos;
        }

    }
}
