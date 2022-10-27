using System.Security.Cryptography;
using System.Text;

namespace PersonalWebsite.Helper.Security
{
    /// <summary>
    /// MD5加密辅助类
    /// </summary>
    public static class MD5Utility
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="value">被加密字符串</param>
        /// <returns></returns>
        public static string MD5Encrypt(string value)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
            for (int i = 0; i < s.Length; i++)
            {
                string ss= s[i].ToString("X2");   
                pwd = pwd + ss;
            }
            return pwd;
        }
    }
}
