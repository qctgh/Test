using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface ITimingService : IServiceSupport
    {
        /// <summary>
        /// 添加定时配置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        long Add(long userId, string time, string weeks);
        /// <summary>
        /// 修改定时配置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="time"></param>
        /// <param name="weeks"></param>
        void Modify(long userId, string time, string weeks);
        /// <summary>
        /// 获取定时对象根据用户ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        TimingDTO GetByUserId(long userId);
        TimingDTO[] GetAll();
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        TimingDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
