using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IMailService : IServiceSupport
    {
        /// <summary>
        /// 验证邮件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        long AddValidateMail(string type, string fromEmail, string toEmail, string title, string body, string result);
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        MailDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
