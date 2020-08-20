using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Elasticsearch
{
    public interface IESClientProvider
    {
        /// <summary>
        /// 获取 ElasticClient
        /// </summary>
        /// <returns></returns>
        ElasticClient GetClient();

        /// <summary>
        /// 根据索引名获取ElasticClient
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        ElasticClient GetClient(string indexName);
    }
}
