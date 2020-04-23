using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skyline.ApplicationCore.Constants;
using Skyline.ApplicationCore.Entities.ContactAggregate;
using Skyline.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Infrastructure.Identity
{
    public class AppIdentityDbContextSeedData
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider, string pw)
        {
            var adminId = await EnsureUser(serviceProvider, "admin_nick", "admin@contoso.com", pw);
            await EnsureRole(serviceProvider, adminId, AppIdentityConstants.Roles.ADMINISTRATORS);

            var managerId = await EnsureUser(serviceProvider, "manager_nick", "manager@contoso.com", pw);
            await EnsureRole(serviceProvider, managerId, AppIdentityConstants.Roles.MANAGERS);

            using (var context = new SkylineDbContext(serviceProvider.GetRequiredService<DbContextOptions<SkylineDbContext>>()))
            {
                SeedContactData(context, adminId);
            }
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

        public static void SeedContactData(SkylineDbContext context, string adminId)
        {
            if (context.Contacts.Any())
            {
                return;   // DB has been seeded
            }

            context.Contacts.AddRange(
                new Contact
                {
                    Name = "叶文洁",
                    Address = "文一路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "yewenjie@example.com",
                    MobileNumber = "",
                    Status = ContactStatus.Approved,
                    OwnerId = adminId
                },
                new Contact
                {
                    Name = "汪淼",
                    Address = "文二路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "wangmiao@example.com",
                    MobileNumber = "",
                    Status = ContactStatus.Submitted,
                    OwnerId = adminId
                },
                new Contact
                {
                    Name = "史强",
                    Address = "文三路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "shiqiang@example.com",
                    MobileNumber = "",
                    Status = ContactStatus.Rejected,
                    OwnerId = adminId
                },
                new Contact
                {
                    Name = "罗辑",
                    Address = "天目山路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "luoji@example.com",
                    MobileNumber = "",
                    Status = ContactStatus.Submitted,
                    OwnerId = adminId
                },
                new Contact
                {
                    Name = "章北海",
                    Address = "万塘路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "zhangbeihai@example.com",
                    MobileNumber = "",
                    OwnerId = adminId
                }
             );
            context.SaveChanges();
        }
    }
}
