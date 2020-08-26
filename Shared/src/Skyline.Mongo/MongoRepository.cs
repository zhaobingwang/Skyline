using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Skyline.Mongo
{
    /// <summary>
    /// MongoDB数据操作仓库
    /// </summary>
    public class MongoRepository
    {
        /// <summary>
        /// MongoDB客户端
        /// </summary>
        private readonly MongoClient client;

        /// <summary>
        /// 操作的数据库
        /// </summary>
        private readonly IMongoDatabase database;

        /// <summary>
        /// MongoDB数据操作仓库
        /// </summary>
        /// <param name="config">MongoDB配置</param>
        public MongoRepository(IMongoConfig config)
        {
            if (config == null || config.ConnectionString.IsNullOrWhiteSpace() || config.Database.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(config));

            client = new MongoClient(config.ConnectionString);
            database = client.GetDatabase(config.Database);
        }

        /// <summary>
        /// 获取MongoDB集合
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="name">集合名称</param>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return database.GetCollection<T>(name);
        }

        #region insert
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
        public async Task InsertOneAsync<T>(string collectionName, T entity) where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            await collection.InsertOneAsync(entity);
        }
        #endregion

        #region delete
        /// <summary>
        /// 删除单个文档，如果匹配度多个文档，则删除第一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collectionName">集合名词</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public long DeleteOne<T>(string collectionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            var result = collection.DeleteOne<T>(filter);
            return result.DeletedCount;
        }

        /// <summary>
        /// 删除单个文档，如果匹配度多个文档，则删除第一个
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<T>(string collcetionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var collection = GetCollection<T>(collcetionName);
            var result = await collection.DeleteOneAsync<T>(filter);
            return result.DeletedCount;
        }

        /// <summary>
        /// 删除多个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public long DeleteMany<T>(string collcetionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var collection = GetCollection<T>(collcetionName);
            var result = collection.DeleteMany<T>(filter);
            return result.DeletedCount;
        }

        /// <summary>
        /// 删除多个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public async Task<long> DeleteManyAsync<T>(string collcetionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var collcetion = GetCollection<T>(collcetionName);
            var result = await collcetion.DeleteManyAsync<T>(filter);
            return result.DeletedCount;
        }
        #endregion

        #region update
        /// <summary>
        /// 更新一个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合类型</param>
        /// <param name="entity">更新对象</param>
        /// <returns></returns>
        public async Task<long> UpdateOneAsync<T>(string collcetionName, T entity) where T : MongoEntity
        {
            var collection = GetCollection<T>(collcetionName);
            return await UpdateOneAsync(collcetionName, x => x.Id == entity.Id, entity);
        }

        /// <summary>
        /// 更新一个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collcetionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="entity">更新对象</param>
        /// <returns></returns>
        public async Task<long> UpdateOneAsync<T>(string collcetionName, Expression<Func<T, bool>> filter, T entity) where T : MongoEntity
        {
            var collcetion = GetCollection<T>(collcetionName);
            var updateDefinitionList = entity.GetUpdateDefinition();

            var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>().Combine(updateDefinitionList);

            var result = await collcetion.UpdateOneAsync<T>(filter, updateDefinitionBuilder);
            return result.ModifiedCount;
        }
        // TODO: 支持局部更新
        #endregion

        #region query

        /// <summary>
        /// 获取一个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public async Task<T> FindOneAsync<T>(string collectionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var finded = await FindManyAsync(collectionName, filter);
            var result = finded.FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 获取一个文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public async Task<T> FindOneAsync<T>(string collectionName, Expression<Func<T, bool>> filter, Func<Sort<T>, Sort<T>> sort) where T : MongoEntity
        {
            var finded = await FindManyAsync(collectionName, filter, sort);
            var result = finded.FirstOrDefault();
            return result;
        }

        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <returns></returns>
        public async Task<List<T>> FindManyAsync<T>(string collectionName, Expression<Func<T, bool>> filter) where T : MongoEntity
        {
            var result = await FindManyAsync<T>(collectionName, filter, null, null);
            return result;
        }

        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="limit">最大文档数</param>
        /// <returns></returns>
        public async Task<List<T>> FindManyAsync<T>(string collectionName, Expression<Func<T, bool>> filter, int? limit) where T : MongoEntity
        {
            var result = await FindManyAsync<T>(collectionName, filter, null, limit);
            return result;
        }

        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public async Task<List<T>> FindManyAsync<T>(string collectionName, Expression<Func<T, bool>> filter, Func<Sort<T>, Sort<T>> sort) where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            var finded = collection.Find(filter);

            if (sort != null)
                finded = finded.Sort(sort.GetSortDefinition());
            return await finded.ToListAsync();
        }

        /// <summary>
        /// 获取文档列表集合
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序</param>
        /// <param name="limit">最大文档数</param>
        /// <returns></returns>
        public async Task<List<T>> FindManyAsync<T>(string collectionName, Expression<Func<T, bool>> filter, Func<Sort<T>, Sort<T>> sort, int? limit) where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            var finded = collection.Find(filter);

            if (sort != null)
                finded = finded.Sort(sort.GetSortDefinition());

            if (limit != null)
                finded = finded.Limit(limit);

            return await finded.ToListAsync();
        }

        #endregion

        #region  paging query
        /// <summary>
        /// 分页获取文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前查询页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public async Task<PageData<T>> PageListAsync<T>(string collectionName, Expression<Func<T, bool>> filter, Func<Sort<T>, Sort<T>> sort, int pageIndex, int pageSize)
            where T : MongoEntity
        {
            var collection = GetCollection<T>(collectionName);
            var count = (int)(await collection.CountDocumentsAsync<T>(filter));
            var finded = collection.Find(filter);

            if (sort != null)
                finded = finded.Sort(sort.GetSortDefinition());

            finded = finded.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
            var items = await finded.ToListAsync();
            return new PageData<T>(pageIndex, pageSize, count, items);
        }

        /// <summary>
        /// 分页获取文档
        /// </summary>
        /// <typeparam name="T">文档类型</typeparam>
        /// <param name="collectionName">集合名称</param>
        /// <param name="filter">过滤器(要过滤的字段)</param>
        /// <param name="projector">投影仪(要取出的字段)</param>
        /// <param name="sort">排序</param>
        /// <param name="pageIndex">当前查询页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        public async Task<PageData<TResult>> PageListAsync<T, TResult>(string collectionName, Expression<Func<T, bool>> filter, Expression<Func<T, TResult>> projector, Func<Sort<T>, Sort<T>> sort, int pageIndex, int pageSize)
            where T : MongoEntity
            where TResult : class
        {
            var collection = GetCollection<T>(collectionName);
            var count = (int)(await collection.CountDocumentsAsync<T>(filter));
            var finded = collection.Find(filter);

            if (sort != null)
                finded = finded.Sort(sort.GetSortDefinition());

            finded = finded.Skip((pageIndex - 1) * pageSize).Limit(pageSize);

            var items = await finded.Project(projector).ToListAsync();

            return new PageData<TResult>(pageIndex, pageSize, count, items);
        }
        #endregion
    }
}
