﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PersonalWebsite.Todo369.Controllers
{
    public class FrontToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}