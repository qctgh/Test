namespace PersonalWebsite.Service.Entity
{
    public class ArticleRateEntity : BaseEntity
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public long ArticleId { get; set; }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string IP { get; set; }
    }
}
