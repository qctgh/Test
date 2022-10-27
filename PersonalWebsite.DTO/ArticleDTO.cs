using System;

namespace PersonalWebsite.DTO
{
    public class ArticleDTO
    {
        public long Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public String Title { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 文章类别，0：普通文章，1：带封皮文章
        /// </summary>
        public int Classification { get; set; }
        /// <summary>
        /// 文章封面图片路径
        /// </summary>
        public string Cover { get; set; }
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
        public string SupportCount { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public int Day { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTime { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }

        public String UserName { get; set; }
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
        /// 频道编码
        /// </summary>
        public string ChannelCode { get; set; }
        /// <summary>
        /// 频道
        /// </summary>
        public String ChannelName { get; set; }
        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentsCount { get; set; }

    }
}
