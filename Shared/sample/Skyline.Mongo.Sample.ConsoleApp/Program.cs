using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Skyline.Mongo.Sample.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoConfig config = new MongoConfig
            {
                ConnectionString = "mongodb://127.0.0.1:27017",
                Database = "logs"
            };

            MongoRepository mongoRepository = new MongoRepository(config);
            try
            {
                int i = 0;
                int j = 10 / i;
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
                mongoRepository.InsertOneAsync("log.exceptions", logEntity).Wait();
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
