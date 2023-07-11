using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.Models;

public class ChatModel : BaseModel
{
    public ChatModel()
    {
        MessageEntities = new List<ChatMessageEntity>(0);
    }

    public ChatModel(List<ChatMessageEntity> messageEntities)
    {
        MessageEntities = messageEntities;
    }

    public List<ChatMessageEntity> MessageEntities { get; set; }
}
