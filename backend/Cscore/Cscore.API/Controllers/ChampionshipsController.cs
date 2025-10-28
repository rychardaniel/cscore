using Cscore.API.Dtos;
using Cscore.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Cscore.API.Controllers;

[ApiController]
[Route("championships")]
public class ChampionshipsController : ControllerBase
{
    private readonly ChampionshipRepository _repository;

    public ChampionshipsController(ChampionshipRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var championships = await _repository.GetAllAsync();

        var championshipDtos = championships.Select(championship =>
            new ChampionshipResponseDto(
                championship.Id ?? "",
                championship.Name,
                championship.StartDate,
                championship.EndDate
            ));

        return Ok(championshipDtos);
    }
}