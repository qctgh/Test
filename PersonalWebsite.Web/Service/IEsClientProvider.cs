using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Web.Service
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
