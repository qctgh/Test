using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class SettingService : ISettingService
    {
        private readonly MyDbContext ctx;
        public SettingService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public void Edit(long id, string name, string value)
        {
            var entity = ctx.Settings.Find(id);
            entity.Name = name;
            entity.Value = value;
            ctx.SaveChanges();
        }

        public SettingDTO GetById(long id)
        {
            return ToDTO(ctx.Settings.Find(id));
        }

        public SettingDTO[] GetAll()
        {


            List<SettingDTO> list = new List<SettingDTO>();
            foreach (var setting in ctx.Settings)
            {
                SettingDTO dto = new SettingDTO();
                dto.CreateDateTime = setting.CreateDateTime;
                dto.Id = setting.Id;
                dto.Name = setting.Name;
                dto.Value = setting.Value;
                list.Add(dto);
            }
            return list.OrderByDescending(p => p.CreateDateTime).ToArray();

        }

        public bool? GetBoolValue(string name)
        {
            string value = GetValue(name);
            if (value == null)
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        public int? GetIntValue(string name)
        {
            string value = GetValue(name);
            if (value == null)
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public string GetValue(string name)
        {

            var setting = ctx.Settings.AsNoTracking().SingleOrDefault(s => s.Name == name);
            if (setting == null)//没有
            {
                return null;
            }
            else
            {
                return setting.Value;
            }

        }

        public void SetBoolValue(string name, bool value)
        {
            SetValue(name, value.ToString());
        }

        public void SetIntValue(string name, int value)
        {
            SetValue(name, value.ToString());
        }

        public void SetValue(string name, string value)
        {

            var setting = ctx.Settings.SingleOrDefault(s => s.Name == name);
            if (setting == null)//没有，则新增
            {
                ctx.Settings.Add(new SettingEntity { Name = name, Value = value });
            }
            else
            {
                setting.Value = value;
            }
            ctx.SaveChanges();

        }

        private SettingDTO ToDTO(SettingEntity entity)
        {
            SettingDTO dto = new SettingDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                CreateDateTime = entity.CreateDateTime,
                IsDeleted = entity.IsDeleted ? "是" : "否",
                DeletedDateTime = entity.DeletedDateTime
            };
            return dto;
        }
    }
}
