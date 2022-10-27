using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class KeyValueService : IKeyValueService
    {
        private readonly MyDbContext ctx;
        public KeyValueService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long AddNew(string key, string value)
        {
            KeyValueEntity idName = new KeyValueEntity { Key = key, Value = value };

            //todo:检查重复性
            ctx.KeyValues.Add(idName);
            ctx.SaveChanges();
            return idName.Id;

        }

        private KeyValueDTO ToDTO(KeyValueEntity entity)
        {
            KeyValueDTO dto = new KeyValueDTO();
            dto.CreateDateTime = entity.CreateDateTime;
            dto.Id = entity.Id;
            dto.Key = entity.Key;
            dto.Value = entity.Value;
            return dto;
        }
        private TagDTO ToTagDTO(KeyValueEntity entity)
        {
            TagDTO dto = new TagDTO();
            dto.Id = entity.Id;
            dto.Name = entity.Value;
            return dto;
        }
        public TagDTO[] GetTags()
        {
            return ctx.KeyValues.Where(p => p.Key == "SongMenuTag").Select(e => ToTagDTO(e)).ToArray();
        }

        public KeyValueDTO[] GetAll(string key)
        {

            return ctx.KeyValues.Where(e => e.Key == key).ToList().Select(e => ToDTO(e)).ToArray();

        }

        public KeyValueDTO[] GetAll()
        {

            return ctx.KeyValues.OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();

        }
        public KeyValueDTO[] GetAll(int pageSize, int currentIndex)
        {

            return ctx.KeyValues.OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();

        }
        public long Count()
        {
            return ctx.KeyValues.Count();
        }

        public KeyValueDTO GetById(long id)
        {

            var model = ctx.KeyValues.SingleOrDefault(e => e.Id == id);
            return ToDTO(model);

        }

        public void Edit(long id, string key, string value)
        {
            var entity = ctx.KeyValues.Find(id);
            entity.Key = key;
            entity.Value = value;
            ctx.SaveChanges();
        }
    }
}
