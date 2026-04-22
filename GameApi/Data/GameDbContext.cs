using GamesCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesCrudApi.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options) { }

        public DbSet<Game> Games => Set<Game>();
    }
}
