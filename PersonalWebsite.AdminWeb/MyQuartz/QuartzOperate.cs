using PersonalWebsite.IService;
using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.AdminWeb.MyQuartz
{
    public class QuartzOperate
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _iocJobfactory;
        private IScheduler _scheduler;
        public QuartzOperate(IJobFactory iocJobfactory, ISchedulerFactory schedulerFactory)
        {
            //1、声明一个调度工厂
            this._schedulerFactory = schedulerFactory;
            this._iocJobfactory = iocJobfactory;
        }
        public async Task<string> Start()
        {
            //2、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            _scheduler.JobFactory = this._iocJobfactory;//  替换默认工厂
            //3、开启调度器
            await _scheduler.Start();

            //4、创建一个触发器
            string strCron = $"0 0 * * * ?";//每小时
            var trigger = TriggerBuilder.Create()
                            //.WithSimpleSchedule(x => x.WithIntervalInSeconds(60).RepeatForever())//每两秒执行一次
                            .WithCronSchedule(strCron)
                            .Build();

            //5、创建任务
            var jobDetail = JobBuilder.Create<SendMail>()
                            .WithIdentity("job", "group")
                            .Build();
            //6、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);
            return await Task.FromResult("将触发器和任务器绑定到调度器中完成");
        }
        public void Stop()
        {
            if (_scheduler == null)
            {
                return;
            }

            if (_scheduler.Shutdown(waitForJobsToComplete: true).Wait(30000))
                _scheduler = null;
            else
            {
            }
        }
    }

}
