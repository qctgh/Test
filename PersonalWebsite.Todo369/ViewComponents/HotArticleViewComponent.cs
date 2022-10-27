using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalWebsite.Todo369.ViewComponents
{
    public class HotArticleViewComponent : ViewComponent
    {
        private readonly IArticleService ArticleService;

        public HotArticleViewComponent(IArticleService ArticleService)
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
            //热门
            var hotArticles = ArticleService.GetHot(10);

            return hotArticles.ToAsyncEnumerable().ToArray();
        }
    }
}
