using System;

namespace PersonalWebsite.DTO
{
    public class UserDTO : BaseDTO
    {
        public String PhoneNum { get; set; }
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
        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }
        public string QQOpenId { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public string LastLoginIP { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }

}
