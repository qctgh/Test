namespace PersonalWebsite.IService
{
    public interface IEmailService : IServiceSupport
    {
        /// <summary>
        /// 发送验证邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="vcode"></param>
        /// <returns></returns>
        string SendValidateEmail(long userId, string email, string vcode);
        /// <summary>
        /// 发送通知邮件
        /// </summary>
        /// <param name="userId"></param>
        void SendNoticeEmail();
    }

}
