using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;
using System.Threading.Tasks;

namespace PersonalWebsite.ZaoJiao.ViewComponents
{
    public class StageViewComponent : ViewComponent
    {
        private readonly IStageService StageService;
        public StageViewComponent(IStageService StageService)
        {
            this.StageService = StageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id, long parentId)
        {
            var items = await GetItemsAsync(id, parentId);
            return View(items);
        }
        private Task<StageModel> GetItemsAsync(long id, long parentId)
        {
            StageModel model = new StageModel();
            model.Stages = StageService.GetAll();
            return Task.FromResult(model);
        }
    }
}
