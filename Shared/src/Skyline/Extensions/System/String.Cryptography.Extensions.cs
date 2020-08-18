using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static partial class StringExtensions
    {

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        /// <param name="source">待转换的字符串</param>
        /// <param name="toLower">是否转为小写</param>
        /// <returns></returns>
        public static string MD5Hash(this string source, bool toLower = false)
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var hashByte in hashBytes)
                {
                    sb.Append(hashByte.ToString("X2"));
                }
                return toLower ? sb.ToString().ToLower() : sb.ToString();
            }
        }
    }
}
