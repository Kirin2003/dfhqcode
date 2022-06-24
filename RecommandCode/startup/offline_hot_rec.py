# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: offline_hot_rec
@time: 2022-06-24 14:37
"""
import sys

from recprocess.offline import OfflineServer

sys.path.append("D:\\工作目录\\PythonProject\\recommandSystem")


if __name__ == "__main__":
    server = OfflineServer()
    print(server.hot_list(sys.argv[1]))
