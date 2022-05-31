using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Service.utils
{
    public class SemanticScholarCrawler
    {
        public void crawlReference(string title)
        {
            string searchhref = "https://www.semanticscholar.org/search?q=" + title + "&sort=relevance";


            var doc = new HtmlWeb().Load(searchhref);
            var node = doc.DocumentNode;


            //string detailhref = node.SelectSingleNode("//div[@class='result-page']/div[1]/a[1]").Attributes["href"].ToString();

            Uri baseuri = new Uri("https://www.semanticscholar.org");
            var nodetest = node.SelectSingleNode("//*[@id='main-content']/div[1]").InnerHtml;

            Console.Out.WriteLine(nodetest);

            //  string detailhref = new Uri(baseuri,node.SelectSingleNode("//div[@class='col-lg-3 item-interact text-center']/div[@class='entity']/a[1]").Attributes["href"].Value).ToString();


            // Console.Out.WriteLine("detail: "+detailhref);
            // var doc2 = new HtmlWeb().Load(detailhref);
            // var node2=  doc.DocumentNode;



        }

    }
}
