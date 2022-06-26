using BackendCode.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BackendCode.Services
{
    public class UserService
    {
        private MySqlContext mySqlContext;

        public UserService(MySqlContext mySqlContext)
        {
            this.mySqlContext = mySqlContext;
        }

        /**
         * 根据用户ID查询用户详细信息
         */
        public User QueryUserById(long id)
        {
            var user = mySqlContext.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        /**
         * 将用户信息存入数据库
         */
        public bool StoreUserInfo(User user)
        {
            try
            {
                mySqlContext.Users.Add(user);
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
         * 更新用户信息
         */
        public bool UpdateUserInfo(long id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }
            try
            {
                mySqlContext.Entry(user).State = EntityState.Modified;
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
         * 删除用户信息
         */
        public bool DeleteUserInfo(long id)
        {
            try
            {
                var user = mySqlContext.Users.FirstOrDefault(t => t.Id == id);
                if (user != null)
                {
                    mySqlContext.Remove(user);
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

        /**
         * 根据用户名和密码查询用户是否存在
         */
        public bool QueryUser(string userName, string password)
        {
            try
            {
                var user = mySqlContext.Users.Where(t => userName.Equals(t.Name))
                                             .Where(t => password.Equals(t.Password))
                                             .FirstOrDefault();
                if (user != null)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                Console.WriteLine(error);
                return false;
            }
            return false;
        }
    }
}
