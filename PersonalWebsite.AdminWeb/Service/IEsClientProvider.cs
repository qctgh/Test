using Nest;

namespace PersonalWebsite.AdminWeb.Service
{
    public interface IEsClientProvider
    {
        ElasticClient GetClient();
    }
}
