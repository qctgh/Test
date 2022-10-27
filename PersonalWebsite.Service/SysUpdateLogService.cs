using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class SysUpdateLogService : ISysUpdateLogService
    {
        private readonly MyDbContext ctx;
        public SysUpdateLogService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="content"></param>
        public void Add(string content)
        {
            SysUpdateLogEntity entity = new SysUpdateLogEntity
            {
                Content = content
            };
            ctx.SysUpdateLogs.Add(entity);
            ctx.SaveChanges();
        }

        public SysUpdateLogDTO[] GetAll(int pageSize, int currentIndex)
        {
            return ctx.SysUpdateLogs.AsNoTracking().OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }
        public long Count()
        {
            return ctx.SysUpdateLogs.Count();
        }


        private SysUpdateLogDTO ToDTO(SysUpdateLogEntity entity)
        {
            SysUpdateLogDTO dto = new SysUpdateLogDTO();
            dto.Id = entity.Id;
            dto.Content = entity.Content;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.PublishDate = Helper.DateTimeHelper.DateTimeToString(entity.CreateDateTime);
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.DeletedDateTime = entity.DeletedDateTime;
            return dto;
        }
    }
}
