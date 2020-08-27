using System;
using System.Collections.Generic;
using System.Linq;
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
        static DateTime now;
        static Guid superAdminId;
        static SeedData()
        {
            now = DateTime.UtcNow;
            superAdminId = Guid.NewGuid();
        }

        public static async Task SeedAsync(IServiceProvider provider)
        {
            var dbContext = provider.GetService<SkylineDbContext>();
            await InitMenus(dbContext);
            await InitRoles(dbContext);
            await InitUsers(dbContext);
            await InitUserRoleMappings(dbContext);
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
                dbContext.Menus.AddRange(
                    new Menu
                    {
                        Guid = dashBoardId,
                        Name = "DashBoard",
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
                        CreateUserGuid = Guid.Empty,
                        CreateUserLoginName = "System",
                        LastModifyTime = now,
                        HideMenu = YesOrNo.No,
                    },
                     new Menu
                     {
                         Guid = Guid.NewGuid(),
                         Name = "工作台",
                         Url = "DashBoard/Index",
                         Alias = "",
                         Icon = null,
                         ParentGuid = dashBoardId,
                         ParentName = null,
                         Level = 0,
                         Description = "工作台页面",
                         Sort = 0,
                         Status = Status.Normal,
                         IsDeleted = IsDeleted.No,
                         IsDefaultRouter = YesOrNo.Yes,
                         CreateTime = now,
                         CreateUserGuid = Guid.Empty,
                         CreateUserLoginName = "System",
                         LastModifyTime = now,
                         HideMenu = YesOrNo.No,
                     },
                     new Menu
                     {
                         Guid = Guid.NewGuid(),
                         Name = "分析",
                         Url = "DashBoard/Analysis",
                         Alias = "",
                         Icon = null,
                         ParentGuid = dashBoardId,
                         ParentName = null,
                         Level = 0,
                         Description = "分析页面",
                         Sort = 0,
                         Status = Status.Normal,
                         IsDeleted = IsDeleted.No,
                         IsDefaultRouter = YesOrNo.Yes,
                         CreateTime = now,
                         CreateUserGuid = Guid.Empty,
                         CreateUserLoginName = "System",
                         LastModifyTime = now,
                         HideMenu = YesOrNo.No,
                     },
                     new Menu
                     {
                         Guid = Guid.NewGuid(),
                         Name = "监控",
                         Url = "DashBoard/Monitor",
                         Alias = "",
                         Icon = null,
                         ParentGuid = dashBoardId,
                         ParentName = null,
                         Level = 0,
                         Description = "监控页面",
                         Sort = 0,
                         Status = Status.Normal,
                         IsDeleted = IsDeleted.No,
                         IsDefaultRouter = YesOrNo.Yes,
                         CreateTime = now,
                         CreateUserGuid = Guid.Empty,
                         CreateUserLoginName = "System",
                         LastModifyTime = now,
                         HideMenu = YesOrNo.No,
                     }
                    );
                await dbContext.SaveChangesAsync();
            }
        }

        private static List<User> GetUsers()
        {
            var users = new List<User>();
            users.Add(new User
            {
                Guid = superAdminId,
                LoginName = "superadmin",
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
                //UserRoleMappings = GetUserRoleMappings().FindAll(x => x.UserGuid == superAdminId)
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
            return maps;
        }
    }
}
