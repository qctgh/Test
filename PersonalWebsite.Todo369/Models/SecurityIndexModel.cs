namespace PersonalWebsite.Todo369.Models
{
    public class SecurityIndexModel
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        public string Current { get; set; } = "aes";

        public string AESText { get; set; }
        public string AESKey { get; set; }
        public string AESResult { get; set; }
        public string Base64Text { get; set; }
        public string Base64Result { get; set; }
        public string DESText { get; set; }
        public string DESKey { get; set; }
        public string DESResult { get; set; }
        public string MD5Text { get; set; }
        public string MD5Result { get; set; }
        public string RSAText { get; set; }
        public string RSAResult { get; set; }
        public string URLText { get; set; }
        public string URLResult { get; set; }

    }
}
