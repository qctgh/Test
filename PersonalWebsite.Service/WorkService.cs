using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class WorkService : IWorkService
    {
        private readonly MyDbContext ctx;
        public WorkService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public long Add(long userId, long timingId, long mailId)
        {
            WorkEntity entity = new WorkEntity
            {
                UserId = userId,
                TimingId = timingId,
                MailId = mailId
            };
            ctx.Works.Add(entity);
            ctx.SaveChanges();
            return entity.Id;

        }

        public long Add(string remark)
        {
            WorkEntity entity = new WorkEntity
            {
                UserId = 0,
                TimingId = 0,
                MailId = 0,
                ReMark = remark
            };
            ctx.Works.Add(entity);
            ctx.SaveChanges();
            return entity.Id;
        }
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        public WorkDTO[] GetAll()
        {
            return ctx.Works.Select(p => ToDTO(p)).ToArray();
        }
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public WorkDTO[] GetAll(int pageSize, int currentIndex)
        {

            return ctx.Works.OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();

        }
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return ctx.Works.Count();
        }

        private WorkDTO ToDTO(WorkEntity entity)
        {
            WorkDTO dto = new WorkDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.UserId = entity.UserId;
            dto.TimingId = entity.TimingId;
            dto.MailId = entity.MailId;
            dto.ReMark = entity.ReMark;
            return dto;
        }

    }
}
