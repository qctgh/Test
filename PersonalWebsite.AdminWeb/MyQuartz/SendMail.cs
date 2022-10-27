using PersonalWebsite.IService;
using Quartz;
using System.Threading.Tasks;

namespace PersonalWebsite.AdminWeb.MyQuartz
{
    public class SendMail : IJob
    {
        //使用静态类来获取依赖注入对象
        private IEmailService EmailService;
        public SendMail()
        {
            this.EmailService = ServiceLocator.Instance.GetService(typeof(IEmailService)) as IEmailService;
        }
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                //定时发送通知邮件
                EmailService.SendNoticeEmail();
            });
        }


    }
}
