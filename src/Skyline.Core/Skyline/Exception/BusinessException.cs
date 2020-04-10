using System;

namespace Skyline.Core
{
    /// <summary>
    /// 业务异常
    /// </summary>
    public class BusinessException : Exception, IBusinessException
    {
        /// <summary>
        /// 异常代码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 业务异常
        /// </summary>
        /// <param name="code">异常代码</param>
        /// <param name="message">异常概要信息</param>
        /// <param name="innerException">内部异常</param>
        public BusinessException(string code = null, string message = null, Exception innerException = null) : base(message, innerException)
        {
            Code = code;
        }
    }
}
