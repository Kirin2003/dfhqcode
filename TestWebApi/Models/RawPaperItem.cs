using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace BackendCode.Models
{
    [ElasticsearchType(RelationName = "paper")]
    [Serializable]
    public class RawPaperItem
    {

        [Keyword(Name="id")]
        public string Id{get;set;} // 论文id，主键
        [Text(Name="title")]
        public string Title { get; set; }
        [Text(Name="Authors")]
        public string Authors { get; set; }
        [Text(Name="paperAbstract")]
        public string PaperAbstract { get; set; }

        [Keyword(Name="paperHref")]
        public string PdfHref { get; set; }
        [Keyword(Name="meeting")]
        public string Meeting { get; set; }

        
        [Keyword(Name="suppHref")]
        public string SuppHref { get; set; }//optional
        [Keyword(Name="bibtex")]
        public string Bibtex { get; set; }
        [Keyword(Name="videoHref")]
        public string VideoHref { get; set; }// optional
        [Keyword(Name="arxivHref")]
        public string ArxivHref { get; set; }//optional
        [Keyword(Name="subject")]
        public string Subject { get; set; } //主题，optional

        [Keyword(Name="doi")]
        public string Doi { get; set; }

        [Date(Format = "yyyy-MM-dd")]
        public DateTime PaperDate { get; set; } // 发表日期

        [Object]
        public List<string> PaperKeywords { get; set; }

       
    }
}
