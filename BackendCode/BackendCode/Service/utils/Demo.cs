using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Service.utils
{
    public class Demo
    {
        static string title = "Does Data Repair Lead to Fair Models? Curating Contextually Fair Data To Reduce Model Bias";
        public static async Task Main(String[] args)
        {
            //string doi = ""; string biburl = "";
            //DblpCrawler.crawlBibtex(title,out doi,out biburl);
            //Console.Out.WriteLine(doi + " " + biburl);
            //await SemanticScholarCrawler.crawlReferenceAsync(title);
            //await SemanticScholarCrawler.crawlCitationAsync(title);

            //await PapersWithCodeCrawler.crawlGithub("does data repair lead to fair models curating", "10.1109/WACV51458.2022.00395");
            string subject;
            ArxivCrawler.crawlSubject("Does Data Repair Lead to Fair Models? Curating Contextually Fair Data To Reduce Model Bias",out subject);

           

        }
    }
}
