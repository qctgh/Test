using PersonalWebsite.DTO;
using System.Collections.Generic;

namespace PersonalWebsite.Todo369.Models
{
    public class HomeIndexModel
    {
        /// <summary>
        /// 综合
        /// </summary>
        public ArticleDTO[] Articles { get; set; }
        /// <summary>
        /// 置顶文章
        /// </summary>
        public ArticleDTO[] TopArticles { get; set; }
        
        /// <summary>
        /// 频道云
        /// </summary>
        public string ChannelYun { get; set; }
    }
}
