using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    [ElasticsearchType(RelationName = "dynamic_info")]
    public class DynamicInfo
    {
        [Keyword(Name="id")]
        public string Id { get; set; }//论文id, 主键
        
        public int CollectionNum { get; set; } = 0; // 收藏数，初始为0
        public int ReadNum { get; set; } = 0; // 阅读数，初始为0
        public Double HotNum { get; set; } = 0; // 热度值，创建论文对象时使用热度值公式计算
       
        public int LikeNum { get; set; } = 0; // 喜欢数，初始为0
        [Keyword(Name="subject")]
        public string Subject { get; set; }

        
    }
}
