using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWebsite.Service
{
    public class KeyValueService : IKeyValueService
    {
        public long AddNew(string typeName, string name)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                KeyValueEntity idName = new KeyValueEntity { Key = name, Value = typeName };

                //todo:检查重复性
                ctx.KeyValues.Add(idName);
                ctx.SaveChanges();
                return idName.Id;
            }
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

        public KeyValueDTO[] GetAll(string key)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.KeyValues.Where(e => e.Key == key).ToList().Select(e => ToDTO(e)).ToArray();
            }
        }

        public KeyValueDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var model = ctx.KeyValues.SingleOrDefault(e => e.Id == id);
                return ToDTO(model);
            }
        }
    }
}
