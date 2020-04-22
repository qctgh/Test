using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class ArticleEntity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 静态地址
        /// </summary>
        public String StaticPath { get; set; }
        /// <summary>
        /// 支持数量
        /// </summary>
        public Int32 SupportCount { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        public UserEntity User { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean IsVisible { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>
        public Boolean IsFirst { get; set; }

        /// <summary>
        /// 频道ID
        /// </summary>
        public long ChannelId { get; set; }
        /// <summary>
        /// 频道
        /// </summary>
        public ChannelEntity Channel { get; set; }

    }
}
