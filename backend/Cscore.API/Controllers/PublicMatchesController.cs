using Cscore.API.Data.MongoDB.Repositories;
using Cscore.API.Models;
using Cscore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("public/matches")]
public class PublicMatchesController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly IMatchEventRepository _eventRepo;

    public PublicMatchesController(
        IMatchService matchService,
        IMatchEventRepository eventRepo)
    {
        _matchService = matchService;
        _eventRepo = eventRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int? championshipId,
        [FromQuery] SportType? sportType,
        [FromQuery] MatchStatus? status,
        [FromQuery] DateTime? date,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        var matches = await _matchService.GetPublicMatches(
            championshipId, sportType, status, date, page, pageSize);
        return Ok(matches);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var match = await _matchService.GetPublicMatchById(id);
        if (match == null)
            return NotFound();

        return Ok(match);
    }

    [HttpGet("live")]
    public async Task<IActionResult> GetLiveMatches()
    {
        var liveMatches = await _matchService.GetLiveMatches();
        return Ok(liveMatches);
    }

    [HttpGet("{id:int}/events")]
    public async Task<IActionResult> GetMatchEvents(int id)
    {
        var events = await _eventRepo.GetByMatchIdAsync(id);
        return Ok(events);
    }
}
