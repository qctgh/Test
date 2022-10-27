using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalWebsite.DTO
{
    public class FilterWordDTO : BaseDTO
    {
        public String WordPattern { get; set; }
        public String ReplaceWord { get; set; }
    }

}
