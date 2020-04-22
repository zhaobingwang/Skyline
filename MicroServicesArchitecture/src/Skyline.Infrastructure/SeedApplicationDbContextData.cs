using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Skyline.Domain.ContactAggregate;
using Skyline.Domain.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Infrastructure
{
    public static class SeedApplicationDbContextData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything
                var adminID = await EnsureUser(serviceProvider, testUserPw, "admin", "admin@contoso.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                // allowed user can create and edit contacts that they create
                var managerID = await EnsureUser(serviceProvider, testUserPw, "manager", "manager@contoso.com");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string nickName, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = UserName,
                    EmailConfirmed = true,
                    NickName = nickName
                };
                await userManager.CreateAsync(user, testUserPw);
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
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<AppUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
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
                    Status = ContactStatus.Approved,
                    OwnerId = adminID
                },
                new Contact
                {
                    Name = "汪淼",
                    Address = "文二路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "wangmiao@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerId = adminID
                },
                new Contact
                {
                    Name = "史强",
                    Address = "文三路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "shiqiang@example.com",
                    Status = ContactStatus.Rejected,
                    OwnerId = adminID
                },
                new Contact
                {
                    Name = "罗辑",
                    Address = "天目山路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "luoji@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerId = adminID
                },
                new Contact
                {
                    Name = "章北海",
                    Address = "万塘路",
                    City = "杭州市",
                    Province = "浙江省",
                    Zip = "310000",
                    Email = "zhangbeihai@example.com",
                    OwnerId = adminID
                }
             );
            context.SaveChanges();
        }
    }
}
