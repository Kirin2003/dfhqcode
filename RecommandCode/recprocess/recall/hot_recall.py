# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: hot_recall
@time: 2022-05-29 15:42
"""
from conf.dao_config import paper_portrait_index_name, paper_portrait_doc_type
from dao.elastic_server import ElasticServer


class HotRecall(object):
    def __init__(self, redis):
        self.es = ElasticServer().elastic_client
        self.index_name = paper_portrait_index_name
        self.doc_type = paper_portrait_doc_type
        self.rec_list_redis = redis

    def group_cate_for_paper_list_to_redis(self, cate_dict):
        """
        按照类别将论文推荐列表存入redis中，并自动根据hot_value排序.
        :param: cate_dict
        :return:
        """
        body = {
            "query": {
                "term": {
                    "subject": ""
                }
            }
        }
        filter_path = ["hits.hits._source.paper_id",
                       "hits.hits._source.hot_value"]
        for cate_id, cate_name in cate_dict.items():
            redis_key = "hot_list_paper_cate:{}".format(cate_id)
            body["query"]["term"]["subject"] = cate_name
            query = self.es.search(index=self.index_name, doc_type=self.doc_type,
                                    filter_path=filter_path, body=body)
            results = query["hits"]["hits"]  # es查询结果的第一页
            total = query["hits"]["total"]  # es 查询出的结果总量
            scroll_id = query["_scroll_id"]  # 游标用于输出es查询的所有结果
            for i in range(0, int(total/100)+1):
                query_scroll = self.es.scroll(scroll_id=scroll_id, scroll="5m")["hits"]["hits"]
                self.rec_list_redis.zadd(redis_key, {"{}_{}".format(cate_name, results["_id"]): results["_source"]["hot_value"]})
                results = query_scroll
            self.rec_list_redis.zadd(redis_key, {"{}_{}".format(cate_name, results["_id"]): results["_source"]["hot_value"]})
