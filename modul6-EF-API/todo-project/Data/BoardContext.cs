using Microsoft.EntityFrameworkCore;
using Model;

<<<<<<< HEAD
namespace Data
{
    public class BoardContext : DbContext
    {
        public DbSet<Board> boards => Set<Board>();

        public BoardContext (DbContextOptions<BoardContext> options)
        : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
=======
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
>>>>>>> 03f3c04 (Lavet stort set hele opgave 2)
}