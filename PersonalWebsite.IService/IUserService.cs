using PersonalWebsite.DTO;
using System;

namespace PersonalWebsite.IService
{
    public interface IUserService : IServiceSupport
    {
        long AddNew(String phoneNum, String password);
        /// <summary>
        /// QQ登录注册添加用户信息
        /// </summary>
        /// <param name="qqOpenId"></param>
        /// <param name="name"></param>
        /// <param name="avatar"></param>
        /// <param name="gender"></param>
        /// <returns></returns>
        UserDTO Add(string qqOpenId, string name, string avatar, string gender);
        UserDTO GetById(long id);
        UserDTO GetByQQOpenId(string qqOpenId);
        UserDTO GetByPhoneNum(String phoneNum);
        UserDTO[] GetAll();

        ////检查用户名密码是否正确（很好体现了分层的思想）
        bool CheckLogin(String phoneNum, String password);
        void UpdatePwd(long userId, String newPassword);


        /// <summary>
        /// 记录一次登录失败
        /// </summary>
        /// <param name="id"></param>
        void IncrLoginError(long id);

        /// <summary>
        /// 充值登录失败信息
        /// </summary>
        /// <param name="id"></param>
        void ResetLoginError(long id);
        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        UserDTO ModifyEmail(long userId, string email);

        /// <summary>
        /// 判断用户是否已经被锁定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool IsLocked(long id);
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="user"></param>
        void Update(string qQOpenId, string lastLoginIP, DateTime? lastLoginTime);
        string ValidateVCode(long userId, string email, string vcode);
        string ValidateVCode(string id);
    }

}
