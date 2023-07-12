using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MainDbContext mainDbContext) : base(mainDbContext)
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
