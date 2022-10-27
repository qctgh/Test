using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class StageController : Controller
    {
        IStageService StageService { get; set; }
        ISongMenuService SongMenuService { get; set; }
        public StageController(ISongMenuService SongMenuService, IStageService StageService)
        {
            this.SongMenuService = SongMenuService;
            this.StageService = StageService;
        }
        public IActionResult Index(string id)
        {
            StageIndexModel model = new StageIndexModel();
            //从阶段里获取有哪些歌单
            var stages = StageService.GetSongMenusById(long.Parse(id));
            model.SongMenus = SongMenuService.GetByIds(stages);
            return View(model);
        }



    }
}