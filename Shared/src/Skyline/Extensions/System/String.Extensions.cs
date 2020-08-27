using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    /// <see cref="string" 的扩展方法/>
    /// </summary>
    public static partial class StringExtensions
    {
        /// <summary>
        /// 指示指定的字符串是 null 还是空字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// 指示指定的字符串不是 null 还是空字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string source)
        {
            return !string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// 指示指定的字符串是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 指示指定的字符串不是 null、空还是仅由空白字符组成
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNotNullOrWhiteSpace(this string source)
        {
            return !string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 使用正则表达式验证是否匹配
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string source, string pattern)
        {
            if (source == null)
                return false;
            return Regex.IsMatch(source, pattern);
        }

        /// <summary>
        /// 使用正则表达式验证匹配字符
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string source, string pattern)
        {
            if (source == null)
                return "";
            return Regex.Match(source, pattern).Value;
        }
    }
}
