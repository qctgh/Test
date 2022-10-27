using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class FilterWordEntity : BaseEntity
    {
        /// <summary>
        /// 单词模式
        /// </summary>
        public String WordPattern { get; set; }
        /// <summary>
        /// 替换词
        /// </summary>
        public String ReplaceWord { get; set; }
    }
}
