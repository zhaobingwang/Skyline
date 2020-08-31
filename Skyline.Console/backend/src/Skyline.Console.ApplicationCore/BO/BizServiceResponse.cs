using Skyline.Extensions.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Console.ApplicationCore.BO
{
    /// <summary>
    /// 业务服务响应实体
    /// </summary>
    public class BizServiceResponse
    {
        /// <summary>
        /// 业务服务响应码
        /// </summary>
        public BizServiceResponseCode Code { get; set; }

        private string message;
        /// <summary>
        /// 业务服务响应消息
        /// </summary>
        public string Message
        {
            set => message = value;
            get
            {
                if (message.IsNotNullOrWhiteSpace())
                    return message;
                if (Exception.IsNotNull())
                    return Exception.Message;
                return null;
            }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        public BizServiceResponse()
        {

        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        public BizServiceResponse(BizServiceResponseCode code)
        {
            Code = code;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        public BizServiceResponse(BizServiceResponseCode code, string message) : this(code)
        {
            Message = message;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="exception">异常信息</param>
        public BizServiceResponse(BizServiceResponseCode code, Exception exception)
        {
            Code = code;
            Exception = exception;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, string message, object data) : this(code, message)
        {
            Data = data;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="exception">异常信息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, Exception exception, object data) : this(code)
        {
            Exception = exception;
            Data = data;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        /// <param name="exception">异常信息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, string message, Exception exception, object data) : this(code)
        {
            Message = message;
            Exception = exception;
            Data = data;
        }

        /// <summary>
        /// 成功
        /// </summary>
        public bool Success => Code == BizServiceResponseCode.Success;

        /// <summary>
        /// 失败
        /// </summary>
        public bool Failed => Code == BizServiceResponseCode.Failed;

        /// <summary>
        /// 异常
        /// </summary>
        public bool Error => Code == BizServiceResponseCode.Error;

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse IsSuccess(string message, object data = null)
        {
            return new BizServiceResponse(BizServiceResponseCode.Success, message, data);
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse IsFailed(string message, object data = null)
        {
            return new BizServiceResponse(BizServiceResponseCode.Failed, message, data);
        }

        /// <summary>
        /// 响应异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse IsError(string message, object data = null)
        {
            return new BizServiceResponse(BizServiceResponseCode.Error, message, data);
        }

        /// <summary>
        /// 响应异常
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BizServiceResponse IsError(Exception ex, object data = null)
        {
            return new BizServiceResponse(BizServiceResponseCode.Error, ex, data);
        }
    }

    /// <summary>
    /// 业务服务响应实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BizServiceResponse<T> where T : class, new()
    {
        /// <summary>
        /// 业务服务响应码
        /// </summary>
        public BizServiceResponseCode Code { get; set; }

        private string message;
        /// <summary>
        /// 业务服务响应消息
        /// </summary>
        public string Message
        {
            set => message = value;
            get
            {
                if (message.IsNotNullOrWhiteSpace())
                    return message;
                if (Exception.IsNotNull())
                    return Exception.Message;
                return null;
            }
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        public BizServiceResponse()
        {

        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        public BizServiceResponse(BizServiceResponseCode code)
        {
            Code = code;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        public BizServiceResponse(BizServiceResponseCode code, string message) : this(code)
        {
            Message = message;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="exception">异常信息</param>
        public BizServiceResponse(BizServiceResponseCode code, Exception exception)
        {
            Code = code;
            Exception = exception;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, string message, T data) : this(code, message)
        {
            Data = data;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="exception">异常信息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, Exception exception, T data) : this(code)
        {
            Exception = exception;
            Data = data;
        }

        /// <summary>
        /// 业务服务响应
        /// </summary>
        /// <param name="code">响应状态码</param>
        /// <param name="message">响应消息</param>
        /// <param name="exception">异常信息</param>
        /// <param name="data">数据</param>
        public BizServiceResponse(BizServiceResponseCode code, string message, Exception exception, T data) : this(code)
        {
            Message = message;
            Exception = exception;
            Data = data;
        }

        /// <summary>
        /// 成功
        /// </summary>
        public bool Success => Code == BizServiceResponseCode.Success;

        /// <summary>
        /// 失败
        /// </summary>
        public bool Failed => Code == BizServiceResponseCode.Failed;

        /// <summary>
        /// 异常
        /// </summary>
        public bool Error => Code == BizServiceResponseCode.Error;

        /// <summary>
        /// 响应成功
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse<T> IsSuccess(string message, T data = null)
        {
            return new BizServiceResponse<T>(BizServiceResponseCode.Success, message, data);
        }

        /// <summary>
        /// 响应失败
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse<T> IsFailed(string message, T data = null)
        {
            return new BizServiceResponse<T>(BizServiceResponseCode.Failed, message, data);
        }

        /// <summary>
        /// 响应异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        public static BizServiceResponse<T> IsError(string message, T data = null)
        {
            return new BizServiceResponse<T>(BizServiceResponseCode.Error, message, data);
        }

        /// <summary>
        /// 响应异常
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static BizServiceResponse<T> IsError(Exception ex, T data = null)
        {
            return new BizServiceResponse<T>(BizServiceResponseCode.Error, ex, data);
        }
    }

    /// <summary>
    /// 业务服务响应码
    /// </summary>
    public enum BizServiceResponseCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 失败
        /// </summary>
        Failed = 2,

        /// <summary>
        /// 发生异常
        /// </summary>
        Error = 9,
    }
}