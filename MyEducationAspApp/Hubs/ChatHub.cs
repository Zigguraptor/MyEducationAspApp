using Microsoft.AspNetCore.SignalR;
using MyEducationAspApp.DAL;
using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.Hubs;

public class ChatHub : Hub
{
    private readonly MainDbContext _mainDbContext;

    public ChatHub(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public async Task SendMessage(string user, string message)
    {
        _mainDbContext.ChatMessages.Add(new ChatMessageEntity
        {
            TimeStamp = Time.GetTimeStamp(),
            UserName = user,
            MessageText = message
        });
        await _mainDbContext.SaveChangesAsync();
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
