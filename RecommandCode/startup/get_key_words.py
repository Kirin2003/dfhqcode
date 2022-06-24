# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: get_key_words
@time: 2022-06-24 11:20
"""
import sys

sys.path.append("D:\\工作目录\\PythonProject\\recommandSystem")

from recprocess.portrait_process.paper_process.utils import get_key_words


if __name__ == '__main__':
    get_key_words(sys.argv[1])
