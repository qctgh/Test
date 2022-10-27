using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class ArticleRateService : IArticleRateService
    {
        private readonly MyDbContext ctx;
        public ArticleRateService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public void Add(long articleId, string ip)
        {
            ArticleRateEntity articleRate = new ArticleRateEntity()
            {
                ArticleId = articleId,
                IP = ip
            };
            ctx.ArticleRates.Add(articleRate);
            ctx.SaveChanges();
        }

        public int Get24HRateCount(long articleId, string ip)
        {
            return ctx.ArticleRates.Where(p => p.ArticleId == articleId && p.IP == ip && DateTime.Now.Subtract(p.CreateDateTime).Hours <= 24).Count();
        }
    }
}
