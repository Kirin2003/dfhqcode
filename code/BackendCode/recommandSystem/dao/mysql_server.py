# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: mysql_server
@time: 2022-05-26 15:50
"""
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker
from conf.dao_config import user_info_db_name, mysql_username, mysql_port, mysql_passwd, mysql_hostname


class MysqlServer(object):
    def __init__(self):
        """
        初始化
        """
        self.username = mysql_username
        self.passwd = mysql_passwd
        self.hostname = mysql_hostname
        self.port = mysql_port
        self.user_db_name = user_info_db_name

    def session(self, db_name):
        """
        链接数据库，绑定映射关系
        :param db_name: 数据库名
        :return:
        """
        # 创建引擎
        engine = create_engine("mysql+pymysql://{}:{}@{}:{}/{}".format(
            self.username, self.passwd, self.hostname, self.port, db_name
        ), encodings="utf-8", echo=False)
        # 创建会话
        session = sessionmaker(bind=engine)
        # 返回engine 和 session，前者用来绑定本地数据，后者用来本地操作数据库
        return engine, session()

    def get_register_user_session(self):
        """
        获取注册用户的session
        :return:
        """
        _, sess = self.session(self.user_db_name)
        return sess

    def get_register_user_engine(self):
        """
        获取注册用户的engine
        :return:
        """
        engine, _ = self.session(self.user_db_name)
        return engine
