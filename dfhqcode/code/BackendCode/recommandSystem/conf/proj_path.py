# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: proj_path
@time: 2022-05-26 15:04
"""
import os

# 项目路径
PROJECT_ABSOLUTE_PATH = os.path.dirname(os.path.dirname(os.path.abspath(__file__)))

# 停用词存储路径
stop_words_path = PROJECT_ABSOLUTE_PATH + "\\conf\\stop_words.txt"
