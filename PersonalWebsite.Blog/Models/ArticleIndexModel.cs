using PersonalWebsite.DTO;

namespace PersonalWebsite.Blog.Models
{
    public class ArticleIndexModel
    {
        /// <summary>
        /// 第一置顶（天下第一）
        /// </summary>
        public ArticleDTO TopOneArticle { get; set; }

        /// <summary>
        /// 置顶文章
        /// </summary>
        public ArticleDTO[] TopArticles { get; set; }
        /// <summary>
        /// 置顶文章
        /// </summary>
        public ArticleDTO[] HotArticles { get; set; }

        /// <summary>
        /// 频道云
        /// </summary>
        public string ChannelYun { get; set; }
        /// <summary>
        /// 标签云
        /// </summary>
        public ChannelDTO[] Channels { get; set; }

    }
}
