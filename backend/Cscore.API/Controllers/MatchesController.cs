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
    private readonly MatchService _matchService;
    private readonly IMapper _mapper;

    public MatchesController(MatchService matchService, IMapper mapper)
    {
        _matchService = matchService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(string idChampionship)
    {
        var matches = await _matchService.GetAllAsync(idChampionship);
        var response = _mapper.Map<List<MatchResponseDto>>(matches);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string idChampionship, [FromRoute] string id)
    {
        var matchModel = await _matchService.GetByIdAsync(idChampionship, id);

        if (matchModel == null)
            return NotFound();

        var response = _mapper.Map<MatchResponseDto>(matchModel);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string idChampionship, [FromBody] CreateMatchDto dto)
    {
        var matchModel = _mapper.Map<MatchModel>(dto);
        MatchModel createdMatch;

        try
        {
            createdMatch = await _matchService.CreateAsync(idChampionship, matchModel);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        var response = _mapper.Map<MatchResponseDto>(createdMatch);

        return CreatedAtAction(nameof(GetById), new
        {
            idChampionship = response.IdChampionship, id = response.Id
        }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string idChampionship, [FromRoute] string id, [FromBody] CreateMatchDto dto)
    {
        var matchModel = _mapper.Map<MatchModel>(dto);
        MatchModel editedMatch;

        try
        {
            editedMatch = await _matchService.UpdateAsync(idChampionship, id, matchModel);
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }

        var response = _mapper.Map<MatchResponseDto>(editedMatch);
        
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string idChampionship, [FromRoute] string id)
    {
        var matchModel = await _matchService.DeleteAsync(idChampionship, id);

        if (matchModel == null)
            return NotFound();
        
        return NoContent();
    }
}