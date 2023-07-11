using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyEducationAspApp.DAL.Entities;

public class ChatMessageEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string TimeStamp { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}
