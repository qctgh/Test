using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IWorkService : IServiceSupport
    {
        /// <summary>
        /// 添加定时配置
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="time"></param>
        /// <param name="weeks"></param>
        /// <returns></returns>
        long Add(long userId, long timingId, long mailId);
        long Add(string remark);
        WorkDTO[] GetAll();
        /// <summary>
        /// 获取全部记录（分页）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        WorkDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 获取全部记录总数
        /// </summary>
        /// <returns></returns>
        long Count();
    }
}
