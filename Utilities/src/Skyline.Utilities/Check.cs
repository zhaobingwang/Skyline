using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Utilities
{
    /// <summary>
    /// 验证参数是否合法
    /// 不合法则抛出相应的异常
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// 验证值是否为NullOrEmpty
        /// 是则抛出<see cref="ArgumentException"/>
        /// 否则返回原始值
        /// </summary>
        /// <param name="value">待校验的值</param>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        public static string IsNullOrEmpty(string value, string parameterName)
        {
            if (value.IsNullOrEmpty())
                throw new ArgumentException($"{parameterName} can not be null or empty!", parameterName);
            return value;
        }

        /// <summary>
        /// 验证值是否为NullOrWhiteSpace
        /// 是则抛出<see cref="ArgumentException"/>
        /// 否则返回原始值
        /// </summary>
        /// <param name="value">待校验的值</param>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        public static string IsNullOrWhiteSpace(string value, string parameterName)
        {
            if (value.IsNullOrWhiteSpace())
                throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
            return value;
        }

        /// <summary>
        /// 验证值是否在范围内[<paramref name="minLength"/>,<paramref name="maxLength"/>]
        /// 是则返回原始值
        /// 否则抛出<see cref="ArgumentException"/>
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string IsInRange(string value, string parameterName, int maxLength = int.MaxValue, int minLength = 0)
        {
            IsNullOrWhiteSpace(value, parameterName);
            if (maxLength < minLength)
                throw new ArgumentException($"{minLength} must be equal to or less than {maxLength}");

            var valLength = value.Length;
            if (valLength > maxLength)
            {
                throw new ArgumentException($"{nameof(parameterName)} length must be equal to or less than {maxLength}", parameterName);
            }
            if (valLength < minLength)
            {
                throw new ArgumentException($"{nameof(parameterName)} length must be equal to or greater than {minLength}", parameterName);
            }
            return value;
        }

        /// <summary>
        /// 为null则抛出<see cref="ArgumentNullException"/>,否则返回原始值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">待校验的值</param>
        /// <param name="parameterName">参数名</param>
        /// <returns></returns>
        public static T IsNull<T>(T value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
            return value;
        }
    }
}
