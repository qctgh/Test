using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.AdminWeb.Models
{
    public class ArticleModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long ChannelId { get; set; }
        public string Content { get; set; }
        public int SupportCount { get; set; }
        public bool IsFirst { get; set; }
        public long UserId { get; set; }
    }
}
