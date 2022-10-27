using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.AdminWeb.Models
{
    public class AccountLoginModel
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号不能为空")]
        public string PhoneNum { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string PassWord { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "验证码不能为空")]
        public string VerifyCode { get; set; }
    }
}
