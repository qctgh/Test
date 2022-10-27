using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class ChannelService : IChannelService
    {
        private readonly MyDbContext ctx;
        public ChannelService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long Add(string name)
        {
            throw new NotImplementedException();
        }

        public ChannelDTO[] GetAll()
        {

            return ctx.Channels.AsNoTracking().Select(p => ToDTO(p)).ToArray();

        }
        public ChannelDTO[] GetChild()
        {

            return ctx.Channels.AsNoTracking().Where(p => p.ParentId != 0).Select(p => ToDTO(p)).ToArray();

        }

        public ChannelDTO GetById(long id)
        {
            throw new NotImplementedException();
        }

        public ChannelDTO GetByCode(string code)
        {

            return ToDTO(ctx.Channels.AsNoTracking().FirstOrDefault(p => p.Code == code));

        }

        private ChannelDTO ToDTO(ChannelEntity entity)
        {
            ChannelDTO dto = new ChannelDTO();
            dto.Id = entity.Id;
            dto.IsDeleted = entity.IsDeleted;
            dto.Code = entity.Code;
            dto.Name = entity.Name;
            dto.ParentId = entity.ParentId;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DeletedDateTime = entity.DeletedDateTime;
            return dto;
        }
    }
}
