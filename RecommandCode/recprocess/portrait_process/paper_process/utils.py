# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: utils
@time: 2022-05-26 18:37
@description: C#爬虫时需要调用这个函数来生成关键词
"""
import re

import nltk
import pandas as pd
import numpy as np
from nltk.corpus import stopwords
from sklearn.feature_extraction.text import TfidfVectorizer


def get_key_words(words_str):
    """
    提取TOPN关键词
    :param words_str: 表示需要提取的文本，是论文的摘要
    :param subject: 论文的主题
    :return:
    """
    # 字符串进行清洗
    words_str.replace('\n', '').replace('\u3000', '').replace('\u00A0', '')
    # 定义标点符号列表
    words_str = re.sub("[0-9.:;,?&$@!()%#^*]", "", words_str)
    # 定义停用词
    stops = set(stopwords.words("english"))

    # 切词
    words_list = nltk.word_tokenize(words_str)
    # 去除停用词
    words_list = [word for word in words_list if word not in stops and word.lower() not in stops]

    vectorizer = TfidfVectorizer()
    X = vectorizer.fit_transform([" ".join(words_list)])
    data = {"key_words": vectorizer.get_feature_names(),
            "tfidf": X.toarray().sum(axis=0).tolist()}
    df = pd.DataFrame(data).sort_values(by="tfidf", ascending=False)
    return np.array(df.iloc[0:10, 0]).tolist()


if __name__ == "__main__":
    get_key_words("The Joy Luck Club is written by famous Chinese American writer Amy Tan in the end of 1980s. It is about the experience of four mothers immigrating from China and their four American born daughters living in the United States. This paper mainly discusses the application of Mitchell’s Women’s Estate in The Joy Luck Club. Based on the position of women in society and family, writer thoroughly analyzes the oppressions on women. The paper includes three chapters, theory of “Women’s Estate”, the position of women in The Joy Luck Club and consciousness-raising reflected in the Joy Luck Club. Accordingly, it is reveals that the oppressions given by men on women in families are inevitable, only can women have the ability to liberate themselves.")
