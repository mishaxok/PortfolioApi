using Microsoft.AspNetCore.Mvc;
using PortfolioApi.Services;

namespace PortfolioApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly StatsService _statsService;

    public StatsController(StatsService statsService)
    {
        _statsService = statsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetStats([FromQuery] string chessUser = "mishaxok", [FromQuery] string githubUser = "mishaxok")
    {
        var stats = await _statsService.GetAggregatedStatsAsync(chessUser, githubUser);
        return Ok(stats);
    }
}