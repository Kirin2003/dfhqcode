using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using BackendCode.Models;

namespace BackendCode.Service.utils
{
    public class CvfCrawler
    {
        Uri baseuri = new Uri("https://openaccess.thecvf.com/");
        public void crawlMeeting(string cvfhref)
        {
            var web = new HtmlWeb();
            var doc = web.Load(baseuri.ToString());
            HtmlNodeCollection htmlNode = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd");
            foreach (HtmlNode htmlNode1 in htmlNode)
            {
                string meetingName = htmlNode1.InnerText.Substring(1, 4);
                //Console.Out.WriteLine("meeting:"+meetingName);
                string relativeuri = htmlNode1.SelectSingleNode("./a[1]").Attributes["href"].Value;
                Uri absoluteuri = new Uri(baseuri, relativeuri);
                string href1 = absoluteuri.ToString();

                //Console.Out.WriteLine("href:"+href1);
                if (meetingName != "WACV")
                {
                    crawlDate(href1, meetingName);
                }
                else
                {

                    string wacvdate = htmlNode1.InnerText.Substring(6, 4) + "01-01";
                    crawlPaperList(href1, meetingName, wacvdate);
                }
            }
        }

        //解析第二个界面，得到ICCV和CVPR的日期和对应日期的链接
        public void crawlDate(string href, string meetingName)
        {

            var doc = new HtmlWeb().Load(href);
            HtmlNodeCollection dateNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd/a");
            foreach (var dateNode in dateNodes)
            {
                // 
                if (dateNode.InnerText.Equals("All Papers"))
                {
                    continue;
                }
                Uri absolute = new Uri(baseuri, dateNode.Attributes["href"].Value);
                string dateHref = absolute.ToString();
                string str = dateNode.InnerText;
                string pattern = @"^Day\s\d:\s(\d{4})-(\d{1,2})-(\d{1,2})$";
                Match m = Regex.Match(str, pattern);
                string date = m.Groups[1] + "-" + m.Groups[2] + "-" + m.Groups[3];
                //Console.Out.WriteLine("dateHref:"+dateHref);
                //Console.Out.WriteLine("date:"+date);
                crawlPaperList(meetingName, date, dateHref);

            }

        }

        public void crawlPaperList(string meetingName, string date, string dateHref)
        {
            var doc = new HtmlWeb().Load(dateHref);
            HtmlNodeCollection paperNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dt/a");
            int num = 0;
            foreach (var paperNode in paperNodes)
            {
                Uri absolute = new Uri(baseuri, paperNode.Attributes["href"].Value);

                string paperHref = absolute.ToString();
                //Console.Out.WriteLine("paperHref:"+paperHref);
                parsePaper(meetingName, date, paperHref);
                if (num > 10) break;
                num++;
            }
        }

        public void parsePaper(string meetingName, string date, string paperHref)
        {
            var doc = new HtmlWeb().Load(paperHref);
            HtmlNode paper = doc.DocumentNode;
            string title = paper.SelectSingleNode("//*[@id='papertitle']").InnerText.TrimStart().TrimEnd();
            string authors = paper.SelectSingleNode("//*[@id='authors']/b/i").InnerText;
            string paperAbstract = paper.SelectSingleNode("//*[@id='abstract']").InnerText.TrimStart().TrimEnd();

            string pdfhref = "";
            string subject = "";
            string supphref = "";
            string videohref = "";
            string arxivhref = "";
            string bibtexhref = "";
            RawPaperItem rawPaperItem = new RawPaperItem();

            HtmlNodeCollection nodes = paper.SelectNodes("//*[@id='content']/dl/dd/a");
            foreach (HtmlNode node in nodes)
            {
                if (node.InnerText.Equals("pdf"))
                { // pdf链接
                    pdfhref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();
                }
                else if (node.InnerText.Equals("supp"))
                { // 补充信息
                    supphref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();

                }
                else if (node.InnerText.Equals("video"))
                {
                    videohref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();
                }
                else if (node.InnerText.Equals("arXiv"))
                {
                    arxivhref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();

                }
                else if (node.InnerText.Equals("bibtex"))
                {
                    bibtexhref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();

                }
            }


            rawPaperItem.Meeting = meetingName;
            rawPaperItem.PaperDate = date;
            rawPaperItem.Title = title;
            rawPaperItem.Authors = authors;
            rawPaperItem.PdfHref = pdfhref;
            rawPaperItem.ArxivHref = arxivhref;
            rawPaperItem.SuppHref = supphref;
            rawPaperItem.Bibtex = bibtexhref;
            rawPaperItem.PaperAbstract = paperAbstract;
            rawPaperItem.VideoHref = videohref;

            if (arxivhref != null)
            {
                // 提供arXiv

            }
           // papers.Add(rawPaperItem);

        }
    }
}
