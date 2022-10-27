using PersonalWebsite.DTO;
using System.Collections.Generic;

namespace PersonalWebsite.Todo369.Models
{
    public class ArticleDetailModel
    {
        public ArticleDTO Article { get; set; }

        public CommentDTO[] Comments { get; set; }

        /// <summary>
        /// 文章ID
        /// </summary>
        public string ArticleIds { get; set; }
        /// <summary>
        /// 当前文章ID集合的索引
        /// </summary>
        public int CurrentIndex { get; set; }
    }
}
