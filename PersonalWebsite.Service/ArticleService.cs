using Microsoft.EntityFrameworkCore;
using PersonalWebsite.DTO;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.Service.Entity;
using System;
using System.Linq;

namespace PersonalWebsite.Service
{
    public class ArticleService : IArticleService 
    {
        private readonly MyDbContext ctx;
        public ArticleService(MyDbContext ctx)
        {
            this.ctx = ctx;
        }

        public ArticleDTO AddArticle(string title, string @abstract, int classification, string cover, long channelId, string content, int supportCount, bool isFirst, long userId)
        {

            ArticleEntity article = new ArticleEntity();
            article.Title = title;
            article.Abstract = @abstract;
            article.Classification = classification;
            article.Cover = cover;
            article.ChannelId = channelId;
            article.Content = content;
            article.SupportCount = supportCount;
            article.IsFirst = isFirst;
            article.UserId = userId;
            ctx.Articles.Add(article);
            ctx.SaveChanges();
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().Where(w => w.Id == article.Id).Select(p => ToDTO(p)).FirstOrDefault();

        }



        public ArticleDTO[] GetAll()
        {
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User
            })).ToArray();

        }
        public ArticleDTO[] GetAll(int pageSize, int currentIndex)
        {

            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }
        public ArticleDTO[] GetAll(string title, int pageSize, int currentIndex)
        {

            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).Where(p => p.Title.Contains(title)).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize);

            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }

        public long Count()
        {
            return ctx.Articles.Count();
        }
        public long Count(string title)
        {
            return ctx.Articles.Where(p => p.Title.Contains(title)).Count();
        }

        public ArticleDTO[] GetByUserId(long userId)
        {
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().Where(p => p.UserId == userId).OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User
            })).ToArray();
        }
        /// <summary>
        /// 获取文章根据用户ID
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public ArticleDTO[] GetByUserId(long userId, int pageSize, int currentIndex)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.UserId == userId).OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User
            })).ToArray();
        }
        /// <summary>
        /// 获取图文文章总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int GetImageAndTextCountByUserId(long userId)
        {
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().Where(p => p.UserId == userId && p.Classification == 2).Count();
        }
        /// <summary>
        /// 获取图文文章根据用户ID（分页）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        public ArticleDTO[] GetImageAndTextByUserId(long userId, int pageSize, int currentIndex)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.UserId == userId && p.Classification == 2).OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                Abstract = p.Abstract,
                Cover = p.Cover
            })).ToArray();
        }
        public ArticleDTO[] GetAllField()
        {
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).AsNoTracking().OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();
        }

        public long GetByChannelIdCount(string code)
        {
            var channel = ctx.Channels.FirstOrDefault(p => p.Code == code);
            return ctx.Articles.AsNoTracking().Where(p => p.ChannelId == channel.Id).Count();
        }

        public ArticleDTO[] GetByChannelId(string code)
        {

            var channel = ctx.Channels.FirstOrDefault(p => p.Code == code);
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.ChannelId == channel.Id).OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }

        public ArticleDTO[] GetAllFieldByChannelId(string code)
        {
            var channel = ctx.Channels.FirstOrDefault(p => p.Code == code);
            return ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.ChannelId == channel.Id).OrderByDescending(p => p.CreateDateTime).Select(p => ToDTO(p)).ToArray();
        }

        public ArticleDTO[] GetByChannelId(string code, int pageSize, int currentIndex)
        {

            var channel = ctx.Channels.FirstOrDefault(p => p.Code == code);
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.ChannelId == channel.Id).OrderByDescending(p => p.CreateDateTime).Skip(currentIndex).Take(pageSize);
            if (code == "huangdi")
            {
                articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.ChannelId == channel.Id).OrderBy(p => p.Id).Skip(currentIndex).Take(pageSize);
            }
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }

        public ArticleDTO[] GetTop()
        {

            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.IsFirst == true).OrderByDescending(p => p.CreateDateTime);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }

        public ArticleDTO[] GetTop(int count)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.IsFirst == true).OrderByDescending(p => p.CreateDateTime).Take(count);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();
        }

        public ArticleDTO GetTop(long userId)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.UserId == userId && p.Classification == 2 && p.IsFirst == true).OrderByDescending(p => p.CreateDateTime).Take(1);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                Abstract = p.Abstract,
                Cover = p.Cover
            })).FirstOrDefault();
        }

        public ArticleDTO[] GetHot(int count)
        {

            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().OrderByDescending(p => p.Comments.Count).Take(count);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();

        }

        public ArticleDTO[] GetRecommend(int count)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().OrderByDescending(p => p.SupportCount).Take(count);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();
        }

        public ArticleDTO[] Search(string kw, int pageSize, int currentIndex)
        {
            var articles = ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).AsNoTracking().Where(p => p.Title.Contains(kw) || p.Content.Contains(kw)).Skip(currentIndex).Take(pageSize);
            return articles.Select(p => ToDTO(new ArticleEntity
            {
                Id = p.Id,
                ChannelId = p.ChannelId,
                Channel = p.Channel,
                IsFirst = p.IsFirst,
                IsVisible = p.IsVisible,
                StaticPath = p.StaticPath,
                SupportCount = p.SupportCount,
                CreateDateTime = p.CreateDateTime,
                Title = p.Title,
                UserId = p.UserId,
                User = p.User,
                Comments = p.Comments
            })).ToArray();
        }

        public ArticleDTO GetById(long id)
        {

            return ToDTO(ctx.Articles.Include(p => p.Channel).Include(p => p.User).Include(p => p.Comments).SingleOrDefault(p => p.Id == id));

        }

        public bool Edit(long id, string title, string @abstract, int classification, string cover, long channelId, string content, int supportCount, bool isFirst, long userId)
        {
            try
            {

                var article = ctx.Articles.Single(p => p.Id == id);
                article.Title = title;
                article.Abstract = @abstract;
                article.Classification = classification;
                article.Cover = cover;
                article.ChannelId = channelId;
                article.Content = content;
                article.SupportCount = supportCount;
                article.IsFirst = isFirst;
                article.UserId = userId;
                ctx.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private ArticleDTO ToDTO(ArticleEntity article)
        {
            ArticleDTO dto = new ArticleDTO();
            dto.Id = article.Id;
            dto.ChannelId = article.ChannelId;
            dto.ChannelCode = article.Channel?.Code;
            dto.ChannelName = article.Channel?.Name;
            dto.Content = article.Content;
            dto.IsFirst = article.IsFirst;
            dto.IsVisible = article.IsVisible;
            dto.StaticPath = article.StaticPath;
            dto.SupportCount = NumberHelper.Simple(article.SupportCount.ToString());
            dto.PublishDate = DateTimeHelper.DateTimeToString(article.CreateDateTime);
            dto.CreateDateTime = article.CreateDateTime;
            dto.Year = article.CreateDateTime.Year;
            dto.Month = article.CreateDateTime.Month;
            dto.Day = article.CreateDateTime.Day;
            dto.Title = article.Title;
            dto.Abstract = article.Abstract;
            dto.Classification = article.Classification;
            dto.Cover = article.Cover;
            dto.UserId = article.UserId;
            dto.UserName = article.User?.Name;
            dto.CommentsCount = article.Comments == null ? 0 : article.Comments.Count;
            return dto;
        }
        private ArticleDTO ToDTOSimple(ArticleEntity article)
        {
            ArticleDTO dto = new ArticleDTO();
            dto.Id = article.Id;
            dto.ChannelId = article.ChannelId;
            dto.ChannelCode = article.Channel?.Code;
            dto.ChannelName = article.Channel?.Name;
            dto.Content = article.Content.Length > 300 ? article.Content.Substring(0, 300) + "……" : article.Content;
            dto.IsFirst = article.IsFirst;
            dto.IsVisible = article.IsVisible;
            dto.StaticPath = article.StaticPath;
            dto.SupportCount = NumberHelper.Simple(article.SupportCount.ToString());
            dto.PublishDate = DateTimeHelper.DateTimeToString(article.CreateDateTime);
            dto.CreateDateTime = article.CreateDateTime;
            dto.Title = article.Title;
            dto.Abstract = article.Abstract;
            dto.Classification = article.Classification;
            dto.Cover = article.Cover;
            dto.UserId = article.UserId;
            dto.UserName = article.User.Name;
            dto.CommentsCount = article.Comments == null ? 0 : article.Comments.Count;
            return dto;
        }


        public void DeleteById(long id)
        {
            var article = ctx.Articles.SingleOrDefault(p => p.Id == id);
            ctx.Remove(article);
            ctx.SaveChanges();

        }
        public bool CheckById(long id)
        {

            var article = ctx.Articles.SingleOrDefault(p => p.Id == id);
            article.IsVisible = true;
            return ctx.SaveChanges() > 0;

        }
        public bool Love(long id)
        {
            var article = ctx.Articles.SingleOrDefault(p => p.Id == id);
            article.SupportCount += 1;
            return ctx.SaveChanges() > 0;
        }

    }
}
