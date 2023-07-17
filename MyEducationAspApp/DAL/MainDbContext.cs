using Microsoft.EntityFrameworkCore;
using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.DAL;

public sealed class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    public DbSet<ChatMessageEntity> ChatMessages { get; set; } = null!;
    public DbSet<VisitorEntity> Visitors { get; set; } = null!;
}
