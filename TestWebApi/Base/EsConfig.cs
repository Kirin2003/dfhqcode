using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Nest;

namespace BackendCode.Base
{
    // 获得es连接
    public class EsConfig
    {
        public  IElasticClient GetClient(string indexName)
        {
            var node = new Uri("http://localhost:9200");
            var settings = new ConnectionSettings(node);
            settings.DefaultIndex(indexName);
            return new ElasticClient(settings);
        }
    }
}
