using Nest;

namespace PaperCrawler;
public class ESHelper{
    private ElasticClient paperClient;
    private ElasticClient relatedPaperClient;
    private ElasticClient githubClient;
    
    public ESHelper() {
        
        Uri uri = new Uri("http://localhost:9200");
        var paperSettings = new ConnectionSettings(uri).DefaultIndex("paper");
        var relatedPaperSettings = new ConnectionSettings(uri).DefaultIndex("relatedpaper");
        var githubSettings = new ConnectionSettings(uri).DefaultIndex("github");
        
        paperClient = new ElasticClient(paperSettings);
        relatedPaperClient = new ElasticClient(relatedPaperSettings);
        githubClient = new ElasticClient(githubSettings);

    }
    
    
    public void addPaperItem(PaperItem paperItem) {
        var indexResponse = paperClient.IndexDocument(paperItem);
    }

    public void addRelatedPaperItem(RelatedPaperItem relatedPaperItem) {

    }

    public void addGithubItem(GithubItem githubItem) {

    }

    public List<PaperItem> queryAll() {
        // TODO 分页查询
    }
    public PaperItem queryByTitle(string title) {
        var searchResponse = client.Search<PaperItem>(s => s
            .AllIndices()
            .From(0)
            .Size(1)
            .Query(q=>q
                .Match(m => m
                    .Field(f =>f.Title)
                    .Query(title))));
        return searchResponse.Documents.First<>;
    }

    public List<PaperItem> queryByAuthor(string author) {
        // TODO
    }

    public List<PaperItem> queryByYear(string year) {

    }

    public List<PaperItem> queryByMeeting(string meeting) {
        
    }

    public List<RelatedPaperItem> queryRelatedPapers(string title) {

    }

    public GithubItem queryGithub(string title) {
        
    }

    public List<PaperItem> queryByKeywords(string keyword) {
        
    }

    
}