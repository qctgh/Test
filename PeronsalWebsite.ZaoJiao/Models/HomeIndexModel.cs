using PersonalWebsite.DTO;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class HomeIndexModel
    {
        public SongMenuDTO[] SongMenus { get; set; }
        /// <summary>
        /// 给您推荐一首歌
        /// </summary>
        public SongDTO Song { get; set; }
    }
}
