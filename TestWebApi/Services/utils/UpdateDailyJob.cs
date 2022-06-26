using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using BackendCode.Services.utils;
using BackendCode.Models;

namespace BackendCode.Services.utils
{
    public class UpdateDailyJob : IJob
    {
        // 每日更新热度值
        
        public async Task Execute(IJobExecutionContext context)
        {
            DynamicInfoService d = new DynamicInfoService();
            RawPaperService r = new RawPaperService();           
            List<DynamicInfo> list = d.QueryAll();
            // 当前时间
            TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
            TimeSpan ts2;
            foreach(DynamicInfo dynInfo in list)
            {
                var paper = r.QueryDocById(dynInfo.Id);
                ts2 = new TimeSpan(paper.PaperDate.Ticks);
                int deltaDate = ts1.Subtract(ts2).Days;
                double newHotNum = (dynInfo.ReadNum + 3 * dynInfo.LikeNum + 6 * dynInfo.CollectionNum) / Math.Exp(deltaDate);
                dynInfo.HotNum = newHotNum;

            }
            Console.WriteLine("update hotnum");
        }
    }
}
