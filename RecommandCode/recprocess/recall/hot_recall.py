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

    def group_cate_for_paper_list(self, cate_dict):
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
                    "hot_num": {
                        "order": "desc"
                    }
                }
            ]
        }
        # 初始化推荐列表
        user_rec = dict()
        for cate_id, cate_name in cate_dict.items():
            user_rec[cate_name] = dict()

        for cate_id, cate_name in cate_dict.items():
            body["query"]["term"]["subject"] = cate_name
            query = self.es.search(index=self.index_name, body=body)["hits"]
            total = query["total"]["value"]
            result = query["hits"]
            for i in range(0, total):
                user_rec[cate_name][result[i]["_id"]] = result[i]["_source"]["hot_num"]

        return user_rec
