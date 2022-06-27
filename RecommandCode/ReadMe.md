# RecommendCode项目结构说明

## 1.Conf目录(存储配置信息)

dao_config.py：存放MySQL、ElasticSearch的配置信息，以及MySQL数据库表信息、ElasticSearch索引信息。

proj_path.py：标识当前项目的路径信息

## 2.dao目录(存储读取数据库的类)

elastic_server.py：提供Python访问ElasticSearch数据库的通用接口

mysql_server.py：提供Python访问MySQL数据库的通用接口

entity目录：存储对应MySQL数据表的映射类的Python文件

1. register_user.py对应register_user数据库表(存储注册用户详细信息)，表结构如下：

   ```
   +----------------+--------------+------+-----+---------+-------+
   | Field          | Type         | Null | Key | Default | Extra |
   +----------------+--------------+------+-----+---------+-------+
   | user_id        | bigint       | NO   | PRI | NULL    |       |
   | username       | varchar(30)  | NO   |     | NULL    |       |
   | password       | varchar(255) | NO   |     | NULL    |       |
   | email          | varchar(255) | YES  |     | NULL    |       |
   | gender         | varchar(10)  | YES  |     | NULL    |       |
   | college        | varchar(100) | YES  |     | NULL    |       |
   | profession     | varchar(100) | YES  |     | NULL    |       |
   | interest_areas | varchar(300) | YES  |     | NULL    |       |
   +----------------+--------------+------+-----+---------+-------+
   ```

2. user_collection.py对应user_collection数据库表(存储用户收藏信息)，表结构如下：

   ```
   +----------+--------------+------+-----+---------+----------------+
   | Field    | Type         | Null | Key | Default | Extra          |
   +----------+--------------+------+-----+---------+----------------+
   | index    | bigint       | NO   | PRI | NULL    | auto_increment |
   | user_id  | bigint       | NO   |     | NULL    |                |
   | paper_id | varchar(200) | NO   |     | NULL    |                |
   | time     | datetime     | NO   |     | NULL    |                |
   +----------+--------------+------+-----+---------+----------------+
   ```

3. user_like.py对应user_like数据库表(存储用户点赞信息)，表结构如下：

   ```
   +----------+--------------+------+-----+---------+----------------+
   | Field    | Type         | Null | Key | Default | Extra          |
   +----------+--------------+------+-----+---------+----------------+
   | index    | bigint       | NO   | PRI | NULL    | auto_increment |
   | user_id  | bigint       | NO   |     | NULL    |                |
   | paper_id | varchar(200) | NO   |     | NULL    |                |
   | time     | datetime     | NO   |     | NULL    |                |
   +----------+--------------+------+-----+---------+----------------+
   ```

4. user_read.py对应user_read数据库表(存储用户浏览信息)，表结构如下：

   ```
   +----------+--------------+------+-----+---------+----------------+
   | Field    | Type         | Null | Key | Default | Extra          |
   +----------+--------------+------+-----+---------+----------------+
   | index    | bigint       | NO   | PRI | NULL    | auto_increment |
   | user_id  | bigint       | NO   |     | NULL    |                |
   | paper_id | varchar(200) | NO   |     | NULL    |                |
   | reads    | int          | YES  |     | 0       |                |
   | time     | datetime     | NO   |     | NULL    |                |
   +----------+--------------+------+-----+---------+----------------+
   ```

## 3.recprocess目录(存储推荐算法的主要文件)

#### 3.1 protrait_process目录(根据数据库中存储信息，生成用户画像和论文画像)

paper_process目录(用于生成论文画像)

1. paper_portrait.py: 该文件包含两个函数：
   1. paper_info_to_dataframe函数首先查询ES数据库存储的所有论文，然后提取paper_id和key_words作为论文画像中的字段，为了方便后续处理，使用Dataframe格式存储。
   2. paper_to_subject函数根据传入的paper_id查找对应论文的论文主体并返回。
2. utils.py：主要包含get_key_words函数，接受传入的字符串，通过TF-IDF算法的处理，提取TOPN关键词。

user_process目录(用于生成用户画像，该目录中只包含一个文件user_portrait.py)

user_portrait.py文件中包含三个函数：

1. user_info_to_dict函数根据输入的user_id，输出用户画像，用字典存储，用户画像的字段结构如下：

   ```
   user_id: 用户ID
   interestAreas：用户感兴趣的论文领域(注册时填写)
   like_30_top_cate: 过去30天用户喜欢最多的TOP3论文类别
   collection_30_top_cate: 过去30天用户收藏最多的TOP3论文类别
   read_30_top_cate: 过去30天用户浏览最多的TOP3论文类别
   like_30_top_keywords: 过去30天用户喜欢论文出现最多的TOP10论文关键词
   collection_30_top_keywords: 过去30天用户收藏论文出现最多的TOP10论文关键词
   read_30_top_keywords: 过去30天用户浏览论文出现最多的TOP10论文关键词
   like_30_paper_ids: 过去30天用户喜欢的论文id集合
   collection_30_paper_ids: 过去30天用户收藏的论文id集合
   read_30_paper_ids: 过去30天用户浏览的论文id集合
   ```

2. get_statistical_feature_from_history_behavior函数通过查询MySQL数据库，获取用户历史行为(read/like/collection)的统计特征，需要调用gen_statistical_feature函数
3. gen_statistical_feature函数根据paper_id，查询ES数据库得到Top3的类别，Top10的关键词。
4. get_user_paper函数通过查询MySQL数据库，获取调用函数之前存储的所有用户交互信息，并将其整合进同一个Dataframe中，用于基于物品的协同过滤算法的实现。

#### 3.2 recall目录(推荐算法)

content_cf.py(存储基于内容的推荐算法的代码)

1. train_model函数：爬虫程序在系统上线之前爬取论文信息存入数据库，进行Doc2Vec模型的训练和保存；当顶会推出新论文后爬虫存入新的论文信息，进行Doc2Vec模型的增量式训练。
2. rec_list函数：将用户画像中的read/collection/like_30_keywords向量输入Doc2Vec模型，根据相似度排序，提取Top30的论文的paper_id输出到用户的推荐列表。

hot_recall.py(存储基于热度的召回算法的代码)

1. group_cate_for_paper_list函数：根据用户感兴趣的论文领域，推荐热度较高的论文到用户的推荐列表中(结果按照论文领域分类)

item_cf.py(存储基于物品的协同过滤算法的代码)

1. calculate_similarity_matrix：根据用户交互信息，计算物品之间的相似度矩阵，其中考虑到论文阅读连续性、类别相同、活跃用户、热门论文等因素，适当进行打压或者提升相似度。
2. rec_papers_to_user：根据用户历史访问的论文，推荐与这些论文相似度最高的TOPK论文，其中需要考虑到时间衰减的因素。

#### 3.3 offline.py文件

离线推荐，主要是基于前面存储好的论文画像和用户画像进行离线计算，根据不同的推荐算法推荐给用户相应的论文。

## 4.startup目录(实现后端与推荐系统的交互)

后端使用命令行调用这些文件，传递对应的参数，Python代码将结果打印到控制台，后端再进行读取，实现后端与推荐系统的交互。

## 5.test目录(用于测试算法)

data目录(存储已经爬取论文信息、模拟的用户信息、模拟的用户交互信息)

model目录(存储训练的模型)

test.ipynb：测试算法的详细代码

