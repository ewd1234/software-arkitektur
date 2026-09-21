using Microsoft.EntityFrameworkCore;
using Model;

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
}