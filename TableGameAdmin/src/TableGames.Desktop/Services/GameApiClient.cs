using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using TableGames.Desktop.Models;

namespace TableGames.Desktop.Services;

/// <summary>
/// Encapsula todas las llamadas HTTP a la REST API de juegos.
/// El ViewModel solo conoce esta clase, no los detalles de HTTP.
/// </summary>
public class GameApiClient
{
    private readonly HttpClient _http;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public GameApiClient(string baseUrl = "http://localhost:5264/")
    {
        _http = new HttpClient { BaseAddress = new System.Uri(baseUrl) };
    }

    public async Task<List<Game>> GetGamesAsync()
    {
        var games = await _http.GetFromJsonAsync<List<Game>>("api/games", JsonOptions);
        return games ?? new List<Game>();
    }

    public async Task<Game?> CreateGameAsync(Game game)
    {
        var response = await _http.PostAsJsonAsync("api/games", ToInput(game));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Game>(JsonOptions);
    }

    public async Task UpdateGameAsync(Game game)
    {
        var response = await _http.PutAsJsonAsync($"api/games/{game.Id}", ToInput(game));
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteGameAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/games/{id}");
        response.EnsureSuccessStatusCode();
    }

    // La API espera solo los campos editables (DTO), no Id ni CreatedAt.
    private static object ToInput(Game g) => new
    {
        name = g.Name,
        category = g.Category,
        minPlayers = g.MinPlayers,
        maxPlayers = g.MaxPlayers,
        pricePerHour = g.PricePerHour,
        isAvailable = g.IsAvailable
    };
}
