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
        long Add(long articleId, string content, string ip, bool isVisible);
        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Del(long id);
        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Check(long id);
        /// <summary>
        /// 获取需要审核的评论所有记录
        /// </summary>
        /// <returns></returns>
        CommentDTO[] GetAll();
        CommentDTO[] GetAll(int pageSize, int currentIndex);
        /// <summary>
        /// 获取评论集合根据文章ID
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        CommentDTO[] GetByArticleId(long articleId);
        CommentDTO[] GetByArticleId(long articleId, int pageSize, int currentIndex);
    }

}
