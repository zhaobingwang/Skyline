using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Skyline.Console.ApplicationCore.Entities
{
    public class User
    {
        public Guid Guid { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string PasswordHash { get; set; }

        public string Avatar { get; set; }
        public DateTime DOB { get; set; }

        public UserType Type { get; set; }
        public Status Status { get; set; }
        public int MyProperty { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid CreateUserId { get; set; }
        public string CreateUserName { get; set; }
        public DateTime LastModifyTime { get; set; }
        public string LastModifyUserId { get; set; }
        public string LastModifyUserName { get; set; }
        public string Description { get; set; }
        public ICollection<UserRoleMapping> UserRoles { get; set; }
    }


}