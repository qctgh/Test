using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class AdminUserRolesEntity : BaseEntity
    {
        /// <summary>
        /// 管理员用户Id
        /// </summary>
        public long AdminUserId { get; set; }

        public AdminUserEntity AdminUser { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        public RoleEntity Role { get; set; }
    }
}


