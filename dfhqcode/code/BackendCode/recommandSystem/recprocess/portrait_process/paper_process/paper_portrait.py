# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: paper_portrait
@time: 2022-05-26 20:18
"""
from dao.elastic_server import ElasticServer


class PaperPortraitServer(object):
    def __init__(self):
        """
        初始化
        """
        self.elastic_server = ElasticServer()

