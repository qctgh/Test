using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.AdminWeb.Models;
using PersonalWebsite.IService;

namespace PersonalWebsite.AdminWeb.Controllers
{
    public class CommentController : Controller
    {
        ICommentService CommentService { get; set; }

        public CommentController(ICommentService CommentService)
        {
            this.CommentService = CommentService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            return View();
        }
        [HttpPost]
        public IActionResult List(ArticleModel model, int page, int limit)
        {
            var comments = CommentService.GetAll(limit, (page - 1) * limit);
            Result result = new Result();
            result.Code = 0;
            result.Data = comments;
            result.Count = CommentService.GetAll().Length;
            return Json(result);
        }

        public IActionResult Check(long id)
        {
            Result result = new Result();
            bool bResult = CommentService.Check(id);
            if (bResult)
            {
                result.Code = 0;
                result.Msg = "审核通过";
            }
            else
            {
                result.Code = 1;
                result.Msg = "审核失败";
            }
            return Json(result);
        }

        public IActionResult Del(long id)
        {
            Result result = new Result();
            bool bResult = CommentService.Del(id);
            if (bResult)
            {
                result.Code = 0;
                result.Msg = "删除成功";
            }
            else
            {
                result.Code = 1;
                result.Msg = "删除失败";
            }
            return Json(result);
        }
    }
}