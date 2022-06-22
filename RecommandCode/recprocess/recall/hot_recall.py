# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: hot_recall
@time: 2022-05-29 15:42
"""
from conf.dao_config import DynamicInfo_index_name
from dao.elastic_server import ElasticServer


class HotRecall(object):
    def __init__(self):
        self.es = ElasticServer().elastic_client
        self.index_name = DynamicInfo_index_name

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
            },
            "sort": [
                {
                    "HotNum": {
                        "order": "desc"
                    }
                }
            ]
        }
        filter_path = ["hits.hits._source.Id"]
        # 初始化推荐列表
        user_rec = dict()
        for cate_id, cate_name in cate_dict.items():
            user_rec[cate_name] = dict()

        for cate_id, cate_name in cate_dict.items():
            body["query"]["term"]["subject"] = cate_name
            query = self.es.search(index=self.index_name, filter_path=filter_path, body=body)
            results = query["hits"]["hits"]  # es查询结果的第一页
            total = query["hits"]["total"]  # es 查询出的结果总量
            scroll_id = query["_scroll_id"]  # 游标用于输出es查询的所有结果
            for i in range(0, int(total/100)+1):
                query_scroll = self.es.scroll(scroll_id=scroll_id, scroll="5m")["hits"]["hits"]
                user_rec[cate_name][results["_id"]] = results["_source"]["HotNum"]
                results = query_scroll
            user_rec[cate_name][results["_id"]] = results["_source"]["HotNum"]

        return user_rec
