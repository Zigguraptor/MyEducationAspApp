using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers;

public class ChatController : BaseController
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
}
