using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class MailService : IMailService
    {
        private readonly MyDbContext ctx;
        public MailService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 验证邮件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromEmail"></param>
        /// <param name="toEmail"></param>
        /// <param name="title"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public long AddValidateMail(string type, string fromEmail, string toEmail, string title, string body, string result)
        {
            MailEntity entity = new MailEntity
            {
                Type = type,
                FromEmail = fromEmail,
                ToEmail = toEmail,
                Title = title,
                Body = body,
                Result = result
            };
            ctx.Mails.Add(entity);
            ctx.SaveChanges();
            return entity.Id;

        }
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public MailDTO[] GetAll(int pageSize, int currentIndex)
        {

            return ctx.Mails.OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();

        }
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return ctx.Mails.Count();
        }

        private MailDTO ToDTO(MailEntity entity)
        {
            MailDTO dto = new MailDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.Type = entity.Type;
            dto.WorkId = entity.WorkId;
            dto.UserId = entity.UserId;
            dto.FromEmail = entity.FromEmail;
            dto.ToEmail = entity.ToEmail;
            dto.Title = entity.Title;
            dto.Body = entity.Body;
            dto.Result = entity.Result;
            return dto;
        }

    }
}
