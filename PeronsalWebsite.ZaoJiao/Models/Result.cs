using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.ZaoJiao.Models
{
    public class Result
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public int Count { get; set; }

        public object Data { get; set; }
    }
}
