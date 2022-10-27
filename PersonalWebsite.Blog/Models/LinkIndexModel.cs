using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Blog.Models
{
    public class LinkIndexModel
    {
        /// <summary>
        /// 友链集合
        /// </summary>
        public LinkDTO[] Links { get; set; }
    }
}
