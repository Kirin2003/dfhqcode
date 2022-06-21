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
                "term": {
                    "new": 1
                }
            }
        }
        filter_path = ["hits.hits._source.key_words"]
        query = self.es.search(index=self.index_name, filter_path=filter_path, body=body)

        paper_info = pd.DataFrame(columns=("paper_id", "key_words"))
        results = query["hits"]["hits"]  # es查询出的结果的第一页
        total = query["hits"]["total"]  # es查询出的结果总量
        scroll_id = query["_scroll_id"]  # 游标用于输出es查询出的所有结果

        for i in range(0, int(total/100)+1):
            query_scroll = self.es.scroll(scroll_id=scroll_id, scroll="5m")["hits"]["hits"]
            results += query_scroll

        for res in results:
            paper_info.at[len(paper_info.index)] = [res["_id"], res["_source"]["key_words"]]
        paper_info.set_index("paper_id", inplace=True)
        return paper_info

    def paper_to_subject(self, paper_id):
        """
        根据论文ID查找论文主题
        :param paper_id:
        :return:
        """
        body = {
            "query": {
                "term": {
                    "Id": paper_id
                }
            }
        }
        filter_path = ["hits.hits._source.subject"]
        query = self.es.search(index=self.index_name, filter_path=filter_path, body=body)
        return query["hits"]["hits"]["_source"]["subject"]
