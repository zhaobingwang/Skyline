namespace System.Collections.Generic
{
    public static class BlocksEnumerableExtensions
    {
        /// <summary>
        /// 使用指定的分隔符连接一个已构造的<see cref="IEnumerable{T}"/>System.String类型集合
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string JoinAsString(this IEnumerable<string> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// 使用指定的分隔符连接集合成员
        /// </summary>
        /// <typeparam name="T">集成成员类型</typeparam>
        /// <param name="source">包含要连接的对象的集合</param>
        /// <param name="separator">字符串类型的作分隔符（仅当值有多个元素时，才将分隔符包含在返回的字符串中）</param>
        /// <returns></returns>
        public static string JoinAsString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }
    }
}
