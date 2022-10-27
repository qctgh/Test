using System.ComponentModel.DataAnnotations;

namespace PersonalWebsite.AdminWeb.Models
{
    public class SettingEditModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
