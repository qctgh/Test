using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Entity
{
    public class ChannelEntity : BaseEntity
    {
        /// <summary>
        /// 父节点ID
        /// </summary>
        public long ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }
    }
}
