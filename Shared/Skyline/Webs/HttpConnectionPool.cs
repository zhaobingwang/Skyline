using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Skyline
{
    internal class HttpConnectionPool
    {
        private readonly ConcurrentStack<HttpClient> _clients = new ConcurrentStack<HttpClient>();
        private string _url = string.Empty;
        private int _size = 1;
        private double _timeOut = 60;

        private readonly object _lockObject = new object();

        /// <summary>
        /// 初始化HttpClient池
        /// </summary>
        /// <param name="url">目标域名（如：https://www.baidu.com）</param>
        /// <param name="num">池里Client数量</param>
        public HttpConnectionPool(string url, int num)
        {
            _url = string.IsNullOrEmpty(url) ? throw new ArgumentNullException(nameof(url)) : url;
            _size = num <= 0 ? throw new ArgumentException(nameof(num)) : num;
            _clients.Clear();
        }

        public HttpClient GetClient()
        {
            if (_clients.TryPop(out var client))
            {
                return client;
            }
            else
            {
                var newClient = new HttpClient()
                {
                    BaseAddress = new Uri(_url),
                    Timeout = TimeSpan.FromSeconds(_timeOut)
                };
                newClient.DefaultRequestHeaders.Add("User-Agent", "App4Net");
                newClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                return newClient;
            }
        }

        public void ReturnClient(HttpClient client)
        {
            lock (_lockObject)
            {
                if (_clients.Count > _size)
                {
                    client.Dispose();
                }
                else
                {
                    _clients.Push(client);
                }
            }
        }
    }
}
