using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class TimingService : ITimingService
    {
        private readonly MyDbContext ctx;
        public TimingService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 添加定时配置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        public long Add(long userId, string time, string weeks)
        {
            TimingEntity entity = new TimingEntity
            {
                UserId = userId,
                Time = time,
                Weeks = weeks
            };
            ctx.Timings.Add(entity);
            ctx.SaveChanges();
            return entity.Id;

        }
        public void Modify(long userId, string time, string weeks)
        {
            var timing = ctx.Timings.First(p => p.UserId == userId);
            timing.Time = time;
            timing.Weeks = weeks;
            ctx.SaveChanges();
        }
        public TimingDTO GetByUserId(long userId)
        {
            var timing = ctx.Timings.FirstOrDefault(p => p.UserId == userId);
            return ToDTO(timing);
        }
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        public TimingDTO[] GetAll()
        {
            return ctx.Timings.Select(p => ToDTO(p)).ToArray();
        }
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public TimingDTO[] GetAll(int pageSize, int currentIndex)
        {

            return ctx.Timings.OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();

        }
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return ctx.Timings.Count();
        }

        private TimingDTO ToDTO(TimingEntity entity)
        {
            TimingDTO dto = new TimingDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.UserId = entity.UserId;
            dto.Time = entity.Time;
            dto.Weeks = entity.Weeks;
            return dto;
        }

    }
}
