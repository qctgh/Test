using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 密码哈希值
        /// </summary>
        public string PasswordHash { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PasswordSalt { get; set; }
        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int LoginErrorTimes { get; set; }
        /// <summary>
        /// 最后一次登录错误时间
        /// </summary>
        public DateTime? LastLoginErrorDateTime { get; set; }
    }
}
