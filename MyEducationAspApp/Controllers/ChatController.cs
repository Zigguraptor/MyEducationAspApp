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
        using var mainDbContext = new MainDbContext(".\\MainDb.db");
        var messageEntities = mainDbContext.ChatMessages
            .OrderByDescending(m => m.TimeStamp)
            .Take(50)
            .OrderBy(m => m.TimeStamp)
            .ToList();

        return View(new ChatModel(messageEntities));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
