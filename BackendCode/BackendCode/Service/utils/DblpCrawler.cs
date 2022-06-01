using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackendCode.Service.utils
{
    public class DblpCrawler
    {
        public static void crawlBibtex(string title, out string doi, out string biburl)
        {
            XElement srcTree = XElement.Load("https://dblp.org/search/publ/api?q=" + title + "&h=1");
            doi = (string)(from el in srcTree.Descendants("doi") select el).First();
            biburl = (string)(from el in srcTree.Descendants("url") select el).First();
        }
    }
}
