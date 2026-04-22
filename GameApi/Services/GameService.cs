using GamesCrudApi.Data;
using GamesCrudApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesCrudApi.Services
{
    public class GameService : IGameService
    {
        private readonly GameDbContext _context;

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
            existing.ReleaseDate = game.ReleaseDate;
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
