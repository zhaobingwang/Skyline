using Nest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Elasticsearch
{
    public interface IESContext
    {
    }

    public abstract class ESContext<T> : IESContext where T : class
    {
        protected IESClientProvider clientProvider;
        public abstract string IndexName { get; }
        public ESContext(IESClientProvider clientProvider)
        {
            this.clientProvider = clientProvider;
        }


        public async Task<bool> InsertOne(T obj)
        {
            var client = clientProvider.GetClient(IndexName);
            var existResponse = await client.Indices.ExistsAsync(IndexName);
            if (!existResponse.Exists)
                await client.CreateIndex<T>(IndexName);
            var response = await client.IndexDocumentAsync(obj);
            return response.IsValid;
        }

        public async Task<bool> InsertMany(List<T> list)
        {
            var client = clientProvider.GetClient(IndexName);
            var existResponse = await client.Indices.ExistsAsync(IndexName);
            if (!existResponse.Exists)
                await client.CreateIndex<T>(IndexName);
            var response = await client.IndexManyAsync(list);
            return response.IsValid;
        }
    }
}
