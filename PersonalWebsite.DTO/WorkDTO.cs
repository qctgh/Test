using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DTO
{
    public class WorkDTO : BaseDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 定时配置ID
        /// </summary>
        public long TimingId { get; set; }
        /// <summary>
        /// 邮件ID
        /// </summary>
        public long MailId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ReMark { get; set; }
    }

}
