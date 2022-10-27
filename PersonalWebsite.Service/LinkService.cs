using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class LinkService : ILinkService
    {
        private readonly MyDbContext ctx;
        public LinkService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long Add(string name, string url, string icon, string describe, int orderIndex)
        {
            LinkEntity link = new LinkEntity { Name = name, Url = url, Icon = icon, Describe = describe, OrderIndex = orderIndex };

            //todo:检查重复性
            ctx.Links.Add(link);
            ctx.SaveChanges();
            return link.Id;

        }
        public void Edit(long id, string name, string url, string icon, string describe, int orderIndex)
        {
            var entity = ctx.Links.Find(id);
            entity.Name = name;
            entity.Url = url;
            entity.Icon = icon;
            entity.Describe = describe;
            entity.OrderIndex = orderIndex;
            ctx.SaveChanges();
        }

        public LinkDTO[] GetAll()
        {

            return ctx.Links.OrderBy(p => p.OrderIndex).Select(p => ToDTO(p)).ToArray();

        }
        public LinkDTO[] GetAll(int pageSize, int currentIndex)
        {

            return ctx.Links.OrderBy(p => p.OrderIndex).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();

        }
        public long Count()
        {
            return ctx.Links.Count();
        }

        private LinkDTO ToDTO(LinkEntity entity)
        {
            LinkDTO dto = new LinkDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.Name = entity.Name;
            dto.Url = entity.Url;
            dto.Icon = entity.Icon;
            dto.Describe = entity.Describe;
            dto.OrderIndex = entity.OrderIndex;
            return dto;
        }

    }
}
