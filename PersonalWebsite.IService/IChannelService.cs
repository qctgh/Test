using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IChannelService : IServiceSupport
    {
        /// <summary>
        /// 获取所有频道
        /// </summary>
        /// <returns></returns>
        ChannelDTO[] GetAll();
        /// <summary>
        /// 获取所有子频道
        /// </summary>
        /// <returns></returns>
        ChannelDTO[] GetChild();
        /// <summary>
        /// 添加频道
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        long Add(string name);
        /// <summary>
        /// 获取频道根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ChannelDTO GetById(long id);
        /// <summary>
        /// 获取频道根据编码
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ChannelDTO GetByCode(string code);
    }
}
