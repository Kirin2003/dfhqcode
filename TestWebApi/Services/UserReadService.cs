using BackendCode.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCode.Services
{
    public class UserReadService
    {
        private MySqlContext mySqlContext;

        public UserReadService(MySqlContext mySqlContext)
        {
            this.mySqlContext = mySqlContext;
        }

        /**
         * 添加用户浏览信息
         */
        public bool StoreUserRead(UserRead userRead)
        {
            var result = SearchRecords(userRead.UserId, userRead.PaperId);
            // 当用户在数据库中已经存有浏览记录时，reads值加一
            if (result != null)
            {
                userRead.reads += result.reads;
                try
                {
                    mySqlContext.Entry(userRead).State = EntityState.Modified;
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
            // 当用户在数据库中没有浏览记录时，添加记录
            try
            {
                mySqlContext.UserReads.Add(userRead);
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
         * 根据用户ID和PaperID查询浏览记录
         */
        public UserRead SearchRecords(long userId, string paperId)
        {
            var userRead = mySqlContext.UserReads
                                           .Where(t => t.UserId == userId)
                                           .Where(t => paperId.Equals(t.PaperId))
                                           .FirstOrDefault();
            return userRead;
        }
    }
}
