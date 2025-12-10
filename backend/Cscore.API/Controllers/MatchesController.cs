using AutoMapper;
using Cscore.API.Data.MongoDB.Entities;
using Cscore.API.Data.MongoDB.Repositories;
using Cscore.API.Dtos;
using Cscore.API.Models;
using Cscore.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Cscore.API.Controllers;

[ApiController]
[Route("championships/{idChampionship}/matches")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly IMatchScoreRepository _scoreRepo;
    private readonly IMatchEventRepository _eventRepo;
    private readonly IMapper _mapper;

    public MatchesController(
        IMatchService matchService,
        IMatchScoreRepository scoreRepo,
        IMatchEventRepository eventRepo,
        IMapper mapper)
    {
        _matchService = matchService;
        _scoreRepo = scoreRepo;
        _eventRepo = eventRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int idChampionship, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var matches = await _matchService.GetAllAsync(idChampionship, page, pageSize);
        var response = _mapper.Map<List<MatchResponseDto>>(matches);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int idChampionship, [FromRoute] int id)
    {
        var matchModel = await _matchService.GetByIdAsync(idChampionship, id);

        if (matchModel == null)
            return NotFound();

        var response = _mapper.Map<MatchResponseDto>(matchModel);

        return Ok(response);
    }

    [HttpPost]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> Create(int idChampionship, [FromBody] CreateMatchDto dto)
    {
        var matchModel = _mapper.Map<MatchModel>(dto);
        var createdMatch = await _matchService.CreateAsync(idChampionship, matchModel);
        
        var response = _mapper.Map<MatchResponseDto>(createdMatch);

        return CreatedAtAction(nameof(GetById), new
            {
                idChampionship = response.ChampionshipId, id = response.Id
            },
            response);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> Put(int idChampionship, [FromRoute] int id, [FromBody] CreateMatchDto dto)
    {
        var matchModel = _mapper.Map<MatchModel>(dto);
        var editedMatch = await _matchService.UpdateAsync(idChampionship, id, matchModel);
        
        var response = _mapper.Map<MatchResponseDto>(editedMatch);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> Delete(int idChampionship, [FromRoute] int id)
    {
        await _matchService.DeleteAsync(idChampionship, id);

        return NoContent();
    }

    // Judge-specific endpoints

    [HttpPost("{id}/start")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> StartMatch(int idChampionship, int id)
    {
        await _matchService.StartMatch(id);
        return Ok(new { message = "Partida iniciada com sucesso" });
    }

    [HttpPost("{id}/finish")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> FinishMatch(int idChampionship, int id)
    {
        await _matchService.FinishMatch(id);
        return Ok(new { message = "Partida finalizada com sucesso" });
    }

    [HttpPut("{id}/score")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> UpdateScore(
        int idChampionship, 
        int id, 
        [FromBody] UpdateScoreDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        var match = await _matchService.GetByIdAsync(idChampionship, id);
        if (match == null)
            return NotFound();
            
        // Criar ou atualizar placar no MongoDB
        var score = await _scoreRepo.GetByMatchIdAsync(id);
        if (score == null)
        {
            score = new MatchScore
            {
                MatchId = id,
                SportType = (int)match.SportType,
                ScoreData = dto.ScoreData,
                UpdatedByUserId = userId
            };
            await _scoreRepo.CreateAsync(score);
            
            // Atualizar referência no PostgreSQL
            match.MongoScoreId = score.Id;
            await _matchService.UpdateAsync(idChampionship, id, match);
        }
        else
        {
            score.ScoreData = dto.ScoreData;
            score.UpdatedByUserId = userId;
            score.UpdatedAt = DateTime.UtcNow;
            await _scoreRepo.UpdateAsync(score);
        }
        
        return Ok(score);
    }

    [HttpPost("{id}/events")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> AddEvent(
        int idChampionship,
        int id,
        [FromBody] CreateMatchEventDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        
        var matchEvent = new MatchEvent
        {
            MatchId = id,
            EventType = dto.EventType,
            GameMinute = dto.GameMinute,
            ParticipantId = dto.ParticipantId,
            Details = dto.Details,
            RegisteredByUserId = userId
        };
        
        await _eventRepo.CreateAsync(matchEvent);
        return CreatedAtAction(nameof(GetById), new { idChampionship, id }, matchEvent);
    }

    [HttpPost("{id}/participants")]
    [Authorize(Policy = "JudgeOrAdmin")]
    public async Task<IActionResult> AddParticipant(
        int idChampionship,
        int id,
        [FromBody] CreateParticipantDto dto)
    {
        var participant = new MatchParticipantModel
        {
            Type = (ParticipantType)dto.Type,
            Name = dto.Name,
            Side = dto.Side,
            LogoUrl = dto.LogoUrl
        };

        var result = await _matchService.AddParticipant(id, participant);
        return CreatedAtAction(nameof(GetById), new { idChampionship, id }, result);
    }
}