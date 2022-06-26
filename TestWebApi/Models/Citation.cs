using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    [ElasticsearchType(RelationName = "citation")]
    public class Citation
    {
     
        [Keyword(Name="id")]
        public string Id { get; set; }//引用关系id
        [Keyword(Name = "paper_id")]
        public string PaperId { get; set; } // 原论文id，是原论文的唯一标识
        [Keyword(Name = "cite_doi")]
        public string CiteDOI { get; set; } // 引用论文doi，是引用论文的唯一标识
        [Text(Name="title")]
        public string Title { get; set; } // 引用论文标题
        [Text(Name="authors")]
        public string Authors { get; set; } // 引用论文作者
        [Keyword(Name="url")]
        public string Url { get; set; } // 引用论文url

        
    }
}
