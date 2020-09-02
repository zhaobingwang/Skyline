using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Enums
{

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserType
    {
        [Description("普通用户")]
        Normal = 0,

        [Description("管理员")]
        Admin = 9,

        [Description("超级管理员")]
        SuperAdmin = 99,
    }


    public enum Status
    {

        [Description("未指定")]
        Unspecified = -1,

        [Description("已禁用")]
        Forbidden = 0,

        [Description("正常")]
        Normal = 1,
    }
}
