using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.IService
{
    public interface IChannelService : IServiceSupport
    {
        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        ChannelDTO[] GetAll();
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
        /// 获取频道根据名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ChannelDTO GetByName(string name);
    }
}
