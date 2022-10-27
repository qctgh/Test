using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DTO
{
    public class MessageDTO : BaseDTO
    {
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
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }
        /// <summary>
        /// 评论者IP
        /// </summary>
        public String IP { get; set; }
        /// <summary>
        /// 评论日期
        /// </summary>
        public string CommentDate { get; set; }

        public MessageDTO[] Messages { get; set; }

    }

}
