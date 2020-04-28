using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Skyline
{
    /// <summary>
    /// Web工具
    /// </summary>
    public sealed class WebUtils
    {
        internal readonly HttpConnectionPool ConnectionPool;
        public WebUtils(string server, int poolSize = 30)
        {
            ConnectionPool = new HttpConnectionPool(server, poolSize);
        }

        /// <summary>
        /// 异步HTTP GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public async Task<string> GetAsync(string url, IDictionary<string, string> parameters, string charset)
        {
            try
            {
                if (parameters != null && parameters.Count > 0)
                {
                    if (url.Contains("?"))
                        url = $"{url}&{BuildQuery(parameters, charset)}";
                    else
                        url = $"{url}?{BuildQuery(parameters, charset)}";
                }

                //var query = new Uri(url).Query;
                var client = ConnectionPool.GetClient();
                var result = await client.GetStringAsync(url);
                ConnectionPool.ReturnClient(client);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public async Task<string> PostAsync(string url, IDictionary<string, string> parameters, string charset)
        {
            try
            {
                var encoding = Encoding.GetEncoding(charset);
                var client = ConnectionPool.GetClient();
                var query = new Uri(url).Query;
                var content = new StringContent(BuildQuery(parameters, charset), encoding, "application/x-www-form-urlencoded");

                var resp = await client.PostAsync(query, content);
                var result = await resp.Content.ReadAsStringAsync();

                ConnectionPool.ReturnClient(client);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 异步HTTP POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">POST数据</param>
        /// <param name="charset">编码字符集</param>
        /// <returns>HTTP响应</returns>
        public async Task<string> PostAsync(string url, string postData, string charset)
        {
            try
            {
                var encoding = Encoding.GetEncoding(charset);
                var client = ConnectionPool.GetClient();
                //var query = new Uri(url).Query;
                var content = new StringContent(postData, encoding, "application/x-www-form-urlencoded");

                var resp = await client.PostAsync(url, content);
                var result = await resp.Content.ReadAsStringAsync();

                ConnectionPool.ReturnClient(client);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 组装普通文本请求参数
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="charset"></param>
        /// <returns></returns>
        public static string BuildQuery(IDictionary<string, string> parameters, string charset)
        {
            var data = new StringBuilder();
            var hasParams = false;
            using (var dem = parameters.GetEnumerator())
            {
                while (dem.MoveNext())
                {
                    var name = dem.Current.Key;
                    var value = dem.Current.Value;
                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                    {
                        if (hasParams)
                            data.Append("&");
                        data.Append(name);
                        data.Append("=");

                        var encodedValue = HttpUtility.UrlEncode(value, Encoding.GetEncoding(charset));

                        data.Append(encodedValue);
                        hasParams = true;
                    }
                }
                return data.ToString();
            }
        }
    }
}
