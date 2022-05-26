using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace PaperCrawler;
public class Crawler
{
    private ESHelper es = new ESHelper();
    public static void Main(string[] args) {
        Crawler c = new Crawler();
        c.crawlMeeting();
    }
    // 解析第一个界面，得到各个顶会的会议名和链接
    public  void crawlMeeting() {
        var web = new HtmlWeb();
        var doc = web.Load("https://openaccess.thecvf.com/menu");
        HtmlNodeCollection htmlNode  = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd");
        foreach(HtmlNode htmlNode1 in htmlNode) {
            string meetingName = htmlNode1.InnerText.Substring(1,4);
            //Console.Out.WriteLine("meeting:"+meetingName);
            string href1 = "https://openaccess.thecvf.com/"+htmlNode1.SelectSingleNode("./a[1]").Attributes["href"].Value;
            //Console.Out.WriteLine("href:"+href1);
            if(meetingName != "WACV") {
                crawlDate(href1,meetingName);
            }
        }
    }

    //解析第二个界面，得到ICCV和CVPR的日期和对应日期的链接
    public void crawlDate(string href, string meetingName) {
        
        var doc = new HtmlWeb().Load(href);
        HtmlNodeCollection dateNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dd/a");
        foreach(var dateNode in dateNodes) {
            // 
            if(dateNode.InnerText.Equals("All Papers")) {
                continue;
            }
            string dateHref = "https://openaccess.thecvf.com"+dateNode.Attributes["href"].Value;
            string str = dateNode.InnerText;
            string pattern = @"^Day\s\d:\s(\d{4})-(\d{1,2})-(\d{1,2})$";
            Match m = Regex.Match(str,pattern);
            string date = m.Groups[1] + "-" + m.Groups[2] + "-" + m.Groups[3];
            //Console.Out.WriteLine("dateHref:"+dateHref);
            //Console.Out.WriteLine("date:"+date);
            crawlPaperList(meetingName, date, dateHref);

        }
        
    }

    public void crawlPaperList(string meetingName, string date,string dateHref) {
        var doc = new HtmlWeb().Load(dateHref);
        HtmlNodeCollection paperNodes = doc.DocumentNode.SelectNodes("//*[@id='content']/dl/dt/a");
        foreach(var paperNode in paperNodes) {
            string paperHref = "https://openaccess.thecvf.com"+paperNode.Attributes["href"].Value;
            //Console.Out.WriteLine("paperHref:"+paperHref);
            parsePaper( meetingName, date, paperHref);
        }
    }

    public void parsePaper(string meetingName, string date, string paperHref) {
        var doc = new HtmlWeb().Load(paperHref);
        HtmlNode paper = doc.DocumentNode;
        string title = paper.SelectSingleNode("//*[@id='papertitle']").InnerText.TrimStart().TrimEnd();
        string authors = paper.SelectSingleNode("//*[@id='authors']/b/i").InnerText;
        string paperAbstract = paper.SelectSingleNode("//*[@id='abstract']").InnerText.TrimStart().TrimEnd();

        string pdfhref = "https://openaccess.thecvf.com"+paper.SelectSingleNode("//*[@id='content']/dl/dd/a[1]").Attributes["href"].Value;

        //string arxiv = paper.SelectSingleNode("//*[@id='content']/dl/dd/a[3]").InnerText;
        //Console.Out.WriteLine("title:"+title);
        //Console.Out.WriteLine("authors:"+authors);
        //Console.Out.WriteLine("abstract:"+paperAbstract);
        //Console.Out.WriteLine("pdfHref:"+pdfhref);
        //Console.Out.WriteLine("arxiv:"+arxiv);
        PaperItem paper1 = new PaperItem( title,  authors,  paperAbstract, date,  paperHref, pdfhref, meetingName, suppHref) ;
        

    }

    public void crawlGithub(string title) {

    }

    public void crawlRelatedPaperList(string title) {
        
    }

    


}
