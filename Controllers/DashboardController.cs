﻿using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
