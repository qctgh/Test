namespace PersonalWebsite.Service.Entity
{
    public class TimingEntity : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public string Time { get; set; }
        /// <summary>
        /// 重复
        /// </summary>
        public string Weeks { get; set; }
    }
}
