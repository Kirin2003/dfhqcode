using BackendCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Base;

namespace BackendCode.Services
{
    public class UserCollectionService
    {
        private MySqlContext mySqlContext;

        public UserCollectionService(MySqlContext mySqlContext)
        {
            this.mySqlContext = mySqlContext;
        }

        /**
         * 添加用户收藏信息
         */
        public bool StoreUserCollection(UserCollection userCollection)
        {
            try
            {
                mySqlContext.UserCollections.Add(userCollection);
                mySqlContext.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                Console.WriteLine(error);
                return false;
            }
            return true;
        }

        /**
         * 删除用户收藏信息
         */
        public bool DeleteUserCollection(long userId, string paperId)
        {
            try
            {
                var userCollection = mySqlContext.UserCollections
                                           .Where(t => t.UserId == userId)
                                           .Where(t => paperId.Equals(t.PaperId))
                                           .FirstOrDefault();
                if (userCollection != null)
                {
                    mySqlContext.Remove(userCollection);
                    mySqlContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                Console.WriteLine(error);
                return false;
            }
            return true;
        }
    }
}
