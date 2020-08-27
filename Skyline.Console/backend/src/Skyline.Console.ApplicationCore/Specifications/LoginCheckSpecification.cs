using Ardalis.Specification;
using Skyline.Console.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.Specifications
{
    public class LoginCheckSpecification : Specification<User>
    {
        public LoginCheckSpecification(string loginName, string password)
        {
            var pwdHash = password.MD5Hash();
            Query.Where(u => u.LoginName == loginName && u.PasswordHash == pwdHash);
        }
    }
}
