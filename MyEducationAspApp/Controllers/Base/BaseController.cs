using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.DAL;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers.Base;

public abstract class BaseController : Controller
{
    protected CountersModel RegisterVisitors()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var todayCounter = 0;
        var totalCounter = 0;
        if (ipAddress != null)
            MainDbManager.VisitorsUpdate(ipAddress, out todayCounter, out totalCounter);

        return new CountersModel
        {
            TodayCounter = todayCounter,
            TotalCounter = totalCounter
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
