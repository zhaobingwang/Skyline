using Skyline;
using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// Object扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <param name="obj">源对象</param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="obj">源对象</param>
        /// <returns></returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static string ToJson(this object obj)
        {
            return JsonUtil.ToJson(obj);
        }
    }
}
