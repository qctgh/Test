using PersonalWebsite.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalWebsite.IService
{
    public interface IArticleService : IServiceSupport
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        bool AddArticle(string title, string introduce, int channelId, string content, int userId);

        ArticleDTO[] GetAll();

    }
}
