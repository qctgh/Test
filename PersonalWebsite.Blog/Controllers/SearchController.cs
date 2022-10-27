using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using PersonalWebsite.DTO;
using PersonalWebsite.Blog.Service;
using Result = PersonalWebsite.Blog.Models.Result;

namespace PersonalWebsite.Blog.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ElasticClient _client;
        public SearchController(ILogger<SearchController> logger, IEsClientProvider clientProvider)
        {
            _logger = logger;
            _client = clientProvider.GetClient();
        }

        public IActionResult Index(string keyword)
        {
            ViewBag.KeyWord = keyword;
            return View();
        }
        //搜索列表
        public IActionResult SearchList(string keyword, int page)
        {
            //todo:搜索规则待调整
            var articleDTOs = _client.Search<ArticleDTO>(s => s
                 .From((page - 1) * 3)
                 .Size(3)
                 .Query(q => q.QueryString(qs => qs.Query(keyword).DefaultOperator(Operator.And)))).Documents.ToArray();
            Result result = new Result
            {
                Code = 0,
                Data = articleDTOs,
                //总页数
                Count = Convert.ToInt32(Math.Ceiling(articleDTOs.Length / 3.0))
            };
            return Json(result);
        }
    }
}