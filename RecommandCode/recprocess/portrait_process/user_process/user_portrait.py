# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: user_portrait
@time: 2022-05-26 22:34
"""
from collections import Counter, defaultdict
from datetime import datetime

from conf.dao_config import user_portrait_index_name, user_portrait_doc_type
from dao.elastic_server import ElasticServer
from dao.entity.user_collection import UserCollections
from dao.entity.user_like import UserLikes
from dao.entity.user_read import UserRead
from dao.mysql_server import MysqlServer


# noinspection PyBroadException,PyNoneFunctionAssignment,PyMethodMayBeStatic
class UserPortrait(object):
    def __init__(self):
        """
        初始化
        """
        self.es = ElasticServer().elastic_client
        self.index_name = user_portrait_index_name
        self.doc_type = user_portrait_doc_type
        self.user_sess = MysqlServer().get_register_user_session()
        self.time_range = 30

    def user_info_to_dict(self, user):
        """
        将MySQL查询出来的结果转换为字典存储
        :param user:
        :return:
        """
        info_dict = dict()

        # 基本属性特征
        info_dict['userid'] = user.user_id
        info_dict['username'] = user.user_name
        info_dict['passwd'] = user.passwd
        info_dict['gender'] = user.gender
        info_dict['age'] = user.age
        info_dict['college'] = user.college
        info_dict['profession'] = user.profession
        info_dict['interestAreas'] = user.interest_areas.replace(' ', '').split(',')

        # 行为特征
        behaviors = ['like', 'collection']
        _, feature_dict = self.get_statistical_feature_from_history_behavior(user.user_id, self.time_range, behaviors)
        for type in feature_dict.keys():
            if feature_dict[type]:
                info_dict["{}_{}_top_cate".format(type, self.time_range)] = feature_dict[type]["top_cate"]  # 历史喜欢/收藏最多的Top3的论文类别
                info_dict["{}_{}_top_keywords".format(type, self.time_range)] = feature_dict[type]["top_keywords"]  # 历史喜欢/收藏论文的Top3的关键词
                info_dict["{}_{}_avg_hot_value".format(type, self.time_range)] = feature_dict[type]["avg_hot_value"]  # 用户喜欢/收藏论文的平均热度
                info_dict["{}_{}_paper_num".format(type, self.time_range)] = feature_dict[type]["paper_num"]  # 用户30天内喜欢/收藏的论文数量
            else:
                info_dict["{}_{}_top_cate".format(type, self.time_range)] = ""
                info_dict["{}_{}_top_keywords".format(type, self.time_range)] = ""
                info_dict["{}_{}_avg_hot_value".format(type, self.time_range)] = 0
                info_dict["{}_{}_paper_num".format(type, self.time_range)] = 0

            return info_dict

    def get_statistical_feature_from_history_behavior(self, user_id, time_range, behavior_types):
        """
        获取用户历史行为的统计特征
        :param user_id: 用户ID
        :param time_range: 时间差
        :param behavior_types: 行为特征["read","like","collection"]
        :return:
        """
        fail_type = []
        table_obj, history = None, None
        feature_dict = defaultdict(dict)

        end = datetime.datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        start = (datetime.datetime.now()+datetime.timedelta(days=-time_range)).strftime("%Y-%m-%d %H:%M:%S")

        for type in behavior_types:
            if type == "read":
                table_obj = UserRead
            elif type == "like":
                table_obj = UserLikes
            elif type == "collection":
                table_obj = UserCollections
            try:
                history = self.user_sess.query(table_obj)\
                                        .filter(table_obj.userid == user_id)\
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
        history_paper_id, history_hot_value, history_paper_cate, history_keyword = list(), list(), list(), list()
        for h in history:
            paper_id = h.paper_id
            body = {
                "query": {
                    "term": {
                        "paper_id": paper_id
                    }
                }
            }
            result = self.es.search(index=self.index_name, doc_type=self.doc_type, body=body)
            # 这里需要根据字段名来修改
            history_paper_id.append(result["hits"]["hits"]["paper_id"])
            history_hot_value.append(result["hits"]["hits"]["_source"]["hot_value"])
            history_paper_cate.append(result["hits"]["hits"]["_source"]["cate"])
            history_keyword += result["hits"]["hits"]["key_words"].split(",")

        feature_dict = dict()
        # 计算平均热度
        feature_dict["avg_hot_value"] = 0 if sum(history_hot_value) < 0.001 else sum(history_hot_value) / len(history_hot_value)

        # 计算Top3的类别
        cate_dict = Counter(history_paper_cate)
        cate_list = sorted(cate_dict.items(), key=lambda d: d[1], reverse=True)
        cate_str = ",".join([item[0] for item in cate_list[:3]] if len(cate_list) >= 3 else [item[0] for item in cate_list])
        feature_dict["top_cate"] = cate_str

        # 计算Top3的关键词
        word_dict = Counter(history_keyword)
        word_list = sorted(word_dict.items(), key=lambda d: d[1], reverse=True)
        word_str = ",".join([item[0] for item in word_list[:3]] if len(cate_list) >= 3 else [item[0] for item in word_list])
        feature_dict["top_keywords"] = word_str

        # 论文数目
        feature_dict["paper_num"] = len(history_paper_id)

        return feature_dict
