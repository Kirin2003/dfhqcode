using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCode.Base;
using BackendCode.Models;
using BackendCode.Services.utils;
using Nest;
using Quartz;
using Quartz.Impl;

namespace BackendCode.Services
{
    public class DynamicInfoService : EsBase<DynamicInfo>
    {
        public override string IndexName => "dynamic_info";

        // 定时任务
        public async Task updateHotnumDailyAsync()
        {
            IJobDetail job = JobBuilder.Create<UpdateDailyJob>()
                .WithIdentity(name: "UpdateHotnumDaily", "Test")
                .Build();

            Quartz.ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("UpdateHotnumDailyTrigger","Test")
                .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(23,59))
                .ForJob("UpdateHotnumDaily", "Test")
                .Build();

            // 启动任务
            StdSchedulerFactory factory = new StdSchedulerFactory();
            //创建任务调度器
            IScheduler scheduler = await factory.GetScheduler();
            //启动任务调度器
            scheduler.Start();

            //将创建的任务和触发器条件添加到创建的任务调度器当中
            scheduler.ScheduleJob(job, trigger);
        }

    }
}
