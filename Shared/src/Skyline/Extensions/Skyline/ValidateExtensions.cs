using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline
{
    /// <summary>
    /// 验证扩展函数
    /// </summary>
    public static partial class ValidateExtensions
    {
        /// <summary>
        /// 校验对象是否为null，为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="paramName">参数名</param>
        public static void CheckNull(this object obj, string paramName)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName);
        }
    }
}
