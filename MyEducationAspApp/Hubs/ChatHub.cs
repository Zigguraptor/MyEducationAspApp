using JustAspEducationalProject;
using Microsoft.AspNetCore.SignalR;
using MyEducationAspApp.DAL;
using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message)
    {
        await using var dbContext = new MainDbContext(".\\MainDb.db");
        dbContext.ChatMessages.Add(new ChatMessageEntity
        {
            TimeStamp = Time.GetTimeStamp(),
            UserName = user,
            MessageText = message
        });
        await dbContext.SaveChangesAsync();
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
