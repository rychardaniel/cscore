using Cscore.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("championships/{championshipId}/judges")]
[Authorize(Policy = "AdminOnly")]
public class ChampionshipJudgesController : ControllerBase
{
    private readonly IChampionshipJudgeService _championshipJudgeService;

    public ChampionshipJudgesController(IChampionshipJudgeService championshipJudgeService)
    {
        _championshipJudgeService = championshipJudgeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetJudges(int championshipId)
    {
        var judges = await _championshipJudgeService.GetJudgesByChampionshipAsync(championshipId);
        return Ok(judges);
    }

    [HttpPost]
    public async Task<IActionResult> AssignJudge(
        int championshipId,
        [FromBody] AssignJudgeDto dto)
    {
        try
        {
            var result = await _championshipJudgeService.AssignJudgeAsync(championshipId, dto.UserId);
            return CreatedAtAction(nameof(GetJudges), new { championshipId }, result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> RemoveJudge(int championshipId, int userId)
    {
        try
        {
            await _championshipJudgeService.RemoveJudgeAsync(championshipId, userId);
            return NoContent();
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }
}

public record AssignJudgeDto(int UserId);
