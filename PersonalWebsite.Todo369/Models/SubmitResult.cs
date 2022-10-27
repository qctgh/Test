using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Todo369.Models
{
    public class SubmitResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 图标代码
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 重定向地址
        /// </summary>
        public string Action { get; set; }
    }
}
