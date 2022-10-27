using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IMessageService : IServiceSupport
    {
        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="articleId">父级ID</param>
        /// <param name="content">内容</param>
        /// <param name="ip">评论者IP</param>
        /// <returns></returns>
        long Add(int appId, long parentId, string content, long userId, string ip, bool isVisible);
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
        long GetAll(int appId);
        MessageDTO[] GetAll(int appId, int pageSize, int currentIndex);
        /// <summary>
        /// 获取评论集合根据文章ID
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns></returns>
        MessageDTO[] GetByParentId(int appId, long articleId);
        MessageDTO[] GetByParentId(int appId, long articleId, int pageSize, int currentIndex);
    }

}
