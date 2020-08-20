using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skyline.Elasticsearch.Sample.WebApp
{
    //public interface IExceptionLogContext<T> : IESContext<ExceptionLogModel>
    //{

    //}
    public class ExceptionLogContext : ESContext<ExceptionLogModel>
    {
        public override string IndexName => "log.exception";
        public ExceptionLogContext(IESClientProvider esClientProvider) : base(esClientProvider)
        {

        }
    }
}
