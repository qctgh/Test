using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.AdminWeb
{
    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }
    }
}
