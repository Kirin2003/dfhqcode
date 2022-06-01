using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using BackendCode.Models;
using System.Text;

namespace BackendCode.Service.utils
{
    public class SemanticScholarCrawler
    {
        
        public static async Task crawlReferenceAsync(string doi)
        {
            string searchapi = "http://api.semanticscholar.org/graph/v1/paper/search?query=" + doi;

            Console.Out.WriteLine(searchapi);
            HttpClient client = new HttpClient();
            string responseBody = await client.GetStringAsync(searchapi);
            JObject jo = JObject.Parse(responseBody);

             string paperId  = (string)jo["paperId"];
            
            string getReferenceApi = "https://api.semanticscholar.org/graph/v1/paper/" + paperId + "/references?fields=title,authors,url";

            string responseBody2 = await client.GetStringAsync(getReferenceApi);
            JObject jo2 = JObject.Parse(responseBody2);
            var referencePapers = jo2["data"];
            foreach(var item in referencePapers)
            {
                ReferencePaper r = new ReferencePaper();
                r.PaperHref = (string)item["citedPaper"]["url"];
                r.Title = (string)item["citedPaper"]["title"];
                StringBuilder sb = new StringBuilder();
                foreach(var author in item["citedPaper"]["authors"])

                {
                    sb.Append(author["name"]+" ");
                }
                r.Authors = sb.ToString();

                Console.Out.WriteLine(r.Authors);
            }


        }

        public static async Task crawlCitationAsync(string doi)
        {
            string searchapi = "http://api.semanticscholar.org/graph/v1/paper/search?query=" + doi;

            HttpClient client = new HttpClient();
            string responseBody = await client.GetStringAsync(searchapi);
            Console.Out.WriteLine(searchapi);
            JObject jo = JObject.Parse(responseBody);

            string paperId = (string)jo["paperId"];
            
            string getReferenceApi = "https://api.semanticscholar.org/graph/v1/paper/" + paperId + "/citations?fields=title,authors,url";

            string responseBody2 = await client.GetStringAsync(getReferenceApi);
            JObject jo2 = JObject.Parse(responseBody2);
            var referencePapers = jo2["data"];
            foreach (var item in referencePapers)
            {
                CitationPaper r = new CitationPaper();
                Console.Out.WriteLine(item);
                r.PaperHref = (string)item["citingPaper"]["url"];
                r.Title = (string)item["citingPaper"]["title"];
                StringBuilder sb = new StringBuilder();
                foreach (var author in item["citingPaper"]["authors"])

                {
                    sb.Append(author["name"] + " ");
                }
                r.Authors = sb.ToString();
                Console.Out.WriteLine(r.Authors);


            }


        }

    }
}
