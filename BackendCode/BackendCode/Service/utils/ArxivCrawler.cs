using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using BackendCode.Models;
using System.Xml;

using System.Xml.Linq;

namespace BackendCode.Service.utils
{
    public class ArxivCrawler
    {
        public static void crawlSubject(string title, out string subject)
        {
            //XmlDocument xd = new XmlDocument();
            //xd.Load("http://export.arxiv.org/api/query?search_query=ti:" + title + "&start=0&max_result=1");
            //subject = "";
            XElement srcTree = XElement.Load("http://export.arxiv.org/api/query?search_query=ti:" + title + "&start=0&max_result=1");
            Console.Out.Write(srcTree);
            var item = srcTree.Descendants("author").FirstOrDefault();
            //subject = (string)item.Attribute("href").Value;
            subject = "";
            if(item != null) Console.Out.Write(item.ToString());
            Console.Out.WriteLine(subject);


        }
    }
}
