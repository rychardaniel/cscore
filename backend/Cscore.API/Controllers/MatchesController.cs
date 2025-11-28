using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;
using Cscore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("championships/{idChampionship}/matches")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;
    private readonly IMapper _mapper;

    public MatchesController(IMatchService matchService, IMapper mapper)
    {
        _matchService = matchService;
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
    public async Task<IActionResult> Put(int idChampionship, [FromRoute] int id, [FromBody] CreateMatchDto dto)
    {
        var matchModel = _mapper.Map<MatchModel>(dto);
        var editedMatch = await _matchService.UpdateAsync(idChampionship, id, matchModel);
        
        var response = _mapper.Map<MatchResponseDto>(editedMatch);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int idChampionship, [FromRoute] int id)
    {
        await _matchService.DeleteAsync(idChampionship, id);

        return NoContent();
    }
}