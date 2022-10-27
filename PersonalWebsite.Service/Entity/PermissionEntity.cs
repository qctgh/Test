using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class PermissionEntity : BaseEntity
    {
        public string Description { get; set; }
        public string Name { get; set; }

        public ICollection<RoleEntity> Roles { get; set; }

        public ICollection<RolePermissionsEntity> RolePermissions { get; set; }
    }
}
