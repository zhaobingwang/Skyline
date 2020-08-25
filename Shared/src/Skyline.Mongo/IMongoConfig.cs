using System;
using System.Collections.Generic;
using System.Text;

namespace Skyline.Mongo
{
    public interface IMongoConfig
    {
        string ConnectionString { get; set; }
        string Database { get; set; }
    }
}
