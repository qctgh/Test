using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.AdminWeb.Models
{
    public class RoleAddModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public long[] PermissionIds { get; set; }
    }
}
