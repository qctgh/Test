using System;
using System.Linq;
using PersonalWebsite.Helper;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using PersonalWebsite.Service;

namespace ZSZ.Service
{
    public class UserService : IUserService
    {
        public long AddNew(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //检查手机号不能重复
                bool exists = ctx.Users.Any(u => u.PhoneNum == phoneNum);
                if (exists)
                {
                    throw new ArgumentException("手机号已经存在");
                }
                UserEntity user = new UserEntity();
                user.PhoneNum = phoneNum;
                string salt = CommonHelper.CreateVerifyCode(5);
                string pwdHash = CommonHelper.CalcMD5(salt + password);
                user.PasswordHash = pwdHash;
                user.PasswordSalt = salt;
                ctx.Users.Add(user);
                ctx.SaveChanges();
                return user.Id;
            }
        }

        public bool CheckLogin(string phoneNum, string password)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.Users.SingleOrDefault(u => u.PhoneNum == phoneNum);
                if (user == null)
                {
                    return false;
                }
                else
                {
                    string dbPwdHash = user.PasswordHash;
                    string salt = user.PasswordSalt;
                    string userPwdHash = CommonHelper.CalcMD5(salt + password);
                    return dbPwdHash == userPwdHash;
                }
            }
        }

        private UserDTO ToDTO(UserEntity user)
        {
            UserDTO dto = new UserDTO();
            dto.CreateDateTime = user.CreateDateTime;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.PhoneNum = user.PhoneNum;
            return dto;
        }

        public UserDTO GetById(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.Users.SingleOrDefault(p => p.Id.Equals(id));
                return user == null ? null : ToDTO(user);
            }
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                var user = ctx.Users.SingleOrDefault(u => u.PhoneNum == phoneNum);
                return user == null ? null : ToDTO(user);
            }
        }



        public void UpdatePwd(long userId, string newPassword)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //检查手机号不能重复
                var user = ctx.Users.SingleOrDefault(p => p.Id.Equals(userId));
                if (user == null)
                {
                    throw new ArgumentException("用户不存在 " + userId);
                }
                string salt = user.PasswordSalt;// CommonHelper.CreateVerifyCode(5);
                string pwdHash = CommonHelper.CalcMD5(salt + newPassword);
                user.PasswordHash = pwdHash;
                user.PasswordSalt = salt;
                ctx.SaveChanges();
            }
        }

        public void IncrLoginError(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //检查手机号不能重复
                var user = ctx.Users.SingleOrDefault(p => p.Id.Equals(id));
                if (user == null)
                {
                    throw new ArgumentException("用户不存在 " + id);
                }
                user.LoginErrorTimes++;
                user.LastLoginErrorDateTime = DateTime.Now;
                ctx.SaveChanges();
            }
        }

        public void ResetLoginError(long id)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //检查手机号不能重复
                var user = ctx.Users.SingleOrDefault(p => p.Equals(id));
                if (user == null)
                {
                    throw new ArgumentException("用户不存在 " + id);
                }
                user.LoginErrorTimes = 0;
                user.LastLoginErrorDateTime = null;
                ctx.SaveChanges();
            }
        }

        public bool IsLocked(long id)
        {
            var user = GetById(id);
            //错误登录次数>=5，最后一次登录错误时间在30分钟之内
            return (user.LoginErrorTimes >= 5
                && user.LastLoginErrorDateTime > DateTime.Now.AddMinutes(-30));
        }
    }
}
