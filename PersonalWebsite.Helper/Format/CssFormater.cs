using System.Text.RegularExpressions;

namespace PersonalWebsite.Helper.Format
{
    public static class CssFormater
    {
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Format(string s)
        {

            s = Regex.Replace(s, @"/\s*([\{\}\:\;\,])\s*/g", "$1");
            s = Regex.Replace(s, @"/;\s *;/ g", ";"); //清除连续分号
            s = Regex.Replace(s, @"/\,[\s\.\#\d]*{/g", "{");
            s = Regex.Replace(s, @"/([^\s])\{([^\s])/g", "$1 {\n\t$2");
            s = Regex.Replace(s, @"/([^\s])\}([^\n]*)/g", "$1\n}\n$2");
            s = Regex.Replace(s, @"/([^\s]);([^\s\}])/g", "$1;\n\t$2");
            return s;
        }
        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Pack(string s)
        {
            s = Regex.Replace(s, @"/\/\*(.|\n)*?\*\//g", ""); //删除注释
            s = Regex.Replace(s, @"/\s*([\{\}\:\;\,])\s*/g", "$1");
            s = Regex.Replace(s, @"/\,[\s\.\#\d]*\{/g", "{"); //容错处理
            s = Regex.Replace(s, @"/;\s*;/ g", ";"); //清除连续分号
            return (s == null) ? "" : s.Trim();
        }
    }
}
