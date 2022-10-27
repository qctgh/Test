using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IStageService : IServiceSupport
    {
        long Count();
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        StageDTO[] GetAll();
        /// <summary>
        /// 获取歌单根据阶段ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long[] GetSongMenusById(long id);

    }
}
