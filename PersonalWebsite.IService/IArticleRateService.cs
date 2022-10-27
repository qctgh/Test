namespace PersonalWebsite.IService
{
    public interface IArticleRateService : IServiceSupport
    {
        int Get24HRateCount(long articleId, string ip);
        void Add(long articleId, string ip);
    }
}
