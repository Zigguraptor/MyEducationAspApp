using Microsoft.EntityFrameworkCore;

namespace MyEducationAspApp.DAL;

public sealed class MainDbContext : DbContext
{
    public MainDbContext(string dbPath)
    {
        DbPath = dbPath;
        Database.EnsureCreated();
    }

    public string DbPath { get; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
