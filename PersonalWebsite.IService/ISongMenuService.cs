using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface ISongMenuService : IServiceSupport
    {
        long Count();
        /// <summary>
        /// 获取随机指定数量的歌曲，（如果根据最大最小ID来限制范围的话，中断的ID就会有问题）
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        SongMenuDTO[] GetRecommend(int count);
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        SongMenuDTO[] GetAll();
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        SongMenuDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 获取歌单根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SongMenuDTO GetById(long id);
        /// <summary>
        /// 获取歌单根据ID集合
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        SongMenuDTO[] GetByIds(long[] ids);
        /// <summary>
        /// 添加歌单
        /// </summary>
        /// <returns></returns>
        SongMenuDTO Add(string name, string tags, string coverImgSrc, string Describe, string OrderIndex);
        /// <summary>
        /// 编辑歌单
        /// </summary>
        /// <returns></returns>
        SongMenuDTO Edit(long id, string name, string tags, string coverImgSrc, string Describe, string OrderIndex);
    }
}
