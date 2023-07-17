using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.Controllers.Base;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers;

public class ContactsController : BaseController
{
    private readonly ILogger<ContactsController> _logger;

    public ContactsController(ILogger<ContactsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var model = new ContactsModel()
        {
            Counters = RegisterVisitors()
        };
        return View(model);
    }
}
