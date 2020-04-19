using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Web.Models
{
    public class Result<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public int Total { get; set; }

        public List<T> Data { get; set; }
    }
}
