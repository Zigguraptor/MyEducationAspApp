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
}
