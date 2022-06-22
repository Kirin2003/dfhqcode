"""
@version: 1.0
@author: zxd3099
@file: user_like
@time: 2022-05-26 22:54
"""
from sqlalchemy import Column, String, Integer
from sqlalchemy.ext.declarative import declarative_base
from sqlalchemy.sql.sqltypes import BigInteger, DateTime

from conf.dao_config import user_likes_table_name
from dao.mysql_server import MysqlServer
from sqlalchemy.sql import func

Base = declarative_base()


class UserLikes(Base):
    """
    用户收藏论文数据
    """
    __tablename__ = user_likes_table_name

    index = Column(Integer(), primary_key=True, autoincrement=True)
    user_id = Column(BigInteger())
    paper_id = Column(String(100))
    time = Column(DateTime(timezone=True), server_default=func.now())

    def __init__(self):
        # 与数据库绑定映射关系
        engine = MysqlServer().get_register_user_engine()
        Base.metadata.create_all(engine)
