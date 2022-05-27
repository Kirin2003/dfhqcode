# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: redis_server
@time: 2022-05-26 16:25
"""
import redis
from conf.dao_config import redis_hostname, redis_port, rec_redis_db_num


class RedisServer(object):
    def __init__(self):
        self.hostname = redis_hostname
        self.port = redis_port
        self.rec_db_num = rec_redis_db_num

    def _redis_db(self, db_num=0):
        res_db = redis.StrictRedis(host=self.hostname, port=self.port, db=db_num, decode_responses=True)
        return res_db

    def get_recommend_list_redis(self):
        """
        用户推荐列表redis数据库
        :return:
        """
        return self._redis_db(self.rec_db_num)
