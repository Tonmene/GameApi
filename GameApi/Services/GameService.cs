using GamesCrudApi.Data;
using GamesCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesCrudApi.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;

        // Npgsql maps .NET DateTime to PostgreSQL `timestamp with time zone` by default.
        // That mapping requires DateTime values to have `Kind=Utc` (or be convertible to UTC).
        private static DateTime ToUtc(DateTime value)
        {
            return value.Kind switch
            {
                DateTimeKind.Utc => value,
                // JSON like "2020-01-01T00:00:00" parses as Unspecified. We treat it as UTC for storage.
                DateTimeKind.Unspecified => DateTime.SpecifyKind(value, DateTimeKind.Utc),
                DateTimeKind.Local => value.ToUniversalTime(),
                _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
            };
        }

        public GameService(GameDbContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> GetAllAsync()
        {
            return await _context.Games.OrderBy(g => g.Id).ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(int id)
        {
            return await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Game> CreateAsync(Game game)
        {
            game.ReleaseDate = ToUtc(game.ReleaseDate);
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> UpdateAsync(int id, Game game)
        {
            var existing = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (existing == null) return false;

            existing.Name = game.Name;
            existing.Genre = game.Genre;
            existing.ReleaseDate = ToUtc(game.ReleaseDate);
            existing.Condition = game.Condition;
            existing.Price = game.Price;
            existing.PurchaseOption = game.PurchaseOption;
            existing.ContentDescription = game.ContentDescription;
            existing.HasInteractiveElements = game.HasInteractiveElements;
            existing.UserRating = game.UserRating;
            existing.UserReview = game.UserReview;
            existing.Critics = game.Critics;
            existing.Features = game.Features;
            existing.OnlinePlayers = game.OnlinePlayers;
            existing.OfflinePlayers = game.OfflinePlayers;
            existing.Publisher = game.Publisher;
            existing.AgeRating = game.AgeRating;
            existing.SpecsAndRequirements = game.SpecsAndRequirements;
            existing.CustomersFrequentlyRented = game.CustomersFrequentlyRented;
            existing.Description = game.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (existing == null) return false;

            _context.Games.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
