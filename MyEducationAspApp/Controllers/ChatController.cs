using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers;

public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new ChatModel
        {
            MessageEntities = MainDbManager.GetLimitedMessageHistory(),
            Counters = RegisterVisitors()
        };
        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
