using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Mongo
{
    public abstract class MongoEntity
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
    }
}
