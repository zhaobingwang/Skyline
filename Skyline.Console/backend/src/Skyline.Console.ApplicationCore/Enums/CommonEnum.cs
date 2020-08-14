using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Console.ApplicationCore.Enums
{
    /// <summary>
    /// 是否已删除
    /// </summary>
    public enum IsDeleted
    {
        [Description("未删除")]
        No = 0,

        [Description("已删除")]
        Yes = 1,
    }

    /// <summary>
    /// 是否锁定
    /// </summary>
    public enum IsLocked
    {
        /// <summary>
        /// 未锁定
        /// </summary>
        [Description("未锁定")]
        UnLocked = 0,

        /// <summary>
        /// 已锁定
        /// </summary>
        [Description("已锁定")]
        Locked = 1
    }

    public enum YesOrNo
    {
        /// <summary>
        /// 未知
        /// </summary>
        UnKnown = -1,

        /// <summary>
        /// 否
        /// </summary>
        No = 0,

        /// <summary>
        /// 是
        /// </summary>
        Yes = 1
    }
}
