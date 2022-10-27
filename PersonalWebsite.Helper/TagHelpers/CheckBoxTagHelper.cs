using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Helper.TagHelpers
{
    public class CheckBoxTagHelper : TagHelper
    {
        public List<CheckBoxItem> Items { set; get; } = new List<CheckBoxItem>();
        public string Name { set; get; } = Guid.NewGuid().ToString();
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "checkbox";
            if (Items != null && Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    var checkbox = new TagBuilder("input");
                    checkbox.TagRenderMode = TagRenderMode.SelfClosing;
                    var item = Items[i];
                    checkbox.Attributes.Add("id", item.Id);
                    checkbox.Attributes.Add("name", Name);
                    if (item.Checked)
                    {
                        checkbox.Attributes.Add("checked", "checked");
                    }
                    if (item.Disabled)
                    {
                        checkbox.Attributes.Add("disabled", "disabled");
                    }
                    checkbox.Attributes.Add("type", "checkbox");
                    checkbox.Attributes.Add("value", item.Value);
                    checkbox.Attributes.Add("title", item.Text);

                    output.Content.AppendHtml(checkbox);
                }
            }
            base.Process(context, output);
        }
    }
}
