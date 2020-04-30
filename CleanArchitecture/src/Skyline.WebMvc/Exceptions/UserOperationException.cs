using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.WebMvc.Exceptions
{
    public class UserOperationException : Exception
    {
        /// <summary>
        /// 用户操作异常
        /// </summary>
        public UserOperationException()
        {

        }
        public UserOperationException(string message) : base(message)
        {

        }
        public UserOperationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
