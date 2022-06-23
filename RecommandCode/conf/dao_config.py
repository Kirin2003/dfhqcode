# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: dao_config
@time: 2022-05-26 15:10
"""
# MySQL配置
mysql_username = "root"
mysql_passwd = "123456"
mysql_hostname = "localhost"
mysql_port = "3306"

# MySQL数据库
user_info_db_name = "userinfo"  # 用户数据相关的数据库
register_user_table_name = "register_user"  # 注册用户数据表
user_collections_table_name = "user_collection"  # 用户收藏数据表
user_read_table_name = "user_read"   # 用户阅读数据表
user_likes_table_name = "user_like"  # 用户点赞数据表

# ElasticSearch配置
elastic_hostname = "127.0.0.1"
elastic_username = "elastic"
elastic_passwd = "JSAEtbtfzVrlJpNy6eEP"
elastic_port = 9200
paper_index_name = "paper"
DynamicInfo_index_name = "dynamic_info"

# 论文的分类
cate_dict = {
    "1": "Computer vision and Pattern recognition",
    "2": "Machine Learning",
    "3": "Robotics",
    "4": "Image and Video processing",
    "5": "Artificial Intelligence",
    "6": "Others"
}
