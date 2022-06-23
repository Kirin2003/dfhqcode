# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: paper_portrait
@time: 2022-05-26 20:18
"""
from dao.elastic_server import ElasticServer
from conf.dao_config import paper_index_name
import pandas as pd


class PaperPortraitServer(object):
    def __init__(self):
        """
        初始化
        """
        self.es = ElasticServer().elastic_client
        self.index_name = paper_index_name

    # noinspection PyMethodMayBeStatic
    def paper_info_to_dataframe(self):
        """
        查询所有的论文，转换为DataFrame格式
        :return:
        """
        body = {
            "query": {
                "match_all": {}
            }
        }
        query = ElasticServer().elastic_client.search(index=paper_index_name, body=body)["hits"]

        paper_info = pd.DataFrame(columns=("paper_id", "key_words"))

        result = query["hits"]
        total = query["total"]["value"]

        for i in range(0, total):
            paper_info.at[len(paper_info.index)] = [result[i]["_id"], result[i]["_source"]["paperKeywords"]]
        paper_info.set_index("paper_id", inplace=True)
        return paper_info

    def paper_to_subject(self, paper_id):
        """
        根据论文ID查找论文主题
        :param paper_id:
        :return:
        """
        query = self.es.get(index=self.index_name, id=paper_id)
        return query["_source"]["subject"]
