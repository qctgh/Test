using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.AdminWeb.Models
{
    public class ChannelModel
    {
        /// <summary>
        /// 频道编号
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级频道Id
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 子频道
        /// </summary>
        public List<ChannelModel> Channels { get; set; } = new List<ChannelModel>();
    }
}
