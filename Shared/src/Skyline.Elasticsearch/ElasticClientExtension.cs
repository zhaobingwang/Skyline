using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Elasticsearch
{
    /// <summary>
    /// ElasticClient扩展
    /// </summary>
    public static class ElasticClientExtension
    {
        public static async Task<bool> CreateIndex<T>(this ElasticClient client, string indexName = "", int numberOfShards = 5, int numberOfReplicas = 1) where T : class
        {
            if (indexName.IsNullOrWhiteSpace())
                indexName = typeof(T).Name;

            var existResponse = await client.Indices.ExistsAsync(indexName);
            if (existResponse.Exists)
                return false;
            var idxState = new IndexState
            {
                Settings = new IndexSettings()
                {
                    NumberOfReplicas = numberOfReplicas,
                    NumberOfShards = numberOfShards
                }
            };
            var response = await client.Indices.CreateAsync(indexName, x => x.InitializeUsing(idxState).Map<T>(i => i.AutoMap()));
            return response.Acknowledged;
        }
    }
}