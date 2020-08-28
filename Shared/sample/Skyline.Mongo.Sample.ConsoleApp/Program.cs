using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Threading.Tasks;

namespace Skyline.Mongo.Sample.ConsoleApp
{
    class Program
    {
        const string COLLECTION_NAME = "log.exceptions";
        static async Task Main(string[] args)
        {
            MongoConfig config = new MongoConfig
            {
                ConnectionString = "mongodb://127.0.0.1:27017",
                Database = "logs"
            };

            MongoRepository mongoRepository = new MongoRepository(config);
            try
            {
                //int i = 0;
                //int j = 10 / i;
                //var deletedCount = await mongoRepository.DeleteOneAsync<LogEntity>(COLLECTION_NAME, x => x.LogTime >= new DateTime(2020, 8, 26));
                //var deletedCount = await mongoRepository.DeleteManyAsync<LogEntity>(COLLECTION_NAME, x => x.LogTime >= new DateTime(2020, 8, 26));
                //Console.WriteLine($"deleted count: {deletedCount}");

                // Query
                //var result = await mongoRepository.FindManyAsync<LogEntity>(COLLECTION_NAME, x => x.LogTime >= new DateTime(2020, 8, 25), x => x.Desc(y => y.LogTime));
                //var result = await mongoRepository.PageListAsync<LogEntity>(COLLECTION_NAME, x => x.LogTime >= new DateTime(2020, 8, 25), x => x.Desc(y => y.LogTime), 2, 2);
                //foreach (var item in result.Items)
                //{
                //    Console.WriteLine($"{item.Id}\t{item.LogTime}\t{item.Type}\t{item.Message}");
                //}

                // update
                var result = await mongoRepository.UpdateOneAsync(COLLECTION_NAME, new LogEntity { Id = ObjectId.Parse("5f44cfd9681430e55dead00f"), Message = "modified" });
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                LogEntity logEntity = new LogEntity
                {
                    Type = ex.GetType().Name,
                    Message = ex.Message,
                    Details = ex.ToString(),
                    LogTime = DateTime.UtcNow
                };
                mongoRepository.InsertOneAsync(COLLECTION_NAME, logEntity).Wait();
                Console.WriteLine(ex.Message);
            }

        }
    }

    class LogEntity : MongoEntity
    {
        [BsonElement("type")]
        public string Type { get; set; }
        [BsonElement("message")]
        public string Message { get; set; }
        [BsonElement("details")]
        public string Details { get; set; }
        [BsonElement("logTime")]
        public DateTime LogTime { get; set; }
    }
}
