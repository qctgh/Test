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
        ArticleDTO AddArticle(string title, long channelId, string content, int supportCount, bool isFirst, long userId);
        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <returns></returns>
        ArticleDTO[] GetAll();
        /// <summary>
        /// 获取全部记录分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        ArticleDTO[] GetAll(int pageSize, int currentIndex);

        /// <summary>
        /// 获取文章根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleDTO GetById(long id);
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="title"></param>
        /// <param name="channelId"></param>
        /// <param name="content"></param>
        /// <param name="supportCount"></param>
        /// <param name="isFirst"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        bool Edit(long id, string title, long channelId, string content, int supportCount, bool isFirst, long userId);
        /// <summary>
        /// 删除文章根据ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        void DeleteById(long id);
        /// <summary>
        /// 审核文章
        /// </summary>
        /// <param name="id"></param>
        bool CheckById(long id);
    }
}
