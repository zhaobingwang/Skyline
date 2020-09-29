using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Skyline.Console.ApplicationCore.Entities;
using Skyline.Console.ApplicationCore.Enums;

namespace Skyline.Console.Infrastructure.Data
{
    public class SeedData
    {
        const string ROLE_CODE_SUPERADMIN = "SYS_SUPER_ADMIN";
        const string ROLE_CODE_ADMIN = "SYS_ADMIN";
        const string ROLE_CODE_GUEST = "SYS_GUEST";

        // 菜单权限
        const string PERM_DASHBOARD_MENU_ROOT = "D.00000";
        const string PERM_DASHBOARD_MENU_WORKBENCH = "D.10000";

        const string PERM_SYS_MANAGEMENT = "SYS.00000";

        const string PERM_SYS_USER = "SYS.10000";
        const string PERM_SYS_USER_VIEW = "SYS.10001";
        const string PERM_SYS_USER_CREATE = "SYS.10002";
        const string PERM_SYS_USER_EDIT = "SYS.10003";
        const string PERM_SYS_USER_DELETE = "SYS.10004";

        const string PERM_SYS_ROLE = "SYS.20000";
        const string PERM_SYS_ROLE_VIEW = "SYS.20001";
        const string PERM_SYS_ROLE_CREATE = "SYS.20002";
        const string PERM_SYS_ROLE_EDIT = "SYS.20003";
        const string PERM_SYS_ROLE_DELETE = "SYS.20004";

        const string PERM_SYS_MENU = "SYS.30000";
        const string PERM_SYS_MENU_VIEW = "SYS.30001";
        const string PERM_SYS_MENU_CREATE = "SYS.30002";
        const string PERM_SYS_MENU_EDIT = "SYS.30003";
        const string PERM_SYS_MENU_DELETE = "SYS.30004";

        const string PERM_SYS_PERMISSION = "SYS.40000";
        const string PERM_SYS_PERMISSION_VIEW = "SYS.40001";
        const string PERM_SYS_PERMISSION_CREATE = "SYS.40002";
        const string PERM_SYS_PERMISSION_EDIT = "SYS.40003";
        const string PERM_SYS_PERMISSION_DELETE = "SYS.40004";

        const string PERM_SYS_ICON = "SYS.50000";
        const string PERM_SYS_ICON_VIEW = "SYS.50001";
        const string PERM_SYS_ICON_CREATE = "SYS.50002";
        const string PERM_SYS_ICON_EDIT = "SYS.50003";
        const string PERM_SYS_ICON_DELETE = "SYS.50004";

        static DateTime now;
        static Guid superAdminId;
        static Guid adminId;
        static Guid guestId;
        static string SuperAdminName = "superadmin";
        static string AdminName = "admin";
        static string guestName = "guest";
        static SeedData()
        {
            now = DateTime.UtcNow;
            superAdminId = Guid.NewGuid();
            adminId = Guid.NewGuid();
            guestId = Guid.NewGuid();
        }

        public static async Task SeedAsync(IServiceProvider provider)
        {
            var dbContext = provider.GetService<SkylineDbContext>();
            await InitMenus(dbContext);
            await InitRoles(dbContext);
            await InitUsers(dbContext);
            await InitUserRoleMappings(dbContext);
            await InitPermissions(dbContext);
            await InitRolePermissionMappings(dbContext);
        }

