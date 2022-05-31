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
        // public string Id{get;set;}
        public string Title { get; set; }
        public string Authors { get; set; }
        public string PaperAbstract { get; set; }

        public string PaperDate { get; set; }

        public string OriginalHref { get; set; }
        public string PdfHref { get; set; }
        public string Meeting { get; set; }


        public string SuppHref { get; set; }
        public string Bibtex { get; set; }
        public string VideoHref { get; set; }
        public string ArxivHref { get; set; }
        public string Subject { get; set; }

        public int Citation { get; set; }
        public int Collections { get; set; } = 0;
        public int ReadNum { get; set; } = 0;
        public int HotNum { get; set; } = 0;




        public override string ToString()
        {
            return "PaperItem: " + Title;
        }

    }
}
