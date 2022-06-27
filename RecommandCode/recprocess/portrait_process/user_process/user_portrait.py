# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: user_portrait
@time: 2022-05-26 22:34
"""
from collections import Counter, defaultdict
from datetime import datetime as dt, timedelta

import pandas as pd

from conf.dao_config import paper_index_name
from dao.elastic_server import ElasticServer
from dao.entity.register_user import RegisterUser
from dao.entity.user_collection import UserCollections
from dao.entity.user_like import UserLikes
from dao.entity.user_read import UserRead
from dao.mysql_server import MysqlServer


# noinspection PyBroadException,PyNoneFunctionAssignment,PyMethodMayBeStatic
class UserPortraitServer(object):
    def __init__(self):
        """
        初始化
        """
        self.es = ElasticServer().elastic_client
        self.index_name = paper_index_name
        self.user_sess = MysqlServer().get_register_user_session()
        self.time_range = 30

    def user_info_to_dict(self, user_id):
        """
        根据输入的user_id，输出用户画像，用字典存储
        :param user_id: 输入用户的ID
        :return:
        """
        info_dict = dict()
        user = self.user_sess.query(RegisterUser)\
                             .filter(RegisterUser.user_id == user_id) \
                             .first()

        # 基本属性特征
        info_dict['userid'] = user.user_id
        info_dict['interestAreas'] = user.interest_areas.split(',')

        # 行为特征
        behaviors = ['like', 'collection', "read"]
        _, feature_dict = self.get_statistical_feature_from_history_behavior(user.user_id, self.time_range, behaviors)
        for type in feature_dict.keys():
            if feature_dict[type]:
                info_dict["{}_{}_top_cate".format(type, self.time_range)] = feature_dict[type]["top_cate"]  # 历史喜欢/收藏、阅读最多的Top3的论文类别
                info_dict["{}_{}_top_keywords".format(type, self.time_range)] = feature_dict[type]["top_keywords"]  # 历史喜欢/收藏/阅读论文的Top10的关键词
                info_dict["{}_{}_paper_ids".format(type, self.time_range)] = feature_dict[type]["paper_id"]     # 用户喜欢/收藏/阅读过的论文
            else:
                info_dict["{}_{}_top_cate".format(type, self.time_range)] = ""
                info_dict["{}_{}_top_keywords".format(type, self.time_range)] = ""
                info_dict["{}_{}_paper_ids".format(type, self.time_range)] = list()

        return info_dict

    def get_statistical_feature_from_history_behavior(self, user_id, time_range, behavior_types):
        """
        获取用户历史行为的统计特征
        :param user_id: 用户ID
        :param time_range: 时间差
        :param behavior_types: 行为特征["read", "like", "collection"]
        :return:
        """
        fail_type = []
        table_obj, history = None, None
        feature_dict = defaultdict(dict)

        end = dt.now().strftime("%Y-%m-%d %H:%M:%S")
        start = (dt.now() + timedelta(days=-time_range)).strftime("%Y-%m-%d %H:%M:%S")

        for type in behavior_types:
            if type == "read":
                table_obj = UserRead
            elif type == "like":
                table_obj = UserLikes
            elif type == "collection":
                table_obj = UserCollections
            try:
                history = self.user_sess.query(table_obj)\
                                        .filter(table_obj.user_id == user_id)\
                                        .filter(table_obj.time >= start)\
                                        .filter(table_obj.time <= end)\
                                        .all()

            except Exception as e:
                print(str(e))
                fail_type.append(type)
                continue

            feature_dict[type] = self.gen_statistical_feature(history)

        return fail_type, feature_dict

    # noinspection DuplicatedCode
    def gen_statistical_feature(self, history):
        """
        为history获取特征
        :param history:
        :return:
        """
        if not len(history):
            return None
        history_paper_cate, history_keyword, history_paper_id = list(), list(), list()
        for h in history:
            paper_id = h.paper_id
            history_paper_id.append(paper_id)
            result = self.es.get(index=self.index_name, id=paper_id)
            # 这里需要根据字段名来修改
            history_paper_cate.append(result["_source"]["subject"])
            history_keyword += result["_source"]["paperKeywords"]

        feature_dict = dict()

        # 计算Top3的类别
        cate_dict = Counter(history_paper_cate)
        cate_list = sorted(cate_dict.items(), key=lambda d: d[1], reverse=True)
        cate_str = ",".join([item[0] for item in cate_list[:3]] if len(cate_list) >= 3 else [item[0] for item in cate_list])
        feature_dict["top_cate"] = cate_str

        # 计算Top10的关键词
        word_dict = Counter(history_keyword)
        word_list = sorted(word_dict.items(), key=lambda d: d[1], reverse=True)
        word_str = ",".join([item[0] for item in word_list[:10]] if len(cate_list) >= 10 else [item[0] for item in word_list])
        feature_dict["top_keywords"] = word_str

        feature_dict["paper_id"] = history_paper_id
        return feature_dict

    def get_user_paper(self):
        """
        返回用户论文行为矩阵
        :return:
        """
        df = pd.DataFrame(columns=("paper_id", "user_id", "like", "collection", "read", "Time"))
        query_result = self.user_sess.query(UserRead).all()
        for read in query_result:
            df.at[len(df.index)] = [read.paper_id, read.user_id, 0, 0, read.reads, read.time]

        query_result = self.user_sess.query(UserLikes).all()
        for like in query_result:
            if len(df.loc[(df["user_id"] == like.user_id) & (df["paper_id"] == like.paper_id)]) == 1:
                df.loc[(df["user_id"] == like.user_id) & (df["paper_id"] == like.paper_id), "like"] = 1
            else:
                df.at[len(df.index)] = [like.paper_id, like.user_id, 1, 0, 0, like.time]

        query_result = self.user_sess.query(UserCollections).all()
        for collection in query_result:
            if len(df.loc[(df["user_id"] == collection.user_id) & (df["paper_id"] == collection.paper_id)]) == 1:
                df.loc[(df["user_id"] == collection.user_id) & (df["paper_id"] == collection.paper_id), "collection"] = 1
            else:
                df.at[len(df.index)] = [collection.paper_id, collection.user_id, 0, 1, 0, collection.time]
        df.sort_values("Time", inplace=True)
        return df
