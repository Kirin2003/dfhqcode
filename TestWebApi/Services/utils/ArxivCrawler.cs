using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using BackendCode.Models;
using System.Xml;
using BackendCode.Services;

using System.Xml.Linq;

namespace BackendCode.Services.utils
{
    public class ArxivCrawler   
    {        
        // 爬取论文主题
        public  void crawlSubject(RawPaperItem paper)
        {
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(paper.ArxivHref);
                HtmlNode htmlNode = doc.DocumentNode.SelectSingleNode("//span[@class=\"primary-subject\"]");
                if(htmlNode == null)
                {
                    return;
                }
                string subject = htmlNode.InnerText;
                Console.WriteLine("subject:" + subject);

                paper.Subject = subject;
            } catch(Exception e)
            {
                Console.WriteLine("网络故障");
            }
       
        }
    }
}
