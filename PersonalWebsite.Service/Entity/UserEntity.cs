using System;

namespace PersonalWebsite.Service.Entity
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 邮箱状态
        /// </summary>
        public string EmailStatus { get; set; }
        /// <summary>
        /// 邮箱验证码
        /// </summary>
        public string VCode { get; set; }
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

        public string QQOpenId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
