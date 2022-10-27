namespace PersonalWebsite.Service.Entity
{
    public class FileEntity : BaseEntity
    {
        /// <summary>
        /// 文件标识
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 上传的用户ID
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 文件Hash值
        /// </summary>
        public string Hash { get; set; }
    }
}
