using System.Collections.Generic;

namespace PersonalWebsite.Service.Entity
{
    public class SongMenuEntity : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// 封面图片路径
        /// </summary>
        public string CoverImgSrc { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 歌曲
        /// </summary>
        public ICollection<SongEntity> Songs { get; set; }
    }
}
