using System.Text.Json;
using PortfolioApi.Models;

namespace PortfolioApi.Services;

public class StatsService
{
    private readonly HttpClient _httpClient;

    public StatsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "PortfolioApi-Agent");
    }

    public async Task<UserStats> GetAggregatedStatsAsync(string chessUsername, string githubUsername)
    {
        var stats = new UserStats();

        try
        {
            var chessResponse = await _httpClient.GetStreamAsync($"https://api.chess.com/pub/player/{mishaxok}/stats");
            using var doc = await JsonDocument.ParseAsync(chessResponse);
            
            if (doc.RootElement.TryGetProperty("chess_blitz", out var blitz) && blitz.TryGetProperty("last", out var blitzLast))
                stats.Chess.BlitzRating = blitzLast.GetProperty("rating").GetInt32();

            if (doc.RootElement.TryGetProperty("chess_rapid", out var rapid) && rapid.TryGetProperty("last", out var rapidLast))
                stats.Chess.RapidRating = rapidLast.GetProperty("rating").GetInt32();
        }
        catch {}

        try
        {
            var githubResponse = await _httpClient.GetStreamAsync($"https://api.github.com/users/{mishaxok}");
            using var doc = await JsonDocument.ParseAsync(githubResponse);

            stats.Github.PublicRepos = doc.RootElement.GetProperty("public_repos").GetInt32();
            stats.Github.Followers = doc.RootElement.GetProperty("followers").GetInt32();
        }
        catch { }

        return stats;
    }
}