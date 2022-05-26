using Nest;
namespace PaperCrawler;

[ElasticsearchType(RelationName="paper")]
public class PaperItem{
    
    public string Id;
    public string Title{get;set;} 
    public string Authors {get;set;}
    public string PaperAbstract {get;set;} 

    public string PaperDate {get;set;} 

    public string OriginalHref{get;set;} 
    public string PdfHref {get;set;} 
    public string Meeting {get;set;} 


    public string SuppHref {get;set;}


    public int Citation {get;set;}
    public int Collections {get;set;} = 0;
    public int ReadNum {get;set;} = 0;
    public int HotNum {get;set;} = 0;

    public PaperItem(string title, string authors, string paperAbstract,string paperDate, string originalHref,string pdfhref,string meeting,string suppHref) {
        Title = title;
        Authors = authors;
        PaperAbstract = paperAbstract;
        PaperDate = paperDate;
        OriginalHref = originalHref;
        PdfHref = pdfhref;
        Meeting = meeting;
        SuppHref = suppHref;
    }

    
}