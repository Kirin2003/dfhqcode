using BackendCode.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Services
{
    public class UserLikeService
    {
        private MySqlContext mySqlContext;

        public UserLikeService(MySqlContext mySqlContext)
        {
            this.mySqlContext = mySqlContext;
        }


        /**
         * 添加用户点赞信息
         */
        public bool StoreUserLike(UserLike userLike)
        {
            try
            {
                mySqlContext.UsersLikes.Add(userLike);
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
         * 删除用户点赞信息
         */
        public bool DeleteUserLike(long userId, string paperId)
        {
            try
            {
                var userLike = mySqlContext.UsersLikes
                                           .Where(t => t.UserId == userId)
                                           .Where(t => paperId.Equals(t.PaperId))
                                           .FirstOrDefault();
                if (userLike != null)
                {
                    mySqlContext.Remove(userLike);
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
