namespace System
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// bool值中文描述
        /// True返回是，False返回否
        /// </summary>
        /// <param name="source">源值</param>
        /// <returns></returns>
        public static string Description(this bool source)
        {
            return source ? "是" : "否";
        }

        /// <summary>
        /// bool值中文描述
        /// True返回是，False返回否
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string Description(this bool? source)
        {
            return source == null ? "" : source.Value.Description();
        }
    }
}
