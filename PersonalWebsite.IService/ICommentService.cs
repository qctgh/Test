using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface ICommentService : IServiceSupport
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="content">内容</param>
        /// <param name="ip">评论者IP</param>
        /// <returns></returns>
        long Add(long articleId, string content, string ip);

        /// <summary>
        /// 添加评论(过滤词处理)
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <param name="content">内容</param>
        /// <param name="ip">评论者IP</param>
        /// <returns></returns>
        string AddComment(long articleId, string content, string ip);
        /// <summary>
        /// 获取评论集合根据文章ID
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        CommentDTO[] GetByArticleId(long articleId);
    }

}
