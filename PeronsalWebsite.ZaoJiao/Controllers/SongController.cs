using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class SongController : Controller
    {
        protected ISongService SongService;

        public SongController(ISongService SongService)
        {
            this.SongService = SongService;
        }

        public IActionResult Index(long id)
        {
            SongIndexModel model = new SongIndexModel();
            var song = SongService.GetById(id);
            var randomSongs = SongService.GetByRandom(3);
            model.Song = song;
            model.RandomSongs = randomSongs;
            return View(model);
        }
        public IActionResult TangPoetry(long id)
        {
            SongTangPoetryModel model = new SongTangPoetryModel();
            var song = SongService.GetById(id);
            var nextSong = SongService.GetTangPoetry();
            model.Song = song;
            model.NextSong = nextSong;
            return View(model);
        }
    }
}