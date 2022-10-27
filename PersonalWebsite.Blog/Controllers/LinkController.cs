using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Blog.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.Blog.Controllers
{
    public class LinkController : Controller
    {
        ILinkService LinkService { get; set; }
        public LinkController(ILinkService LinkService)
        {
            this.LinkService = LinkService;
        }
        public IActionResult Index()
        {
            LinkIndexModel model = new LinkIndexModel();
            var links = LinkService.GetAll();
            model.Links = links;
            return View(model);
        }
    }
}