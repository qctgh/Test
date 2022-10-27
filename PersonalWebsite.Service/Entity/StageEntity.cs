using System.Collections.Generic;

namespace PersonalWebsite.Service.Entity
{
    public class StageEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 歌单集合
        /// </summary>
        public string SongMenus { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
    }
}
