using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    public class CitationPaper
    {
        public string Title { get; set; }

        public string Authors { get; set; }
        public string PaperHref { get; set; }

        public override string ToString()
        {
            return "reference paper " + Title;
        }
    }
}
