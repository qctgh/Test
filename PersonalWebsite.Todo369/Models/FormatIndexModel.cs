namespace PersonalWebsite.Todo369.Models
{
    public class FormatIndexModel
    {
        /// <summary>
        /// 当前选中项
        /// </summary>
        public string Current { get; set; } = "html";

        public string HtmlText { get; set; } = @"<html><body><h1>我的第一个标题</h1><p>我的第一个段落。</p></body></html>";
        public string HtmlResult { get; set; }
        public string CSSText { get; set; } = @"p{color:rgb(255,0,0);}p{color:rgb(100%,0%,0%);}";
        public string CSSResult { get; set; }
        public string JsonText { get; set; } = "{\"employees\":[{\"firstName\":\"Bill\",\"lastName\":\"Gates\"},{\"firstName\":\"George\",\"lastName\":\"Bush\"},{\"firstName\":\"Thomas\",\"lastName\":\"Carter\"}]}";
        public string JsonResult { get; set; }
        public string XmlText { get; set; } = "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><note><to>George</to><from>John</from><heading>Reminder</heading><body>Don't forget the meeting!</body></note>";
        public string XmlResult { get; set; }
        public string SqlText { get; set; } = "SELECT column_name(s) FROM table_name1 LEFT JOIN table_name2 ON table_name1.column_name=table_name2.column_name";
        public string SqlResult { get; set; }
    }
}
