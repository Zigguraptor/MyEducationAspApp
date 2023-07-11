using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyEducationAspApp.DAL.Entities;

[Index("VisitDate")]
public class VisitorEntity
{
    [Key] public string IPAddress { get; set; } = null!;
    public DateOnly VisitDate { get; set; }
}
