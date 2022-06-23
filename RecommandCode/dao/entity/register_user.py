# encoding: utf-8
"""
@version: 1.0
@author: zxd3099
@file: register_user
@time: 2022-05-26 15:47
"""
from sqlalchemy import Column, String, Integer
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.sql.sqltypes import BigInteger

from conf.dao_config import register_user_table_name
from dao.mysql_server import MysqlServer

# 创建对象的基类
Base = declarative_base()


class RegisterUser(Base):
    """
    用户注册数据
    """
    __tablename__ = register_user_table_name

    # 表结构
    user_id = Column(BigInteger(), primary_key=True)
    username = Column(String(30))
    password = Column(String(255))
    email = Column(String(255))
    gender = Column(String(10))
    college = Column(String(100))
    profession = Column(String(100))
    interest_areas = Column(String(300))

    def __init__(self):
        engine = MysqlServer().get_register_user_engine()
        Base.metadata.create_all(engine)
