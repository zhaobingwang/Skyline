using Skyline.Console.ApplicationCore.Enums;
using Skyline.Console.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Console.WebMvc.Services
{
    [Obsolete("见ApplicationCore.Service")]
    public class AdministratorService
    {
        private readonly SkylineDbContext _dbContext;
        public AdministratorService(SkylineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool LoginCheck(string loginName, string password)
        {
            var pwdHash = password.MD5Hash();
            var user = _dbContext.Users.Where(u => u.LoginName == loginName & u.PasswordHash == pwdHash).FirstOrDefault();
            if (user == null)
                return false;
            if (user.Status != Status.Normal)
                return false;
            if (user.IsDeleted == IsDeleted.Yes)
                return false;
            return true;
        }
    }
}
