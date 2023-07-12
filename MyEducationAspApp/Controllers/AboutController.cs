using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers;

public class AboutController : BaseController
{
    private readonly ILogger<AboutController> _logger;

    public AboutController(ILogger<AboutController> logger, MainDbContext mainDbContext) : base(mainDbContext)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new AboutModel()
        {
            Counters = RegisterVisitors()
        };
        return View(model);
    }
}
