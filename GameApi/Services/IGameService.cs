using GamesCrudApi.Models;

namespace GamesCrudApi.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(int id);

        Task<Game> CreateAsync(Game game);
        Task<bool> UpdateAsync(int id, Game game);
        Task<bool> DeleteAsync(int id);
        Task<RentGameResult> RentAsync(int id);
    }
}
