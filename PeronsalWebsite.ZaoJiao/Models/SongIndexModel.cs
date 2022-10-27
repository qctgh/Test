using PersonalWebsite.DTO;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class SongIndexModel
    {
        public SongDTO Song { get; set; }
        public SongDTO[] RandomSongs { get; set; }
    }
}
