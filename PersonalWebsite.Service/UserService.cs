using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.Helper.Security;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class UserService : IUserService
    {
        private readonly MyDbContext ctx;
        public UserService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }
        public long AddNew(string phoneNum, string password)
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

        public UserDTO Add(string qqOpenId, string name, string avatar, string gender)
        {
            //检查手机号不能重复
            bool exists = ctx.Users.Any(u => u.QQOpenId == qqOpenId);
            if (exists)
            {
                throw new ArgumentException("用户已经存在");
            }
            UserEntity user = new UserEntity();
            user.QQOpenId = qqOpenId;
            user.Name = name;
            user.Avatar = avatar;
            user.Gender = gender;
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return ToDTO(user);
        }

        public bool CheckLogin(string phoneNum, string password)
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

        private UserDTO ToDTO(UserEntity user)
        {
            UserDTO dto = new UserDTO();
            dto.CreateDateTime = user.CreateDateTime;
            dto.Id = user.Id;
            dto.LastLoginErrorDateTime = user.LastLoginErrorDateTime;
            dto.LoginErrorTimes = user.LoginErrorTimes;
            dto.PhoneNum = user.PhoneNum;
            dto.Email = user.Email;
            dto.EmailStatus = user.EmailStatus;
            dto.VCode = user.VCode;
            dto.QQOpenId = user.QQOpenId;
            dto.Name = user.Name;
            dto.Avatar = user.Avatar;
            dto.Gender = user.Gender;
            dto.LastLoginIP = user.LastLoginIP;
            dto.LastLoginTime = user.LastLoginTime;
            return dto;
        }

        public UserDTO GetById(long id)
        {

            var user = ctx.Users.SingleOrDefault(p => p.Id.Equals(id));
            return user == null ? null : ToDTO(user);

        }

        public UserDTO GetByQQOpenId(string qqOpenId)
        {
            var user = ctx.Users.SingleOrDefault(p => p.QQOpenId == qqOpenId);
            return user == null ? null : ToDTO(user);
        }

        public UserDTO GetByPhoneNum(string phoneNum)
        {

            var user = ctx.Users.SingleOrDefault(u => u.PhoneNum == phoneNum);
            return user == null ? null : ToDTO(user);

        }

        public UserDTO[] GetAll()
        {

            return ctx.Users.OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();

        }



        public void UpdatePwd(long userId, string newPassword)
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
        public void Update(string qqOpenId, string lastLoginIP, DateTime? lastLoginTime)
        {
            //检查openId不能重复
            var user = ctx.Users.SingleOrDefault(p => p.QQOpenId.Equals(qqOpenId));
            if (user == null)
            {
                throw new ArgumentException("用户不存在 " + qqOpenId);
            }
            user.LastLoginIP = lastLoginIP;
            user.LastLoginTime = lastLoginTime;
            ctx.SaveChanges();
        }

        public void IncrLoginError(long id)
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

        public void ResetLoginError(long id)
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
        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="email"></param>
        public UserDTO ModifyEmail(long userId, string email)
        {
            var user = ctx.Users.First(p => p.Id == userId);

            string strGuid = Guid.NewGuid().ToString();
            user.Email = email;
            user.EmailStatus = "notactive";
            user.VCode = strGuid.Substring(strGuid.Length - 4);
            ctx.SaveChanges();
            return ToDTO(user);
        }
        public bool IsLocked(long id)
        {
            var user = GetById(id);
            //错误登录次数>=5，最后一次登录错误时间在30分钟之内
            return (user.LoginErrorTimes >= 5
                && user.LastLoginErrorDateTime > DateTime.Now.AddMinutes(-30));
        }

        public string ValidateVCode(long userId, string email, string vcode)
        {
            string result = "";
            var user = ctx.Users.First(p => p.Id == userId);
            //比对验证链接中的邮箱和验证码是否一致
            if (user.Email == email && user.VCode == vcode)
            {
                user.EmailStatus = "active";
                //默认定时配置
                TimingEntity timing = new TimingEntity
                {
                    UserId = userId,
                    Time = "8",
                    Weeks = "1,2,3,4,5,6,7"
                };
                ctx.Timings.Add(timing);
                ctx.SaveChanges();
                result = "邮箱已激活，快去看看别的吧！";
            }
            else
            {
                result = "邮箱激活失败，请稍后再试！";
            }
            return result;
        }
        public string ValidateVCode(string id)
        {
            string result = "";
            string str = Des.DesDecrypt(id);
            //用户ID
            long userId = long.Parse(str.Split('&')[0]);
            //用户邮箱
            string email = str.Split('&')[1];
            //邮箱验证码
            string vcode = str.Split('&')[2];

            var user = ctx.Users.First(p => p.Id == userId);
            //比对验证链接中的邮箱和验证码是否一致
            if (user.Email == email && user.VCode == vcode)
            {
                user.EmailStatus = "active";
                //默认定时配置(有则修改，无则新增)
                var timing = ctx.Timings.FirstOrDefault(p => p.UserId == userId);
                if (timing != null)
                {
                    timing.Time = "8";//后期改为自定义
                    timing.Weeks = "1,2,3,4,5,6,7";
                }
                else
                {
                    timing = new TimingEntity
                    {
                        UserId = userId,
                        Time = "8",
                        Weeks = "1,2,3,4,5,6,7"
                    };
                    ctx.Timings.Add(timing);
                }
                ctx.SaveChanges();
                result = "邮箱已激活，快去看看别的吧！";
            }
            else
            {
                result = "邮箱激活失败，请稍后再试！";
            }
            return result;
        }


    }
}
