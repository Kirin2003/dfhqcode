# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: item_cf
@time: 2022-05-31 15:26
"""
import math
from collections import defaultdict
from operator import itemgetter


class ItemCF(object):
    def __init__(self, user_portrait, paper_portrait):
        self.raw_data = user_portrait.get_user_paper()
        self.paper_portrait = paper_portrait
        self.user_set = set()
        self.paper_sim_matrix = dict()
        self.paper_interacted_num = defaultdict(int)

    def calculate_similarity_matrix(self):
        user2items = self.raw_data.groupby("user_id")["paper_id"].apply(list).reset_index()
        # ItemCF的第一阶段
        for idx, row in user2items.iterrows():
            self.user_set.add(row["user_id"])
            for idx1, item_1 in enumerate(row['paper_id']):
                self.paper_interacted_num[item_1] += 1
                self.paper_sim_matrix.setdefault(item_1, {})
                for idx2, item_2 in enumerate(row["paper_id"]):
                    if item_2 == item_1:
                        continue
                    self.paper_sim_matrix[item_1].setdefault(item_2, 0)
                    # 论文阅读可能具有连续性，即后续阅读的论文与前面阅读的论文相似度更高
                    related_score = 1 if idx1 > idx2 else 0.8
                    # 如果二者类别相同，论文之间的相似度更高
                    related_score *= 1 if self.paper_portrait.paper_to_subject(item_1) == \
                                          self.paper_portrait.paper_to_subject(item_2) else 0.5

                    # 活跃用户在计算论文之间相似度时，贡献小于非活跃用户
                    self.paper_sim_matrix[item_1][item_2] += related_score / math.log(1 + len(row['paper_id']))

        # 热门论文与很多论文之间的相似度都很高,需要对热门论文进行打压
        for item_1, related_items in self.paper_sim_matrix:
            for item_2, weight in related_items.items():
                # 打压热门论文
                self.paper_sim_matrix[item_1][item_2] = \
                    weight / math.sqrt(self.paper_interacted_num[item_1] * self.paper_interacted_num[item_2])

    def rec_papers_to_user(self, user_id, _n=50, _topk=20):
        """
        ItemCF召回
        :param user_id: 需要推荐的User的ID
        :param _n: 选取论文数目
        :param _topk: 推荐TOPK的论文
        :return:
        """
        user2items = self.raw_data.groupby("user_id")["paper_id"].apply(list).reset_index()

        user_rec = dict()
        rank = defaultdict(int)
        his_items = user2items.loc[user_id]
        # 遍历用户历史交互论文
        for his_item in his_items:
            # 选取与his_item相似度最高的_n个论文
            for candidate_paper, paper_smi_score in \
                sorted(self.paper_sim_matrix[his_item].items(), key=itemgetter(1), reverse=True)[:_n]:
                # 如果推荐的论文已经被推荐过了，不纳入推荐
                if candidate_paper in his_items:
                    continue
                rank[candidate_paper] += paper_smi_score
        rec_papers = [item[0] for item in sorted(rank.items(), key=itemgetter(1), reverse=True)[:_topk]]
        user_rec[user_id] = rec_papers

        return user_rec
