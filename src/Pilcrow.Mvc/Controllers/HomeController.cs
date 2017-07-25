using System;

using Microsoft.AspNetCore.Mvc;

namespace Pilcrow.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
