using AutoMapper;
using Cscore.API.Dtos;
using Cscore.API.Models;
using Cscore.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("championships")]
public class ChampionshipsController : ControllerBase
{
    private readonly IChampionshipService _championshipService;
    private readonly IMapper _mapper;

    public ChampionshipsController(IChampionshipService championshipService, IMapper mapper)
    {
        _championshipService = championshipService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var championships = await _championshipService.GetAllAsync(page, pageSize);
        var response = _mapper.Map<List<ChampionshipResponseDto>>(championships);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var championshipModel = await _championshipService.GetByIdAsync(id);

        if (championshipModel == null)
            return NotFound();

        var response = _mapper.Map<ChampionshipResponseDto>(championshipModel);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateChampionshipDto dto)
    {
        var championshipModel = _mapper.Map<ChampionshipModel>(dto);
        var createdChampionship = await _championshipService.CreateAsync(championshipModel);
        var response = _mapper.Map<ChampionshipResponseDto>(createdChampionship);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CreateChampionshipDto dto)
    {
        var championshipModel = _mapper.Map<ChampionshipModel>(dto);
        var editedChampionship = await _championshipService.UpdateAsync(id, championshipModel);
        var response = _mapper.Map<ChampionshipResponseDto>(editedChampionship);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        await _championshipService.DeleteAsync(id);
        return NoContent();
    }
}