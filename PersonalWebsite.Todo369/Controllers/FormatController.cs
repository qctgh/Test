using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.Helper.Format;
using PersonalWebsite.Todo369.Models;

namespace PersonalWebsite.Todo369.Controllers
{
    public class FormatController : Controller
    {
        [HttpGet]
        public IActionResult Index(string id)
        {
            FormatIndexModel model = new FormatIndexModel();
            model.Current = id;
            return View(model);
        }
        [HttpPost]
        public IActionResult Index(FormatIndexModel model, string id, string type)
        {
            model.Current = id;
            switch (id)
            {
                case "html":
                    if (string.IsNullOrEmpty(model.HtmlText))
                    {
                        return View(model);
                    }
                    if (type == "format")
                    {
                        //异常则加载原文本
                        try
                        {
                            model.HtmlResult = HtmlFormater.ConvertToXml(model.HtmlText, true);
                        }
                        catch
                        {
                            model.HtmlResult = model.HtmlText;
                        }

                    }
                    else
                    {
                        try
                        {
                            model.HtmlResult = HtmlFormater.Compress(model.HtmlText);
                        }
                        catch
                        {
                            model.HtmlResult = model.HtmlText;
                        }

                    }
                    break;
                case "css":
                    if (string.IsNullOrEmpty(model.CSSText))
                    {
                        return View(model);
                    }
                    if (type == "format")
                    {
                        model.CSSResult = CssFormater.Format(model.CSSText);
                    }
                    else
                    {
                        model.CSSResult = CssFormater.Pack(model.CSSText);
                    }
                    break;
                case "json":
                    if (string.IsNullOrEmpty(model.JsonText))
                    {
                        return View(model);
                    }
                    if (type == "format")
                    {
                        try
                        {
                            model.JsonResult = JsonFormater.ConvertJsonString(model.JsonText);
                        }
                        catch
                        {
                            model.JsonResult = model.JsonText;
                        }
                    }
                    else
                    {
                        try
                        {
                            model.JsonResult = JsonFormater.Compress(model.JsonText);
                        }
                        catch
                        {
                            model.JsonResult = model.JsonText;
                        }
                    }
                    break;
                case "xml":
                    if (string.IsNullOrEmpty(model.XmlText))
                    {
                        return View(model);
                    }
                    if (type == "format")
                    {
                        try
                        {
                            model.XmlResult = XmlFormater.FormatXml(model.XmlText);
                        }
                        catch
                        {
                            model.XmlResult = model.XmlText;
                        }
                    }
                    break;
                case "sql":
                    if (string.IsNullOrEmpty(model.SqlText))
                    {
                        return View(model);
                    }
                    if (type == "format")
                    {
                        model.SqlResult = HtmlFormater.ConvertToXml(model.SqlText, true);
                    }
                    else
                    {
                        model.SqlResult = HtmlFormater.Compress(model.SqlText);
                    }
                    break;
            }
            return View(model);
        }
    }
}