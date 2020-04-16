using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class RolePermissionsEntity : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public long RoleId { get; set; }
        public RoleEntity Role { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long PermissionId { get; set; }
        public PermissionEntity Permission { get; set; }
    }
}
