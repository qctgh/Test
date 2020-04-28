using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersonalWebsite.Service
{
    public class ArticleService : IArticleService
    {
        public bool AddArticle(string title, long channelId, string content, int supportCount, bool isFirst, long userId)
        {

            using (MyDbContext ctx = new MyDbContext())
            {
                ArticleEntity article = new ArticleEntity();
                article.Title = title;
                article.ChannelId = channelId;
                article.Content = content;
                article.SupportCount = supportCount;
                article.IsFirst = isFirst;
                article.UserId = userId;
                ctx.Articles.Add(article);
                return ctx.SaveChanges() > 0;
            }
        }

        public ArticleDTO[] GetAll()
        {
            using (MyDbContext ctx = new MyDbContext())
            {
                return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().Select(p => ToDTO(p)).ToArray();
            }
        }

        private ArticleDTO ToDTO(ArticleEntity article)
        {
            ArticleDTO dto = new ArticleDTO();
            dto.Id = article.Id;
            dto.ChannelId = article.ChannelId;
            dto.ChannelName = article?.Channel.Name;
            dto.Content = article.Content;
            dto.IsFirst = article.IsFirst;
            dto.IsVisible = article.IsVisible;
            dto.StaticPath = article.StaticPath;
            dto.SupportCount = article.SupportCount;
            dto.CreateDateTime = article.CreateDateTime;
            dto.Title = article.Title;
            dto.UserId = article.UserId;
            dto.UserName = article.User.PhoneNum;
            return dto;
        }
    }
}
