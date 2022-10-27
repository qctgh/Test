using PersonalWebsite.DTO;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class SongMenuIndexModel
    {
        /// <summary>
        /// 歌单集合
        /// </summary>
        public SongMenuDTO[] SongMenus { get; set; }
        /// <summary>
        /// 标签集合
        /// </summary>
        public TagDTO[] Tags { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public string Page { get; set; }

    }
}
