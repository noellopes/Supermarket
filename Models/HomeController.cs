﻿using Microsoft.AspNetCore.Mvc;

namespace Supermarket.Models
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
