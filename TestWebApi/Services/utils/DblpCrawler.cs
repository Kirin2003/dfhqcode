using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BackendCode.Models;


namespace BackendCode.Services.utils
{
    
    public class DblpCrawler
    {
        // 爬取bibtex信息
        public void crawlBibtex(RawPaperItem paper)
        {
            try {
                XElement srcTree = XElement.Load("https://dblp.org/search/publ/api?q=" + paper.Title + "&h=1");
                Console.WriteLine("搜索api:" + "https://dblp.org/search/publ/api?q=" + paper.Title + "&h=1");
                string doi = "";
                string biburl = "";
                if (srcTree.Descendants("doi").Any())
                {
                    doi = (string)(from el in srcTree.Descendants("doi") select el).First();
                }
                if (srcTree.Descendants("url").Any())
                {
                    biburl = (string)(from el in srcTree.Descendants("url") select el).First();
                }


                Console.WriteLine("doi:" + doi);
                paper.Doi = doi;
                paper.Bibtex = biburl;

            } catch(Exception e )
            {
                Console.WriteLine("网络故障6-------------" + "https://dblp.org/search/publ/api?q=" + paper.Title + "&h=1");
            }
            }
    }
}
