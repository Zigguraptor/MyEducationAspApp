using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers;

public class ChatController : BaseController
{
    private readonly ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger, MainDbContext mainDbContext) : base(mainDbContext)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        using var mainDbContext = MainDbContext;
        var messageHistory = mainDbContext.ChatMessages
            .OrderByDescending(m => m.TimeStamp)
            .Take(50)
            .OrderBy(m => m.TimeStamp)
            .ToList();

        var model = new ChatModel
        {
            MessageEntities = messageHistory,
            Counters = RegisterVisitors()
        };
        return View(model);
    }
}
