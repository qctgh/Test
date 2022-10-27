using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class SongMenuService : ISongMenuService
    {
        private readonly MyDbContext ctx;
        private string FileServer = "";
        public SongMenuService(MyDbContext ctx)
        {
            this.ctx = ctx;
            FileServer = ctx.Settings.SingleOrDefault(p => p.Name == "文件服务").Value;
        }
        public long Count()
        {
            return ctx.SongMenus.Count();
        }
        /// <summary>
        /// 获取推荐歌单（首页使用）
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public SongMenuDTO[] GetRecommend(int count)
        {
            //2020年12月25日11:13:18
            //突然想到还有一种方法，把随机的ID集合存到一张表里，只需要随机取每条记录的ID就好了，这样效率会超级高，更有利的是可以自定义高频推荐组合，但缺点是需要自己手动组合配置，不过这也不是问题，可以用程序来实现随机组合的记录
            //所有歌单ID的集合
            var songMenuIds = ctx.SongMenus.AsNoTracking().Select(p => p.Id);
            //用来装随机歌单的盒子，防止ID重复，HashSet里不会出现重复值
            HashSet<SongMenuEntity> hsIds = new HashSet<SongMenuEntity>();
            Random random = new Random();
            while (true)
            {
                //如果盒子的数量和指定的数量一致，那就跳出循环
                if (hsIds.Count == count)
                {
                    break;
                }
                long randomId = songMenuIds.ToArray()[random.Next(0, songMenuIds.Count())];
                var songMenuEntity = ctx.SongMenus.SingleOrDefault(p => p.Id == randomId);
                hsIds.Add(songMenuEntity);
            }
            //根据随机ID到库里查询相对应的数据（根据随机的ID去查询相对应的数据这样是不可取的，sql里in始终会按照关键字升序排列，没有起到乱序的效果，所以这里只能随机到一个ID，就查一次，但是效率低，要不断的访问数据库）
            var songMenus = hsIds.Select(p => ToDTO(p)).ToArray();
            return songMenus;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SongMenuDTO[] GetAll()
        {
            return ctx.SongMenus.Include(p => p.Songs).AsNoTracking().OrderBy(p => p.OrderIndex).Select(p => ToDTO(p)).ToArray();
        }
        public SongMenuDTO[] GetAll(int pageSize, int currentIndex)
        {
            return ctx.SongMenus.Include(p => p.Songs).AsNoTracking().OrderBy(p => p.OrderIndex).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }

        public SongMenuDTO GetById(long id)
        {
            return ToDTO(ctx.SongMenus.AsNoTracking().FirstOrDefault(p => p.Id == id));
        }
        public SongMenuDTO[] GetByIds(long[] ids)
        {
            return ctx.SongMenus.AsNoTracking().Where(p => ids.Contains(p.Id)).Select(p => ToDTO(p)).ToArray();
        }

        public SongMenuDTO Add(string name, string tags, string coverImgSrc, string Describe, string OrderIndex)
        {
            SongMenuEntity entity = new SongMenuEntity
            {
                Name = name,
                Tags = tags,
                CoverImgSrc = coverImgSrc,
                Describe = Describe,
                OrderIndex = int.Parse(OrderIndex)
            };
            ctx.SongMenus.Add(entity);
            ctx.SaveChanges();
            return ToDTO(entity);
        }

        public SongMenuDTO Edit(long id, string name, string tags, string coverImgSrc, string Describe, string OrderIndex)
        {
            var songMenu = ctx.SongMenus.FirstOrDefault(p => p.Id == id);
            songMenu.Name = name;
            songMenu.Tags = tags;
            songMenu.CoverImgSrc = coverImgSrc;
            songMenu.Describe = Describe;
            songMenu.OrderIndex = int.Parse(OrderIndex);
            ctx.SaveChanges();
            return ToDTO(songMenu);
        }

        private SongMenuDTO ToDTO(SongMenuEntity entity)
        {
            SongMenuDTO dto = new SongMenuDTO();
            dto.Id = entity.Id;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.Name = entity.Name;
            dto.Tags = entity.Tags;
            dto.OrderIndex = entity.OrderIndex;
            dto.SongCount = entity.Songs?.Count;
            dto.CoverImgSrc = FileServer + entity.CoverImgSrc;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DateTime = DateTimeHelper.DateTimeToString(entity.CreateDateTime);
            dto.DeletedDateTime = entity.DeletedDateTime;
            dto.Describe = entity.Describe;
            return dto;
        }
    }
}
