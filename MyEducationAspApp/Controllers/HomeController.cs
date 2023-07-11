using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeModel
            {
                Counters = RegisterVisitors()
            };
            return View(model);
        }
    }
}
