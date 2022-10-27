using System.IO;
using System.Text;
using System.Xml;

namespace PersonalWebsite.Helper.Format
{
    public static class XmlFormater
    {

        /// <summary>
        /// 格式化XML格式字符串
        /// </summary>
        /// <param name="unformattedXml">原始的XML字符串</param>
        /// <param name="indent">缩进长度</param>
        /// <returns></returns>
        public static string FormatXml(string unformattedXml, int indent = 2)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(unformattedXml);

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            XmlTextWriter xtw = null;

            try
            {
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xtw.Indentation = indent;
                xtw.IndentChar = ' ';

                xd.WriteTo(xtw);
            }

            finally
            {
                if (xtw != null)
                    xtw.Close();
            }

            return sb.ToString();
        }
    }
}
