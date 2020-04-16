using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class AdminUserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }

        public int LoginErrorTimes { get; set; }
        public DateTime? LastLoginErrorDateTime { get; set; }

        public ICollection<RoleEntity> Roles { get; set; }

        public ICollection<AdminUserRolesEntity> AdminUserRoles { get; set; }
    }
}
