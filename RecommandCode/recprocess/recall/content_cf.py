# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: itemcf
@time: 2022-05-30 15:23
"""
import os

from gensim.models.doc2vec import Doc2Vec, TaggedDocument

from dao.redis_server import RedisServer


class ContentCF(object):
    def __init__(self, paper_portrait):
        self.paper_portrait = paper_portrait
        self.rec_list_redis = RedisServer().get_recommend_list_redis()

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

    def rec_list_to_redis(self, user_info):
        """
        按照与用户画像的相似度生成论文推荐列表，并存入redis中
        :param user_info: 用户信息
        :return:
        """
        for type in ["read", "like", "collection"]:
            redis_key = "itemCF_{}_paper_list".format(type)
            inferred_vector = self.model.infer_vector(user_info["{}_{}_top_keywords".format(type, 30)])
            sims = self.model.docvecs.most_similar([inferred_vector], topn=20)
            tmp = [paper_id for (paper_id, sim) in sims if paper_id not in user_info["{}_{}_paper_ids".format(type, 30)]]
            self.rec_list_redis.sadd(redis_key, *tmp)
