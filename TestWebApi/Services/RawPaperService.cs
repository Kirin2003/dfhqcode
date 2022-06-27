using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Base;
using BackendCode.Models;
using Nest;

namespace BackendCode.Services
{
    public class RawPaperService : EsBase<RawPaperItem>
    {
    

        public override string IndexName => "paper";

        // 分页查询所有
        public List<RawPaperItem> QueryAll(int page, int pageSize)
        {
            var client = _esConfig.GetClient(IndexName);
            var searchResponse = client.Search<RawPaperItem>(s => s
                .Query(q => q
                .MatchAll()
                )
                .From(page)
                .Size(pageSize)
            );
            return searchResponse.Documents.ToList<RawPaperItem>();
        }


        

        // 多条件查询
        public List<RawPaperItem> SearchByMeetingAndKeyword(string meeting, string keyword,int page, int pageSize)
        {
            var client = _esConfig.GetClient(IndexName);

            // 在某个顶会（精确查找）和热词（模糊查找）中查找 
            var result = client.Search<RawPaperItem>(x => x.
            From(page)
            .Size(pageSize)
            .Query(q => q
                .Bool(b => b
                .Must(
                    
                    bm => bm.MatchPhrase(m => m.Field(f => f.Meeting).Query(meeting)), 
                    bm => bm.MultiMatch(mp => mp
                         .Query(keyword)
                             .Fields(f => f.
                                   Fields(f1 => f1.Title, f2 => f2.PaperAbstract, f3 => f3.Authors)))))));




            return result.Documents.ToList<RawPaperItem>();
                          
        }

        // 按关键词查找，模糊搜索
        public List<RawPaperItem> SearchByKeywords(string keywords, int page, int pageSize)
        {
            var client = _esConfig.GetClient(IndexName);

            // 在某个顶会（精确查找）和热词（模糊查找）中查找 
            var result = client.Search<RawPaperItem>(x => x.
            From(page)
            .Size(pageSize)
            .Query(q => q
                .Bool(b => b
                .Must(

                    bm => bm.MultiMatch(mp => mp
                         .Query(keywords)
                             .Fields(f => f.
                                   Fields(f1 => f1.Title, f2 => f2.PaperAbstract, f3 => f3.Authors)))))));

            return result.Documents.ToList<RawPaperItem>();
        }



    }
}
