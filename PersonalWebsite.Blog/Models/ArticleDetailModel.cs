using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Blog.Models
{
    public class ArticleDetailModel
    {
        /// <summary>
        /// 文章实体
        /// </summary>
        public ArticleDTO Article { get; set; }
        /// <summary>
        /// 评论集合
        /// </summary>
        public CommentDTO[] Comments { get; set; }

    }
}
