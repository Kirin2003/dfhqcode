using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using BackendCode.Models;
using Nest;
using NPOI.XSSF;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Threading;
using System.Xml;


namespace BackendCode.Services.utils
{
    public class CvfCrawler //爬取三大顶会的论文的列表
    {
        private RawPaperService dbService;
        public List<RawPaperItem> papers = new List<RawPaperItem>();
        public List<GithubRepository> githubs = new List<GithubRepository>();
        public List<Citation> citations = new List<Citation>();
        public List<DynamicInfo> dynamicInfos = new List<DynamicInfo>();
        public CvfCrawler()
        {
            this.dbService = new RawPaperService();
           
        }

        Uri baseuri = new Uri("https://openaccess.thecvf.com/");//三大顶会论文下载的官方网站的url

        public void crawl()
        {
            List<string> meetingLinks = crawlMeeting();
            Dictionary<string, string> papersList = new Dictionary<string, string>();
            foreach (string meetingLink in meetingLinks)
            {
                string meeting = meetingLink.Substring(30, 4);
                string year = meetingLink.Substring(34, 4);
                Console.WriteLine(year);

                // WACV 和 年份在2017之前的没有日期页
                if (meeting != "WACV" && Int32.Parse(year) > 2017)
                {
                    crawlDate(meetingLink, ref papersList);

                } else
                {

                    papersList.Add(meetingLink, meetingLink.Substring(30, 8) + "-01-01");
                }
            }

            Dictionary<string, string> papers = new Dictionary<string, string>();
            foreach (string papersLink in papersList.Keys)
            {
                crawlPapersList(papersLink, papersList[papersLink], ref papers);
            }

            // 写入xml
            using (XmlTextWriter writer = new XmlTextWriter("papers.xml", null))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("papers");
                foreach (string link in papers.Keys)
                {
                    writer.WriteStartElement("paper");
                    writer.WriteElementString("link", link);
                    writer.WriteElementString("meetingdate", papers[link]);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }


        }