        private static async Task InitUsers(SkylineDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var users = GetUsers();
                dbContext.Users.AddRange(users);
                await dbContext.SaveChangesAsync();
            }
        }
        private static async Task InitRoles(SkylineDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitUserRoleMappings(SkylineDbContext dbContext)
        {

            if (!dbContext.UserRoleMappings.Any())
            {
                var maps = GetUserRoleMappings();
                dbContext.UserRoleMappings.AddRange(maps);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitMenus(SkylineDbContext dbContext)
        {
            var now = DateTime.UtcNow;
            if (!dbContext.Menus.Any())
            {
                var dashBoardId = Guid.NewGuid();
                var menus = GetMenus();
                dbContext.Menus.AddRange(menus);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitPermissions(SkylineDbContext dbContext)
        {
            if (!dbContext.Permissions.Any())
            {
                var permissions = GetPermissions();
                dbContext.Permissions.AddRange(permissions);
                await dbContext.SaveChangesAsync();
            }
        }

        private static async Task InitRolePermissionMappings(SkylineDbContext dbContext)
        {
            if (!dbContext.RolePermissionMappings.Any())
            {
                var maps = GetRolePermissionMappings();
                dbContext.RolePermissionMappings.AddRange(maps);
                await dbContext.SaveChangesAsync();
            }
        }

        private static List<User> GetUsers()
        {
            var users = new List<User>();
            users.Add(new User
            {
                Guid = superAdminId,
                LoginName = SuperAdminName,
                Avatar = "",
                CreateTime = now,
                CreateUserId = Guid.Empty,
                CreateUserName = "System",
                Description = "超级管理员",
                DOB = new DateTime(2000, 8, 1),
                IsDeleted = IsDeleted.No,
                NickName = "超级管理员",
                Status = Status.Normal,
                Type = UserType.SuperAdmin,
                PasswordHash = "123456".MD5Hash(),
                LastModifyTime = now,
                LastModifyUserId = Guid.Empty,
                LastModifyUserName = "System",
            });
            users.Add(new User
            {
                Guid = adminId,
                LoginName = AdminName,
                Avatar = "",
                CreateTime = now,
                CreateUserId = Guid.Empty,
                CreateUserName = "System",
                Description = "管理员",
                DOB = new DateTime(2000, 8, 1),
                IsDeleted = IsDeleted.No,
                NickName = "管理员",
                Status = Status.Normal,
                Type = UserType.Admin,
                PasswordHash = "123456".MD5Hash(),
                LastModifyTime = now,
                LastModifyUserId = Guid.Empty,
                LastModifyUserName = "System",
            });
            users.Add(new User
            {
                Guid = guestId,
                LoginName = guestName,
                Avatar = "",
                CreateTime = now,
                CreateUserId = Guid.Empty,
                CreateUserName = "System",
                Description = "普通用户",
                DOB = new DateTime(2000, 8, 1),
                IsDeleted = IsDeleted.No,
                NickName = "普通用户",
                Status = Status.Normal,
                Type = UserType.Normal,
                PasswordHash = "123456".MD5Hash(),
                LastModifyTime = now,
                LastModifyUserId = Guid.Empty,
                LastModifyUserName = "System",
            });
            return users;
        }

        private static List<Role> GetRoles()
        {
            var roles = new List<Role>();
            roles.Add(new Role
            {
                Code = ROLE_CODE_SUPERADMIN,
                Name = "超级管理员",
                CreateTime = now,
                CreateUserGuidId = Guid.Empty,
                CreateUserName = "System",
                Builtin = true,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsSuperAdministrator = true,
                Description = "超级管理员",
                ModifiyUserId = Guid.Empty,
                ModifyTime = now,
                ModifyUserName = "System"
            });

            roles.Add(new Role
            {
                Code = ROLE_CODE_ADMIN,
                Name = "管理员",
                CreateTime = now,
                CreateUserGuidId = Guid.Empty,
                CreateUserName = "System",
                Builtin = true,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsSuperAdministrator = true,
                Description = "超级管理员",
                ModifiyUserId = Guid.Empty,
                ModifyTime = now,
                ModifyUserName = "System"
            });

            roles.Add(new Role
            {
                Code = ROLE_CODE_GUEST,
                Name = "普通用户",
                CreateTime = now,
                CreateUserGuidId = Guid.Empty,
                CreateUserName = "System",
                Builtin = true,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsSuperAdministrator = true,
                Description = "普通用户",
                ModifiyUserId = Guid.Empty,
                ModifyTime = now,
                ModifyUserName = "System"
            });
            return roles;
        }

        private static List<UserRoleMapping> GetUserRoleMappings()
        {
            var maps = new List<UserRoleMapping>();
            maps.Add(new UserRoleMapping
            {
                UserGuid = superAdminId,
                RoleCode = ROLE_CODE_SUPERADMIN,
                CreateTime = now
            });

            maps.Add(new UserRoleMapping
            {
                UserGuid = adminId,
                RoleCode = ROLE_CODE_ADMIN,
                CreateTime = now
            });

            maps.Add(new UserRoleMapping
            {
                UserGuid = guestId,
                RoleCode = ROLE_CODE_GUEST,
                CreateTime = now
            });
            return maps;
        }

        static Guid dashBoardId = Guid.NewGuid();
        static string dashBoardName = "DashBoard";
        static Guid dashBoardWorkbenchId = Guid.NewGuid();
        static Guid dashBoardAnalysisId = Guid.NewGuid();
        static Guid dashBoardMonitorId = Guid.NewGuid();

        static Guid sysId = Guid.NewGuid();
        static string sysName = "系统管理";
        static Guid sysUserId = Guid.NewGuid();
        static Guid sysRoleId = Guid.NewGuid();
        static Guid sysMenuId = Guid.NewGuid();
        static Guid sysPermissionId = Guid.NewGuid();
        static Guid sysIconId = Guid.NewGuid();
        private static List<Menu> GetMenus()
        {
            var menus = new List<Menu>();

            #region DashBoard
            menus.Add(new Menu
            {
                Guid = dashBoardId,
                Name = "仪表盘",
                Url = null,
                Alias = "",
                Icon = "layui-icon-console",
                ParentGuid = Guid.Empty,
                ParentName = null,
                Level = 0,
                Description = "仪表盘",
                Sort = 0,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = dashBoardWorkbenchId,
                Name = "工作台",
                Url = "/DashBoard/Index",
                Alias = "",
                Icon = "iconfont icon-gongzuotai",
                ParentGuid = dashBoardId,
                ParentName = dashBoardName,
                Level = 0,
                Description = "工作台页面",
                Sort = 1,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = dashBoardAnalysisId,
                Name = "分析",
                Url = "/DashBoard/Analysis",
                Alias = "",
                Icon = "iconfont icon-fenxi",
                ParentGuid = dashBoardId,
                ParentName = dashBoardName,
                Level = 0,
                Description = "分析页面",
                Sort = 2,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = dashBoardMonitorId,
                Name = "监控",
                Url = "/DashBoard/Monitor",
                Alias = "",
                Icon = "iconfont icon-monitor",
                ParentGuid = dashBoardId,
                ParentName = dashBoardName,
                Level = 0,
                Description = "监控页面",
                Sort = 3,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            #endregion

            #region RBAC
            menus.Add(new Menu
            {
                Guid = sysId,
                Name = "系统管理",
                Url = null,
                Alias = "",
                Icon = "layui-icon-set-fill",
                ParentGuid = Guid.Empty,
                ParentName = null,
                Level = 0,
                Description = "仪表盘",
                Sort = 999,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = sysUserId,
                Name = "用户管理",
                Url = "/User/Index",
                Alias = "",
                Icon = "layui-icon-user",
                ParentGuid = sysId,
                ParentName = sysName,
                Level = 0,
                Description = "用户管理维护",
                Sort = 1,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = sysRoleId,
                Name = "角色管理",
                Url = "/Role/Index",
                Alias = "",
                Icon = "layui-icon-group",
                ParentGuid = sysId,
                ParentName = sysName,
                Level = 0,
                Description = "角色管理维护",
                Sort = 2,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = sysMenuId,
                Name = "菜单管理",
                Url = "/Menu/Index",
                Alias = "",
                Icon = "iconfont icon-menu",
                ParentGuid = sysId,
                ParentName = sysName,
                Level = 0,
                Description = "菜单管理维护",
                Sort = 3,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = sysPermissionId,
                Name = "权限管理",
                Url = "/Permission/Index",
                Alias = "",
                Icon = "iconfont icon-lock",
                ParentGuid = sysId,
                ParentName = sysName,
                Level = 0,
                Description = "权限管理维护",
                Sort = 4,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            menus.Add(new Menu
            {
                Guid = sysIconId,
                Name = "图标管理",
                Url = "/Icon/Index",
                Alias = "",
                Icon = "layui-icon-diamond",
                ParentGuid = sysId,
                ParentName = sysName,
                Level = 0,
                Description = "图标管理维护",
                Sort = 5,
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                IsDefaultRouter = YesOrNo.Yes,
                CreateTime = now,
                CreateUserGuid = superAdminId,
                CreateUserLoginName = SuperAdminName,
                LastModifyTime = now,
                LastModifyUserGuid = superAdminId,
                LastModifyUserLoginName = SuperAdminName,
                HideMenu = YesOrNo.No,
            });
            #endregion

            return menus;
        }

        private static List<Permission> GetPermissions()
        {
            var permissions = new List<Permission>();
            permissions.Add(new Permission
            {
                Code = PERM_DASHBOARD_MENU_ROOT,
                MenuGuid = dashBoardId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
            });

            permissions.Add(new Permission
            {
                Code = PERM_DASHBOARD_MENU_WORKBENCH,
                MenuGuid = dashBoardWorkbenchId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.Yes,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
            });

            permissions.Add(new Permission
            {
                Code = PERM_SYS_MANAGEMENT,
                MenuGuid = sysId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.Yes,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
            });

            #region 用户管理
            permissions.Add(new Permission
            {
                Code = PERM_SYS_USER,
                MenuGuid = sysUserId,
                Name = "用户管理",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看用户的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_USER_VIEW,
                MenuGuid = sysUserId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看用户的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_USER_CREATE,
                MenuGuid = sysUserId,
                Name = "创建",
                ActionCode = "create",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "创建用户的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_USER_EDIT,
                MenuGuid = sysUserId,
                Name = "编辑",
                ActionCode = "edit",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "编辑用户的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_USER_DELETE,
                MenuGuid = sysUserId,
                Name = "删除",
                ActionCode = "delete",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "删除用户的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            #endregion

            #region 角色管理
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ROLE,
                MenuGuid = sysRoleId,
                Name = "角色管理",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看角色的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ROLE_VIEW,
                MenuGuid = sysRoleId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看角色的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ROLE_CREATE,
                MenuGuid = sysRoleId,
                Name = "创建",
                ActionCode = "create",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "创建角色的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ROLE_EDIT,
                MenuGuid = sysRoleId,
                Name = "编辑",
                ActionCode = "edit",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看角色的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ROLE_DELETE,
                MenuGuid = sysRoleId,
                Name = "删除",
                ActionCode = "delete",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "删除角色的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            #endregion

            #region 菜单管理
            permissions.Add(new Permission
            {
                Code = PERM_SYS_MENU,
                MenuGuid = sysMenuId,
                Name = "菜单管理",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看菜单的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_MENU_VIEW,
                MenuGuid = sysMenuId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看菜单的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_MENU_CREATE,
                MenuGuid = sysMenuId,
                Name = "创建",
                ActionCode = "create",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "创建菜单的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_MENU_EDIT,
                MenuGuid = sysMenuId,
                Name = "编辑",
                ActionCode = "edit",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "编辑菜单的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_MENU_DELETE,
                MenuGuid = sysMenuId,
                Name = "删除",
                ActionCode = "delete",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "删除菜单的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            #endregion

            #region 权限管理
            permissions.Add(new Permission
            {
                Code = PERM_SYS_PERMISSION,
                MenuGuid = sysPermissionId,
                Name = "权限管理",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看权限的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_PERMISSION_VIEW,
                MenuGuid = sysPermissionId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看权限的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_PERMISSION_CREATE,
                MenuGuid = sysPermissionId,
                Name = "创建",
                ActionCode = "create",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "创建权限的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_PERMISSION_EDIT,
                MenuGuid = sysPermissionId,
                Name = "编辑",
                ActionCode = "edit",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "编辑权限的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_PERMISSION_DELETE,
                MenuGuid = sysPermissionId,
                Name = "删除",
                ActionCode = "delete",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "删除权限的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            #endregion

            #region 图标管理
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ICON,
                MenuGuid = sysIconId,
                Name = "图标管理",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看图标的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ICON_VIEW,
                MenuGuid = sysIconId,
                Name = "查看",
                ActionCode = "view",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Menu,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "查看图标的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ICON_CREATE,
                MenuGuid = sysIconId,
                Name = "创建",
                ActionCode = "create",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "创建图标的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ICON_EDIT,
                MenuGuid = sysIconId,
                Name = "编辑",
                ActionCode = "edit",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "编辑图标的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            permissions.Add(new Permission
            {
                Code = PERM_SYS_ICON_DELETE,
                MenuGuid = sysIconId,
                Name = "删除",
                ActionCode = "delete",
                Status = Status.Normal,
                IsDeleted = IsDeleted.No,
                Type = PermissionType.Action,
                CreateTime = now,
                CreateUserGuid = Guid.Empty,
                CreateUserLoginName = "System",
                Description = "删除图标的权限",
                LastModifyTime = now,
                LastModifyUserGuid = Guid.Empty,
                LastModifyUserLoginName = "System",
            });
            #endregion

            return permissions;
        }

        private static List<RolePermissionMapping> GetRolePermissionMappings()
        {
            var maps = new List<RolePermissionMapping>();

            #region 普通用户权限
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_GUEST,
                PermissionCode = PERM_DASHBOARD_MENU_ROOT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_GUEST,
                PermissionCode = PERM_DASHBOARD_MENU_WORKBENCH,
                CreateTime = now
            });

            #endregion

            #region 管理员权限
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_DASHBOARD_MENU_ROOT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_DASHBOARD_MENU_WORKBENCH,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MANAGEMENT,
                CreateTime = now
            });

            #region 用户
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_USER,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_USER_CREATE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_USER_EDIT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_USER_VIEW,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_USER_DELETE,
                CreateTime = now
            });
            #endregion

            #region 角色
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ROLE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ROLE_VIEW,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ROLE_CREATE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ROLE_EDIT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ROLE_DELETE,
                CreateTime = now
            });
            #endregion

            #region 菜单
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MENU,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MENU_VIEW,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MENU_CREATE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MENU_EDIT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_MENU_DELETE,
                CreateTime = now
            });
            #endregion

            #region 权限
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_PERMISSION,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_PERMISSION_VIEW,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_PERMISSION_CREATE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_PERMISSION_EDIT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_PERMISSION_DELETE,
                CreateTime = now
            });
            #endregion

            #region 图标
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ICON,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ICON_VIEW,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ICON_CREATE,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ICON_EDIT,
                CreateTime = now
            });
            maps.Add(new RolePermissionMapping
            {
                RoleCode = ROLE_CODE_ADMIN,
                PermissionCode = PERM_SYS_ICON_DELETE,
                CreateTime = now
            });
            #endregion

            #endregion

            return maps;
        }

    }
}
