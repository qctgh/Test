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
        bool AddArticle(string title, long channelId, string content, int supportCount, bool isFirst, long userId);

        ArticleDTO[] GetAll();

    }
}
