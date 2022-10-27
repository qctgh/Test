using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper;
using PersonalWebsite.IService;
using PersonalWebsite.ZaoJiao.Models;

namespace PersonalWebsite.ZaoJiao.Controllers
{
    public class SearchController : Controller
    {
        ISongService SongService;
        public SearchController(ISongService SongService)
        {
            this.SongService = SongService;
        }

        public IActionResult Index(string key, int pageIndex = 1)
        {
            SearchIndexModel model = new SearchIndexModel();
            var songs = SongService.GetByKey(key, 8, (pageIndex - 1) * 8);
            int count = SongService.GetCountByKey(key);
            //分页
            var ps = new PageString();

            /*可选参数*/

            ps.SetIsEnglish = false;// 是否是英文       (默认：false)
            ps.SetIsShowText = false;//是否显示分页文字 (默认：true)
                                    //ps.TextFormat=""                         (默认值：《span class=\"pagetext\"》《strong》总共《/strong》:{0} 条 《strong》当前《/strong》:{1}/{2}《/span》)
                                    //ps.SetPageIndexName  Request["pageIndex"](默认值："pageIndex")
            ps.SetIsAjax = false;//                    (默认值："false")

            /*函数参数*/
            int total = count;
            int pageSize = 8;
            string url = Url.Content($"~/Search?key={key}&");
            var page = ps.ToString(total, pageSize, pageIndex, url);
            model.Count = count;
            model.Key = key;
            model.Page = page;
            model.Songs = songs;
            return View(model);
        }
    }
}