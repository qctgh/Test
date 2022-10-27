using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class StageService : IStageService
    {
        private readonly MyDbContext ctx;
        public StageService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long Count()
        {
            return ctx.Stages.Count();
        }

        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        public StageDTO[] GetAll()
        {
            return ctx.Stages.Select(p => ToDTO(p)).ToArray();
        }

        /// <summary>
        /// 获取歌单根据阶段ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long[] GetSongMenusById(long id)
        {
            var stage = ctx.Stages.FirstOrDefault(p => p.Id == id);
            string[] songMenus = stage.SongMenus.Split(',');
            long[] result = new long[songMenus.Length];
            for (int i = 0; i < songMenus.Length; i++)
            {
                result[i] = long.Parse(songMenus[i]);
            }
            return result;
        }


        private StageDTO ToDTO(StageEntity entity)
        {
            StageDTO dto = new StageDTO();
            dto.Id = entity.Id;
            dto.IsDeleted = entity.IsDeleted ? "是" : "否";
            dto.Name = entity.Name;
            dto.OrderIndex = entity.OrderIndex;
            dto.CreateDateTime = entity.CreateDateTime;
            dto.DeletedDateTime = entity.DeletedDateTime;
            dto.Describe = entity.Describe;
            return dto;
        }

    }
}
