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
using System.Xml.Linq;
using System.Threading;
using Quartz;

namespace BackendCode.Services.utils
{
    
    public class SemanticScholarCrawler
    {


        // 爬取引用论文信息
        public async Task crawlReferenceAsync(string id, string title)
        {
            try {
                // 第一步：调用模糊搜索api，由标题得到paperId
                string searchapi = "http://api.semanticscholar.org/graph/v1/paper/search?query=" + title;

                Console.Out.WriteLine("引用论文链接-----------------:" + searchapi);
                HttpClient client = new HttpClient();
                string responseBody = await client.GetStringAsync(searchapi);
                Console.WriteLine(responseBody);
                JObject jo = JObject.Parse(responseBody);
                JArray ja = (JArray)jo["data"];
                if (ja.Count == 0) return;
                string paperId = (string)ja[0]["paperId"];
                if (paperId == null)
                {
                    // 没有收录该论文
                    return;
                }

                // 第二步，得到引用论文列表
                string getReferenceApi = "https://api.semanticscholar.org/graph/v1/paper/" + paperId + "/references?fields=title,authors,url";
                Console.WriteLine("reference api-----------------------:" + getReferenceApi);

                string responseBody2 = await client.GetStringAsync(getReferenceApi);
                JObject jo2 = JObject.Parse(responseBody2);
                var referencePapers = jo2["data"];
                foreach (var item in referencePapers)
                {
                    Citation c = new Citation();
                    c.Id = Guid.NewGuid().ToString();
                    c.PaperId = id;
                    c.Url = (string)item["citedPaper"]["url"];
                    c.Title = (string)item["citedPaper"]["title"];
                    StringBuilder sb = new StringBuilder();
                    foreach (var author in item["citedPaper"]["authors"])

                    {
                        sb.Append(author["name"] + " ");
                    }
                    c.Authors = sb.ToString();

                    Console.Out.WriteLine(c.Authors);

                    // 得到引用论文的doi
                    XElement srcTree = XElement.Load("https://dblp.org/search/publ/api?q=" + c.Title + "&h=1");
                    string citedoi = (string)(from el in srcTree.Descendants("doi") select el).First();

                    c.CiteDOI = citedoi;

                    // 加入Citation索引

                    CitationService dbService = new CitationService();
                    Console.WriteLine(c);
                    dbService.CreateDocument<Citation>(c, c.Id);
                }

            } catch(Exception e)
            {
                Console.WriteLine("网络故障3：---------------" + "http://api.semanticscholar.org/graph/v1/paper/search?query=" + title);
            }
            }

    }
}
