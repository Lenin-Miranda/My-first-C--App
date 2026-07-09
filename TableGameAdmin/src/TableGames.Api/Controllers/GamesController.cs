using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableGames.Api.Data;
using TableGames.Api.Dtos;
using TableGames.Api.Models;

namespace TableGames.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly AppDbContext _db;

    public GamesController(AppDbContext db)
    {
        _db = db;
    }

    // GET: api/games
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Game>>> GetAll()
    {
        var games = await _db.Games.OrderBy(g => g.Name).ToListAsync();
        return Ok(games);
    }

    // GET: api/games/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Game>> GetById(int id)
    {
        var game = await _db.Games.FindAsync(id);
        return game is null ? NotFound() : Ok(game);
    }

    // POST: api/games
    [HttpPost]
    public async Task<ActionResult<Game>> Create(GameInput input)
    {
        if (input.MaxPlayers < input.MinPlayers)
            return BadRequest("MaxPlayers no puede ser menor que MinPlayers.");

        var game = new Game
        {
            Name = input.Name,
            Category = input.Category,
            MinPlayers = input.MinPlayers,
            MaxPlayers = input.MaxPlayers,
            PricePerHour = input.PricePerHour,
            IsAvailable = input.IsAvailable,
            CreatedAt = DateTime.UtcNow
        };

        _db.Games.Add(game);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = game.Id }, game);
    }

    // PUT: api/games/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, GameInput input)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return NotFound();

        if (input.MaxPlayers < input.MinPlayers)
            return BadRequest("MaxPlayers no puede ser menor que MinPlayers.");

        game.Name = input.Name;
        game.Category = input.Category;
        game.MinPlayers = input.MinPlayers;
        game.MaxPlayers = input.MaxPlayers;
        game.PricePerHour = input.PricePerHour;
        game.IsAvailable = input.IsAvailable;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/games/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var game = await _db.Games.FindAsync(id);
        if (game is null) return NotFound();

        _db.Games.Remove(game);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
