using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface ISysUpdateLogService : IServiceSupport
    {
        
        SysUpdateLogDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 记录总数
        /// </summary>
        /// <returns></returns>
        long Count();
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="content"></param>
        void Add(string content);
    }
}
