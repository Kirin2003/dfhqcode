# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: contentcf
@time: 2022-05-30 15:23
"""
import os

from gensim.models.doc2vec import Doc2Vec, TaggedDocument


class ContentCF(object):
    def __init__(self, paper_portrait):
        self.paper_portrait = paper_portrait

    def train_model(self):
        """
        增量式训练Doc2Vec模型
        :return:
        """
        model_path = "model/doc2vec.model"

        paper_info = self.paper_portrait.paper_info_to_dataframe()
        documents = [TaggedDocument(key_words, [paper_id]) for paper_id, key_words \
                     in paper_info["key_words"].iteritems()]

        if os.path.exists(model_path) and os.path.isfile(model_path):
            # load model
            self.model = Doc2Vec.load(model_path)
        else:
            self.model = Doc2Vec(documents, vector_size=100, window=3, min_count=1, workers=4, epochs=20)

        tte = self.model.corpus_count + len(documents)
        self.model.train(documents, total_examples=tte, epochs=20)
        self.model.save(model_path)

    def rec_list(self, user_info):
        """
        生成用户画像的相似度生成论文推荐列表
        :param user_info: 用户信息
        :return:
        """
        user_rec = dict()
        for type in ["read", "like", "collection"]:
            inferred_vector = self.model.infer_vector(user_info["{}_{}_top_keywords".format(type, 30)])
            sims = self.model.docvecs.most_similar([inferred_vector], topn=20)
            tmp = [paper_id for (paper_id, sim) in sims if paper_id not in user_info["{}_{}_paper_ids".format(type, 30)]]
            user_rec[type] = tmp
        return user_rec
