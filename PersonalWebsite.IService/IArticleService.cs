using PersonalWebsite.DTO;

namespace PersonalWebsite.IService
{
    public interface IArticleService : IServiceSupport
    {
        /// <summary>
        /// 添加文章
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        ArticleDTO AddArticle(string title, string @abstract, int classification, string cover, long channelId, string content, int supportCount, bool isFirst, long userId);
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
        /// 获取全部记录分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        ArticleDTO[] GetAll(string title, int pageSize, int currentIndex);

        long Count();

        long Count(string title);
        /// <summary>
        /// 获取用户下的
        /// </summary>
        /// <returns></returns>
        ArticleDTO[] GetByUserId(long userId);
        ArticleDTO[] GetByUserId(long userId, int pageSize, int currentIndex);
        /// <summary>
        /// 获取图文文章总数
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        int GetImageAndTextCountByUserId(long userId);
        /// <summary>
        /// 获取图文文章根据用户ID（分页）
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        ArticleDTO[] GetImageAndTextByUserId(long userId, int pageSize, int currentIndex);
        /// <summary>
        /// 获取全部字段、记录
        /// </summary>
        /// <returns></returns>
        ArticleDTO[] GetAllField();

        long GetByChannelIdCount(string code);
        /// <summary>
        /// 获取某个频道下的全部记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ArticleDTO[] GetByChannelId(string code);
        /// <summary>
        /// 获取某个频道下的全部记录，分页
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        ArticleDTO[] GetByChannelId(string code, int pageSize, int currentIndex);
        /// <summary>
        /// 获取某个频道下全部字段的全部记录
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ArticleDTO[] GetAllFieldByChannelId(string code);
        /// <summary>
        /// 获取置顶的记录
        /// </summary>
        /// <returns></returns>
        ArticleDTO[] GetTop();
        ArticleDTO[] GetTop(int count);
        /// <summary>
        /// 获取某个用户下的置顶
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        ArticleDTO GetTop(long userId);
        /// <summary>
        /// 获取本周热议记录
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ArticleDTO[] GetHot(int count);
        /// <summary>
        /// 获取今日推荐记录
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ArticleDTO[] GetRecommend(int count);

        ArticleDTO[] Search(string kw, int pageSize, int currentIndex);

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
        bool Edit(long id, string title, string @abstract, int classification, string cover, long channelId, string content, int supportCount, bool isFirst, long userId);
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
        /// <summary>
        /// 喜欢文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Love(long id);

    }
}
