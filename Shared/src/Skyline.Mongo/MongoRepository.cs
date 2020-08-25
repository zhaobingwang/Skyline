using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Mongo
{
    public class MongoRepository
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public MongoRepository(IMongoConfig config)
        {
            if (config == null || config.ConnectionString.IsNullOrWhiteSpace() || config.Database.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(config));

            client = new MongoClient(config.ConnectionString);
            database = client.GetDatabase(config.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }

        /// <summary>
        /// 插入单个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合名称</param>
        /// <param name="entity">文档对象</param>
        public void InsertOne<T>(string collcetionName, T entity) where T : MongoEntity
        {
            var collection = GetCollection<T>(collcetionName);
            collection.InsertOne(entity);
        }

        /// <summary>
        /// 插入单个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="entity">文档对象</param>
        /// <returns></returns>
        public Task InsertOneAsync<T>(string collectionName, T entity) where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            return collection.InsertOneAsync(entity);
        }
    }
}
