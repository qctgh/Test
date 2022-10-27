namespace PersonalWebsite.DTO
{
    public class SongMenuDTO : BaseDTO
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
        /// 歌曲数量
        /// </summary>
        public int? SongCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string DateTime { get; set; }

    }
    
}
