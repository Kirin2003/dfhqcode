# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: elastic_server
@time: 2022-05-26 18:10
"""
from elasticsearch import Elasticsearch
from conf.dao_config import elastic_hostname, elastic_passwd, elastic_username, elastic_port


class ElasticServer(object):
    def __init__(self):
        self.elastic_client = Elasticsearch("http://{}:{}".format(elastic_hostname, elastic_port),
                                            basic_auth=(elastic_username, elastic_passwd))
