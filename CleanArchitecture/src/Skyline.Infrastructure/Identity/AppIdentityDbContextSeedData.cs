using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skyline.ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Identity
{
    public class AppIdentityDbContextSeedData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, string pw)
        {
            var adminId = await EnsureUser(serviceProvider, "admin_nick", "admin@contoso.com", pw);
            await EnsureRole(serviceProvider, adminId, IdentityConstants.Roles.ADMINISTRATORS);

            var managerId = await EnsureUser(serviceProvider, "manager_nick", "manager@contoso.com", pw);
            await EnsureRole(serviceProvider, managerId, IdentityConstants.Roles.MANAGERS);
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string nickName, string userName, string pw)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = userName,
                    NickName = nickName,
                    EmailConfirmed = true,
                };
                await userManager.CreateAsync(user, pw);
            }
            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }
            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;

            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            if (roleManager == null)
            {
                throw new Exception("rolemanager is null!");
            }
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var user = await userManager.FindByIdAsync(uid);
            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }
            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
