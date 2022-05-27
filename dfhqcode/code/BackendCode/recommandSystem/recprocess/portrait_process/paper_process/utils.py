# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: utils
@time: 2022-05-26 18:37
@description: C#爬虫时需要调用这个函数来生成关键词
"""
import nltk
import re
from conf.proj_path import stop_words_path
from sklearn.feature_extraction.text import TfidfVectorizer
import pandas as pd


def get_key_words(words_str):
    """
    提取英文中的关键词
    :param words_str:
    :return:
    """
    # 字符串进行清洗
    words_str.replace('\n', '').replace('\u3000', '').replace('\u00A0', '')
    # 去除数字，特殊字符
    words_str = re.sub('[0-9.。:：,，]', '', words_str)
    # 切词
    words_list = nltk.word_tokenize(words_str)

    # 加载停用词
    stop_word_set = set()
    for line in open(stop_words_path, 'r'):
        stop_word_set.add(line.rstrip())
        stop_word_set.add(line.rstrip().capitalize())

    # 去除停用词
    new_words_list = list()
    for word in words_list:
        if word in stop_word_set:
            continue
        new_words_list.append(word)

    new_words_str = list()
    new_words_str.append(" ".join(new_words_list))

    # 计算TF-IDF，提取关键词
    vectorizer = TfidfVectorizer()
    X = vectorizer.fit_transform(new_words_str)

    data = {
        "word": vectorizer.get_feature_names(),
        "tfidf": X.toarray().sum(axis=0).tolist()
    }
    df = pd.DataFrame(data).sort_values(by="tfidf", ascending=False)

    tfidf_text_rank_array = df.head()[["word"]].values.tolist()

    tfidf_text_rank_list = list()
    for item in tfidf_text_rank_array:
        tfidf_text_rank_list.extend(item)
    return tfidf_text_rank_list


if __name__ == "__main__":
    tfidf_text_rank_list = get_key_words("The Joy Luck Club is written by famous Chinese American writer Amy Tan in the end of 1980s. It is about the experience of four mothers immigrating from China and their four American born daughters living in the United States. This paper mainly discusses the application of Mitchell’s Women’s Estate in The Joy Luck Club. Based on the position of women in society and family, writer thoroughly analyzes the oppressions on women. The paper includes three chapters, theory of “Women’s Estate”, the position of women in The Joy Luck Club and consciousness-raising reflected in the Joy Luck Club. Accordingly, it is reveals that the oppressions given by men on women in families are inevitable, only can women have the ability to liberate themselves.")
    print(tfidf_text_rank_list)
