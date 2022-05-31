using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace BackendCode.Service.utils
{
    public class PapersWithCodeCrawler
    {
        public async void crawlGithub(string title)
        {
            var response = await new HttpClient().GetStringAsync("https://paperswithcode.com/api/v1/search/?q=" + title);
            Console.Out.WriteLine(response);
            JObject jo = (JObject)JsonConvert.DeserializeObject(response);
            foreach (var item in jo)
            {

                Console.Out.WriteLine(item.Key);
            }
            //githubhref = " ";
        }
    }
}
