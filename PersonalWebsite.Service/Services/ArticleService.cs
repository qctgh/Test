using PersonalWebsite.IService;
using PersonalWebsite.Service.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.Service.Services
{
    public class ArticleService : IArticleService
    {
        public bool AddArticle(string title, string introduce, int channelId, string content, int userId)
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                //CommonService<ArticleEntity> cs = new CommonService<ArticleEntity>(ctx);
                ArticleEntity article = new ArticleEntity();
                article.Title = title;
                article.Introduce = introduce;
                article.Content = content;
                ctx.ArticleEntities.Add(article);
                return ctx.SaveChanges() > 0;
            }
        }
    }
}
