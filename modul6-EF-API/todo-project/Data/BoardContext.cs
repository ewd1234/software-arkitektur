using Microsoft.EntityFrameworkCore;
using Model;

namespace Data;

public class BoardContext : DbContext
{
    public DbSet<Board> Boards => Set<Board>();

    public string DbPath { get; }

    public BoardContext()
    {
        DbPath = "todo.db";
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}