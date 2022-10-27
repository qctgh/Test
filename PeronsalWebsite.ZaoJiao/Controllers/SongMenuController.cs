using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;
using System;
using System.Collections.Generic;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class SongMenuController : Controller
    {
        protected ISongMenuService SongMenuService;
        protected ISongService SongService;
        protected IKeyValueService KeyValueService;
        public SongMenuController(ISongMenuService SongMenuService, ISongService SongService, IKeyValueService KeyValueService)
        {
            this.SongMenuService = SongMenuService;
            this.SongService = SongService;
            this.KeyValueService = KeyValueService;
        }


        public IActionResult Index(int pageIndex = 1)
        {
            SongMenuIndexModel model = new SongMenuIndexModel();
            var tags = KeyValueService.GetTags();
            var songMenus = SongMenuService.GetAll(8, (pageIndex - 1) * 8);


            //分页
            var ps = new PageString();

            /*可选参数*/

            ps.SetIsEnglish = false;// 是否是英文       (默认：false)
            ps.SetIsShowText = false;//是否显示分页文字 (默认：true)
                                     //ps.TextFormat=""                         (默认值：《span class=\"pagetext\"》《strong》总共《/strong》:{0} 条 《strong》当前《/strong》:{1}/{2}《/span》)
                                     //ps.SetPageIndexName  Request["pageIndex"](默认值："pageIndex")
            ps.SetIsAjax = false;//                    (默认值："false")

            /*函数参数*/
            int total = (int)SongMenuService.Count();
            int pageSize = 8;
            string url = Url.Content("~/SongMenu/Index?");
            var page = ps.ToString(total, pageSize, pageIndex, url);


            model.Tags = tags;
            model.SongMenus = songMenus;
            model.Page = page;
            return View(model);
        }

        //歌单详情页
        public IActionResult DetailBak(long id)
        {
            SongMenuDetailModel model = new SongMenuDetailModel();
            var songMunu = SongMenuService.GetById(id);
            var songs = SongService.GetBySongMenuId(id);

            List<SongModel> songModels = new List<SongModel>();
            foreach (var item in songs)
            {
                SongModel songModel = new SongModel();
                songModel.title = item.Name;
                songModel.artist = item.Artist;
                songModel.cover = item.Cover;
                songModel.mp3 = Url.Content($"~{item.Mp3}");
                songModel.background = item.Cover;
                songModels.Add(songModel);
            }
            //model.Songs = JsonConvert.SerializeObject(songModels);
            model.SongMenu = songMunu;
            return View(model);
        }
        //歌单详情页
        public IActionResult Detail(long id, int pageIndex = 1)
        {
            SongMenuDetailModel model = new SongMenuDetailModel();
            var songMunu = SongMenuService.GetById(id);
            var songs = SongService.GetBySongMenuId(id, 10, (pageIndex - 1) * 10);

            //分页
            var ps = new PageString();

            /*可选参数*/

            ps.SetIsEnglish = false;// 是否是英文       (默认：false)
            ps.SetIsShowText = false;//是否显示分页文字 (默认：true)
                                     //ps.TextFormat=""                         (默认值：《span class=\"pagetext\"》《strong》总共《/strong》:{0} 条 《strong》当前《/strong》:{1}/{2}《/span》)
                                     //ps.SetPageIndexName  Request["pageIndex"](默认值："pageIndex")
            ps.SetIsAjax = false;//                    (默认值："false")

            /*函数参数*/
            int total = (int)SongService.GetCountBySongMenuId(id);
            int pageSize = 10;
            string url = Url.Content($"~/SongMenu/Detail/{id}?");
            var page = ps.ToString(total, pageSize, pageIndex, url);

            model.Songs = songs;
            model.SongMenu = songMunu;
            model.Page = page;
            model.PageIndex = pageIndex;
            return View(model);
        }
        //歌曲列表
        public IActionResult List(int page)
        {
            var articles = SongMenuService.GetAll(6, (page - 1) * 6);
            //天花板，3.0取3,3.1取4
            var count = Math.Ceiling(SongMenuService.Count() / 6.0);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                //总页数
                Count = Convert.ToInt32(count)
            };
            return Json(result);
        }
    }
}