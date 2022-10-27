using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface ISongService : IServiceSupport
    {
        SongDTO GetById(long id);
        int GetCountByKey(string key);
        SongDTO[] GetByKey(string key, int pageSize, int currentIndex);
        SongDTO[] GetAll();
        SongDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 记录总数
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// 获取随机一首唐诗
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        SongDTO GetTangPoetry();
        long GetCountBySongMenuId(long id);
        SongDTO[] GetBySongMenuId(long id);
        SongDTO[] GetBySongMenuId(long id, int pageSize, int currentIndex);
        SongDTO[] GetByRandom(int count);
        SongDTO Add(string name, string artist, string album, string Cover, string mp3, long SongMenuId);
    }
}
