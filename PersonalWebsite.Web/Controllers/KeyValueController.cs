using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonalWebsite.IService;

namespace PersonalWebsite.Web.Controllers
{
    public class KeyValueController : Controller
    {
        public IKeyValueService KeyValueService { get; set; }

        public KeyValueController(IKeyValueService KeyValueService)
        {
            this.KeyValueService = KeyValueService;
        }

        public IActionResult Index()
        {
            var keyValue = KeyValueService.GetAll("key1");

            return Content(keyValue[0].Key + keyValue[0].Value);
        }
    }
}