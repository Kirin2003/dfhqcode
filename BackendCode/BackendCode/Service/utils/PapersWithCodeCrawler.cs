using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using BackendCode.Models;

namespace BackendCode.Service.utils
{
    public class PapersWithCodeCrawler
    {
        public static async Task crawlGithub(string title, string doi)
        {
            var response = await new HttpClient().GetStringAsync("https://paperswithcode.com/api/v1/search/?q=" + title);
            
            JObject jo = JObject.Parse(response);
            var item = jo["results"][0]["repository"];
            if(item != null)
            {
                GithubRepository github = new GithubRepository();
                github.url = (string)item["url"];
                github.name = (string)item["name"];
                github.doi = doi;
                Console.Out.WriteLine(github);

            }
            
        }
    }
}
