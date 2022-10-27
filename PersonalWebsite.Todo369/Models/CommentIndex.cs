using PersonalWebsite.DTO;
using System.Collections.Generic;

namespace PersonalWebsite.Todo369.Models
{
    public class CommentIndex
    {
        /// <summary>
        /// 频道
        /// </summary>
        public List<ChannelModel> Channels { get; set; }
        /// <summary>
        /// 热门文章
        /// </summary>
        public ArticleDTO[] HotArticles { get; set; }
        /// <summary>
        /// 推荐文章
        /// </summary>
        public ArticleDTO[] RecommendArticles { get; set; }
    }
}
