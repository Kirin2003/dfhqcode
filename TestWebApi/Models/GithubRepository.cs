using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace BackendCode.Models
{
    [ElasticsearchType(RelationName = "github")]
    public class GithubRepository
    {
        [Keyword(Name="id")]
        public string Id { get; set; } // 论文id，是主键, （注意，有些论文没有公开代码，GithubRepository的Id的集合是RawPaperItem的Id集合的子集）

        [Keyword(Name="url")]
        public string url { get; set; }
        [Keyword(Name="name")]
        public string name { get; set; }

        
    }

     
}
