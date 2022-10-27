using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DTO
{
    public class CommentDTO : BaseDTO
    {
        public long ArticleId { get; set; }
        public String Content { get; set; }
        public bool IsVisible { get; set; }
        public int LikeCount { get; set; }
        public String IP { get; set; }
        public string CommentDate { get; set; }

    }

}
