using Microsoft.EntityFrameworkCore;
using MyEducationAspApp.DAL.Entities;

namespace MyEducationAspApp.DAL;

public sealed class MainDbContext : DbContext
{
    public MainDbContext(string dbPath)
    {
        DbPath = dbPath;
        Database.EnsureCreated();
    }

    private string DbPath { get; }
    public DbSet<ChatMessageEntity> ChatMessages { get; set; } = null!;
    public DbSet<VisitorEntity> Visitors { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
