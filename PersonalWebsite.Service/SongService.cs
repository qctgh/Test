using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class SongService : ISongService
    {
        private readonly MyDbContext ctx;
        private string FileServer = "";
        public SongService(MyDbContext ctx)
        {
            this.ctx = ctx;
            FileServer = ctx.Settings.SingleOrDefault(p => p.Name == "文件服务").Value;
        }
        public SongDTO GetById(long id)
        {
            var song = ctx.Songs.SingleOrDefault(p => p.Id == id);
            return ToDTO(song);
        }
        public int GetCountByKey(string key)
        {
            return ctx.Songs.AsNoTracking().Where(p => p.Name.Contains(key)).Count();
        }
        public SongDTO[] GetByKey(string key, int pageSize, int currentIndex)
        {
            return ctx.Songs.AsNoTracking().Where(p => p.Name.Contains(key)).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }
        public SongDTO[] GetAll()
        {
            return ctx.Songs.Include(p => p.SongMenu).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();
        }
        public SongDTO[] GetAll(int pageSize, int currentIndex)
        {
            return ctx.Songs.Include(p => p.SongMenu).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }
        public long Count()
        {
            return ctx.Songs.Count();
        }
        /// <summary>
        /// 获取随机一首唐诗
        /// </summary>
        /// <returns></returns>
        public SongDTO GetTangPoetry()
        {
            var songs = GetBySongMenuId(4);
            Random random = new Random();
            //从唐诗集合中随机获取一个ID
            int rNum = random.Next(0, songs.Count());
            var song = songs[rNum];
            return song;
        }
        public long GetCountBySongMenuId(long id)
        {
            return ctx.Songs.AsNoTracking().Where(p => p.SongMenuId == id).Count();
        }
        public SongDTO[] GetBySongMenuId(long id)
        {
            return ctx.Songs.AsNoTracking().Where(p => p.SongMenuId == id).OrderBy(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();
        }
        public SongDTO[] GetBySongMenuId(long id, int pageSize, int currentIndex)
        {
            return ctx.Songs.AsNoTracking().Where(p => p.SongMenuId == id).OrderBy(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize).Select(p => ToDTO(p)).ToArray();
        }
        /// <summary>
        /// 获取随机指定数量的歌曲，（如果根据最大最小ID来限制范围的话，中断的ID就会有问题）
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public SongDTO[] GetByRandom(int count)
        {
            //最小ID
            int minId = (int)ctx.Songs.OrderBy(p => p.Id).FirstOrDefault().Id;
            //最大ID
            int maxId = (int)ctx.Songs.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            Random random = new Random();
            List<int> randomNums = new List<int>();
            //随机插入count个随机的数
            for (int i = 0; i < count; i++)
            {
                randomNums.Add(random.Next(minId, maxId));
            }
            return ctx.Songs.AsNoTracking().Where(p => randomNums.Contains(Convert.ToInt32(p.Id))).Select(p => ToDTO(p)).ToArray();
        }
        public SongDTO Add(string name, string artist, string album, string Cover, string mp3, long songMenuId)
        {
            SongEntity entity = new SongEntity
            {
                Name = name,
                Artist = artist,
                Album = album,
                Cover = Cover,
                Mp3 = mp3,
                SongMenuId = songMenuId
            };
            ctx.Songs.Add(entity);
            ctx.SaveChanges();
            return ToDTO(entity);
        }

        private SongDTO ToDTO(SongEntity entity)
        {
            SongDTO dto = new SongDTO();
            dto.Id = entity.Id;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.Name = entity.Name;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DeletedDateTime = entity.DeletedDateTime;
            dto.Mp3 = entity.Mp3;
            dto.SongMenuId = entity.SongMenuId;
            dto.Album = entity.Album;
            dto.Artist = entity.Artist;
            dto.Cover = FileServer + entity.Cover;
            dto.SongMenuName = entity.SongMenu?.Name;
            return dto;
        }
    }
}
