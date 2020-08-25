using System;

namespace Skyline.Mongo
{
    public class MongoConfig : IMongoConfig
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
