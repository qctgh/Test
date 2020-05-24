using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using PersonalWebsite.DTO;
using PersonalWebsite.Web.Service;

namespace PersonalWebsite.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ILogger<ArticleController> _logger;
        private readonly ElasticClient _client;
        public SearchController(ILogger<ArticleController> logger, IEsClientProvider clientProvider)
        {
            _logger = logger;
            _client = clientProvider.GetClient();
        }

        public IActionResult Index(string keyword)
        {
            var articleDTOs = _client.Search<ArticleDTO>(s => s
                 .From(0)
                 .Size(10)
                 .Query(q => q.QueryString(qs => qs.Query(keyword).DefaultOperator(Operator.And)))).Documents.ToArray();
            return View();
        }
    }
}