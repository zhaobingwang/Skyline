using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyline.Elasticsearch
{
    public class ESClientProvider : IESClientProvider
    {
        readonly IOptions<ESConfig> config;
        public ESClientProvider(IOptions<ESConfig> config)
        {
            this.config = config;
        }
        public ElasticClient GetClient()
        {
            if (config?.Value == null || config.Value.Urls == null || config.Value.Urls.Count < 1)
                throw new ArgumentNullException(nameof(config));
            var urls = config.Value.Urls.ToArray();
            if (urls.Length > 1)
            {
                // 为何只有一个地址的时候，会请求一个不存在的地址：http://172.18.0.2:9200？
                return GetClient(config.Value.Urls.ToArray(), "");
            }
            return GetClient(urls[0]);
        }

        public ElasticClient GetClient(string indexName)
        {
            if (config?.Value == null || config.Value.Urls == null || config.Value.Urls.Count < 1)
                throw new ArgumentNullException(nameof(config));
            var urls = config.Value.Urls.ToArray();
            if (urls.Length > 1)
            {
                // 为何只有一个地址的时候，会请求一个不存在的地址：http://172.18.0.2:9200？
                return GetClient(config.Value.Urls.ToArray(), indexName);
            }
            return GetClient(urls[0], indexName);
        }

        private ElasticClient GetClient(string url, string defaultIndex = "")
        {
            if (url.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(url));
            var uri = new Uri(url);
            var connectionSetting = new ConnectionSettings(uri);
            connectionSetting.DefaultIndex(defaultIndex);

            var uname = config.Value.UserName;
            var upwd = config.Value.Password;
            if (!uname.IsNullOrWhiteSpace() && !upwd.IsNullOrWhiteSpace())
                connectionSetting.BasicAuthentication(uname, upwd);
            return new ElasticClient(connectionSetting);
        }

        private ElasticClient GetClient(string[] urls, string defaultIndex = "")
        {
            if (urls == null || urls.Length < 1)
                throw new ArgumentNullException(nameof(urls));

            var uris = urls.Select(x => new Uri(x)).ToArray();
            var connectionPool = new SniffingConnectionPool(uris);
            var connectionSetting = new ConnectionSettings(connectionPool);
            if (defaultIndex.IsNullOrWhiteSpace())
                connectionSetting.DefaultIndex(defaultIndex);

            var uname = config.Value.UserName;
            var upwd = config.Value.Password;
            if (!uname.IsNullOrWhiteSpace() && !upwd.IsNullOrWhiteSpace())
                connectionSetting.BasicAuthentication(uname, upwd);
            return new ElasticClient(connectionSetting);
        }
    }
}
