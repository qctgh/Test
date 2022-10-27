using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;

namespace PersonalWebsite.Todo369.Controllers
{
    public class EventController : Controller
    {
        private readonly IDistributedCache _distributedCache;
        public EventController(IDistributedCache _distributedCache)
        {
            this._distributedCache = _distributedCache;
        }

        public IActionResult Index()
        {
            ViewBag.NowYear = DateTime.Now.Year + 1;
            return View();
        }

    }
}