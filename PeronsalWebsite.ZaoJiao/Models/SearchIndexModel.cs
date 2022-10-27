using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class SearchIndexModel
    {
        /// <summary>
        /// 歌曲集合
        /// </summary>
        public SongDTO[] Songs { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 歌曲总数
        /// </summary>
        public int Count { get; set; }
    }
}
