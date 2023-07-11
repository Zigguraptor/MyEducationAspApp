using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.DAL;

public static class MainDbManager
{
    private const string DbPath = ".\\MainDb.db";

    public static List<ChatMessageEntity> GetLimitedMessageHistory()
    {
        using var mainDbContext = new MainDbContext(".\\MainDb.db");
        return mainDbContext.ChatMessages
            .OrderByDescending(m => m.TimeStamp)
            .Take(50)
            .OrderBy(m => m.TimeStamp)
            .ToList();
    }

    public static void VisitorsUpdate(string ip, out int uniqueToDay, out int uniqueAllTime)
    {
        var dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

        using var context = new MainDbContext(DbPath);
        var visitor = context.Visitors.FirstOrDefault(entity => entity.IpAddress == ip);
        if (visitor is null)
        {
            visitor = new VisitorEntity
            {
                IpAddress = ip,
                VisitDate = dateNow
            };
            context.Visitors.Add(visitor);
            context.SaveChanges();
        }
        else
        {
            if (visitor.VisitDate != dateNow)
            {
                visitor.VisitDate = dateNow;
                context.SaveChanges();
            }
        }

        uniqueAllTime = context.Visitors.Count();
        uniqueToDay = context.Visitors.Count(v => v.VisitDate == dateNow);
    }
}
