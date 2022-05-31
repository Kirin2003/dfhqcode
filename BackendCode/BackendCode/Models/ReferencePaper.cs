using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Models
{
    public class ReferencePaper
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Author { get; set; }
        public string PaperHref { get; set; }

        public override string ToString()
        {
            return "reference paper " + Title;
        }
    }
}
