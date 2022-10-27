using System.Collections.Generic;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class UserSettingModel
    {
        public string Time { get; set; }
        public string Weeks { get; set; }

        public Dictionary<string, string> CBWeeks { get; set; }
    }
}
