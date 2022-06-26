using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Base;
using BackendCode.Models;
using Nest;

namespace BackendCode.Services
{
    public class CitationService : EsBase<Citation>
    {

        private RawPaperService paperService;
        public CitationService()
        {
            paperService = new RawPaperService();
        }

        // 根据id查询被引文章,先在paper表中查找doi，再查citation
        public List<Citation> GetCitations(string id, int page, int pageSize)
        {
            var client = _esConfig.GetClient(IndexName);
            var paper = paperService.QueryDocById(id); // 查找论文

            var result = client.Search<Citation>(x => x.
            From(page)
            .Size(pageSize)
            .Query(q => q
               .Bool(m => m
               .Must(
                    bm => bm.MatchPhrase(m => m.Field(f => f.PaperId).Query(paper.Id))))));


            return result.Documents.ToList<Citation>();
        }


        public override string IndexName => "citation";

        // 根据id查询被引文章,先在paper表中查找doi，再查citation
        public List<Citation> GetReferences(string id,int page, int pageSize )
        {
            var client = _esConfig.GetClient(IndexName);
            var paper = paperService.QueryDocById(id);
            string doi = paper.Doi;
            var result = client.Search<Citation>(x => x.
            From(page)
            .Size(pageSize)
            .Query(q => q
               .Bool(m => m
               .Must(
                    bm => bm.MatchPhrase(m => m.Field(f => f.CiteDOI).Query(doi))))));

                   
            return result.Documents.ToList<Citation>();
        }


    }
}
