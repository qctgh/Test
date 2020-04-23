using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.DTO
{
    public class ChannelDTO
    {
        public long Id { get; set; }
        public long Name { get; set; }
        public long ParentId { get; set; }
        public string ParentName { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateTime { get; set; }
    }
}
