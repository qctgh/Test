using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Web.Models
{
    public class HomeIndexModel
    {
        /// <summary>
        /// 文章集合
        /// </summary>
        public ArticleDTO[] Articles { get; set; }
    }
}
