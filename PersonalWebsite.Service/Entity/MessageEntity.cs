using System;

namespace PersonalWebsite.Service.Entity
{
    public class MessageEntity : BaseEntity
    {
        /// <summary>
        /// 应用ID
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public long ParentId { get; set; }
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
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 评论者IP
        /// </summary>
        public String IP { get; set; }

        public UserEntity User { get; set; }
    }
}
