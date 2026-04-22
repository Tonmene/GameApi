using GamesCrudApi.Models;
using GamesCrudApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamesCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _service;

        public GamesController(IGameService service)
        {
            _service = service;
        }

        // GET: api/games
        [HttpGet]
        public async Task<ActionResult<List<Game>>> GetAll()
        {
            var games = await _service.GetAllAsync();
            return Ok(games);
        }

        // GET: api/games/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetById(int id)
        {
            var game = await _service.GetByIdAsync(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        // POST: api/games
        [HttpPost]
        public async Task<ActionResult<Game>> Create(Game game)
        {
            var created = await _service.CreateAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/games/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Game game)
        {
            var updated = await _service.UpdateAsync(id, game);
            if (!updated) return NotFound();
            return NoContent(); // 204
        }

        // DELETE: api/games/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent(); // 204
        }
    }
}
