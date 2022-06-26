using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using BackendCode.Models;
using BackendCode.Services;
using System.Threading;

namespace BackendCode.Services.utils
{
    
    public class PapersWithCodeCrawler
    {
        // 爬取github信息
        public async Task crawlGithub(string id, string title)
        {
            try {
                var response = await new HttpClient().GetStringAsync("https://paperswithcode.com/api/v1/search/?q=" + title);
                JObject jo = JObject.Parse(response);
                var item = jo["results"][0]["repository"];


                if (item != null)
                {
                    GithubRepository github = new GithubRepository();
                    github.url = (string)item["url"];
                    github.name = (string)item["name"];
                    Console.Out.WriteLine(github);
                    GithubService dbService = new GithubService();
                    Console.WriteLine("----------" + github.url + "  " + github.name);
                    dbService.CreateDocument<GithubRepository>(github, id);

                }

            }catch(Exception e)
            {
                Console.WriteLine("网络故障5：-----------------" + "https://paperswithcode.com/api/v1/search/?q=" + title);
            }
            }
    }
}