        public void parse()
        {
            // 读取xml
            Dictionary<string, string> papers = new Dictionary<string, string>();
            bool isLink = true;
            string link = "";
            using (XmlReader reader = new XmlTextReader(@"D:\SoftwareConstruction\BackendCode\BackendCode\papers.xml"))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.HasAttributes)
                            {
                                reader.MoveToFirstAttribute();
                                do
                                {
                                    Console.WriteLine(reader.Name + ":" + reader.Value);
                                } while (reader.MoveToNextAttribute());
                            }
                            break;
                        case XmlNodeType.Text:
                            if (isLink) {
                                papers.Add(reader.Value, "");
                                link = reader.Value;
                                isLink = false;
                            } else
                            {
                                papers[link] = reader.Value;
                                isLink = true;
                            }
                            break;
                    }
                }
             
            }

            // 爬论文信息
            int i = 0;
            foreach (string paperlink in papers.Keys)
            {
                Console.WriteLine("start parse paper");
                Console.WriteLine("---------i=" + (i++));
                Console.WriteLine("parse paper");
                parsePaper(paperlink, papers[paperlink]);


            }

        }

        // 爬取会议页
        public List<string> crawlMeeting()
        {
           
            List<string> meetingLinks = new List<string>();
            var web = new HtmlWeb();
            var doc = web.Load(baseuri.ToString());
            HtmlNodeCollection htmlNode = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd");
            foreach (HtmlNode htmlNode1 in htmlNode)
            {
               
                string meetingName = htmlNode1.InnerText.Substring(1, 4);

                string relativeuri = htmlNode1.SelectSingleNode("./a[1]").Attributes["href"].Value;
                Uri absoluteuri = new Uri(baseuri, relativeuri);
                string href1 = absoluteuri.ToString();
                meetingLinks.Add(href1);
                Console.WriteLine("meeting:" + meetingName);
            }
            return meetingLinks;
        }

        // 爬取日期页
        public void crawlDate(string meetingLink, ref Dictionary<string, string> papersList)
        {

            string meetingName = meetingLink.Substring(30, 4);
            HtmlDocument doc = new HtmlWeb().Load(meetingLink);
            HtmlNodeCollection dateNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd/a");

            foreach (var dateNode in dateNodes)
            {
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
                string meetingAndDate = meetingName + date;
                papersList.Add(dateHref, meetingAndDate);
            }


        }


        // 爬取论文浏览页
        public void crawlPapersList(string link, string meetingAndDate, ref Dictionary<string, string> papers)
        {
            HtmlDocument doc;
            try
            {
                doc = new HtmlWeb().Load(link);

                Console.WriteLine("link:" + link);
                HtmlNodeCollection paperNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dt/a");

                foreach (var paperNode in paperNodes)
                {
                    Uri absolute = new Uri(baseuri, paperNode.Attributes["href"].Value);

                    string paperHref = absolute.ToString();
                    papers.Add(paperHref, meetingAndDate);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("爬取论文列表：link:" + link);
                Console.WriteLine("网络故障1");

            }

        }

        // 论文详情页，解析论文信息
        public async void parsePaper(string paperHref, string meetingAndDate)
        {
            try {
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
                        // 视频
                        videohref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();
                    }
                    else if (node.InnerText.Equals("arXiv"))
                    {
                        // arxiv链接
                        arxivhref = (new Uri(baseuri, node.Attributes["href"].Value)).ToString();

                    }
                }

               

                // 将爬取的值赋给rawPaperItem, VideoHref,ArxivHref,SuppHref是可选项，没有就被赋以初值：""
                rawPaperItem.Id = Guid.NewGuid().ToString();//唯一标识
                rawPaperItem.Meeting = meetingAndDate.Substring(0, 4);
                string year = meetingAndDate.Substring(4, 4);
                string month = meetingAndDate.Substring(9, 2);
                string date = meetingAndDate.Substring(12, 2);
                rawPaperItem.PaperDate = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(date));
                rawPaperItem.Title = title;
                rawPaperItem.Authors = authors;
                rawPaperItem.PdfHref = pdfhref;
                rawPaperItem.ArxivHref = arxivhref;
                rawPaperItem.SuppHref = supphref;
                rawPaperItem.Subject = subject;
                rawPaperItem.PaperAbstract = paperAbstract;
                rawPaperItem.VideoHref = videohref;
                rawPaperItem.Doi = "";
                rawPaperItem.Bibtex = "";
                rawPaperItem.PaperKeywords = new List<string>();
                Console.WriteLine(rawPaperItem);
                Console.WriteLine("----------------" + pdfhref);
                if (arxivhref != null)
                {
                    ArxivCrawler a = new ArxivCrawler();
                    a.crawlSubject(rawPaperItem);
                }
                // 调用分词，得到论文的关键词
                RecommendService r = new RecommendService();
                r.getKeywords(rawPaperItem);

                // 得到doi, bibtex
                DblpCrawler d = new DblpCrawler();
                d.crawlBibtex(rawPaperItem);

                papers.Add(rawPaperItem);
                Console.WriteLine(rawPaperItem);
                // 存入论文数据库
                dbService.CreateDocument(rawPaperItem, rawPaperItem.Id);



                // 初始化论文的动态信息，存放到DynamicInfo表中
                Task addDynInfo = new Task(() =>
                {
                    DynamicInfo dynInfo = new DynamicInfo() { Id = rawPaperItem.Id, CollectionNum = 0, HotNum = 0, LikeNum = 0, ReadNum = 0 };
                    new DynamicInfoService().CreateDocument(dynInfo, dynInfo.Id);
                    Console.WriteLine(dynInfo);

                    
                }
                );

                addDynInfo.Start();
                await addDynInfo;




                // 爬取论文的github，存放到github索引中，注意有些论文没有公开代码
                Task addGithub = new Task(() =>
                {
                    PapersWithCodeCrawler p = new PapersWithCodeCrawler();
                    p.crawlGithub(rawPaperItem.Id, rawPaperItem.Title);

                });
                addGithub.Start();
                await addGithub;



                // 爬取论文的引用论文，存放到citation索引中

                Task addCitation = new Task(async () =>
                {
                    SemanticScholarCrawler s = new SemanticScholarCrawler();
                    await s.crawlReferenceAsync(rawPaperItem.Id, rawPaperItem.Title);
                });
                addCitation.Start();
                await addCitation;

            }  catch (Exception e )
                {
                    Console.WriteLine("爬取原始论文：-----------");
                    Console.WriteLine("网络故障2");

                }
}
    }
}
