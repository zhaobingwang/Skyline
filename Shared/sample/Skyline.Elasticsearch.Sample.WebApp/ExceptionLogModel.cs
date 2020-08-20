using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Elasticsearch.Sample.WebApp
{
    public class ExceptionLogModel
    {
        public string Code { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionDetails { get; set; }
        public DateTime LogTime { get; set; }
    }
}
