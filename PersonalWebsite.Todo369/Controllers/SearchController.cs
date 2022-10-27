using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using PersonalWebsite.DTO;
using PersonalWebsite.IService;
using PersonalWebsite.Todo369.Models;
using PersonalWebsite.Todo369.Service;
using System.Linq;
using Result = PersonalWebsite.Todo369.Models.Result;

namespace PersonalWebsite.Todo369.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ElasticClient _client;
        IChannelService ChannelService { get; set; }
        IArticleService ArticleService { get; set; }

        public SearchController(ILogger<SearchController> logger, IEsClientProvider clientProvider, IChannelService ChannelService, IArticleService ArticleService)
        {
            _logger = logger;
            _client = clientProvider.GetClient();
            this.ChannelService = ChannelService;
            this.ArticleService = ArticleService;
        }

        public IActionResult Index(string kw)
        {
            SearchIndexModel model = new SearchIndexModel();
            model.KeyWord = kw;

            return View(model);
        }
        //搜索列表
        public IActionResult SearchList(string kw, int page)
        {
            //todo:搜索规则待调整
            var articleDTOs = _client.Search<ArticleDTO>(s => s
                 .Query(q => q.QueryString(qs => qs.Query(kw).DefaultOperator(Operator.And)))).Documents.ToArray();
            Result result = new Result
            {
                Code = 0,
                Data = articleDTOs,
                //总页数
                Count = 1
            };
            return Json(result);
        }
        /// <summary>
        /// 因服务器配置低，ES运行占用内存比较大，暂且用like的方式处理搜索
        /// </summary>
        /// <param name="kw"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IActionResult Search(string kw, int page)
        {
            var articles = ArticleService.Search(kw, 10, (page - 1) * 10);
            Result result = new Result
            {
                Code = 0,
                Data = articles,
                //总页数
                Count = articles.Count()
            };
            return Json(result);
        }
    }
}