using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class CommentEntity : BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public String Content { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public Boolean IsVisible { get; set; }
        /// <summary>
        /// 点赞数量
        /// </summary>
        public int LikeCount { get; set; }
        /// <summary>
        /// 评论者IP
        /// </summary>
        public String IP { get; set; }
    }
}
