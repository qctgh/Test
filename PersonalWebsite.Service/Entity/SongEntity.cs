using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class SongEntity : BaseEntity
    {
        /// <summary>
        /// 歌名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 专辑封皮
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 歌曲路径
        /// </summary>
        public string Mp3 { get; set; }
        /// <summary>
        /// 歌单ID
        /// </summary>
        public long SongMenuId { get; set; }
        public SongMenuEntity SongMenu { get; set; }
    }
}

