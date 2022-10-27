using PersonalWebsite.DTO;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class SongMenuDetailModel
    {
        public SongMenuDTO SongMenu { get; set; }
        //public string Songs { get; set; }
        public SongDTO[] Songs { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public string Page { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }
    }
}
