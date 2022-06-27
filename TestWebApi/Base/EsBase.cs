using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace BackendCode.Base
{
    public abstract class EsBase<T> where T : class
    {
        protected readonly EsConfig _esConfig;
        public abstract string IndexName { get; }

       public EsBase()
        {
            _esConfig = new EsConfig();
            
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public bool InsertMany(List<T> tList)
        {
            var client = _esConfig.GetClient(IndexName);
            
            var response = client.IndexMany<T>(tList);
            return response.IsValid;
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <returns></returns>
        //public bool CreateIndex()
        //{
        //    var client = EsConfig.GetClient(IndexName);

        //    if (!client.Indices.Exists(IndexName).Exists)
        //    {
        //        var flag = client.CreateIndex<T>(IndexName);
        //        return flag;
        //    }

        //    return false;
        //}

        /// <summary>
        /// 创建单个文档
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CreateDocument<TEntity>(TEntity entity, string id) where TEntity : class
        {
            Console.WriteLine("------------加入数据库");
            var client = _esConfig.GetClient(IndexName);

            //if (!client.Indices.Exists(IndexName).Exists)
            //{
            //    client.CreateIndex<T>(IndexName);
            //}

            var response = client.Create<TEntity>(entity, t => t.Index(IndexName).Id(id));
            return response.IsValid;
        }

        /// <summary>
        /// 获取索引文档总数量
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            var client = _esConfig.GetClient(IndexName);
            var search = new SearchDescriptor<T>().MatchAll();
            var response = client.Search<T>(search);
            return response.Total;
        }

        /// <summary>
        /// 根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteById(string id)
        {
            var client = _esConfig.GetClient(IndexName);
            var response = client.Delete<T>(id);
            return response.IsValid;
        }

        /// <summary>
        /// 根据ID更新文档
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateDocument<TEntity>(TEntity entity, string id) where TEntity : class
        {
            var client = _esConfig.GetClient(IndexName);
            var response = client.Update<TEntity>(id, t => t.Doc(entity));
            return response.IsValid;
        }

        // 按id查询文档
        public T QueryDocById(string id)
        {
            var client = _esConfig.GetClient(IndexName);
            var result = client.Get<T>(id, idx => idx.Index(IndexName));
            return result.Source;
        }

        public List<T> QueryAll ()
        {
            var client = _esConfig.GetClient(IndexName);
            var searchResponse = client.Search<T>(s => s
                .Query(q => q
                .MatchAll()
                ));
            return searchResponse.Documents.ToList<T>();
            
        }

    }
}
