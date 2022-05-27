# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: elastic_server
@time: 2022-05-26 18:10
"""
from elasticsearch import Elasticsearch
from conf.dao_config import elastic_hostname, elastic_port, elastic_passwd, elastic_username


class ElasticServer(object):
    def __init__(self):
        self.elastic_client = Elasticsearch([elastic_hostname],
                                            http_auth=(elastic_username, elastic_passwd),
                                            port=elastic_port)
