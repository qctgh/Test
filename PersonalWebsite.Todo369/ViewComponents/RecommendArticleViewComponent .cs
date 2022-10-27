using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Todo369.ViewComponents
{
    public class RecommendArticleViewComponent : ViewComponent
    {
        private readonly IArticleService ArticleService;

        public RecommendArticleViewComponent(IArticleService ArticleService)
        {
            this.ArticleService = ArticleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<ArticleDTO[]> GetItemsAsync()
        {
            //推荐
            var recommendArticles = ArticleService.GetRecommend(10);

            return recommendArticles.ToAsyncEnumerable().ToArray();
        }
    }
}
