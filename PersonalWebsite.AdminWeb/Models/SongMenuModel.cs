namespace PersonalWebsite.AdminWeb.Models
{
    public class SongMenuModel
    {
        public long Id { get; set; }
        /// <summary>
        /// 歌单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 封面图片路径
        /// </summary>
        public string CoverImgSrc { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string OrderIndex { get; set; }

    }
}
