using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DTO
{
    public class MailDTO : BaseDTO
    {
        /// <summary>
        /// 类型，1：用户触发，2：定时发送
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 作业ID
        /// </summary>
        public long WorkId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string FromEmail { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string ToEmail { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }
    }

}
