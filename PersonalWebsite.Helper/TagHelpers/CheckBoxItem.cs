using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Helper.TagHelpers
{
    /// <summary>
    /// 复选框项
    /// </summary>
    public class CheckBoxItem
    {
        /// <summary>
        /// 复选框id
        /// </summary>
        public string Id { set; get; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 复选框值
        /// </summary>
        public string Value { set; get; }
        /// <summary>
        /// 复选框文本内容
        /// </summary>
        public string Text { set; get; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Checked { set; get; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Disabled { set; get; }
    }
}
