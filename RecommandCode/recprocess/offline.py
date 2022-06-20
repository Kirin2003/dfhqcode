# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: offline
@time: 2022-05-29 15:42
"""
from conf.dao_config import cate_dict
from recprocess.portrait_process.paper_process.paper_portrait import PaperPortraitServer
from recprocess.portrait_process.user_process.user_portrait import UserPortraitServer
from recprocess.recall.content_cf import ContentCF
from recprocess.recall.hot_recall import HotRecall
from recprocess.recall.item_cf import ItemCF


class OfflineServer(object):
    def __init__(self):
        self.name2id_cate_dict = {v: k for k, v in cate_dict.items()}
        self.user_portrait = UserPortraitServer()
        self.paper_portrait = PaperPortraitServer()
        self.hot_recall = HotRecall()
        self.content_cf = ContentCF(self.paper_portrait)
        self.item_cf = ItemCF(self.user_portrait, self.paper_portrait)

    def hot_list_to_redis(self, user_id):
        """
        热门页面，对于每一个新增用户，根据其感兴趣的领域推荐热度高的paper
        :param: user_id
        :return:
        """
        user_interest_cate = list()

        user_info = self.user_portrait.user_info_to_dict(user_id)
        top_cate = user_info["interestAreas"]
        for type in ["read", "collection", "like"]:
            top_cate = list(set(top_cate + user_info["{}_{}_top_cate".format(type, 30)]))

        for cate in top_cate:
            user_interest_cate[self.name2id_cate_dict[cate]] = cate
        self.hot_recall.group_cate_for_paper_list_to_redis(user_interest_cate)

    def paper_content_cf(self, user_id):
        """
        基于内容的推荐
        :param user_id:
        :return:
        """
        user_info = self.user_portrait.user_info_to_dict(user_id)
        return self.content_cf.rec_list(user_info)

    def paper_to_paper(self, users):
        """
        基于ItemCF的推荐
        :param users: 待推荐用户的ID列表
        :return:
        """
        self.item_cf.calculate_similarity_matrix()
        return self.item_cf.rec_papers_to_user(users)
