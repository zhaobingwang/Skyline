namespace Skyline.Core
{
    /// <summary>
    /// 业务异常接口
    /// </summary>
    public interface IBusinessException
    {
        /// <summary>
        /// 异常代码
        /// </summary>
        string Code { get; }
    }
}
