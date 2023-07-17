using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyEducationAspApp.DAL;
using MyEducationAspApp.DAL.Entities;
using MyEducationAspApp.Models;

namespace MyEducationAspApp.Controllers.Base;

public abstract class BaseController : Controller
{
    protected readonly MainDbContext MainDbContext;

    protected BaseController(MainDbContext mainDbContext)
    {
        MainDbContext = mainDbContext;
    }

    protected CountersModel RegisterVisitors()
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

        var todayCounter = 0;
        var totalCounter = 0;
        if (ipAddress != null)
            VisitorsUpdate(ipAddress, out todayCounter, out totalCounter);

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

    private void VisitorsUpdate(string ip, out int uniqueToDay, out int uniqueAllTime)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

        var visitor = MainDbContext.Visitors.FirstOrDefault(entity => entity.IpAddress == ip);
        if (visitor is null)
        {
            visitor = new VisitorEntity
            {
                IpAddress = ip,
                VisitDate = dateNow
            };
            MainDbContext.Visitors.Add(visitor);
            MainDbContext.SaveChanges();
        }
        else
        {
            if (visitor.VisitDate != dateNow)
            {
                visitor.VisitDate = dateNow;
                MainDbContext.SaveChanges();
            }
        }

        uniqueAllTime = MainDbContext.Visitors.Count();
        uniqueToDay = MainDbContext.Visitors.Count(v => v.VisitDate == dateNow);
    }
}
