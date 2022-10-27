using PersonalWebsite.DTO;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class MessageIndexModel
    {
        public int Count { get; set; }
        public MessageDTO[] Messages { get; set; }
        public string Page { get; set; }
    }
}
