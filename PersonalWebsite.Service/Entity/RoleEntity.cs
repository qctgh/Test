using System.Collections.Generic;

namespace PersonalWebsite.Service.Entity
{
    public class RoleEntity : BaseEntity
    {
        public string Name { get; set; }


        //既可以一张表对应一个Entity，关系表也建立实体，也可以像这样直接让对象带属性，隐式的关系表
        //public virtual ICollection<PermissionEntity> Permissions { get; set; } = new List<PermissionEntity>();

        //public ICollection<AdminUserEntity> AdminUsers { get; set; }

        public ICollection<AdminUserRolesEntity> AdminUserRoles { get; set; }

        public ICollection<RolePermissionsEntity> RolesPermissions { get; set; } = new List<RolePermissionsEntity>();
    }
}