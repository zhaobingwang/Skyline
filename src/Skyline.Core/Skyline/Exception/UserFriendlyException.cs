using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Core
{
    /// <summary>
    /// 用户友好异常
    /// </summary>
    public class UserFriendlyException : BusinessException, IUserFriendlyException
    {
        /// <summary>
        /// 用户友好异常
        /// </summary>
        /// <param name="code">异常代码</param>
        /// <param name="message">异常信息</param>
        /// <param name="innerException">内部异常</param>
        public UserFriendlyException(string code = null, string message = null, Exception innerException = null) : base(code, message, innerException)
        {

        }
    }
}
