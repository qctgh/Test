namespace PersonalWebsite.AdminWeb.Models
{
    public class SongModel
    {
        /// <summary>
        /// 歌单ID
        /// </summary>
        public long SongMenuId { get; set; }
        /// <summary>
        /// 专辑
        /// </summary>
        public string Album { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 专辑封皮
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 导入的歌曲目录
        /// </summary>
        public string Src { get; set; }
    }
}
