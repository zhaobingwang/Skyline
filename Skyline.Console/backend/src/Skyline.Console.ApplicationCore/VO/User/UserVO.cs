using Skyline.Console.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.VO
{
    public class UserVO
    {
        public Guid id { get; set; }
        public string loginName { get; set; }
        public string nickName { get; set; }
        public string avatar { get; set; }
        public DateTime DOB { get; set; }
        public UserType Type { get; set; }
        public Status status { get; set; }
        public IsDeleted IsDeleted { get; set; }
        public DateTime lastModifyTime { get; set; }
        public string lastModifyUserLoginName { get; set; }
    }
}
